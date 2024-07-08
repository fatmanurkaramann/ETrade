using ETradeAPI.Application.Abstraction.Hubs;
using ETradeAPI.Application.Features.Commands.Product.CreateProduct;
using ETradeAPI.Application.Repositories;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace ETradeAPI.Application.Features.Product.Commands.CreateProduct
{
    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommandRequest, CreateProductCommandResponse>
    {
        private readonly IProductWriteRepository _productWriteRepository;
        readonly IProductHubService _productHub;    

        public CreateProductCommandHandler(IProductWriteRepository productWriteRepository, IProductHubService productHub)
        {
            _productWriteRepository = productWriteRepository;
            _productHub = productHub;
        }

        public async Task<CreateProductCommandResponse> Handle(CreateProductCommandRequest request, CancellationToken cancellationToken)
        {
            var base64File = await ConvertToBase64Async(request.File);
            await  _productWriteRepository.AddAsync(new()
            {
                Name = request.Name,
                Price = request.Price,
                Stock = request.Stock,
                Path=base64File.Path,
                FileName= base64File.FileName
            });
            await _productWriteRepository.SaveAsync();
            await _productHub.ProductAddedMessageAsync($"{request.Name} isminde ürün eklenmiştir.");
            return new CreateProductCommandResponse();
        }
        public static async Task<ETradeAPI.Domain.Entities.File> ConvertToBase64Async(IFormFile file)
        {
           
            using (var memoryStream = new MemoryStream())
            {
                await file.CopyToAsync(memoryStream);
                var uploadedFile = new ETradeAPI.Domain.Entities.File
                {
                    FileName = file.Name,
                    Path = Convert.ToBase64String(memoryStream.ToArray())

                };
                return uploadedFile;
            }
        }
    }
}
