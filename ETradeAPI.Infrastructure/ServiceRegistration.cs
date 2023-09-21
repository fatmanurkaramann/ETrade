using ETradeAPI.Application.Abstraction.Token;
using ETradeAPI.Application.Services;
using ETradeAPI.Infrastructure.Services;
using ETradeAPI.Infrastructure.Services.Token;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TokenHandler = ETradeAPI.Infrastructure.Services.Token.TokenHandler;

namespace ETradeAPI.Infrastructure
{
    public static class ServiceRegistration
    {
        public static void AddInfrastructureServices(this IServiceCollection services)
        {
            services.AddScoped<IFileService, FileService>();
            services.AddScoped<ITokenHandler, TokenHandler>();
        }
    }
}
