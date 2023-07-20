using ETradeAPI.Application.Features.Commands.Product.UpdateProduct;
using ETradeAPI.Application.Repositories;
using MediatR;

namespace ETradeAPI.Application.Features.Product.Commands.UpdateProduct
{
    public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommandRequest, UpdateProductCommandResponse>
    {
        private readonly IProductReadRepository _productReadRepository;
        private readonly IProductWriteRepository _productWriteRepository;

        public UpdateProductCommandHandler(IProductReadRepository productReadRepository, IProductWriteRepository productWriteRepository)
        {
            _productReadRepository = productReadRepository;
            _productWriteRepository = productWriteRepository;
        }

        public async Task<UpdateProductCommandResponse> Handle(UpdateProductCommandRequest request, CancellationToken cancellationToken)
        {
           ETradeAPI.Domain.Entities.Product product = await _productReadRepository.GetByIdAsync(request.Id);

            product.Name = request.Name;
            product.Price = request.Price;
            product.Stock = request.Stock;
            await _productWriteRepository.SaveAsync();
            return new();
        }
    }
}
