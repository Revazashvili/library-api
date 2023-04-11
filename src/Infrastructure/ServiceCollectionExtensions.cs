using Application.Common.Interfaces;
using Infrastructure.Persistence;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddDbContext<LibraryDbContext>(builder => builder.UseInMemoryDatabase(nameof(LibraryDbContext)));

        services.AddScoped<IUnitOfWork, UnitOfWork>();
        
        return services;
    }
}