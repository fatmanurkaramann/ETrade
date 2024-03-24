using ETradeAPI.Application.Abstraction.Services;
using ETradeAPI.Application.Repositories;
using ETradeAPI.Application.Repositories.Basket;
using ETradeAPI.Application.Repositories.BasketItem;
using ETradeAPI.Application.ViewModels.Basket;
using ETradeAPI.Domain.Entities;
using ETradeAPI.Domain.Entities.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

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
        readonly IBasketReadRepository _basketReadRepository;
        public BasketService(IHttpContextAccessor httpContextAccessor, UserManager<AppUser> userManager, IOrderReadRepository orderReadRepository, IBasketWriteRepository basketWriteRepository, IBasketItemWriteRepository basketItemWriteRepository, IBasketReadRepository basketReadRepository, IBasketItemReadRepository basketItemReadRepository)
        {
            _httpContextAccessor = httpContextAccessor;
            _userManager = userManager;
            _orderReadRepository = orderReadRepository;
            _basketWriteRepository = basketWriteRepository;
            _basketItemWriteRepository = basketItemWriteRepository;
            _basketReadRepository = basketReadRepository;
            _basketItemReadRepository = basketItemReadRepository;
        }
        private async Task<Basket?> ContextUser()
        {
            var userName = _httpContextAccessor?.HttpContext?.User?.Identity?.Name;
            if (!string.IsNullOrEmpty(userName))
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

                if (_basket.Any(b => b.Order is null))
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
            Basket? basket = await ContextUser();
            if (basket != null)
            {
                BasketItem _basketItem = null;
                try
                {
                    _basketItem = await _basketItemReadRepository.GetSingleAsync(bi => bi.BasketId == basket.Id && bi.ProductId == Guid.Parse(basketItem.ProductId));
                }
                catch (Exception ex)
                {
                    // Hata durumunu ele al
                    Console.WriteLine("GetSingleAsync metodu çağrılırken bir hata oluştu: " + ex.Message);
                }
                if (_basketItem != null)
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
            Basket? result = await _basketReadRepository.Table
              .Include(b => b.BasketItems)
              .ThenInclude(bi => bi.Product)
                 .FirstOrDefaultAsync(b=>b.Id==basket.Id);
            return result.BasketItems.ToList();
        }

        public async Task RemoveBasketItemAsync(string id)
        {
            BasketItem? basketItem = await _basketItemReadRepository.GetByIdAsync(id);
            if(basketItem is not null)
            {
                _basketItemWriteRepository.Remove(basketItem);
                await _basketItemWriteRepository.SaveAsync();
            }
        }

        public async Task UpdateQuantityAsync(UpdateBasketVM basketItem)
        {
            BasketItem? _basketItem = await _basketItemReadRepository.GetByIdAsync(basketItem.BasketItemId);
            if(_basketItem is not null)
            {
                _basketItem.Quantity=basketItem.Quantity;
                await _basketItemWriteRepository.SaveAsync();
            }
        }
    }
}
