﻿using ETradeAPI.Application.Abstraction;
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
        public async void GetProducts()
        {
           await _writeRepo.AddRangeAsync(new()
            {
                new() {Id=Guid.NewGuid(),Name="ürün",Price=100,Stock=10,CreatedDate=DateTime.UtcNow},
                new() {Id=Guid.NewGuid(),Name="ürün",Price=100,Stock=10,CreatedDate=DateTime.UtcNow}
            });
           await _writeRepo.SaveAsync();
        }
    }
}
