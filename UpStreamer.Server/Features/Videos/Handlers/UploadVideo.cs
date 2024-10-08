using MediatR;
using System.Net.Http.Headers;

namespace UpStreamer.Server.Features.Videos.Handlers
{
    public class UploadVideoCommand(IFormFileCollection files) : IRequest<string>
    {
        public IFormFileCollection? Files { get; private set; } = files;
    }

    public class UploadVideoHandler : IRequestHandler<UploadVideoCommand, string>
    {
        public async Task<string> Handle(UploadVideoCommand command, CancellationToken cancellationToken)
        {
            var folderName = Path.Combine("Upload","Videos");
            var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);

            if (command.Files != null && command.Files.Count == 1)
            {
                var fileName = ContentDispositionHeaderValue.Parse(command.Files[0].ContentDisposition).FileName?.Trim('"') ?? "";
                var physicalfileName = $"{fileName}_{DateTimeOffset.UtcNow.ToUnixTimeSeconds()}";

                var fullPath = Path.Combine(pathToSave, physicalfileName);
                var dbPath = Path.Combine(folderName, physicalfileName);

                // TODO: save logical file name
                // TODO: security

                using (var stream = new FileStream(fullPath, FileMode.Create))
                {
                    command.Files[0].CopyTo(stream);
                }

                return dbPath;
            }
            else
            {
                throw new Exception(); // TODO: validation and error handling
            }
        }
    }
}
