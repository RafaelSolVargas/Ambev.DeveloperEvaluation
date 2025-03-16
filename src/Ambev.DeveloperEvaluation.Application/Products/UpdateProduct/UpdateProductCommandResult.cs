using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Products.UpdateProduct;

public class UpdateProductCommand : IRequest<UpdateProductResult>
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
}

public class UpdateProductResult
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
}