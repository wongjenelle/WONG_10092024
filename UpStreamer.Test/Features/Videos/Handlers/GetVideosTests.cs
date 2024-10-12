using AutoFixture.Xunit2;
using FluentAssertions;
using Microsoft.EntityFrameworkCore.Query;
using Moq;
using System.Linq.Expressions;
using UpStreamer.Server.Common.DTOs;
using UpStreamer.Server.Common.Repository;
using UpStreamer.Server.Database.Entities;
using UpStreamer.Server.Features.Videos.Handlers;
using UpStreamer.Test.Features.Videos.Helpers;
using Xunit;

namespace UpStreamer.Test.Features.Videos.Handlers
{
    public class GetVideosTests
    {
        [Theory]
        [InlineAutoData]
        public async Task When_GetVideosHasEmptyParameters_Return_DefaultPagedResult([Frozen] Mock<IGenericRepository<Video>> repository)
        {
            // arrange
            var pagedParameters = new PagedDto { Skip = null, Take = null };
            repository.Setup(x => x.GetList(It.Is<Expression<Func<Video, bool>>>(x => true),
                    It.IsAny<Func<IQueryable<Video>, IIncludableQueryable<Video, object>>>()))
                .Returns(VideoTestHelper.GetVideos());

            // act
            var sut = new GetVideosHandler(repository.Object, new MockMapper().GetMapper());
            var result = await sut.Handle(new GetVideosQuery(pagedParameters), default);

            // assert
            result.Videos.Count.Should().Be(10);
            result.Total.Should().BeGreaterThan(10);
        }

        [Theory]
        [InlineAutoData]
        public async Task When_GetVideosHasPagedParameters_Return_PagedResult([Frozen] Mock<IGenericRepository<Video>> repository)
        {
            // arrange
            var pagedParameters = new PagedDto { Skip = 0, Take = 15 };
            repository.Setup(x => x.GetList(It.Is<Expression<Func<Video, bool>>>(x => true),
                    It.IsAny<Func<IQueryable<Video>, IIncludableQueryable<Video, object>>>()))
                .Returns(VideoTestHelper.GetVideos());

            // act
            var sut = new GetVideosHandler(repository.Object, new MockMapper().GetMapper());
            var result = await sut.Handle(new GetVideosQuery(pagedParameters), default);

            // assert
            result.Videos.Count.Should().Be(15);
            result.Total.Should().BeGreaterThan(10);
        }

        [Theory]
        [InlineAutoData]
        public async Task When_GetVideosHasNoResults_Return_EmptyPagedResult([Frozen] Mock<IGenericRepository<Video>> repository)
        {
            // arrange
            var pagedParameters = new PagedDto { Skip = 0, Take = 15 };
            repository.Setup(x => x.GetList(It.Is<Expression<Func<Video, bool>>>(x => true),
                    It.IsAny<Func<IQueryable<Video>, IIncludableQueryable<Video, object>>>()))
                .Returns([]);

            // act
            var sut = new GetVideosHandler(repository.Object, new MockMapper().GetMapper());
            var result = await sut.Handle(new GetVideosQuery(pagedParameters), default);

            // assert
            result.Videos.Count.Should().Be(0);
            result.Total.Should().Be(0);
        }


    }
}
