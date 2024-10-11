using AutoFixture.Xunit2;
using FluentAssertions;
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
        public async Task When_GetVideoDetail_IdExisting_Return_VideoDetails([Frozen] Mock<IGenericRepository<Video>> repository, int id)
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
            repository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<Video, bool>>>()))
                .ReturnsAsync(video);

            // act
            var sut = new GetVideoDetailHandler(repository.Object, new MockMapper().GetMapper());
            var result = await sut.Handle(new GetVideoDetailQuery(id), default);

            // assert
            result.Should().NotBeNull();
            result.FilePath.Should().NotStartWith(video.FilePath); // TODO: should start with directory
        }

        [Theory]
        [InlineAutoData]
        public async Task When_GetVideoDetail_IdNotExisting_Return_NotFoundError([Frozen] Mock<IGenericRepository<Video>> repository, int id)
        {
            // arrange
            repository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<Video, bool>>>()))
                .ReturnsAsync((Video)null);

            // act
            var sut = new GetVideoDetailHandler(repository.Object, new MockMapper().GetMapper());
            var result = await sut.Handle(new GetVideoDetailQuery(id), default);

            // assert
            // TODO: should throw 404 not found
        }
    }
}
