using Ambev.DeveloperEvaluation.Domain.Repositories;
using FluentValidation;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Products.UpdateProduct
{
    public class UpdateProductHandler(IProductRepository _productRepository) : IRequestHandler<UpdateProductCommand, UpdateProductResult>
    {
        public async Task<UpdateProductResult> Handle(UpdateProductCommand command, CancellationToken cancellationToken)
        {
            var validator = new UpdateProductCommandValidator();
            var validationResult = await validator.ValidateAsync(command, cancellationToken);

            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }

            var product = await _productRepository.GetByIdAsync(command.Id, cancellationToken)
                ?? throw new InvalidOperationException("Product not found.");

            var existingProductWithSameName = await _productRepository.GetByNameAsync(command.Name, cancellationToken);
            if (existingProductWithSameName != null && existingProductWithSameName.Id != command.Id)
            {
                throw new InvalidOperationException("A product with this name already exists.");
            }

            product.Name = command.Name;
            product.Description = command.Description;
            product.UpdatedAt = DateTime.UtcNow;

            product = await _productRepository.UpdateAsync(product, cancellationToken);

            return new UpdateProductResult
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description
            };
        }
    }
}
