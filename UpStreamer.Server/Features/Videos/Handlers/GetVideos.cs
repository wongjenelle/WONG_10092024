using MediatR;
using UpStreamer.Server.Common.DTOs;
using UpStreamer.Server.Features.Videos.DTOs;

namespace UpStreamer.Server.Features.Videos.Handlers
{
    public class GetVideosQuery(PagedDto request) : IRequest<GetVideosResponse>
    {
        public PagedDto PagedParameters { get; private set; } = request;

    }

    public class GetVideosHandler : IRequestHandler<GetVideosQuery, GetVideosResponse>
    {
        public async Task<GetVideosResponse> Handle(GetVideosQuery query, CancellationToken cancellationToken)
        {
            return new();
        }
    }
}
