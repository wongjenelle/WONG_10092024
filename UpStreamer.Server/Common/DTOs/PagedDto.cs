namespace UpStreamer.Server.Common.DTOs
{
    public class PagedDto
    {
        public int? Skip { get; init; } = 0;
        public int? Take { get; init; } = 10;
    }
}
