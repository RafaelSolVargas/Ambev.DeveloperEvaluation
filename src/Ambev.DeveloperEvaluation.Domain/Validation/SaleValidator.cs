using Ambev.DeveloperEvaluation.Domain.Entities.Sales;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.Domain.Validation;

public class SaleValidator : AbstractValidator<Sale>
{
    public SaleValidator()
    {
        RuleFor(sale => sale.ClientId)
            .NotEmpty().WithMessage("O ID do cliente não pode estar vazio.");

        RuleFor(sale => sale.BranchId)
            .NotEmpty().WithMessage("O ID da filial não pode estar vazio.");

        RuleFor(sale => sale.Number)
            .NotEmpty().WithMessage("O número da venda não pode estar vazio.")
            .MaximumLength(50).WithMessage("O número da venda não pode ter mais de 50 caracteres.");

        RuleFor(sale => sale.DateSold)
            .NotEmpty().WithMessage("A data da venda não pode estar vazia.")
            .LessThanOrEqualTo(DateTime.UtcNow).WithMessage("A data da venda não pode ser no futuro.");

        RuleFor(sale => sale.Products)
            .NotEmpty().WithMessage("A venda deve conter pelo menos um produto.");

        RuleForEach(sale => sale.Products)
            .SetValidator(new SaleProductValidator());
    }
}