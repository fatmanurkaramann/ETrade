using ETradeAPI.Application.Features.Commands.Basket.AddItemToBasket;
using ETradeAPI.Application.Features.Commands.Basket.RemoveBasketItem;
using ETradeAPI.Application.Features.Commands.Basket.UpdateQuantity;
using ETradeAPI.Application.Features.Queries.Basket.GetBasketItems;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ETradeAPI.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = "Admin")]
    public class BasketsController : ControllerBase
    {
        readonly IMediator _mediatr;

        public BasketsController(IMediator mediatr)
        {
            _mediatr = mediatr;
        }

        [HttpGet]
        public async Task<IActionResult> GetBasketItems([FromQuery] GetBasketItemsQueryRequest request)
        {
          List<GetBasketItemsQueryResponse> res = await _mediatr.Send(request);
            return Ok(res);
        }
        [HttpPost]
        public async Task<IActionResult> AddItemToBasket(AddItemToBasketCommandRequest request)
        {
            AddItemToBasketCommandResponse res = await _mediatr.Send(request);
            return Ok(res);
        }
        [HttpPut]
        public async Task<IActionResult> UpdateQuantity(UpdateQuantityCommandRequest request)
        {
            UpdateQuantityCommandResponse res = await _mediatr.Send(request);
            return Ok(res);
        }
        [HttpDelete("{Id}")]
        public async Task<IActionResult> RemoveBasketItem([FromRoute] RemoveBasketItemCommandRequest request)
        {
            var res = await _mediatr.Send(request);
            return Ok(res);
        }
    }
}
