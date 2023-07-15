using ETradeAPI.Application.Abstraction;
using ETradeAPI.Application.Repositories;
using ETradeAPI.Persistance.Concretes;
using ETradeAPI.Persistance.Contexts;
using ETradeAPI.Persistance.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;


namespace ETradeAPI.Persistance
{
    //IOC Container
    public static class ServiceRegistration
    {
        public static void AddPersistanceServices(this IServiceCollection services)
        {
            
            services.AddSingleton<IProductService, ProductService>();
            services.AddDbContext<ETradeAPIDbContext>(options => options.UseNpgsql(Configuration.ConnectionString), ServiceLifetime.Singleton);
            services.AddSingleton<ICustomerReadRepository, CustomerReadRepository>();
            services.AddSingleton<ICustomerWriteRepository, CustomerWriteRepository>();
            services.AddSingleton<IProductReadRepository, ProductReadRepository>();
            services.AddSingleton<IProductWriteRepository, ProductWriteRepository>();
            services.AddSingleton<IOrderWriteRepository, OrderWriteRepository>();
            services.AddSingleton<IOrderReadRepository, OrderReadRepository>();

        }
    }
}
