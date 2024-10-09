namespace UpStreamer.Server.Features.Videos.DTOs
{
    public class CreateVideoRequestDto
    {
        public string Title { get; init; }
        public string Description { get; init; }
        public string Category { get; init; }
        public string FilePath { get; init; }

    }
}
