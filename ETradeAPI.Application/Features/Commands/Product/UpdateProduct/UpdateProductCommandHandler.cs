using ETradeAPI.Application.Features.Commands.Product.UpdateProduct;
using ETradeAPI.Application.Repositories;
using Google.Apis.Logging;
using MediatR;
using Microsoft.Extensions.Logging;

namespace ETradeAPI.Application.Features.Product.Commands.UpdateProduct
{
    public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommandRequest, UpdateProductCommandResponse>
    {
        private readonly IProductReadRepository _productReadRepository;
        private readonly IProductWriteRepository _productWriteRepository;
        readonly ILogger<UpdateProductCommandHandler> _logger;

        public UpdateProductCommandHandler(IProductReadRepository productReadRepository, IProductWriteRepository productWriteRepository, ILogger<UpdateProductCommandHandler> logger)
        {
            _productReadRepository = productReadRepository;
            _productWriteRepository = productWriteRepository;
            _logger = logger;
        }

        public async Task<UpdateProductCommandResponse> Handle(UpdateProductCommandRequest request, CancellationToken cancellationToken)
        {
           ETradeAPI.Domain.Entities.Product product = await _productReadRepository.GetByIdAsync(request.Id);

            product.Name = request.Name;
            product.Price = request.Price;
            product.Stock = request.Stock;
            await _productWriteRepository.SaveAsync();
            _logger.LogInformation("Product güncellendi");
            return new();
        }
    }
}
