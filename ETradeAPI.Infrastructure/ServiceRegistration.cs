using ETradeAPI.Application.Abstraction.Storage;
using ETradeAPI.Application.Abstraction.Token;
using ETradeAPI.Infrastructure.enums;
using ETradeAPI.Infrastructure.Services.Storage;
using ETradeAPI.Infrastructure.Services.Storage.Local;
using Microsoft.Extensions.DependencyInjection;
using TokenHandler = ETradeAPI.Infrastructure.Services.Token.TokenHandler;

namespace ETradeAPI.Infrastructure
{
    public static class ServiceRegistration
    {
        public static void AddInfrastructureServices(this IServiceCollection services)
        {
            services.AddScoped<ITokenHandler, TokenHandler>();
            services.AddScoped<IStorageService, StorageService>();
        }
        public static void AddStorage<T>(this IServiceCollection services) where T : class, IStorage
        {
            services.AddScoped<IStorage, T>();
        }
        public static void AddStorage(this IServiceCollection services,StorageType storageType)
        {
            switch (storageType)
            {
                case StorageType.Local:
                    services.AddScoped<IStorage, LocalStorage>();
                    break;
                default:
                     services.AddScoped<IStorage, LocalStorage>();
                    break;

            }
        }
    }
}
