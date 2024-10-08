
using FluentValidation;
using System.Reflection;
using UpStreamer.Server.Behaviors;
using UpStreamer.Server.Features.Videos.Handlers;

namespace UpStreamer.Server.Infrastructure
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection ConfigureServices(this IServiceCollection services)
        {
            services.AddMediatR(cfg => {
                cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
                cfg.AddOpenBehavior(typeof(ValidationBehavior<,>));
            });
            services.Configure<RouteOptions>(options => options.LowercaseUrls = true);
            services.AddValidatorsFromAssemblyContaining<UploadVideoValidator>();

            return services;
        }
    }
}
