using System.IO;
using System.Threading.Tasks;
using Project.DataAccess.Database1.Files.Models;
using Project.Framework.DataAccess.Data;

namespace Project.DataAccess.Database1.Files.Contracts
{
	public interface IBinaryMetaRepository : IQueryRepository<BinaryMeta>
    {
		BinaryMeta Create(string type, string name);
	    Task<BinaryMeta> FindMetaAsync(int id);
	    Task<BinaryMeta> SaveFileAsync(BinaryMeta meta, Stream data);
	    Task<Stream> GetFileBodyAsync(int id);
	    Task<Stream> GetFileBodyAsync(BinaryMeta meta);
	    Task RemoveFileAsync(int id);
    }
}