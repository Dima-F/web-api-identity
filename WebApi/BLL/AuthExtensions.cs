    using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;

namespace WebApi.BLL;

public static class AuthExtensions
{
    public static IServiceCollection AddAuth(this IServiceCollection serviceCollection, IConfiguration configuration)
    {
        var authSettings = configuration.GetSection(nameof(AuthSettings)).Get<AuthSettings>();
        serviceCollection
            .AddSingleton<IAuthorizationHandler, PermissionRequirenmentsHandler>()
            .AddAuthorization(
                x => x.AddPolicy("OnlyForMicrosoft", builder => builder.RequireClaim("Company", "Microsoft")))
            .AddAuthorization(x => x.AddPolicy(Permissions.Read, builder => builder.Requirements.Add(new PermissionRequirements(Permissions.Read))))
            .AddAuthorization(x => x.AddPolicy(Permissions.Delete, builder => builder.Requirements.Add(new PermissionRequirements(Permissions.Delete))))
            .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(o =>
            {
                if (authSettings?.SecretKey != null)
                    o.TokenValidationParameters = new TokenValidationParameters()
                    {
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(authSettings.SecretKey))
                    };
            });
        return serviceCollection;
    }
}