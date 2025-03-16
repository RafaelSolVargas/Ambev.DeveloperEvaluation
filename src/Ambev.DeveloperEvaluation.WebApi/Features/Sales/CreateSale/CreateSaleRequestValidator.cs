namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.CreateSale;

using FluentValidation;
using System;

public class CreateSaleRequestValidator : AbstractValidator<CreateSaleRequest>
{
    public CreateSaleRequestValidator()
    {
        // Validação para o número da venda
        RuleFor(x => x.Number)
            .NotEmpty()
            .WithMessage("O número da venda é obrigatório.")
            .MaximumLength(50)
            .WithMessage("O número da venda não pode ter mais de 50 caracteres.");

        // Validação para a data da venda
        RuleFor(x => x.DateSold)
            .NotEmpty()
            .WithMessage("A data da venda é obrigatória.")
            .LessThanOrEqualTo(DateTime.UtcNow)
            .WithMessage("A data da venda não pode ser no futuro.");

        // Validação para o ID do cliente
        RuleFor(x => x.CustomerId)
            .NotEmpty()
            .WithMessage("O ID do cliente é obrigatório.")
            .NotEqual(Guid.Empty)
            .WithMessage("O ID do cliente não pode ser vazio.");

        // Validação para o ID da filial
        RuleFor(x => x.BranchId)
            .NotEmpty()
            .WithMessage("O ID da filial é obrigatório.")
            .NotEqual(Guid.Empty)
            .WithMessage("O ID da filial não pode ser vazio.");

        // Validação para a lista de produtos
        RuleFor(x => x.Products)
            .NotEmpty()
            .WithMessage("A venda deve conter pelo menos um produto.")
            .Must(products => products.Count <= 100)
            .WithMessage("A venda não pode conter mais de 100 produtos.");

        // Validação para cada produto na lista
        RuleForEach(x => x.Products)
            .SetValidator(new SaleProductRequestValidator());
    }
}

public class SaleProductRequestValidator : AbstractValidator<SaleProductRequest>
{
    public SaleProductRequestValidator()
    {
        // Validação para o ID do produto
        RuleFor(x => x.ProductId)
            .NotEmpty()
            .WithMessage("O ID do produto é obrigatório.")
            .NotEqual(Guid.Empty)
            .WithMessage("O ID do produto não pode ser vazio.");

        // Validação para a quantidade
        RuleFor(x => x.Quantity)
            .NotEmpty()
            .WithMessage("A quantidade é obrigatória.")
            .GreaterThan(0)
            .WithMessage("A quantidade deve ser maior que zero.")
            .LessThanOrEqualTo(20)
            .WithMessage("A quantidade não pode ser maior que 20.");

        // Validação para o preço unitário
        RuleFor(x => x.UnitPrice)
            .NotEmpty()
            .WithMessage("O preço unitário é obrigatório.")
            .GreaterThan(0)
            .WithMessage("O preço unitário deve ser maior que zero.");
    }
}