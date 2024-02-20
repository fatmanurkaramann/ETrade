using ETradeAPI.Application.Abstraction.Services;
using ETradeAPI.Application.Repositories;
using ETradeAPI.Application.Repositories.Basket;
using ETradeAPI.Application.Repositories.BasketItem;
using ETradeAPI.Application.Repositories.File;
using ETradeAPI.Domain.Entities.Identity;
using ETradeAPI.Persistance.Contexts;
using ETradeAPI.Persistance.Repositories;
using ETradeAPI.Persistance.Repositories.Basket;
using ETradeAPI.Persistance.Repositories.BasketItem;
using ETradeAPI.Persistance.Services;
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
            
            services.AddDbContext<ETradeAPIDbContext>(options => options.UseNpgsql(Configuration.ConnectionString));

            services.AddIdentity<AppUser, AppRole>(options =>
            {
                options.Password.RequiredLength = 3;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;
                options.User.RequireUniqueEmail = false;

            }).AddEntityFrameworkStores<ETradeAPIDbContext>();

            services.AddScoped<ICustomerReadRepository, CustomerReadRepository>();
            services.AddScoped<ICustomerWriteRepository, CustomerWriteRepository>();
            services.AddScoped<IProductReadRepository, ProductReadRepository>();
            services.AddScoped<IProductWriteRepository, ProductWriteRepository>();
            services.AddScoped<IOrderWriteRepository, OrderWriteRepository>();
            services.AddScoped<IOrderReadRepository, OrderReadRepository>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IFileReadRepository, FileReadRepository>();
            services.AddScoped<IBasketItemReadRepository,BasketItemReadRepository>();
            services.AddScoped<IBasketItemWriteRepository, BasketItemWriteRepository>();
            services.AddScoped<IBasketReadRepository, BasketReadRepository>();
            services.AddScoped<IBasketWriteRepository, BasketWriteRepository>();



        }
    }
}
