using System.ComponentModel.DataAnnotations.Schema;

namespace Project.DataAccess.Database1.Files.Models
{
    public class FileStreamData
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public string Path { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public byte[] Transaction { get; set; }
    }
}