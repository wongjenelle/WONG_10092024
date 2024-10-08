using FluentValidation.TestHelper;
using Microsoft.AspNetCore.Http;
using System.Text;
using UpStreamer.Server.Features.Videos.Handlers;
using Xunit;

namespace UpStreamer.Test.Features.Videos.Handlers
{
    public class UploadVideoTests
    {
        [Fact]
        public void When_UploadRequestHasSingleFile_Return_NoValidationError()
        {
            var files = new FormFileCollection { 
                new FormFile(new MemoryStream(Encoding.UTF8.GetBytes("File1")), 0, 0, "Data", "file1.mp4")};
            
            var result = new UploadVideoValidator().TestValidate(new UploadVideoCommand(files));
            result.ShouldNotHaveValidationErrorFor(x => x.Files!.Count);
        }

        [Fact]
        public void When_UploadRequestHasMultipleFiles_Return_ValidationError()
        {
            var files = new FormFileCollection {
                new FormFile(new MemoryStream(Encoding.UTF8.GetBytes("File1")), 0, 0, "Data", "file1.mp4"),
                new FormFile(new MemoryStream(Encoding.UTF8.GetBytes("File2")), 0, 0, "Data", "file2.mp4") };

            var result = new UploadVideoValidator().TestValidate(new UploadVideoCommand(files));
            result.ShouldHaveValidationErrorFor(x => x.Files!.Count);
        }

        [Fact]
        public void When_UploadRequestHasNoFiles_Return_ValidationError()
        {
            var result = new UploadVideoValidator().TestValidate(new UploadVideoCommand(new FormFileCollection()));
            result.ShouldHaveValidationErrorFor(x => x.Files);
        }
    }
}
