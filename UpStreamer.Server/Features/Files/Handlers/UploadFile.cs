using FluentValidation;
using MediatR;
using UpStreamer.Server.Common.Extensions;
using UpStreamer.Server.Features.Files.Constants;
using UpStreamer.Server.Features.Videos.DTOs;

namespace UpStreamer.Server.Features.Files.Handlers
{
    public class UploadFileValidator : AbstractValidator<UploadFileCommand>
    {
        public UploadFileValidator()
        {
            RuleFor(x => x.Files).NotEmpty().WithMessage("Uploaded file cannot be empty!");
            RuleFor(x => x.Files!.Count).Equal(1)
                .When(x => x.Files.IsNullOrEmpty() == false)
                .WithMessage("Multiple file upload is not supported!");
            RuleFor(x => x.Files![0].Length).NotEmpty()
                .When(x => x.Files.Count == 1)
                .WithMessage("Uploaded file cannot be empty!");
            RuleFor(x => x.Files![0].Length).LessThanOrEqualTo(FilesConstants.MAX_BYTES)
                    .When(x => x.Files.Count == 1)
                    .WithMessage("Uploaded file cannot exceed 100MB!");
            RuleFor(x => x.Files![0]).Must(file => FilesConstants.ALLOWED_EXTENSIONS.Any(ext => ext.Equals(Path.GetExtension(file.FileName))))
                .When(x => x.Files.Count == 1)
                .WithMessage("File format is invalid! Accepted file formats: .mp4, .avi, .mov");
        }
    }

    public class UploadFileCommand(IFormFileCollection files) : IRequest<UploadFileResponseDto>
    {
        public IFormFileCollection Files { get; private set; } = files;
    }

    public class UploadFileHandler() : IRequestHandler<UploadFileCommand, UploadFileResponseDto>
    {
        public async Task<UploadFileResponseDto> Handle(UploadFileCommand command, CancellationToken cancellationToken)
        {
            var file = command!.Files![0];

            // TODO: upload file to a configured file upload area, not GetCurrentDirectory()
            string pathToSave = Path.Combine(Directory.GetCurrentDirectory(), FilesConstants.FOLDER_UPLOAD);
            if (!Directory.Exists(pathToSave))
            {
                Directory.CreateDirectory(pathToSave);
            }

            var fileName = $"{DateTimeOffset.UtcNow.ToUnixTimeSeconds()}{Path.GetExtension(file.FileName)}";
            using (var stream = new FileStream(Path.Combine(pathToSave, fileName), FileMode.Create))
            {
                command.Files[0].CopyTo(stream);
            }

            return new() { FilePath = Path.Combine(FilesConstants.FOLDER_UPLOAD, fileName) };
        }
    }
}
