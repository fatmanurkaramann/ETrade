using ETradeAPI.Application.Abstraction.Services;
using ETradeAPI.Application.ViewModels.Basket;
using ETradeAPI.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETradeAPI.Persistance.Services
{
    public class BasketService : IBasketService
    {
        public Task AddItemToBasketAsync(CreateBasketVM basketItem)
        {
            throw new NotImplementedException();
        }

        public Task<List<BasketItem>> GetBasketItemsAsync()
        {
            throw new NotImplementedException();
        }

        public Task RemoveBasketItemAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task UpdateQuantityAsync(UpdateBasketVM basketItem)
        {
            throw new NotImplementedException();
        }
    }
}
