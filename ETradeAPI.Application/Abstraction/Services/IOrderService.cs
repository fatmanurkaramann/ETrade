using ETradeAPI.Application.DTOs.Order;

namespace ETradeAPI.Application.Abstraction.Services
{
    public interface IOrderService
    {
        Task CreateOrder(CreateOrder order);
    }
}
