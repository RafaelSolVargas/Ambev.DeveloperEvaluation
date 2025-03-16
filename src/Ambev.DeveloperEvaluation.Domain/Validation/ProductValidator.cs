using Ambev.DeveloperEvaluation.Domain.Entities.Sales;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.Domain.Validation
{
    public class ProductValidator : AbstractValidator<Product>
    {
        public ProductValidator()
        {
            RuleFor(product => product.Name)
                .NotEmpty().WithMessage("O nome do produto não pode estar vazio.")
                .MinimumLength(3).WithMessage("O nome do produto deve ter pelo menos 3 caracteres.")
                .MaximumLength(100).WithMessage("O nome do produto não pode ter mais de 100 caracteres.");

            RuleFor(product => product.Description)
                .NotEmpty().WithMessage("A descrição do produto não pode estar vazia.")
                .MaximumLength(500).WithMessage("A descrição do produto não pode ter mais de 500 caracteres.");

            RuleFor(product => product.CreatedAt)
                .LessThanOrEqualTo(DateTime.UtcNow).WithMessage("A data de criação não pode ser no futuro.");

            RuleFor(product => product.UpdatedAt)
                .GreaterThanOrEqualTo(product => product.CreatedAt)
                .When(product => product.UpdatedAt.HasValue)
                .WithMessage("A data de atualização não pode ser anterior à data de criação.");
        }
    }
}