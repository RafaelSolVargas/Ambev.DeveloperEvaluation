using FluentAssertions;
using NSubstitute;
using Xunit;
using Ambev.DeveloperEvaluation.Application.Products.GetProductById;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Domain.Entities.Sales;

namespace Ambev.DeveloperEvaluation.Unit.Application.Tests.Products
{
    public class GetProductByIdHandlerTests
    {
        private readonly IProductRepository _productRepository;
        private readonly GetProductByIdHandler _handler;

        public GetProductByIdHandlerTests()
        {
            _productRepository = Substitute.For<IProductRepository>();
            _handler = new GetProductByIdHandler(_productRepository);
        }

        [Fact(DisplayName = "Given valid product ID. When getting product. Returns product details.")]
        public async Task Handle_ValidProductId_ReturnsProductDetails()
        {
            // Arrange
            var query = new GetProductByIdQuery
            {
                Id = Guid.NewGuid()
            };

            var product = new Product("Product Name", "Product Description")
            {
                Id = query.Id,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            _productRepository.GetByIdAsync(query.Id, Arg.Any<CancellationToken>()).Returns(product);

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            result.Should().NotBeNull();
            result!.Id.Should().Be(product.Id);
            result.Name.Should().Be(product.Name);
            result.Description.Should().Be(product.Description);
            result.CreatedAt.Should().Be(product.CreatedAt);
            result.UpdatedAt.Should().Be(product.UpdatedAt);

            await _productRepository.Received(1).GetByIdAsync(query.Id, Arg.Any<CancellationToken>());
        }

        [Fact(DisplayName = "Given non-existent product ID. When getting product. Throws KeyNotFoundException.")]
        public async Task Handle_NonExistentProductId_ThrowsException()
        {
            // Arrange
            var query = new GetProductByIdQuery
            {
                Id = Guid.NewGuid()
            };

            _productRepository.GetByIdAsync(query.Id, Arg.Any<CancellationToken>()).Returns((Product)null!);

            // Act & Assert
            Func<Task> act = async () => await _handler.Handle(query, CancellationToken.None);
            await act.Should().ThrowAsync<KeyNotFoundException>().WithMessage("There isn't any product with this ID");

            await _productRepository.Received(1).GetByIdAsync(query.Id, Arg.Any<CancellationToken>());
        }
    }
}