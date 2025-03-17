using Ambev.DeveloperEvaluation.Domain.Repositories;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Products.DeleteProduct
{
    public class DeleteProductHandler(IProductRepository _productRepository,
        ISaleRepository saleRepository) : IRequestHandler<DeleteProductCommand, bool>
    {
        public async Task<bool> Handle(DeleteProductCommand command, CancellationToken cancellationToken)
        {
            var product = await _productRepository.GetByIdAsync(command.Id, cancellationToken);
            if (product == null)
            {
                return false;
            }

            var existingSaleWithProduct = await saleRepository.ExistsWithProduct(command.Id, cancellationToken);
            if (existingSaleWithProduct)
            {
                throw new InvalidOperationException("There is sales associated with this product, you cannot delete it");
            }

            await _productRepository.DeleteAsync(product.Id, cancellationToken);

            return true;
        }
    }
}
