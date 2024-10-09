namespace UpStreamer.Server.Features.Videos.DTOs
{
    public class CreateVideoRequest
    {
        public string Title { get; init; }
        public string Description { get; init; }
        public string Category { get; init; }
        public string FilePath { get; init; }

    }
}
