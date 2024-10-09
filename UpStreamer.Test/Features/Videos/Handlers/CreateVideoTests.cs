using AutoFixture.Xunit2;
using FluentAssertions;
using FluentValidation.TestHelper;
using Moq;
using System.Linq.Expressions;
using UpStreamer.Server.Common.Repository;
using UpStreamer.Server.Database.Entities;
using UpStreamer.Server.Features.Videos.DTOs;
using UpStreamer.Server.Features.Videos.Handlers;
using Xunit;

namespace UpStreamer.Test.Features.Videos.Handlers
{
    public class CreateVideoTests
    {
        [Fact]
        public void When_CreateRequestIsValid_Return_NoValidationError()
        {
            var request = new CreateVideoRequestDto
            {
                Title = "Title",
                Description = "Description",
                Category = "Category",
            };

            var result = new CreateVideoValidator().TestValidate(new CreateVideoCommand(request));
            result.IsValid.Should().BeTrue();
        }

        [Fact]
        public void When_CreateRequestHasExceededTextLimit_Return_ValidationError()
        {
            var request = new CreateVideoRequestDto
            {
                Title = new string('A', 101),
                Description = new string('A', 161),
                Category = "Category"
            };

            var result = new CreateVideoValidator().TestValidate(new CreateVideoCommand(request));
            result.IsValid.Should().BeFalse();
            result.ShouldHaveValidationErrorFor(x => x.Request.Title.Length);
            result.ShouldHaveValidationErrorFor(x => x.Request.Description.Length);
        }

        [Fact]
        public void When_CreateRequestHasEmptyTitle_Return_ValidationError()
        {
            var request = new CreateVideoRequestDto
            {
                Title = "",
                Description = new string('A', 160),
                Category = "Category"
            };

            var result = new CreateVideoValidator().TestValidate(new CreateVideoCommand(request));
            result.IsValid.Should().BeFalse();
            result.ShouldHaveValidationErrorFor(x => x.Request.Title);
            result.ShouldNotHaveValidationErrorFor(x => x.Request.Description.Length);
        }

        [Theory]
        [InlineAutoData]
        public async Task When_CreateVideoUnsuccessful_Expect_NoDatabaseChanges(
            [Frozen] Mock<IGenericRepository<Video>> videoRepo, 
            [Frozen] Mock<IGenericRepository<Category>> categoryRepo, 
            CreateVideoRequestDto request)
        {
            // arrange
            categoryRepo.Setup(x => x.Get(It.IsAny<Expression<Func<Category, bool>>>())).Throws<Exception>();

            // act
            var sut = new CreateVideoHandler(videoRepo.Object, categoryRepo.Object);
            Func<Task> createAction = async () => await sut.Handle(new CreateVideoCommand(request), default);

            // assert
            await createAction.Should().ThrowAsync<Exception>();
            videoRepo.Verify(x => x.Create(It.IsAny<Video>()), Times.Never);
        }

        [Theory]
        [InlineAutoData]
        public async Task When_UploadSuccessful_Return_VideoId(
            [Frozen] Mock<IGenericRepository<Video>> videoRepo,
            [Frozen] Mock<IGenericRepository<Category>> categoryRepo,
            CreateVideoRequestDto request)
        {
            // arrange
            categoryRepo.Setup(x => x.Get(It.IsAny<Expression<Func<Category, bool>>>())).Returns(new Category { Id = 1, Name = "category"});

            // act
            var sut = new CreateVideoHandler(videoRepo.Object, categoryRepo.Object);
            var result = await sut.Handle(new CreateVideoCommand(request), default);

            // assert
            videoRepo.Verify(x => x.Create(It.IsAny<Video>()), Times.Once);
        }
    }
}
