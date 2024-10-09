using FluentValidation;
using MediatR;
using UpStreamer.Server.Database;
using UpStreamer.Server.Entities;
using UpStreamer.Server.Features.Videos.Constants;
using UpStreamer.Server.Features.Videos.DTOs;

namespace UpStreamer.Server.Features.Videos.Handlers
{
    public class CreateVideoValidator : AbstractValidator<CreateVideoCommand>
    {
        public CreateVideoValidator()
        {
            RuleFor(x => x.Request.Title).NotEmpty()
               .WithMessage("Video title is required!");
            RuleFor(x => x.Request.Title.Length).LessThanOrEqualTo(VideosConstants.MAXSIZE_TITLE)
               .WithMessage("Description must not exceed 100 characters!");
            RuleFor(x => x.Request.Description.Length).LessThanOrEqualTo(VideosConstants.MAXSIZE_DESCRIPTION)
                .WithMessage("Description must not exceed 160 characters!");
        }
    }

    public class CreateVideoCommand(CreateVideoRequest request) : IRequest<int>
    {
        public CreateVideoRequest Request { get; private set; } = request;
    }

    public class CreateVideoHandler(IGenericRepository<Video> videoRepo, IGenericRepository<Category> categoryRepo) : IRequestHandler<CreateVideoCommand, int>
    {
        public async Task<int> Handle(CreateVideoCommand command, CancellationToken cancellationToken)
        {
            var request = command.Request;

            var category = categoryRepo.Get(x => x.Name.ToLower().Equals(request.Category.ToLower()));
            category ??= new Category { Name = request.Category };

            var video = new Video { Title = request.Title, Description = request.Description, Category = category };
            videoRepo.Create(video);
            videoRepo.Save();

            return video.Id;
        }
    }
}
