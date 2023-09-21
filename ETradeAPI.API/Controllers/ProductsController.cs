using ETradeAPI.Application.Features.Commands.Product.CreateProduct;
using ETradeAPI.Application.Features.Commands.Product.RemoveProduct;
using ETradeAPI.Application.Features.Commands.Product.UpdateProduct;
using ETradeAPI.Application.Features.Queries.Product.GetAllProduct;
using ETradeAPI.Application.Features.Queries.Product.GetByIdProduct;
using ETradeAPI.Application.Services;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace ETradeAPI.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {

        readonly IMediator _mediatr;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IFileService _fileService;
        public ProductsController(IMediator mediatr, IWebHostEnvironment webHostEnvironment, IFileService fileService)
        {
            _mediatr = mediatr;
            _webHostEnvironment = webHostEnvironment;
            _fileService = fileService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllProducts([FromQuery] GetAllProductQueryRequest req)
        {
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
            if (ModelState.IsValid)
            {

            }
            CreateProductCommandResponse res = await _mediatr.Send(req);
            return StatusCode((int)HttpStatusCode.Created);
        }
        [HttpPost("[action]")]
        public async Task<IActionResult> Upload()
        {
            await _fileService.UploadAsync("resource/product-images", Request.Form.Files);
            return Ok();
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
