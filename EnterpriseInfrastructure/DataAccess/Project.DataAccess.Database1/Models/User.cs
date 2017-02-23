using System;
using System.ComponentModel.DataAnnotations;
using Project.Framework.DataAccess.ModelContract;

namespace Project.DataAccess.Database1.Models
{
    public class User : IEntityKey<int>, ICreatedAt
    {
        public int Id { get; set; }
        [Required]
        [StringLength(255)]
        public string Login { get; set; }
        [StringLength(500)]
        public string Name { get; set; }
        [StringLength(255)]
        public string Email { get; set; }
        [StringLength(10)]
        public string Lang { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
        public DateTimeOffset LastActivityDate { get; set; }
    }
}