using BookStoreApp.API.Configurations;
using BookStoreApp.API.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Serilog;

namespace BookStoreApp.API.Extensions;

public static class ServiceExtensions
{
    public static void ConfigureDatabaseContext(this IServiceCollection services, IConfiguration configuration)
    {
        var connString = configuration.GetConnectionString("BookStoreAppDbConnection");
        services.AddDbContext<BookstoreContext>(options =>
            options.UseSqlServer(connString));
    }

    public static void ConfigureIdentity(this IServiceCollection services)
    {
        services.AddIdentityCore<ApiUser>()
            .AddRoles<IdentityRole>()
            .AddEntityFrameworkStores<BookstoreContext>();
    }

    public static void ConfigureAutoMapper(this IServiceCollection services)
    {
        services.AddAutoMapper(typeof(MapperConfig));
    }

    public static void ConfigureSwagger(this IServiceCollection services)
    {
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "Book Store App API", Version = "v1" });

            c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Description = "JWT Authorization header using the Bearer scheme. Example: \"Bearer {token}\"",
                Name = "Authorization",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.Http,
                Scheme = "Bearer",
                BearerFormat = "JWT"
            });

            c.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    Array.Empty<string>()
                }
            });
        });
    }

    public static void ConfigureLogging(this IHostBuilder host)
    {
        host.UseSerilog((ctx, lc) =>
            lc.WriteTo.Console().ReadFrom.Configuration(ctx.Configuration));
    }

    public static void ConfigureCors(this IServiceCollection services)
    {
        services.AddCors(options =>
        {
            options.AddPolicy("AllowAll",
                b => b.AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowAnyOrigin());
        });
    }
}