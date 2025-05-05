using DocAccessApproval.Application.Security.Encryption;
using DocAccessApproval.Application.Security.JWT;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Web;

namespace DocAccessApproval.Application.Extensions;

public static class JwtRegistrationExtensions
{
    public static IServiceCollection AddJwtAuthentication(this IServiceCollection services, IConfiguration configuration, bool forSignalR = false)
    {
        TokenOptions? tokenOptions = configuration.GetSection("TokenOptions").Get<TokenOptions>();
        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidIssuer = tokenOptions.Issuer,
                ValidAudience = tokenOptions.Audience,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = SecurityKeyHelper.CreateSecurityKey(tokenOptions.SecurityKey),
            };
            if (forSignalR)
            {
                options.Events = new JwtBearerEvents
                {
                    OnMessageReceived = context =>
                    {
                        var accessToken = HttpUtility.UrlDecode(context.Request.Query["access_token"]);

                        var pathValue = context.HttpContext.Request.Path.Value;
                        if (pathValue != null && !string.IsNullOrEmpty(accessToken) && pathValue.StartsWith("/hubs/"))
                        {
                            context.Token = accessToken;
                        }
                        return Task.CompletedTask;
                    }
                };
            }
        });

        return services;
    }

    public static IServiceCollection AddSwaggerWithJwt(this IServiceCollection services)
    {
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen(opt =>
        {
            opt.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Name = "Authorization",
                Type = SecuritySchemeType.ApiKey,
                Scheme = "Bearer",
                BearerFormat = "JWT",
                In = ParameterLocation.Header,
                Description =
                    "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 12345.54321\""
            });
            opt.AddSecurityRequirement(new OpenApiSecurityRequirement {
            {
                new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference
                    {
                       Type = ReferenceType.SecurityScheme,
                       Id = "Bearer"
                    }
                },
                new string[] { }
            }});
        });
        return services;
    }
}
