using Ambev.DeveloperEvaluation.Domain.Entities;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.Domain.Validation
{
    public class BranchValidator : AbstractValidator<Branch>
    {
        public BranchValidator()
        {
            RuleFor(branch => branch.Name)
                .NotEmpty().WithMessage("O nome da filial não pode estar vazio.")
                .MinimumLength(3).WithMessage("O nome da filial deve ter pelo menos 3 caracteres.")
                .MaximumLength(100).WithMessage("O nome da filial não pode ter mais de 100 caracteres.");

            RuleFor(branch => branch.Address)
                .NotEmpty().WithMessage("O endereço da filial não pode estar vazio.")
                .MaximumLength(200).WithMessage("O endereço da filial não pode ter mais de 200 caracteres.");

            RuleFor(branch => branch.CreatedAt)
                .LessThanOrEqualTo(DateTime.UtcNow).WithMessage("A data de criação não pode ser no futuro.");

            RuleFor(branch => branch.UpdatedAt)
                .GreaterThanOrEqualTo(branch => branch.CreatedAt)
                .When(branch => branch.UpdatedAt.HasValue)
                .WithMessage("A data de atualização não pode ser anterior à data de criação.");
        }
    }
}