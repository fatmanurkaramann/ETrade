using ETradeAPI.Application.Features.Commands.Product.CreateProduct;
using ETradeAPI.Application.Features.Commands.Product.RemoveProduct;
using ETradeAPI.Application.Features.Commands.Product.UpdateProduct;
using ETradeAPI.Application.Features.Queries.Product.GetAllProduct;
using ETradeAPI.Application.Features.Queries.Product.GetByIdProduct;
using ETradeAPI.Application.Repositories;
using ETradeAPI.Application.ViewModels.Products;
using ETradeAPI.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace ETradeAPI.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {

        readonly IMediator _mediatr;

        public ProductsController(IMediator mediatr)
        {
            _mediatr = mediatr;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllProducts()
        {
            GetAllProductQueryRequest req = new GetAllProductQueryRequest();
            GetAllProductQueryResponse res = await _mediatr.Send(req);
            return Ok(res);
        }


        [HttpGet("{Id}")]
        public async Task<IActionResult> GetProduct(string Id)
        {
            GetByIdProductQueryRequest req = new GetByIdProductQueryRequest { Id = Id };
            GetByIdProductQueryResponse res = await _mediatr.Send(req);
            return Ok(res);
        }


        [HttpPost]
        public async Task<IActionResult> AddProducts(CreateProductCommandRequest req)
        {
            CreateProductCommandResponse res = await _mediatr.Send(req);
            return StatusCode((int)HttpStatusCode.Created);
        }


        [HttpPut]
        public async Task<IActionResult> UpdateProduct(UpdateProductCommandRequest request)
        {
            await _mediatr.Send(request);
            return Ok();

        }


        [HttpDelete("{Id}")]
        public async Task<IActionResult> DeleteProduct([FromRoute] RemoveProductCommandRequest req)
        {
            RemoveProductCommandResponse res = await _mediatr.Send(req);

            return Ok();

        }
    }
}
