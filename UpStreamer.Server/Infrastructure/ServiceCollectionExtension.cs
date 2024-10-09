
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using UpStreamer.Server.Common.Repository;
using UpStreamer.Server.Database;
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
            services.AddExceptionHandler<GlobalExceptionHandler>();
            services.AddProblemDetails();

            return services;
        }

        public static IServiceCollection ConfigureDatabase(this IServiceCollection services, string? connectionString)
        {
            ArgumentNullException.ThrowIfNull(connectionString);

            services.AddDbContext<UpStreamerDbContext>(opt => opt.UseNpgsql(connectionString));
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

            return services;
        }
    }
}
