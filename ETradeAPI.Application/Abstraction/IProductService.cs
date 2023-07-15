using ETradeAPI.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETradeAPI.Application.Abstraction
{
    public interface IProductService
    {
        List<Product> GetAllProduct();
        List<Product> Get();
    }
}
