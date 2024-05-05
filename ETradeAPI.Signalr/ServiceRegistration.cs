using ETradeAPI.Application.Abstraction.Hubs;
using ETradeAPI.Signalr.HubServices;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETradeAPI.Signalr
{
    public static class ServiceRegistration
    {
        public static void AddSignalrServices(this IServiceCollection services)
        {
            services.AddTransient<IProductHubService, ProductHubService>();
            services.AddSignalR();
        }
    }
}
