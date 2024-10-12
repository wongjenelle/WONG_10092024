
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using UpStreamer.Server.Common.Repository;
using UpStreamer.Server.Database.Entities;
using UpStreamer.Server.Features.Videos.DTOs;

namespace UpStreamer.Server.Features.Videos.Handlers
{
    public class GetVideoDetailQuery(int id) : IRequest<GetVideoDetailResponseDto>
    {
        public int Id { get; private set; } = id;

    }

    public class GetVideoDetailHandler(IGenericRepository<Video> repository, IMapper mapper, IConfiguration configuration) : IRequestHandler<GetVideoDetailQuery, GetVideoDetailResponseDto>
    {
        public async Task<GetVideoDetailResponseDto> Handle(GetVideoDetailQuery query, CancellationToken cancellationToken)
        {
            var video = await repository.GetAsync(predicate: x => x.Id == query.Id, include: x => x.Include(y => y.Category)) 
                ?? throw new KeyNotFoundException();

            var response = mapper.Map<Video, GetVideoDetailResponseDto>(video);

            if (!string.IsNullOrEmpty(response.FilePath))
            {
                var uriBuilder = new UriBuilder(configuration["FileHostUrl"]!)
                {
                    Path =  response.FilePath
                };
                response.FilePath = uriBuilder.Uri.ToString();
            }


            return response;
        }
    }
}
