using System.ComponentModel.DataAnnotations;

namespace UpStreamer.Server.Entities
{
    public class Video
    {
        [Key]
        public int Id { get; set; }
        [MaxLength(160)]
        public required string Title { get; set; }
        [MaxLength(160)]
        public string? Description { get; set; }
        public int? CategoryId { get; set; }
        public Category? Category { get; set; }
    }
}
