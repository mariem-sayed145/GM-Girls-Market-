using GM.Application.Abstractions.Persistence;
using GM.Infrastructure.Persistence.Context;
using GM.Infrastructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using GM.Application.Abstractions.Services;
using GM.Infrastructure.Services;
using GM.Infrastructure.Authentication;

namespace GM.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddDbContext<GMDbContext>(options =>
            options.UseSqlServer(
                configuration.GetConnectionString("DefaultConnection")));

        services.AddScoped<IContactInquiryRepository,
            ContactInquiryRepository>();

        services.AddScoped<IProductRepository,
            ProductRepository>();

        services.AddScoped<IFileStorageService, FileStorageService>();

        services.AddScoped<IContactInquiryRepository,
        ContactInquiryRepository>();

        services.AddScoped<IProductRepository,
            ProductRepository>();

        services.AddScoped<IFileStorageService,
            FileStorageService>();

        services.AddScoped<IPasswordHasher,
           PasswordHasher>();

        services.AddScoped<IUserRepository,
          UserRepository>();

        services.AddScoped<IJwtService, JwtService>();

        return services;
    }
}