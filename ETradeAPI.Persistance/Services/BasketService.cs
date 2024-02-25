using ETradeAPI.Application.Abstraction.Services;
using ETradeAPI.Application.Repositories;
using ETradeAPI.Application.Repositories.Basket;
using ETradeAPI.Application.Repositories.BasketItem;
using ETradeAPI.Application.ViewModels.Basket;
using ETradeAPI.Domain.Entities;
using ETradeAPI.Domain.Entities.Identity;
using ETradeAPI.Persistance.Repositories.BasketItem;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETradeAPI.Persistance.Services
{
    public class BasketService : IBasketService
    {
        readonly IHttpContextAccessor _httpContextAccessor;
        readonly UserManager<AppUser> _userManager;
        readonly IOrderReadRepository _orderReadRepository;
        readonly IBasketWriteRepository _basketWriteRepository;
        readonly IBasketItemWriteRepository _basketItemWriteRepository;
        readonly IBasketItemReadRepository _basketItemReadRepository;
        public BasketService(IHttpContextAccessor httpContextAccessor, UserManager<AppUser> userManager, IOrderReadRepository orderReadRepository, IBasketWriteRepository basketWriteRepository, IBasketItemWriteRepository basketItemWriteRepository)
        {
            _httpContextAccessor = httpContextAccessor;
            _userManager = userManager;
            _orderReadRepository = orderReadRepository;
            _basketWriteRepository = basketWriteRepository;
            _basketItemWriteRepository = basketItemWriteRepository;
        }
        private async Task<Basket?> ContextUser()
        {
            var userName = _httpContextAccessor?.HttpContext?.User?.Identity?.Name;
            if(string.IsNullOrEmpty(userName))
            {
               AppUser user =
                    await _userManager.Users.Include(u => u.Baskets).FirstOrDefaultAsync(u => u.UserName == userName);
                var _basket = from basket in user.Baskets
                              join order in _orderReadRepository.Table on
                              basket.Id equals order.Id into BasketOrders
                              from ba in BasketOrders.DefaultIfEmpty()
                              select new
                              {
                                 Basket = basket,
                                 Order = ba
                              };
                Basket? targetBasket = null;

                if(_basket.Any(b=>b.Order is null ))
                {
                    targetBasket = _basket.FirstOrDefault(b => b.Order is null)?.Basket;
                }
                else
                {
                    targetBasket = new();
                    user.Baskets.Add(targetBasket);
                }
                await _basketWriteRepository.SaveAsync();
                return targetBasket;
            }
            throw new Exception("Beklenmeyen hata!");
        }
        public async Task AddItemToBasketAsync(CreateBasketVM basketItem)
        {
            Basket basket = await ContextUser();
            if(basket is not null)
            {
                BasketItem _basketItem = await _basketItemReadRepository.GetSingleAsync(b => b.BasketId == basket.Id && b.ProductId ==
                Guid.Parse(basketItem.ProductId));
                if (_basketItem is not null)
                    _basketItem.Quantity++;
                else
                    await _basketItemWriteRepository.AddAsync(new()
                    {
                        BasketId = basket.Id,
                        ProductId = Guid.Parse(basketItem.ProductId),
                        Quantity = basketItem.Quantity
                    });
                await _basketItemWriteRepository.SaveAsync();
                
            }
        }

        public async Task<List<BasketItem>> GetBasketItemsAsync()
        {
            Basket basket = await ContextUser();
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
