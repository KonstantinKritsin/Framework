using System.ComponentModel.DataAnnotations.Schema;

namespace Project.DataAccess.Database1.Files.Models
{
    public class File1MData
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }
        public byte[] Data { get; set; }
    }
}