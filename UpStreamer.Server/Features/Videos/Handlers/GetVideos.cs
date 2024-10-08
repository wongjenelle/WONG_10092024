using MediatR;

namespace UpStreamer.Server.Features.Videos.Handlers
{
    public class GetVideosQuery() : IRequest<string>
    {
    }

    public class GetVideosHandler : IRequestHandler<GetVideosQuery, string>
    {
        public async Task<string> Handle(GetVideosQuery query, CancellationToken cancellationToken)
        {
            return "hello";
        }
    }
}
