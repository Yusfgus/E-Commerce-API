using System.Text;
using System.Text.Json.Serialization;
using ControllerFluentValidations.Validators;
using E_Commerce.Data;
using E_Commerce.Data.Repositories.Abstractions;
using E_Commerce.Data.Repositories.Implementations;
using E_Commerce.Filters;
using E_Commerce.Services.Abstractions;
using E_Commerce.Services.Identity;
using E_Commerce.Services.Implementations;
using FluentValidation;
using FluentValidation.AspNetCore;
using MechanicShop.Infrastructure.Data.Interceptors;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.IdentityModel.Tokens;

namespace E_Commerce;

public static class DependencyInjection
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
    {
        return services
            .AddControllerWithConfiguration()
            .AddAppDbContext(configuration)
            .AddAppRepositories()
            .AddAppServices()
            .AddJwtAuthentication(configuration)
            .AddFluentValidation()
            .AddOtherServices(configuration);
    }

    private static IServiceCollection AddControllerWithConfiguration(this IServiceCollection services)
    {
        services.AddControllers(options =>
        {
            options.Filters.Add<ApiResponseFilter>();
        })
        .AddJsonOptions(options =>
        {
            // options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
            options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
        });

        return services;
    }

    private static IServiceCollection AddAppDbContext(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<ISaveChangesInterceptor, AuditableEntityInterceptor>();

        services.AddDbContext<AppDbContext>((sp, options) =>
        {
            options.UseSqlite(configuration.GetConnectionString("SQLite"))
                   .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);

            options.AddInterceptors(sp.GetServices<ISaveChangesInterceptor>());
        });

        return services;
    }

    private static IServiceCollection AddAppRepositories(this IServiceCollection services)
    {
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();
        services.AddScoped<ICustomerRepository, CustomerRepository>();
        services.AddScoped<IVendorRepository, VendorRepository>();
        services.AddScoped<IProductRepository, ProductRepository>();
        services.AddScoped<ICategoryRepository, CategoryRepository>();
        services.AddScoped<IOrderRepository, OrderRepository>();

        return services;
    }

    private static IServiceCollection AddAppServices(this IServiceCollection services)
    {
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<IAdminService, AdminService>();
        services.AddScoped<IProductService, ProductService>();
        services.AddScoped<IOrderService, OrderService>();
        services.AddScoped<JwtTokenProvider>();

        return services;
    }

    private static IServiceCollection AddOtherServices(this IServiceCollection services, IConfiguration configuration)
    {
        services
            .AddSingleton(TimeProvider.System);

        return services;
    }

    private static IServiceCollection AddFluentValidation(this IServiceCollection services)
    {
        services.AddFluentValidationAutoValidation();

        services.AddValidatorsFromAssemblyContaining<CreateProductRequestValidator>();

        return services;
    }

    private static IServiceCollection AddJwtAuthentication(this IServiceCollection services, IConfiguration configuration)
    {        
        var jwtSettings = configuration.GetSection("JwtSettings");

        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer(options =>
        {

            options.TokenValidationParameters = new()
            {
                ValidateIssuer = true,
                ValidIssuer = jwtSettings["Issuer"],
                ValidateAudience = true,
                ValidAudience = jwtSettings["Audience"],
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["SecretKey"]!)),
                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero,
            };
        });

        return services;
    }
}
