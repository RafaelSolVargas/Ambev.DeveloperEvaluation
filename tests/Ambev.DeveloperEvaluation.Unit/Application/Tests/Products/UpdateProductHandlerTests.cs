using FluentAssertions;
using NSubstitute;
using Xunit;
using Ambev.DeveloperEvaluation.Application.Products.UpdateProduct;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Domain.Entities.Sales;
using StackExchange.Redis;

namespace Ambev.DeveloperEvaluation.Unit.Application.Tests.Products
{
    public class UpdateProductHandlerTests
    {
        private readonly IProductRepository _productRepository;
        private readonly ISaleRepository _saleRepository;
        private readonly UpdateProductHandler _handler;
        private readonly IConnectionMultiplexer _redis;

        public UpdateProductHandlerTests()
        {
            _productRepository = Substitute.For<IProductRepository>();
            _saleRepository = Substitute.For<ISaleRepository>();
            _redis = Substitute.For<IConnectionMultiplexer>();
            _handler = new UpdateProductHandler(_productRepository, _saleRepository, _redis);
        }

        [Fact(DisplayName = "Given valid product. When updating product. Returns success response.")]
        public async Task Handle_ValidProduct_ReturnsSuccessResponse()
        {
            // Arrange
            var command = new UpdateProductCommand
            {
                Id = Guid.NewGuid(),
                Name = "Updated Product",
                Description = "Updated Description"
            };

            var existingProduct = new Product("Old Name", "Old Description")
            {
                Id = command.Id
            };

            _productRepository.GetByIdAsync(command.Id, Arg.Any<CancellationToken>()).Returns(existingProduct);
            _saleRepository.GetAllWithProduct(command.Id, Arg.Any<CancellationToken>()).Returns([]);
            _productRepository.GetByNameAsync(command.Name, Arg.Any<CancellationToken>()).Returns((Product)null);
            _productRepository.UpdateAsync(Arg.Any<Product>(), Arg.Any<CancellationToken>()).Returns(existingProduct);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            result.Should().NotBeNull();
            result.Id.Should().Be(command.Id);
            result.Name.Should().Be(command.Name);
            result.Description.Should().Be(command.Description);

            await _productRepository.Received(1).GetByIdAsync(command.Id, Arg.Any<CancellationToken>());
            await _productRepository.Received(1).GetByNameAsync(command.Name, Arg.Any<CancellationToken>());
            await _productRepository.Received(1).UpdateAsync(Arg.Any<Product>(), Arg.Any<CancellationToken>());
        }

        [Fact(DisplayName = "Given non-existent product. When updating product. Then throws InvalidOperationException.")]
        public async Task Handle_NonExistentProduct_ThrowsException()
        {
            // Arrange
            var command = new UpdateProductCommand
            {
                Id = Guid.NewGuid(),
                Name = "Updated Product",
                Description = "Updated Description"
            };

            _productRepository.GetByIdAsync(command.Id, Arg.Any<CancellationToken>()).Returns((Product)null);

            // Act & Assert
            Func<Task> act = async () => await _handler.Handle(command, CancellationToken.None);
            await act.Should().ThrowAsync<InvalidOperationException>().WithMessage("Product not found.");

            await _productRepository.Received(1).GetByIdAsync(command.Id, Arg.Any<CancellationToken>());
            await _productRepository.DidNotReceive().GetByNameAsync(Arg.Any<string>(), Arg.Any<CancellationToken>());
            await _productRepository.DidNotReceive().UpdateAsync(Arg.Any<Product>(), Arg.Any<CancellationToken>());
        }

        [Fact(DisplayName = "Given duplicate product name. When updating product. Then throws InvalidOperationException.")]
        public async Task Handle_DuplicateProductName_ThrowsException()
        {
            // Arrange
            var command = new UpdateProductCommand
            {
                Id = Guid.NewGuid(),
                Name = "Duplicate Product",
                Description = "Updated Description"
            };

            var existingProduct = new Product("Old Name", "Old Description")
            {
                Id = command.Id
            };

            var duplicateProduct = new Product(command.Name, "Some Description")
            {
                Id = Guid.NewGuid() // ID diferente do produto que está sendo atualizado
            };

            _productRepository.GetByIdAsync(command.Id, Arg.Any<CancellationToken>()).Returns(existingProduct);
            _productRepository.GetByNameAsync(command.Name, Arg.Any<CancellationToken>()).Returns(duplicateProduct);

            // Act & Assert
            Func<Task> act = async () => await _handler.Handle(command, CancellationToken.None);
            await act.Should().ThrowAsync<InvalidOperationException>().WithMessage("A product with this name already exists.");

            await _productRepository.Received(1).GetByIdAsync(command.Id, Arg.Any<CancellationToken>());
            await _productRepository.Received(1).GetByNameAsync(command.Name, Arg.Any<CancellationToken>());
            await _productRepository.DidNotReceive().UpdateAsync(Arg.Any<Product>(), Arg.Any<CancellationToken>());
        }
    }
}