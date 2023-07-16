using ETradeAPI.Application.Abstraction;
using ETradeAPI.Application.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ETradeAPI.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductWriteRepository _writeRepo;
        private readonly IProductReadRepository _readRepo;

        public ProductsController(IProductReadRepository readRepo, IProductWriteRepository writeRepo)
        {
            _readRepo = readRepo;
            _writeRepo = writeRepo;
        }

        [HttpGet]
        public async Task GetProducts()
        {
            _writeRepo.AddAsync(new() { Name="ürün",Price=10.5F,Stock=10});
        }
    }
}
