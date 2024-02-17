using ETradeAPI.Application.Abstraction.Token;
using Microsoft.Extensions.DependencyInjection;
using TokenHandler = ETradeAPI.Infrastructure.Services.Token.TokenHandler;

namespace ETradeAPI.Infrastructure
{
    public static class ServiceRegistration
    {
        public static void AddInfrastructureServices(this IServiceCollection services)
        {
            services.AddScoped<ITokenHandler, TokenHandler>();
        }
    }
}
