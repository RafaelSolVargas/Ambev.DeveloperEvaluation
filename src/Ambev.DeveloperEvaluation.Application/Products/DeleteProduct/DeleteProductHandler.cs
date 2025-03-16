using Ambev.DeveloperEvaluation.Domain.Repositories;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Products.DeleteProduct
{
    public class DeleteProductHandler(IProductRepository _productRepository) : IRequestHandler<DeleteProductCommand, bool>
    {
        public async Task<bool> Handle(DeleteProductCommand command, CancellationToken cancellationToken)
        {
            var product = await _productRepository.GetByIdAsync(command.Id, cancellationToken);

            // TODO -> Check if there isn't any sale associated with this product

            if (product == null)
            {
                return false;
            }

            await _productRepository.DeleteAsync(product.Id, cancellationToken);

            return true;
        }
    }
}
