using System;
using System.ComponentModel;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Data.SqlTypes;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Project.Common.Extensions;
using Project.DataAccess.Database1.Files.Contracts;
using Project.DataAccess.Database1.Files.Models;
using Project.DataAccess.EntityFramework;
using Project.Framework.Common.DependencyResolver;

namespace Project.DataAccess.Database1.Files.Repositories
{
    public class BinaryMetaRepository : EntityFrameworkQueryRepository<BinaryMeta>, IBinaryMetaRepository
    {
        private readonly IDependencyResolver _dependencyResolver;

        private static readonly string ReadSql =
            $@"
select
@p0 '{CommonExtensions.NameOf((FileStreamData t) => t.Id)}',
Data.PathName() '{CommonExtensions.NameOf((FileStreamData t) => t.Path)}',
GET_FILESTREAM_TRANSACTION_CONTEXT() '{CommonExtensions.NameOf((FileStreamData t) => t.Transaction)}'
from [dbo].[{nameof(FileStreamData)}] 
where {CommonExtensions.NameOf((FileStreamData t) => t.Id)}=@p0";

        private static readonly string WriteSql = $"insert into [dbo].[{nameof(FileStreamData)}]({CommonExtensions.NameOf((FileStreamData t) => t.Id)}) VALUES(@p0);";
        private static readonly string DeleteFileStreamSql = $"delete from [dbo].[{nameof(FileStreamData)}] where Id=@p0";
        private static readonly string DeleteByteArraySql = $"delete from [dbo].[{nameof(File1MData)}] where Id=@p0";


        public BinaryMetaRepository(IDependencyResolver dependencyResolver) : base(typeof(ProjectFilesContext))
        {
            _dependencyResolver = dependencyResolver;
        }

        public BinaryMeta Create(string type, string name)
        {
            return new BinaryMeta
            {
                Type = type,
                Name = name
            };
        }

        public Task<BinaryMeta> FindMetaAsync(int id)
        {
            return Context.Set<BinaryMeta>().FindAsync(id);
        }

        public async Task RemoveFileAsync(int id)
        {
            var meta = await Context.Set<BinaryMeta>().FindAsync(id);
            if (meta != null)
            {
               Context.Set<BinaryMeta>().Remove(meta);
               await RemoveFromStorage(meta.Id, meta.Storage);
            }
        }

        public async Task<BinaryMeta> SaveFileAsync(BinaryMeta meta, Stream data)
        {
            if (data.CanSeek)
                meta.Size = data.Length;

            var hasData = meta.Id > 0;

            Context.Set<BinaryMeta>().AddOrUpdate(meta);

            if (meta.Id == 0)
                await Context.SaveChangesAsync();

            if (meta.Size == 0 || meta.Size > 1048576)
            {
                if (hasData && meta.Storage != StorageType.FileStream)
                    await RemoveFromStorage(meta.Id, meta.Storage);
                await CreateStreamFile(meta, data);
            }
            else
            {
                if (hasData && meta.Storage != StorageType.ByteArray)
                    await RemoveFromStorage(meta.Id, meta.Storage);
                await Create1MFile(meta, data);
            }

            return meta;
        }

        public async Task<Stream> GetFileBodyAsync(int id)
        {
            var meta = await Context.Set<BinaryMeta>().FindAsync(id);
            if (meta == null)
                return null;

            return await GetFileBodyAsync(meta);
        }

        public async Task<Stream> GetFileBodyAsync(BinaryMeta meta)
        {
            switch (meta.Storage)
            {
                case StorageType.ByteArray:
                    var file1M = await Context.Set<File1MData>().FindAsync(meta.Id);
                    return file1M == null ? null : new MemoryStream(file1M.Data);
                case StorageType.FileStream:
                    var fileStream = await Context.Set<FileStreamData>().FindAsync(meta.Id);
                    return fileStream == null ? null : new FileStreamWrapper(meta.Id, _dependencyResolver);
            }

            throw new InvalidEnumArgumentException(nameof(meta.Storage), (int)meta.Storage, typeof(StorageType));
        }

        private async Task Create1MFile(BinaryMeta meta, Stream data)
        {
            byte[] arr;
            var memoryStream = data as MemoryStream;
            if (memoryStream != null)
            {
                memoryStream.Position = 0;
                arr = memoryStream.ToArray();
            }
            else
            {
                arr = new byte[data.Length];
                await data.ReadAsync(arr, 0, (int)data.Length);
            }

            Context.Set<File1MData>().AddOrUpdate(new File1MData
            {
                Id = meta.Id,
                Data = arr
            });

            if (meta.Storage != StorageType.ByteArray || meta.Size != arr.Length)
            {
                meta.Size = arr.Length;
                meta.Storage = StorageType.ByteArray;
                Context.Entry(meta).State = EntityState.Modified;
            }
        }

        private async Task CreateStreamFile(BinaryMeta meta, Stream data)
        {
            await Context.Database.ExecuteSqlCommandAsync(WriteSql, meta.Id);
            var record = await Context.Database.SqlQuery<FileStreamData>(ReadSql, meta.Id).SingleAsync();
            using (var sqlBinary = new SqlFileStream(record.Path, record.Transaction, FileAccess.Write, FileOptions.Asynchronous, 0))
            {
                await data.CopyToAsync(sqlBinary);
                meta.Size = sqlBinary.Length;
            }
            meta.Storage = StorageType.FileStream;

            Context.Entry(meta).State = EntityState.Modified;
        }

        private Task RemoveFromStorage(int id, StorageType storage)
        {
            switch (storage)
            {
                case StorageType.FileStream:
                    return Context.Database.ExecuteSqlCommandAsync(DeleteFileStreamSql, id);
                case StorageType.ByteArray:
                    return Context.Database.ExecuteSqlCommandAsync(DeleteByteArraySql, id);
            }

            throw new InvalidEnumArgumentException(nameof(storage), (int)storage, typeof(StorageType));
        }

        private class FileStreamWrapper : Stream
        {
            private readonly int _id;
            private ProjectFilesContext _context;
            private DbContextTransaction _transactionScope;
            private Lazy<Stream> _fileStream;
            public FileStreamWrapper(int id, IDependencyResolver dependencyResolver)
            {
                _id = id;
                _context = dependencyResolver.Get<ProjectFilesContext>();
                _fileStream = new Lazy<Stream>(GetFileFromDb);
            }

            private Stream GetFileFromDb()
            {
                _transactionScope = _context.Database.BeginTransaction();
                var record = _context.Database.SqlQuery<FileStreamData>(ReadSql, _id).Single();
                return new SqlFileStream(record.Path, record.Transaction, FileAccess.Read, FileOptions.Asynchronous | FileOptions.RandomAccess, 0);
            }

            protected override void Dispose(bool disposing)
            {
                if (disposing)
                {
                    if (_fileStream.IsValueCreated)
                        _fileStream.Value.Dispose();
                    _context.Dispose();

                    _fileStream = null;
                    _context = null;

                    if (_transactionScope != null)
                    {
                        _transactionScope.Dispose();
                        _transactionScope = null;
                    }
                }
            }

            public override void Flush()
            {
                throw new NotSupportedException();
            }

            public override long Seek(long offset, SeekOrigin origin)
            {
                throw new NotSupportedException();
            }

            public override void SetLength(long value)
            {
                throw new NotSupportedException();
            }

            public override int Read(byte[] buffer, int offset, int count)
            {
                return _fileStream.Value.Read(buffer, offset, count);
            }

            public override void Write(byte[] buffer, int offset, int count)
            {
                throw new NotSupportedException();
            }

            public override bool CanRead => _fileStream.Value.CanRead;

            public override bool CanSeek { get; } = false;

            public override bool CanWrite { get; } = false;

            public override long Length { get { throw new NotSupportedException(); } }

            public override long Position
            {
                get { return _fileStream.Value.Position; }
                set { throw new NotSupportedException(); }
            }
        }
    }
}