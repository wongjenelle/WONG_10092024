
using FluentValidation;
using System.Reflection;
using UpStreamer.Server.Features.Files.Handlers;
using UpStreamer.Server.Infrastructure.Middleware;

namespace UpStreamer.Server.Infrastructure
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection ConfigureServices(this IServiceCollection services)
        {
            services.AddMediatR(cfg => {
                cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
                cfg.AddOpenBehavior(typeof(ValidationMiddleware<,>));
            });
            services.Configure<RouteOptions>(options => options.LowercaseUrls = true);
            services.AddValidatorsFromAssemblyContaining<UploadFileValidator>();

            return services;
        }
    }
}
