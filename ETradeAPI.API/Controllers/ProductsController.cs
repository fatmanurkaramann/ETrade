using ETradeAPI.Application.Abstraction.Storage;
using ETradeAPI.Application.Features.Commands.Product.CreateProduct;
using ETradeAPI.Application.Features.Commands.Product.RemoveProduct;
using ETradeAPI.Application.Features.Commands.Product.UpdateProduct;
using ETradeAPI.Application.Features.Queries.Product.GetAllProduct;
using ETradeAPI.Application.Features.Queries.Product.GetByIdProduct;
using ETradeAPI.Application.Repositories;
using ETradeAPI.Application.Repositories.File;
using ETradeAPI.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace ETradeAPI.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    [Authorize(AuthenticationSchemes ="Admin")]
    public class ProductsController : ControllerBase
    {

        readonly IMediator _mediatr;
        private readonly IWebHostEnvironment _webHostEnvironment;
        readonly IStorageService _storageService;
        readonly IFileReadRepository _fileRepo;
        public ProductsController(IMediator mediatr, IWebHostEnvironment webHostEnvironment, IStorageService storageService, IFileReadRepository fileRepo)
        {
            _mediatr = mediatr;
            _webHostEnvironment = webHostEnvironment;
            _storageService = storageService;
            _fileRepo = fileRepo;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllProducts([FromQuery] GetAllProductQueryRequest req)
        {
            GetAllProductQueryResponse res = await _mediatr.Send(req);
            return Ok(res);
        }

        [HttpGet("images")]
        public async Task<IActionResult> GetAllImages()
        {
            var res = _fileRepo.GetAll();
            return Ok(res);
        }


        [HttpGet("detail")]
        public async Task<IActionResult> GetProduct([FromQuery]string Id)
        {
            GetByIdProductQueryRequest req = new GetByIdProductQueryRequest { Id = Id };
            GetByIdProductQueryResponse res = await _mediatr.Send(req);
            return Ok(res);
        }


        [HttpPost]
        public async Task<IActionResult> AddProducts([FromForm]CreateProductCommandRequest req)
        {
            CreateProductCommandResponse res = await _mediatr.Send(req);
            return StatusCode((int)HttpStatusCode.Created);
        }
        [HttpPost("[action]")]
        public async Task<IActionResult> Upload()
        {
            await _storageService.UploadAsync("resources/files", Request.Form.Files);
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
