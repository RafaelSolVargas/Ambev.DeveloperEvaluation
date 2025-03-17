using Ambev.DeveloperEvaluation.Application.Sales.CreateSale;
using Ambev.DeveloperEvaluation.Application.Sales.GetSale;
using Ambev.DeveloperEvaluation.Domain.Entities.Sales;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Domain.Entities;
using FluentAssertions;
using NSubstitute;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application.Tests.Sales
{
    public class GetSaleByIdHandlerTests
    {
        private readonly ISaleRepository _saleRepository;
        private readonly IUserRepository _userRepository;
        private readonly IBranchRepository _branchRepository;
        private readonly IProductRepository _productRepository;
        private readonly GetSaleByIdHandler _handler;

        public GetSaleByIdHandlerTests()
        {
            _saleRepository = Substitute.For<ISaleRepository>();
            _userRepository = Substitute.For<IUserRepository>();
            _branchRepository = Substitute.For<IBranchRepository>();
            _productRepository = Substitute.For<IProductRepository>();
            _handler = new GetSaleByIdHandler(_saleRepository, _userRepository, _productRepository, _branchRepository);
        }

        [Fact(DisplayName = "Given valid sale ID. When getting sale. Returns sale details.")]
        public async Task Handle_ValidSaleId_ReturnsSaleDetails()
        {
            // Arrange
            var saleId = Guid.NewGuid();
            var sale = new Sale(
                Guid.NewGuid(), // ClientId
                Guid.NewGuid(), // BranchId
                "123",         // Number
                DateTime.UtcNow, // DateSold
                [
                    new ()
                    {
                        ProductId = Guid.NewGuid(),
                        Quantity = 10,
                        UnitPrice = 100,
                        FixedDiscount = 0,
                        PercentageDiscount = 0.20m
                    }
                ]
            )
            {
                Id = saleId,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            var customer = new User { Id = sale.ClientId };
            var branch = new Branch { Id = sale.BranchId };

            _saleRepository.GetByIdAsync(saleId, Arg.Any<CancellationToken>()).Returns(sale);
            _userRepository.GetByIdAsync(sale.ClientId, Arg.Any<CancellationToken>()).Returns(customer);
            _branchRepository.GetByIdAsync(sale.BranchId, Arg.Any<CancellationToken>()).Returns(branch);

            var query = new GetSaleByIdQuery { Id = saleId };

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            result.Should().NotBeNull();
            result!.Id.Should().Be(saleId);
            result.Number.Should().Be(sale.Number);
            result.DateSold.Should().Be(sale.DateSold);
            result.Customer.Should().Be(customer);
            result.Branch.Should().Be(branch);
            result.Products.Should().HaveCount(1);
            result.CreatedAt.Should().Be(sale.CreatedAt);
            result.UpdatedAt.Should().Be(sale.UpdatedAt);

            await _saleRepository.Received(1).GetByIdAsync(saleId, Arg.Any<CancellationToken>());
            await _userRepository.Received(1).GetByIdAsync(sale.ClientId, Arg.Any<CancellationToken>());
            await _branchRepository.Received(1).GetByIdAsync(sale.BranchId, Arg.Any<CancellationToken>());
        }

        [Fact(DisplayName = "Given non-existent sale ID. When getting sale. Returns null.")]
        public async Task Handle_NonExistentSaleId_ReturnsNull()
        {
            // Arrange
            var saleId = Guid.NewGuid();
            _saleRepository.GetByIdAsync(saleId, Arg.Any<CancellationToken>()).Returns((Sale)null!);

            var query = new GetSaleByIdQuery { Id = saleId };

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            result.Should().BeNull();

            await _saleRepository.Received(1).GetByIdAsync(saleId, Arg.Any<CancellationToken>());
            await _userRepository.DidNotReceive().GetByIdAsync(Arg.Any<Guid>(), Arg.Any<CancellationToken>());
            await _branchRepository.DidNotReceive().GetByIdAsync(Arg.Any<Guid>(), Arg.Any<CancellationToken>());
        }

        [Fact(DisplayName = "Given valid sale ID. When getting sale. Returns sale details with products.")]
        public async Task Handle_ValidSaleId_ReturnsSaleDetailsWithProducts()
        {
            // Arrange
            var saleId = Guid.NewGuid();
            var productId = Guid.NewGuid();
            var sale = new Sale(
                Guid.NewGuid(), // ClientId
                Guid.NewGuid(), // BranchId
                "123",         // Number
                DateTime.UtcNow, // DateSold
                [
                    new ()
                    {
                        ProductId = productId,
                        Quantity = 10,
                        UnitPrice = 100,
                        FixedDiscount = 0,
                        PercentageDiscount = 0.20m
                    }
                ]
                )
            {
                Id = saleId,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            var customer = new User { Id = sale.ClientId };
            var branch = new Branch { Id = sale.BranchId };
            var product = new Product("Product Name", "Product Description")
            {
                Id = productId
            };

            _saleRepository.GetByIdAsync(saleId, Arg.Any<CancellationToken>()).Returns(sale);
            _userRepository.GetByIdAsync(sale.ClientId, Arg.Any<CancellationToken>()).Returns(customer);
            _branchRepository.GetByIdAsync(sale.BranchId, Arg.Any<CancellationToken>()).Returns(branch);
            _productRepository.GetByIdAsync(productId, Arg.Any<CancellationToken>()).Returns(product);

            var query = new GetSaleByIdQuery { Id = saleId };

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            result.Should().NotBeNull();
            result.Id.Should().Be(saleId);
            result.Number.Should().Be(sale.Number);
            result.DateSold.Should().Be(sale.DateSold);
            result.Customer.Should().Be(customer);
            result.Branch.Should().Be(branch);
            result.Products.Should().HaveCount(1);

            var saleProductResponse = result.Products[0];
            saleProductResponse.ProductId.Should().Be(productId);
            saleProductResponse.Product.Should().NotBeNull();
            saleProductResponse.Product.Id.Should().Be(productId);
            saleProductResponse.Product.Name.Should().Be(product.Name);
            saleProductResponse.Product.Description.Should().Be(product.Description);

            await _saleRepository.Received(1).GetByIdAsync(saleId, Arg.Any<CancellationToken>());
            await _userRepository.Received(1).GetByIdAsync(sale.ClientId, Arg.Any<CancellationToken>());
            await _branchRepository.Received(1).GetByIdAsync(sale.BranchId, Arg.Any<CancellationToken>());
            await _productRepository.Received(1).GetByIdAsync(productId, Arg.Any<CancellationToken>());
        }
    }
}