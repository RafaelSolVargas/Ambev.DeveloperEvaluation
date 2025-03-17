using FluentAssertions;
using NSubstitute;
using Xunit;
using Ambev.DeveloperEvaluation.Application.Products.DeleteProduct;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Domain.Entities.Sales;
using Ambev.DeveloperEvaluation.ORM.Repositories;

namespace Ambev.DeveloperEvaluation.Unit.Application.Tests.Products
{
    public class DeleteProductHandlerTests
    {
        private readonly IProductRepository _productRepository;
        private readonly ISaleRepository _saleRepository;
        private readonly DeleteProductHandler _handler;

        public DeleteProductHandlerTests()
        {
            _productRepository = Substitute.For<IProductRepository>();
            _saleRepository = Substitute.For<ISaleRepository>();
            _handler = new DeleteProductHandler(_productRepository, _saleRepository);
        }

        [Fact(DisplayName = "Given valid product. When deleting product. Returns true.")]
        public async Task Handle_ValidProduct_ReturnsTrue()
        {
            // Arrange
            var command = new DeleteProductCommand
            {
                Id = Guid.NewGuid()
            };

            var product = new Product("Product Name", "Product Description")
            {
                Id = command.Id
            };

            _productRepository.GetByIdAsync(command.Id, Arg.Any<CancellationToken>()).Returns(product);
            _productRepository.DeleteAsync(command.Id, Arg.Any<CancellationToken>()).Returns(true);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            result.Should().BeTrue();

            await _productRepository.Received(1).GetByIdAsync(command.Id, Arg.Any<CancellationToken>());
            await _productRepository.Received(1).DeleteAsync(command.Id, Arg.Any<CancellationToken>());
        }

        [Fact(DisplayName = "Given non-existent product. When deleting product. Returns false.")]
        public async Task Handle_NonExistentProduct_ReturnsFalse()
        {
            // Arrange
            var command = new DeleteProductCommand
            {
                Id = Guid.NewGuid()
            };

            _productRepository.GetByIdAsync(command.Id, Arg.Any<CancellationToken>()).Returns((Product)null!);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            result.Should().BeFalse();

            await _productRepository.Received(1).GetByIdAsync(command.Id, Arg.Any<CancellationToken>());
            await _productRepository.DidNotReceive().DeleteAsync(Arg.Any<Guid>(), Arg.Any<CancellationToken>());
        }

        [Fact(DisplayName = "Delete product. When associated with sale. Throw invalid operation.")]
        public async Task Handle_DeletingProductAssociatedToSale_ThrowsException()
        {
            // Arrange
            var command = new DeleteProductCommand
            {
                Id = Guid.NewGuid()
            };

            var product = new Product("Product Name", "Product Description")
            {
                Id = command.Id
            };

            _saleRepository.ExistsWithProduct(command.Id, Arg.Any<CancellationToken>()).Returns(true);
            _productRepository.GetByIdAsync(command.Id, Arg.Any<CancellationToken>()).Returns(product);

            // When
            var act = () => _handler.Handle(command, CancellationToken.None);

            // Result
            await act.Should().ThrowAsync<InvalidOperationException>();
        }
    }
}