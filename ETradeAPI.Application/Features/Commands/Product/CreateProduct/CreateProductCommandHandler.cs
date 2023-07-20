﻿using ETradeAPI.Application.Features.Commands.Product.CreateProduct;
using ETradeAPI.Application.Repositories;
using MediatR;

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
            await  _productWriteRepository.AddAsync(new()
            {
                Name = request.Name,
                Price = request.Price,
                Stock = request.Stock,
            });
            await _productWriteRepository.SaveAsync();
            return new();
        }
    }
}
