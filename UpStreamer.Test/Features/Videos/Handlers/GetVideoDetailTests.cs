using AutoFixture.Xunit2;
using FluentAssertions;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.Extensions.Configuration;
using Moq;
using System.Linq.Expressions;
using UpStreamer.Server.Common.Repository;
using UpStreamer.Server.Database.Entities;
using UpStreamer.Server.Features.Videos.Handlers;
using UpStreamer.Test.Features.Videos.Helpers;
using Xunit;

namespace UpStreamer.Test.Features.Videos.Handlers
{
    public class GetVideoDetailTests
    {
        [Theory]
        [InlineAutoData]
        public async Task When_GetVideoDetail_IdExisting_Return_VideoDetails([Frozen] Mock<IGenericRepository<Video>> repository,
            [Frozen] Mock<IConfiguration> configuration,
            int id)
        {
            // arrange
            var video = new Video()
            {
                Id = id,
                Title = "title",
                Description = "description",
                Category = new() { Name = "name" },
                FilePath = "Upload\\Video.mp4"
            };
            repository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<Video, bool>>>(),
                    It.IsAny<Func<IQueryable<Video>, IIncludableQueryable<Video, object>>>()))
                .ReturnsAsync(video);
            configuration.Setup(x => x["FileHostUrl"]).Returns("http://localhost:8080");

            // act
            var sut = new GetVideoDetailHandler(repository.Object, new MockMapper().GetMapper(), configuration.Object);
            var result = await sut.Handle(new GetVideoDetailQuery(id), default);

            // assert
            result.Should().NotBeNull();
            result.FilePath.Should().NotStartWith(video.FilePath);
        }

        [Theory]
        [InlineAutoData]
        public async Task When_GetVideoDetail_HasNoFilePath_Return_VideoDetailsWithoutFilePath([Frozen] Mock<IGenericRepository<Video>> repository,
            [Frozen] Mock<IConfiguration> configuration,
            int id)
        {
            // arrange
            var video = new Video()
            {
                Id = id,
                Title = "title",
                Description = "description",
                Category = new() { Name = "name" },
                FilePath = ""
            };
            repository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<Video, bool>>>(),
                    It.IsAny<Func<IQueryable<Video>, IIncludableQueryable<Video, object>>>()))
                .ReturnsAsync(video);
            configuration.Setup(x => x["FileHostUrl"]).Returns("http://localhost:8080");

            // act
            var sut = new GetVideoDetailHandler(repository.Object, new MockMapper().GetMapper(), configuration.Object);
            var result = await sut.Handle(new GetVideoDetailQuery(id), default);

            // assert
            result.Should().NotBeNull();
            result.FilePath.Should().BeEmpty();
            configuration.Verify(x => x["FileHostUrl"], Times.Never());
        }

        [Theory]
        [InlineAutoData]
        public async Task When_GetVideoDetail_IdNotExisting_Return_NotFoundError([Frozen] Mock<IGenericRepository<Video>> repository,
            [Frozen] Mock<IConfiguration> configuration,
            int id)
        {
            // arrange
            repository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<Video, bool>>>(),
                    It.IsAny<Func<IQueryable<Video>, IIncludableQueryable<Video, object>>>()))
                .ReturnsAsync((Video)null);
            configuration.Setup(x => x["FileHostUrl"]).Returns("http://localhost:8080");

            // act
            var sut = new GetVideoDetailHandler(repository.Object, new MockMapper().GetMapper(), configuration.Object);
            Func<Task> createAction = async () => await sut.Handle(new GetVideoDetailQuery(id), default);

            // assert
            await createAction.Should().ThrowAsync<KeyNotFoundException>();
        }
    }
}
