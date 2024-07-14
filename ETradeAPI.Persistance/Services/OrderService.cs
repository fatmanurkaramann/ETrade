using ETradeAPI.Application.Abstraction.Services;
using ETradeAPI.Application.DTOs.Order;
using ETradeAPI.Application.Repositories;

namespace ETradeAPI.Persistance.Services
{
    public class OrderService : IOrderService
    {
        readonly IOrderWriteRepository _orderWriteRepository;

        public OrderService(IOrderWriteRepository orderWriteRepository)
        {
            _orderWriteRepository = orderWriteRepository;
        }

        public async Task CreateOrder(CreateOrder order)
        {
            await _orderWriteRepository.AddAsync(new()
            {
                Address = order.Address,
                Description = order.Description,
                Id=Guid.Parse(order.BasketId)
            });
            await _orderWriteRepository.SaveAsync();

        }
    }
}
