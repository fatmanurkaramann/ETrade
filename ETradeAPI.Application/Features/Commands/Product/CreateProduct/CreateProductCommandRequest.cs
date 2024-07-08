using MediatR;
using Microsoft.AspNetCore.Http;

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
