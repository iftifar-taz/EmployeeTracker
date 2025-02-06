using Asp.Versioning;
using FluentValidation;
using FluentValidation.AspNetCore;
using Identity.Application.Behaviors.Validators;
using Identity.Application.Features.Sessions.Commands;
using Identity.Core.Interfaces;
using Identity.Infrastructure.Persistence;
using Identity.Infrastructure.Services;

namespace Identity.Web.Configurations
{
    public static class ApplicationConfiguration
    {
        public static IServiceCollection ConfigureApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IJwtTokenService, JwtTokenService>();

            services.AddMediatR(config =>
            {
                config.RegisterServicesFromAssemblyContaining<CreateSessionCommand>();
            });

            services.AddValidatorsFromAssemblyContaining<CreateSessionCommandValidator>();
            services.AddFluentValidationAutoValidation();

            services.AddApiVersioning(options =>
            {
                options.DefaultApiVersion = new ApiVersion(1, 0);
                options.AssumeDefaultVersionWhenUnspecified = true;
                options.ReportApiVersions = true;
            }).AddApiExplorer(options =>
            {
                options.GroupNameFormat = "'v'VVV";
                options.SubstituteApiVersionInUrl = true;
            });

            services.AddControllers();
            services.AddEndpointsApiExplorer();
            return services;
        }
    }
}
