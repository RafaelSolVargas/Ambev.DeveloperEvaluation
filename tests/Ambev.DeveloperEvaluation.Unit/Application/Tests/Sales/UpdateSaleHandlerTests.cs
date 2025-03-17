using Xunit;
using NSubstitute;
using Ambev.DeveloperEvaluation.Application.Sales.UpdateSale;
using Ambev.DeveloperEvaluation.Domain.Entities.Sales;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Rebus.Bus;
using Ambev.DeveloperEvaluation.Domain.Events;

namespace Ambev.DeveloperEvaluation.Unit.Application.Tests.Sales
{
    public class UpdateSaleHandlerTests
    {
        private readonly ISaleRepository _saleRepository;
        private readonly UpdateSaleHandler _handler;
        private readonly IBus _bus;

        public UpdateSaleHandlerTests()
        {
            _saleRepository = Substitute.For<ISaleRepository>();
            _bus = Substitute.For<IBus>();
            _handler = new UpdateSaleHandler(_saleRepository, _bus);
        }

        [Fact]
        public async Task Handle_ShouldPublishSaleModifiedEvent_WhenSaleIsUpdatedSuccessfully()
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
            _saleRepository.UpdateAsync(sale).Returns(sale);

            var command = new UpdateSaleCommand
            {
                Id = saleId,
                BranchId = Guid.NewGuid(),
                ClientId = Guid.NewGuid(),
                Number = "SALE002",
                DateSold = DateTime.UtcNow.AddDays(-1),
                Products = []
            };

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.True(result);

            // Verifica se o evento SaleModifiedEvent foi publicado
            await _bus.Received(1).Publish(Arg.Is<SaleModifiedEvent>(e =>
                e.SaleId == sale.Id
            ));
        }

        [Fact]
        public async Task Handle_ShouldReturnTrue_WhenSaleIsUpdatedSuccessfully()
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
            _saleRepository.UpdateAsync(sale).Returns(sale);

            var command = new UpdateSaleCommand
            {
                Id = saleId,
                BranchId = Guid.NewGuid(),
                ClientId = Guid.NewGuid(),
                Number = "SALE002",
                DateSold = DateTime.UtcNow.AddDays(-1),
                Products = []
            };

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.True(result);
            await _saleRepository.Received(1).GetByIdAsync(saleId);
            await _saleRepository.Received(1).UpdateAsync(sale);
        }

        [Fact]
        public async Task Handle_ShouldReturnFalse_WhenSaleIsNotFound()
        {
            // Arrange
            var saleId = Guid.NewGuid();
            _saleRepository.GetByIdAsync(saleId).Returns((Sale)null!);

            var command = new UpdateSaleCommand
            {
                Id = saleId,
                BranchId = Guid.NewGuid(),
                ClientId = Guid.NewGuid(),
                Number = "SALE002",
                DateSold = DateTime.UtcNow.AddDays(-1),
                Products = new List<UpdateSaleProductCommand>()
            };

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.False(result);
            await _saleRepository.Received(1).GetByIdAsync(saleId);
            await _saleRepository.DidNotReceive().UpdateAsync(Arg.Any<Sale>());
        }
    }
}