using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RentCar.API.Settings;

namespace RentCar.API.Extensions
{
    public static class CorsExtension
    {
        public static void AddCorsServices(this IServiceCollection services, IConfiguration configuration)
        {
            var corsSettings = configuration.GetSection("CorsSettings").Get<CorsSettings>();

            services.AddCors(opt =>
            {
                opt.AddPolicy(corsSettings.PolicyName, builder =>
                {
                    builder.WithOrigins(corsSettings.Origin)
                    .AllowAnyHeader()
                    .WithExposedHeaders("Content-Disposition")
                    .AllowAnyMethod();
                });
            });
        }

        public static IApplicationBuilder UseCors(this IApplicationBuilder app, IConfiguration configuration)
        {
            var corsSettings = configuration.GetSection("CorsSettings").Get<CorsSettings>();
            return app.UseCors(corsSettings.PolicyName);
        }
    }
}
