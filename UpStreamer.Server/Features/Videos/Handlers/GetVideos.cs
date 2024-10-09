using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using UpStreamer.Server.Common.DTOs;
using UpStreamer.Server.Common.Repository;
using UpStreamer.Server.Database.Entities;
using UpStreamer.Server.Features.Videos.DTOs;

namespace UpStreamer.Server.Features.Videos.Handlers
{
    public class GetVideosQuery(PagedDto request) : IRequest<GetVideosResponseDto>
    {
        public PagedDto PagedParameters { get; private set; } = request;

    }

    public class GetVideosHandler(IGenericRepository<Video> repository, IMapper mapper) : IRequestHandler<GetVideosQuery, GetVideosResponseDto>
    {
        public async Task<GetVideosResponseDto> Handle(GetVideosQuery query, CancellationToken cancellationToken)
        {
            var list = repository.GetList(predicate: x => true, include: x => x.Include(y => y.Category));

            var filteredList = list.Skip(query.PagedParameters.Skip.GetValueOrDefault())
                .Take(query.PagedParameters.Take.GetValueOrDefault(10));

            var pagedResult = new List<GetVideosObjectDto>();
            foreach(var item in filteredList)
            {
                pagedResult.Add(mapper.Map<GetVideosObjectDto>(item));
            }

            var response = new GetVideosResponseDto()
            {
                Videos = pagedResult,
                Total = list.Count
            };

            return response;
        }
    }
}
