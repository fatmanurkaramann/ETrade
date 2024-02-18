using ETradeAPI.Application.Features.Commands.Product.CreateProduct;
using ETradeAPI.Application.Repositories;
using MediatR;
using Microsoft.AspNetCore.Http;
using System.Data;

namespace ETradeAPI.Application.Features.Product.Commands.CreateProduct
{
    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommandRequest, CreateProductCommandResponse>
    {
        private readonly IProductWriteRepository _productWriteRepository;

        public CreateProductCommandHandler(IProductWriteRepository productWriteRepository)
        {
            _productWriteRepository = productWriteRepository;
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
