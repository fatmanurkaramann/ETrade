using ETradeAPI.Application.ViewModels.Basket;
using ETradeAPI.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETradeAPI.Application.Abstraction.Services
{
    public interface IBasketService
    {
        public Task<List<BasketItem>> GetBasketItemsAsync();
        public Task AddItemToBasketAsync(CreateBasketVM basketItem);
        public Task UpdateQuantityAsync(UpdateBasketVM basketItem);
        public Task RemoveBasketItemAsync(string id);
    }
}
