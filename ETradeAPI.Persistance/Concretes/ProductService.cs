using ETradeAPI.Application.Abstraction;
using ETradeAPI.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETradeAPI.Persistance.Concretes
{
    public class ProductService : IProductService
    {
        List<Product> _products = new List<Product>();
        public List<Product> Get()
        {
            Product product = new Product() {
                Id = Guid.NewGuid(),
                Name = "Ürün1",
                Price = 100,
                Stock = 10
            };
            _products.Add(product);
            return _products;
        }
        public List<Product> GetAllProduct()
            => new()
            {
                new(){Id=Guid.NewGuid(),
                Name="Ürün1",
                Price=100,
                Stock=10
                },
                  new(){Id=Guid.NewGuid(),
                Name="Ürün2",
                Price=100,
                Stock=10
                }
            };
    }
}
