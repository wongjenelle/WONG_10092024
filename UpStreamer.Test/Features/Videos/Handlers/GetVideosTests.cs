using AutoFixture.Xunit2;
using FluentAssertions;
using MediatR.Pipeline;
using Microsoft.EntityFrameworkCore.Query;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
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
            repository.Setup(x => x.GetList(It.IsAny<Expression<Func<Video, bool>>>(), It.IsAny<Func<IQueryable<Video>, IIncludableQueryable<Video, Category>>>()))
                .Returns(VideoTestHelper.GetVideos().AsQueryable());
            
            // act
            var sut = new GetVideosHandler(repository.Object);
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
            repository.Setup(x => x.GetList(It.IsAny<Expression<Func<Video, bool>>>(), It.IsAny<Func<IQueryable<Video>, IIncludableQueryable<Video, Category>>>()))
                .Returns(VideoTestHelper.GetVideos().AsQueryable());

            // act
            var sut = new GetVideosHandler(repository.Object);
            var result = await sut.Handle(new GetVideosQuery(pagedParameters), default);

            // assert
            result.Videos.Count.Should().Be(15);
            result.Total.Should().BeGreaterThan(10);
        }

        
    }
}
