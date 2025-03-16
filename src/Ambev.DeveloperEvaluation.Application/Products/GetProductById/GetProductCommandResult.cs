using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Products.GetProductById;

public class GetProductByIdQuery : IRequest<GetProductByIdResult>
{
    public Guid Id { get; set; }
}

public class GetProductByIdResult
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}