using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace UpStreamer.Server.Entities
{
    [Index(nameof(Name), IsUnique = true)]
    public class Category
    {
        [Key]
        public int Id { get; set; }
        [MaxLength(50)]
        [Required]
        public string Name { get; set; }
    }
}
