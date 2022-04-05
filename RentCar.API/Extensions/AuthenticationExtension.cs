using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using RentCar.API.Settings;
using System.Text.Json;

namespace RentCar.API.Extensions
{
    public static class AuthenticationExtension
    {
        public static void AddAuthenticationServices(this IServiceCollection services, IConfiguration configuration)
        {
            var openIdConnectSettings = configuration.GetSection("OpenIdConnectSettings").Get<OpenIdConnectSettings>();

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.Audience = openIdConnectSettings.Audience;
                    options.Authority = openIdConnectSettings.Issuer;
                    options.RequireHttpsMetadata = false;
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateAudience = true,
                        ValidateIssuerSigningKey = true,
                        ValidateIssuer = true,
                        ValidateLifetime = true,
                        ValidIssuer = openIdConnectSettings.Issuer,
                        ValidAudience = openIdConnectSettings.Audience
                    };

                    options.Events = new JwtBearerEvents
                    {
                        OnAuthenticationFailed = ctx =>
                        {
                            ctx.Fail(ctx.Exception);
                            ctx.Response.StatusCode = 401;
                            ctx.Response.ContentType = "application/json";

                            return ctx.Response.WriteAsync(JsonSerializer.Serialize(new
                            {
                                StatusCode = 401,
                                Message = ctx.Exception.Message
                            }));
                        }
                    };
                });
        }
    }
}
