namespace UpStreamer.Server.Features.Videos.DTOs
{
    public class GetVideosResponse
    {
        public List<GetVideosObject> Videos { get; init; }
    }

    public class GetVideosObject
    {
        public string Id { get; init; }
        public string Title { get; init; }
        public string Description { get; init; }
        public string Category { get; init; }
        public string FilePath { get; init; }
    }
}
