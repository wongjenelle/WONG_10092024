using FluentValidation;
using MediatR;
using System.Net.Http.Headers;
using System.Transactions;
using UpStreamer.Server.Database;
using UpStreamer.Server.Entities;
using UpStreamer.Server.Features.Videos.Constants;

namespace UpStreamer.Server.Features.Videos.Handlers
{
    public class UploadVideoValidator : AbstractValidator<UploadVideoCommand>
    {
        public UploadVideoValidator()
        {
            RuleFor(x => x.Files).NotNull().NotEmpty();
            RuleFor(x => x.Files!.Count).LessThan(2).WithMessage("Multiple file upload is not supported!")
                .When(x => x.Files is not null);
        }
    }

    public class UploadVideoCommand(IFormFileCollection files) : IRequest
    {
        public IFormFileCollection? Files { get; private set; } = files;
    }

    public class UploadVideoHandler() : IRequestHandler<UploadVideoCommand>
    {
        public async Task Handle(UploadVideoCommand command, CancellationToken cancellationToken)
        {
            if (command.Files != null)
            {
                var folderPath = Path.Combine(VideosConstants.FOLDER_UPLOAD, DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString());
                var fileName = ContentDispositionHeaderValue.Parse(command.Files[0].ContentDisposition).FileName?.Trim('"') ?? VideosConstants.DEFAULT_FILENAME;
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), folderPath, fileName);
                var dbPath = Path.Combine(folderPath, fileName);
                // TODO: security

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    command.Files[0].CopyTo(stream);
                }
            }
        }
    }
}
