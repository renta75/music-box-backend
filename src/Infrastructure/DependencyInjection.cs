using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using CleanArchitecture.Application.Common.Interfaces;
using CleanArchitecture.Infrastructure.Files;
using CleanArchitecture.Infrastructure.Identity;
using CleanArchitecture.Infrastructure.Persistence;
using CleanArchitecture.Infrastructure.Services;
using Duende.IdentityServer.Models;
using IdentityModel;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace CleanArchitecture.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        if (configuration.GetValue<bool>("UseInMemoryDatabase"))
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseInMemoryDatabase("CleanArchitectureDb"));
        }
        else
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    configuration.GetConnectionString("DefaultConnection"),
                    b => b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));
        }

        services.AddScoped<IApplicationDbContext>(provider => provider.GetRequiredService<ApplicationDbContext>());

        services.AddScoped<IDomainEventService, DomainEventService>();

        services
            .AddDefaultIdentity<ApplicationUser>()
            .AddRoles<IdentityRole>()
            .AddEntityFrameworkStores<ApplicationDbContext>();

        services.AddIdentityServer()
            .AddApiAuthorization<ApplicationUser, ApplicationDbContext>(options =>
            {
                options.Clients.Add(new Client
                {
                    ClientId = "Flutter",
                    AllowedGrantTypes = { GrantType.ResourceOwnerPassword },
                    ClientSecrets = { new Secret("secret".Sha256()) },
                    AllowedScopes = { "api1", "openid", "profile" },
                    AlwaysIncludeUserClaimsInIdToken = true
                });
            })
            .AddInMemoryApiScopes(GetApiScopes())
            .AddInMemoryApiResources(GetApiResources());
        
                
                

        services.AddTransient<IDateTime, DateTimeService>();
        services.AddTransient<IIdentityService, IdentityService>();
        services.AddTransient<ICsvFileBuilder, CsvFileBuilder>();

        JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();

                                
        services.AddAuthentication("Bearer").AddJwtBearer(
            
        options =>
             {
                 //options.Events = new JwtBearerEvents()
                 //{
                 //    OnTokenValidated = context =>
                 //    {
                 //        var appIdentity = new ClaimsIdentity(JwtBearerDefaults.AuthenticationScheme);


                 //        return Task.CompletedTask;
                 //    }
                 //};

                 options.Authority = "https://matrixcode.hr:7500";
                 //options.RequireHttpsMetadata = false;

                 options.TokenValidationParameters = new
                    TokenValidationParameters()
                    {
                        ValidateAudience = false,
                        NameClaimType= JwtClaimTypes.Subject,
                        ValidateIssuerSigningKey = true
                    };
             });

        services.AddAuthorization(options => {
            options.AddPolicy("CanPurge", policy => policy.RequireRole("Administrator"));
            options.AddPolicy("api1", policy => policy.RequireClaim("scope","api1"));
        });

        




        return services;
    }

    public static IEnumerable<ApiResource> GetApiResources()
    {
        return new List<ApiResource>
            {
                new ApiResource("api1", "BACKEND")
                {
                    Scopes = new List<string>()
                    {
                        "api1"
                    },
                    UserClaims=new[] { "api1" }
                }
            };
    }

    public static IEnumerable<ApiScope> GetApiScopes()
    {
        return new[]
        {
                new ApiScope(name: "api1",   displayName: "Access API Backend", userClaims: new[] { "api1" } )
            };
    }
}
