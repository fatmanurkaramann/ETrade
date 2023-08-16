using ETradeAPI.Application.Features.Commands.Product.CreateProduct;
using FluentValidation;

namespace ETradeAPI.Application.Validators.Products
{
    public class CreateProductValidator : AbstractValidator<CreateProductCommandRequest>
    {
        public CreateProductValidator()
        {
            RuleFor(x => x.Name).NotEmpty().NotNull()
                .WithMessage("Lütfen ürün adı giriniz.")
                .MaximumLength(150).MinimumLength(3).WithMessage("Ürün ismi 5 ile 150 karakter arasında olmalıdır.");

            RuleFor(x => x.Stock).NotNull().NotEmpty()
                .WithMessage("Stok giriniz")
                .Must(s => s >= 0).WithMessage("Stok bilgisi sıfırdan küçük olamaz");

            RuleFor(x => x.Price).NotNull().NotEmpty()
               .WithMessage("Fiyat giriniz")
               .Must(s => s >= 0).WithMessage("Fiyat bilgisi sıfırdan küçük olamaz");
        }
    }
}
