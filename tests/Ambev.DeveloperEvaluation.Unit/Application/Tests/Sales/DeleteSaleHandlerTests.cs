using Xunit;
using NSubstitute;
using Ambev.DeveloperEvaluation.Application.Sales.DeleteSale;
using Ambev.DeveloperEvaluation.Domain.Entities.Sales;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using StackExchange.Redis;

namespace Ambev.DeveloperEvaluation.Unit.Application.Tests.Sales
{
    public class DeleteSaleHandlerTests
    {
        private readonly ISaleRepository _saleRepository;
        private readonly DeleteSaleHandler _handler;
        private readonly IConnectionMultiplexer _redis;
        public DeleteSaleHandlerTests()
        {
            _redis = Substitute.For<IConnectionMultiplexer>();
            _saleRepository = Substitute.For<ISaleRepository>();
            _handler = new DeleteSaleHandler(_saleRepository, _redis);
        }

        [Fact]
        public async Task Handle_ShouldReturnTrue_WhenSaleIsDeletedSuccessfully()
        {
            // Arrange
            var saleId = Guid.NewGuid();
            var sale = new Sale(
                clientId: Guid.NewGuid(),
                branchId: Guid.NewGuid(),
                number: "SALE001",
                dateSold: DateTime.UtcNow,
                products: []
            );

            _saleRepository.GetByIdAsync(saleId).Returns(sale);
            _saleRepository.DeleteAsync(sale.Id).Returns(true);

            var command = new DeleteSaleCommand { Id = saleId };

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.True(result);
            await _saleRepository.Received(1).GetByIdAsync(saleId);
            await _saleRepository.Received(1).DeleteAsync(sale.Id);
        }

        [Fact]
        public async Task Handle_ShouldReturnFalse_WhenSaleIsNotFound()
        {
            // Arrange
            var saleId = Guid.NewGuid();
            _saleRepository.GetByIdAsync(saleId).Returns((Sale)null!);

            var command = new DeleteSaleCommand { Id = saleId };

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.False(result);
            await _saleRepository.Received(1).GetByIdAsync(saleId);
            await _saleRepository.DidNotReceive().DeleteAsync(Arg.Any<Guid>());
        }
    }
}