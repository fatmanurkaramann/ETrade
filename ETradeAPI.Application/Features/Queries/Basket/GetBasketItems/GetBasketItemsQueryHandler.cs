using ETradeAPI.Application.Abstraction.Services;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETradeAPI.Application.Features.Queries.Basket.GetBasketItems
{
    public class GetBasketItemsQueryHandler : IRequestHandler<GetBasketItemsQueryRequest, List<GetBasketItemsQueryResponse>>
    {
        readonly IBasketService _basketService;

        public GetBasketItemsQueryHandler(IBasketService basketService)
        {
            _basketService = basketService;
        }

        public async Task<List<GetBasketItemsQueryResponse>> Handle(GetBasketItemsQueryRequest request, CancellationToken cancellationToken)
        {
          var basketItems =  await _basketService.GetBasketItemsAsync();
            return basketItems.Select(b => new GetBasketItemsQueryResponse
            {
                Id = b.Id.ToString(),
                Name = b.Product.Name,
                Price = b.Product.Price,
                Quantity = b.Quantity
            }).ToList();

        }
    }
}
