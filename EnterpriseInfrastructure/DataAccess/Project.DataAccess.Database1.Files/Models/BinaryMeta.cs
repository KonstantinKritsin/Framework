using System;
using System.ComponentModel.DataAnnotations;
using Project.Framework.DataAccess.ModelContract;

namespace Project.DataAccess.Database1.Files.Models
{
	public class BinaryMeta : IEntityKey<int>, ICreatedAt
    {
        public int Id { get; set; }
        [Required]
	    public string Type { get; set; }
        public string Name { get; set; }
        public long Size { get; set; }
        public StorageType Storage { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
    }

    public enum StorageType : byte
    {
        ByteArray = 0,
        FileStream = 1
    }
}
