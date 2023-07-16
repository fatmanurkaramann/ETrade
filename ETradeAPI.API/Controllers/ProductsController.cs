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
            //await _writeRepo.AddRangeAsync(new()
            // {
            //     new() {Id=Guid.NewGuid(),Name="ürün1",Price=100,Stock=10,CreatedDate=DateTime.UtcNow},
            //     new() {Id=Guid.NewGuid(),Name="ürün1",Price=100,Stock=10,CreatedDate=DateTime.UtcNow}
            // });
            //  await _writeRepo.SaveAsync();

           var p = await _readRepo.GetByIdAsync("c87f361a-e16d-438b-8d86-8d0d730acb09");
            p.Name = "test";
            await _writeRepo.SaveAsync();
        }
    }
}
