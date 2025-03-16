using Ambev.DeveloperEvaluation.Application.Products.CreateProduct;
using Ambev.DeveloperEvaluation.Application.Sales.CreateSale;
using Bogus;

namespace Ambev.DeveloperEvaluation.Unit.Application.TestData;

public static class CreateSaleHandlerTestData
{
    private static readonly Faker<CreateSaleCommand> createBranchHandlerFaker = new Faker<CreateSaleCommand>()
        .RuleFor(u => u.Number, f => f.Random.AlphaNumeric(10)) // Gera um número alfanumérico de 10 caracteres
        .RuleFor(u => u.CustomerId, f => Guid.NewGuid()) // Gera um GUID para o CustomerId
        .RuleFor(u => u.BranchId, f => Guid.NewGuid()) // Gera um GUID para o BranchId
        .RuleFor(u => u.DateSold, f => f.Date.Past()) // Gera uma data no passado
        .RuleFor(u => u.Products, f => new Faker<CreateSaleProductCommand>()
            .RuleFor(p => p.ProductId, f => Guid.NewGuid()) // Gera um GUID para o ProductId
            .RuleFor(p => p.Quantity, f => f.Random.Int(1, 20)) // Gera uma quantidade entre 1 e 20
            .RuleFor(p => p.UnitPrice, f => f.Random.Decimal(10, 1000)) // Gera um preço unitário entre 10 e 1000
            .Generate(f.Random.Int(1, 20))); // Gera entre 1 e 5 produtos

    public static CreateSaleCommand GenerateValidCommand()
    {
        return createBranchHandlerFaker.Generate();
    }
}
