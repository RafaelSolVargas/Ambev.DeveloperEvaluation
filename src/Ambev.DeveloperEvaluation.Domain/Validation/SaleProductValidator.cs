using Ambev.DeveloperEvaluation.Domain.Entities.Sales;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.Domain.Validation
{
    public class SaleProductValidator : AbstractValidator<SaleProduct>
    {
        public SaleProductValidator()
        {
            RuleFor(sp => sp.SaleId)
                .NotEmpty().WithMessage("O ID da venda não pode estar vazio.");

            RuleFor(sp => sp.ProductId)
                .NotEmpty().WithMessage("O ID do produto não pode estar vazio.");

            RuleFor(sp => sp.Quantity)
                .GreaterThan(0).WithMessage("A quantidade deve ser maior que zero.")
                .LessThanOrEqualTo(20).WithMessage("A quantidade não pode exceder 20.");

            RuleFor(sp => sp.UnitPrice)
                .GreaterThan(0).WithMessage("O preço unitário deve ser maior que zero.");

            RuleFor(sp => sp.PercentageDiscount)
                .InclusiveBetween(0, 1).WithMessage("O desconto percentual deve estar entre 0 e 1.");

            RuleFor(sp => sp.FixedDiscount)
                .GreaterThanOrEqualTo(0).WithMessage("O desconto fixo não pode ser negativo.");

            RuleFor(sp => sp.DateSold)
                .LessThanOrEqualTo(DateTime.UtcNow).WithMessage("A data da venda não pode ser no futuro.");
        }
    }
}