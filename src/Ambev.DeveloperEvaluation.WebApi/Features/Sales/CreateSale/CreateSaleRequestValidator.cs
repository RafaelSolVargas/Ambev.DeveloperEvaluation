namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.CreateSale;

using FluentValidation;
using System;

public class CreateSaleRequestValidator : AbstractValidator<CreateSaleRequest>
{
    public CreateSaleRequestValidator()
    {
        // Valida��o para o n�mero da venda
        RuleFor(x => x.Number)
            .NotEmpty()
            .WithMessage("O n�mero da venda � obrigat�rio.")
            .MaximumLength(50)
            .WithMessage("O n�mero da venda n�o pode ter mais de 50 caracteres.");

        // Valida��o para a data da venda
        RuleFor(x => x.DateSold)
            .NotEmpty()
            .WithMessage("A data da venda � obrigat�ria.")
            .LessThanOrEqualTo(DateTime.UtcNow)
            .WithMessage("A data da venda n�o pode ser no futuro.");

        // Valida��o para o ID do cliente
        RuleFor(x => x.CustomerId)
            .NotEmpty()
            .WithMessage("O ID do cliente � obrigat�rio.")
            .NotEqual(Guid.Empty)
            .WithMessage("O ID do cliente n�o pode ser vazio.");

        // Valida��o para o ID da filial
        RuleFor(x => x.BranchId)
            .NotEmpty()
            .WithMessage("O ID da filial � obrigat�rio.")
            .NotEqual(Guid.Empty)
            .WithMessage("O ID da filial n�o pode ser vazio.");

        // Valida��o para a lista de produtos
        RuleFor(x => x.Products)
            .NotEmpty()
            .WithMessage("A venda deve conter pelo menos um produto.")
            .Must(products => products.Count <= 100)
            .WithMessage("A venda n�o pode conter mais de 100 produtos.");

        // Valida��o para cada produto na lista
        RuleForEach(x => x.Products)
            .SetValidator(new SaleProductRequestValidator());
    }
}

public class SaleProductRequestValidator : AbstractValidator<SaleProductRequest>
{
    public SaleProductRequestValidator()
    {
        // Valida��o para o ID do produto
        RuleFor(x => x.ProductId)
            .NotEmpty()
            .WithMessage("O ID do produto � obrigat�rio.")
            .NotEqual(Guid.Empty)
            .WithMessage("O ID do produto n�o pode ser vazio.");

        // Valida��o para a quantidade
        RuleFor(x => x.Quantity)
            .NotEmpty()
            .WithMessage("A quantidade � obrigat�ria.")
            .GreaterThan(0)
            .WithMessage("A quantidade deve ser maior que zero.")
            .LessThanOrEqualTo(20)
            .WithMessage("A quantidade n�o pode ser maior que 20.");

        // Valida��o para o pre�o unit�rio
        RuleFor(x => x.UnitPrice)
            .NotEmpty()
            .WithMessage("O pre�o unit�rio � obrigat�rio.")
            .GreaterThan(0)
            .WithMessage("O pre�o unit�rio deve ser maior que zero.");
    }
}