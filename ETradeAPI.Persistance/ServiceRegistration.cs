using ETradeAPI.Application.Abstraction;
using ETradeAPI.Persistance.Concretes;
using ETradeAPI.Persistance.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;


namespace ETradeAPI.Persistance
{
    public static class ServiceRegistration
    {
        public static void AddPersistanceServices(this IServiceCollection services)
        {
            
            services.AddSingleton<IProductService, ProductService>();
            services.AddDbContext<ETradeAPIDbContext>(options => options.UseNpgsql(
               Configuration.ConnectionString));
        }
    }
}
