
using AutoMapper;
using MediatR;
using UpStreamer.Server.Common.Repository;
using UpStreamer.Server.Database.Entities;
using UpStreamer.Server.Features.Videos.DTOs;

namespace UpStreamer.Server.Features.Videos.Handlers
{
    public class GetVideoDetailQuery(int id) : IRequest<GetVideoDetailResponseDto>
    {
        public int Id { get; private set; } = id;

    }

    public class GetVideoDetailHandler(IGenericRepository<Video> repository, IMapper mapper) : IRequestHandler<GetVideoDetailQuery, GetVideoDetailResponseDto>
    {
        public async Task<GetVideoDetailResponseDto> Handle(GetVideoDetailQuery query, CancellationToken cancellationToken)
        {
            // repository get by id

            var response = new GetVideoDetailResponseDto()
            {
                Id = 1,
                Category = "Cat",
                Description = "Description",
                Title = "Title",
                FilePath = "FilePath.mp4"
            };

            return response;
        }
    }
}
