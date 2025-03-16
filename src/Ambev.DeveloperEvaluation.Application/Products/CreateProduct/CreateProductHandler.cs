using Ambev.DeveloperEvaluation.Domain.Entities.Sales;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using FluentValidation;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Products.CreateProduct
{
    public class CreateProductHandler(IProductRepository _productRepository) : IRequestHandler<CreateProductCommand, CreateProductResult>
    {
        public async Task<CreateProductResult> Handle(CreateProductCommand command, CancellationToken cancellationToken)
        {
            var validator = new CreateProductCommandValidator();
            var validationResult = await validator.ValidateAsync(command, cancellationToken);

            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }

            var existingProduct = await _productRepository.GetByNameAsync(command.Name, cancellationToken);

            if (existingProduct != null)
            {
                throw new InvalidOperationException("Already exists a product with this name");
            }

            var product = new Product(command.Name, command.Description);
            
            product = await _productRepository.CreateAsync(product, cancellationToken);

            return new CreateProductResult
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description
            };
        }
    }
}
