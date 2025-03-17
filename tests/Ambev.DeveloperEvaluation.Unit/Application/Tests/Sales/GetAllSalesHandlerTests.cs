using Microsoft.EntityFrameworkCore;
using Xunit;
using FluentAssertions;
using Ambev.DeveloperEvaluation.Application.Sales.GetAllSales;
using Ambev.DeveloperEvaluation.Domain.Entities.Sales;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.ORM.Repositories;
using Ambev.DeveloperEvaluation.ORM;
using NSubstitute;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.Unit.Application.Tests.Sales
{
    public class GetAllSalesHandlerTests : IDisposable
    {
        private readonly DefaultContext _context;
        private readonly GetAllSalesHandler _handler;

        public GetAllSalesHandlerTests()
        {
            // Configura o contexto em memória
            var options = new DbContextOptionsBuilder<DefaultContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()) // Nome único para cada teste
                .Options;

            _context = new DefaultContext(options);

            // Configura o handler com os repositórios reais (usando o contexto em memória)
            var saleRepository = new SaleRepository(_context);
            var userRepository = Substitute.For<IUserRepository>();
            var branchRepository = Substitute.For<IBranchRepository>();
            var productRepository = Substitute.For<IProductRepository>();
            var mapper = Substitute.For<IMapper>();

            _handler = new GetAllSalesHandler(saleRepository, userRepository, branchRepository, productRepository, mapper);
        }

        public void Dispose()
        {
            // Limpa o contexto após cada teste
            _context.Database.EnsureDeleted();
            _context.Dispose();
        }

        [Fact(DisplayName = "Given date range filters. When getting sales. Then returns sales within the date range.")]
        public async Task Handle_DateRangeFilters_ReturnsSalesWithinDateRange()
        {
            // Arrange
            var sales = new List<Sale>
            {
                new(Guid.NewGuid(), Guid.NewGuid(), "123", DateTime.UtcNow, []),
                new(Guid.NewGuid(), Guid.NewGuid(), "456", DateTime.UtcNow.AddDays(-5), []),
                new(Guid.NewGuid(), Guid.NewGuid(), "789", DateTime.UtcNow.AddDays(-10), [])
            };

            await _context.Sales.AddRangeAsync(sales);
            await _context.SaveChangesAsync();

            var query = new GetAllSalesQuery
            {
                StartDate = DateTime.UtcNow.AddDays(-7),
                EndDate = DateTime.UtcNow.AddDays(-1)
            };

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            result.Should().HaveCount(1);
            result[0].Number.Should().Be("456"); // Verifica que apenas a venda dentro do intervalo de datas é retornada
        }

        [Fact(DisplayName = "Given pagination parameters. When getting sales. Then returns paginated results.")]
        public async Task Handle_PaginationParameters_ReturnsPaginatedResults()
        {
            // Arrange
            var sales = new List<Sale>
            {
                new(Guid.NewGuid(), Guid.NewGuid(), "123", DateTime.UtcNow, []),
                new(Guid.NewGuid(), Guid.NewGuid(), "456", DateTime.UtcNow.AddDays(-1), []),
                new(Guid.NewGuid(), Guid.NewGuid(), "789", DateTime.UtcNow.AddDays(-2), [])
            };

            await _context.Sales.AddRangeAsync(sales);
            await _context.SaveChangesAsync();

            var query = new GetAllSalesQuery
            {
                Page = 2,
                PageSize = 1
            };

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            result.Should().HaveCount(1);
            result[0].Number.Should().Be("456"); // Verifica a segunda página com 1 item
        }

        [Fact(DisplayName = "Given branch filter. When getting sales. Then returns sales for the specified branch.")]
        public async Task Handle_BranchFilter_ReturnsSalesForBranch()
        {
            // Arrange
            var branchId = Guid.NewGuid();
            var sales = new List<Sale>
            {
                new(Guid.NewGuid(), branchId, "123", DateTime.UtcNow, []),
                new(Guid.NewGuid(), Guid.NewGuid(), "456", DateTime.UtcNow, []),
                new(Guid.NewGuid(), branchId, "789", DateTime.UtcNow, [])
            };

            await _context.Sales.AddRangeAsync(sales);
            await _context.SaveChangesAsync();

            var query = new GetAllSalesQuery
            {
                BranchId = branchId
            };

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            result.Should().HaveCount(2);
        }

        [Fact(DisplayName = "Given multiple filters. When getting sales. Then returns sales that match all filters.")]
        public async Task Handle_MultipleFilters_ReturnsSalesThatMatchAllFilters()
        {
            // Arrange
            var branchId = Guid.NewGuid();
            var customerId = Guid.NewGuid();
            var sales = new List<Sale>
            {
                new(Guid.NewGuid(), branchId, "123", DateTime.UtcNow, []),
                new(Guid.NewGuid(), branchId, "456", DateTime.UtcNow.AddDays(-5), []),
                new(Guid.NewGuid(), Guid.NewGuid(), "789", DateTime.UtcNow.AddDays(-5), []),
                new(customerId, branchId, "101", DateTime.UtcNow.AddDays(-5), [])
            };

            await _context.Sales.AddRangeAsync(sales);
            await _context.SaveChangesAsync();

            var query = new GetAllSalesQuery
            {
                BranchId = branchId,
                StartDate = DateTime.UtcNow.AddDays(-7),
                EndDate = DateTime.UtcNow.AddDays(-1),
                CustomerId = customerId
            };

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            result.Should().HaveCount(1);
            result[0].Number.Should().Be("101"); // Verifica que apenas a venda que corresponde a todos os filtros é retornada
        }

        [Fact(DisplayName = "Given ascending order. When getting sales. Then returns sales in ascending order.")]
        public async Task Handle_AscendingOrder_ReturnsSalesInAscendingOrder()
        {
            // Arrange
            var sales = new List<Sale>
            {
                new(Guid.NewGuid(), Guid.NewGuid(), "123", DateTime.UtcNow.AddDays(-2), []),
                new(Guid.NewGuid(), Guid.NewGuid(), "456", DateTime.UtcNow.AddDays(-1), []),
                new(Guid.NewGuid(), Guid.NewGuid(), "789", DateTime.UtcNow, [])
            };

            await _context.Sales.AddRangeAsync(sales);
            await _context.SaveChangesAsync();

            var query = new GetAllSalesQuery
            {
                SortBy = "DateSold",
                Ascending = true
            };

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            result.Should().HaveCount(3);
            result[0].Number.Should().Be("123");
            result[1].Number.Should().Be("456");
            result[2].Number.Should().Be("789");
        }

        [Fact(DisplayName = "Given descending order. When getting sales. Then returns sales in descending order.")]
        public async Task Handle_DescendingOrder_ReturnsSalesInDescendingOrder()
        {
            // Arrange
            var sales = new List<Sale>
            {
                new(Guid.NewGuid(), Guid.NewGuid(), "123", DateTime.UtcNow.AddDays(-2), []),
                new(Guid.NewGuid(), Guid.NewGuid(), "456", DateTime.UtcNow.AddDays(-1), []),
                new(Guid.NewGuid(), Guid.NewGuid(), "789", DateTime.UtcNow, [])
            };

            await _context.Sales.AddRangeAsync(sales);
            await _context.SaveChangesAsync();

            var query = new GetAllSalesQuery
            {
                SortBy = "DateSold",
                Ascending = false
            };

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            result.Should().HaveCount(3);
            result[0].Number.Should().Be("789");
            result[1].Number.Should().Be("456");
            result[2].Number.Should().Be("123");
        }

        [Fact(DisplayName = "Given invalid filters. When getting sales. Then returns no sales.")]
        public async Task Handle_InvalidFilters_ReturnsNoSales()
        {
            // Arrange
            var sales = new List<Sale>
            {
                new(Guid.NewGuid(), Guid.NewGuid(), "123", DateTime.UtcNow, []),
                new(Guid.NewGuid(), Guid.NewGuid(), "456", DateTime.UtcNow.AddDays(-1), []),
                new(Guid.NewGuid(), Guid.NewGuid(), "789", DateTime.UtcNow.AddDays(-2), [])
            };

            await _context.Sales.AddRangeAsync(sales);
            await _context.SaveChangesAsync();

            var query = new GetAllSalesQuery
            {
                BranchId = Guid.NewGuid(), // Filtro que não corresponde a nenhuma venda
                StartDate = DateTime.UtcNow.AddDays(-10),
                EndDate = DateTime.UtcNow.AddDays(-5)
            };

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            result.Should().BeEmpty();
        }

        [Fact(DisplayName = "Given sorting by non-existent property. When getting sales. Then throws an exception.")]
        public async Task Handle_SortingByNonExistentProperty_ThrowsException()
        {
            // Arrange
            var sales = new List<Sale>
            {
                new(Guid.NewGuid(), Guid.NewGuid(), "123", DateTime.UtcNow, []),
                new(Guid.NewGuid(), Guid.NewGuid(), "456", DateTime.UtcNow.AddDays(-1), []),
                new(Guid.NewGuid(), Guid.NewGuid(), "789", DateTime.UtcNow.AddDays(-2), [])
            };

            await _context.Sales.AddRangeAsync(sales);
            await _context.SaveChangesAsync();

            var query = new GetAllSalesQuery
            {
                SortBy = "NonExistentProperty",
                Ascending = true
            };

            // Act & Assert
            await Assert.ThrowsAsync<ArgumentException>(() => _handler.Handle(query, CancellationToken.None));
        }
    }
}