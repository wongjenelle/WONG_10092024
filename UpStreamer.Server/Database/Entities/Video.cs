using System.ComponentModel.DataAnnotations;

namespace UpStreamer.Server.Database.Entities
{
    public class Video
    {
        [Key]
        public int Id { get; set; }
        [MaxLength(100)]
        [Required]
        public required string Title { get; set; }
        [MaxLength(255)]
        public string? Description { get; set; }
        [MaxLength(255)]
        public string? FilePath { get; set; }
        public int CategoryId { get; set; }
        public required Category Category { get; set; }
    }
}
