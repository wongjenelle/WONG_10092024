﻿namespace UpStreamer.Server.Features.Videos.DTOs
{
    public class GetVideosResponseDto
    {
        public List<GetVideosObjectDto> Videos { get; init; }
        public int Total { get; init; }
    }

    public class GetVideosObjectDto
    {
        public int Id { get; init; }
        public string Title { get; init; }
        public string Description { get; init; }
        public string Category { get; init; }
    }
}
