using ETradeAPI.Application.Features.Queries.Basket.GetBasketItems;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ETradeAPI.API.Controllers
{
    public class BasketsController : ControllerBase
    {
        readonly IMediator _mediatr;

        public BasketsController(IMediator mediatr)
        {
            _mediatr = mediatr;
        }

        [HttpGet]
        public async Task<IActionResult> GetBasketItems(GetBasketItemsQueryRequest request)
        {
          List<GetBasketItemsQueryResponse> res = await _mediatr.Send(request);
            return Ok(res);
        }
    }
}
