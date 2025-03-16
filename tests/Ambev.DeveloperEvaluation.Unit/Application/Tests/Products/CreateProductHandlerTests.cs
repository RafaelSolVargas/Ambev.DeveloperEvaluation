using FluentAssertions;
using NSubstitute;
using Xunit;
using Ambev.DeveloperEvaluation.Application.Products.CreateProduct;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using FluentValidation;
using Ambev.DeveloperEvaluation.Domain.Entities.Sales;

namespace Ambev.DeveloperEvaluation.Unit.Application.Tests.Products
{
    public class CreateProductHandlerTests
    {
        private readonly IProductRepository _productRepository;
        private readonly CreateProductHandler _handler;

        public CreateProductHandlerTests()
        {
            _productRepository = Substitute.For<IProductRepository>();
            _handler = new CreateProductHandler(_productRepository);
        }

        [Fact(DisplayName = "Given valid product. When creating product. Returns success response.")]
        public async Task Handle_ValidProduct_ReturnsSuccessResponse()
        {
            // Arrange
            var command = new CreateProductCommand
            {
                Name = "Product Name",
                Description = "Product Description"
            };

            var product = new Product(command.Name, command.Description)
            {
                Id = Guid.NewGuid()
            };

            _productRepository.GetByNameAsync(command.Name, Arg.Any<CancellationToken>()).Returns((Product)null!);
            _productRepository.CreateAsync(Arg.Any<Product>(), Arg.Any<CancellationToken>()).Returns(product);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            result.Should().NotBeNull();
            result.Id.Should().Be(product.Id);
            result.Name.Should().Be(product.Name);
            result.Description.Should().Be(product.Description);

            await _productRepository.Received(1).GetByNameAsync(command.Name, Arg.Any<CancellationToken>());
            await _productRepository.Received(1).CreateAsync(Arg.Any<Product>(), Arg.Any<CancellationToken>());
        }

        [Fact(DisplayName = "Given existing product name. When creating product. Then throws InvalidOperationException.")]
        public async Task Handle_ExistingProductName_ThrowsException()
        {
            // Arrange
            var command = new CreateProductCommand
            {
                Name = "Existing Product",
                Description = "Product Description"
            };

            var existingProduct = new Product(command.Name, command.Description);

            _productRepository.GetByNameAsync(command.Name, Arg.Any<CancellationToken>()).Returns(existingProduct);

            // Act & Assert
            Func<Task> act = async () => await _handler.Handle(command, CancellationToken.None);
            await act.Should().ThrowAsync<InvalidOperationException>().WithMessage("Already exists a product with this name");

            await _productRepository.Received(1).GetByNameAsync(command.Name, Arg.Any<CancellationToken>());
            await _productRepository.DidNotReceive().CreateAsync(Arg.Any<Product>(), Arg.Any<CancellationToken>());
        }

        [Fact(DisplayName = "Given invalid command. When creating product. Then throws ValidationException.")]
        public async Task Handle_InvalidCommand_ThrowsValidationException()
        {
            // Arrange
            var command = new CreateProductCommand
            {
                Name = "", // Invalid name
                Description = "" // Invalid description
            };

            // Act & Assert
            Func<Task> act = async () => await _handler.Handle(command, CancellationToken.None);
            await act.Should().ThrowAsync<ValidationException>();

            await _productRepository.DidNotReceive().GetByNameAsync(Arg.Any<string>(), Arg.Any<CancellationToken>());
            await _productRepository.DidNotReceive().CreateAsync(Arg.Any<Product>(), Arg.Any<CancellationToken>());
        }
    }
}