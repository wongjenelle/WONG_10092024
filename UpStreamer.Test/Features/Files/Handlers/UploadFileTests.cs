using FluentAssertions;
using FluentValidation.TestHelper;
using Microsoft.AspNetCore.Http;
using System.Text;
using UpStreamer.Server.Features.Files.Handlers;
using Xunit;

namespace UpStreamer.Test.Features.Files.Handlers
{
    public class UploadFileTests
    {
        [Fact]
        public void When_UploadRequestHasSingleFile_Return_NoValidationError()
        {
            var files = new FormFileCollection {
                new FormFile(new MemoryStream(Encoding.UTF8.GetBytes("File1")), 0, 1000, "Data", "file1.mp4")
            };

            var result = new UploadFileValidator().TestValidate(new UploadFileCommand(files));
            result.IsValid.Should().BeTrue();
        }

        [Fact]
        public void When_UploadRequestHasSingleFileExceedingMaxLength_Return_ValidationError()
        {
            var files = new FormFileCollection {
                new FormFile(new MemoryStream(Encoding.UTF8.GetBytes("File1")), 0, 1001, "Data", "file1.mp4")
            };

            var result = new UploadFileValidator().TestValidate(new UploadFileCommand(files));
            result.IsValid.Should().BeFalse();
            result.ShouldHaveValidationErrorFor(x => x.Files![0].Length);
        }

        [Fact]
        public void When_UploadRequestHasMultipleFiles_Return_ValidationError()
        {
            var files = new FormFileCollection {
                new FormFile(new MemoryStream(Encoding.UTF8.GetBytes("File1")), 0, 0, "Data", "file1.mp4"),
                new FormFile(new MemoryStream(Encoding.UTF8.GetBytes("File2")), 0, 0, "Data", "file2.mp4") };

            var result = new UploadFileValidator().TestValidate(new UploadFileCommand(files));
            result.IsValid.Should().BeFalse();
            result.ShouldHaveValidationErrorFor(x => x.Files!.Count);
        }

        [Fact]
        public void When_UploadRequestHasNoFiles_Return_ValidationError()
        {
            var result = new UploadFileValidator().TestValidate(new UploadFileCommand(new FormFileCollection()));
            result.IsValid.Should().BeFalse();
            result.ShouldHaveValidationErrorFor(x => x.Files);
        }

        [Fact]
        public void When_UploadRequestFileIsEmpty_Return_ValidationError()
        {
            var files = new FormFileCollection {
                new FormFile(new MemoryStream(Encoding.UTF8.GetBytes("")), 0, 0, "Data", "file1.mp4")};

            var result = new UploadFileValidator().TestValidate(new UploadFileCommand(files));
            result.IsValid.Should().BeFalse();
            result.ShouldHaveValidationErrorFor(x => x.Files![0].Length);
        }

        [Fact]
        public void When_UploadRequestFileHasInvalidFormat_Return_ValidationError()
        {
            var files = new FormFileCollection {
                new FormFile(new MemoryStream(Encoding.UTF8.GetBytes("File1")), 0, 0, "Data", "file1.txt")};

            var result = new UploadFileValidator().TestValidate(new UploadFileCommand(files));
            result.IsValid.Should().BeFalse();
            result.ShouldHaveValidationErrorFor(x => x.Files![0]);
        }
    }
}
