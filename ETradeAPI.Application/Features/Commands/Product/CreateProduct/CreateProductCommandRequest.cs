using ETradeAPI.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using File = ETradeAPI.Domain.Entities.File;

namespace ETradeAPI.Application.Features.Commands.Product.CreateProduct
{
    public class CreateProductCommandRequest:IRequest<CreateProductCommandResponse>
    {
        public string Name { get; set; }
        public int Stock { get; set; }
        public float Price { get; set; }
        public IFormFile File { get; set; }
    }
}
