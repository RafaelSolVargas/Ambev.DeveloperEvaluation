using Ambev.DeveloperEvaluation.Domain.Repositories;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Products.GetProductById
{
    public class GetProductByIdHandler(IProductRepository _productRepository) : IRequestHandler<GetProductByIdQuery, GetProductByIdResult?>
    {
        public async Task<GetProductByIdResult?> Handle(GetProductByIdQuery query, CancellationToken cancellationToken)
        {
            var product = await _productRepository.GetByIdAsync(query.Id, cancellationToken) ?? throw new KeyNotFoundException("There isn't any product with this ID");
            
            return new GetProductByIdResult
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                CreatedAt = product.CreatedAt,
                UpdatedAt = product.UpdatedAt
            };
        }
    }
}
