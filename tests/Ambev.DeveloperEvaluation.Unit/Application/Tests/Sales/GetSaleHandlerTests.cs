using Ambev.DeveloperEvaluation.Application.Sales.GetSale;
using Ambev.DeveloperEvaluation.Domain.Entities.Sales;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Domain.Entities;
using FluentAssertions;
using NSubstitute;
using Xunit;
using AutoMapper;
using StackExchange.Redis;
using Ambev.DeveloperEvaluation.Domain.Cache;
using Newtonsoft.Json;

namespace Ambev.DeveloperEvaluation.Unit.Application.Tests.Sales
{
    public class GetSaleByIdHandlerTests
    {
        private readonly ISaleRepository _saleRepository;
        private readonly IUserRepository _userRepository;
        private readonly IBranchRepository _branchRepository;
        private readonly IProductRepository _productRepository;
        private readonly IConnectionMultiplexer _redis;
        private readonly GetSaleByIdHandler _handler;
        private readonly IMapper _mapper;
        private readonly IDatabase _redisDb;
        public GetSaleByIdHandlerTests()
        {
            _saleRepository = Substitute.For<ISaleRepository>();
            _userRepository = Substitute.For<IUserRepository>();
            _branchRepository = Substitute.For<IBranchRepository>();
            _redis = Substitute.For<IConnectionMultiplexer>();
            _productRepository = Substitute.For<IProductRepository>();
            _mapper = Substitute.For<IMapper>();
            _redisDb = Substitute.For<IDatabase>();
            _redis.GetDatabase().Returns(_redisDb);
            _handler = new GetSaleByIdHandler(_saleRepository, _userRepository, _productRepository, _branchRepository, _mapper, _redis);
        }

        [Fact(DisplayName = "Given valid sale ID. When getting sale and cache is populated. Returns sale details from cache.")]
        public async Task Handle_CacheHit_ReturnsSaleDetailsFromCache()
        {
            // Arrange
            var saleId = Guid.NewGuid();
            var cachedSale = new GetSaleByIdResult
            {
                Id = saleId,
                Number = "123",
                DateSold = DateTime.UtcNow,
                Customer = new GetCustomerResult { Id = Guid.NewGuid() },
                Branch = new GetBranchResult { Id = Guid.NewGuid() },
                Products = [],
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            _redisDb.StringGetAsync($"{CacheKeys.Sales}{saleId}").Returns(JsonConvert.SerializeObject(cachedSale));

            var query = new GetSaleByIdQuery { Id = saleId };

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            result.Should().NotBeNull();
            result!.Id.Should().Be(saleId);

            // Verifica se o repositório não foi chamado
            await _saleRepository.DidNotReceive().GetByIdAsync(Arg.Any<Guid>(), Arg.Any<CancellationToken>());
            await _userRepository.DidNotReceive().GetByIdAsync(Arg.Any<Guid>(), Arg.Any<CancellationToken>());
            await _branchRepository.DidNotReceive().GetByIdAsync(Arg.Any<Guid>(), Arg.Any<CancellationToken>());
        }

        [Fact(DisplayName = "Given non-existent sale ID. When getting sale. Returns null and does not store in cache.")]
        public async Task Handle_NonExistentSaleId_ReturnsNullAndDoesNotStoreInCache()
        {
            // Arrange
            var saleId = Guid.NewGuid();
            _saleRepository.GetByIdAsync(saleId, Arg.Any<CancellationToken>()).Returns((Sale)null!);

            var query = new GetSaleByIdQuery { Id = saleId };

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            result.Should().BeNull();

            // Verifica se a cache não foi alterada
            await _redisDb.DidNotReceive().StringSetAsync(
                Arg.Any<RedisKey>(), // Use RedisKey em vez de string
                Arg.Any<RedisValue>(),
                Arg.Any<TimeSpan?>(), // Use TimeSpan? em vez de TimeSpan
                Arg.Any<When>(),      // Adicione When
                Arg.Any<CommandFlags>() // Adicione CommandFlags
            );

            await _saleRepository.Received(1).GetByIdAsync(saleId, Arg.Any<CancellationToken>());
            await _userRepository.DidNotReceive().GetByIdAsync(Arg.Any<Guid>(), Arg.Any<CancellationToken>());
            await _branchRepository.DidNotReceive().GetByIdAsync(Arg.Any<Guid>(), Arg.Any<CancellationToken>());
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

            var customerResponse = _mapper.Map<GetCustomerResult>(customer);
            var branchResponse = _mapper.Map<GetBranchResult>(branch);

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
            result.Customer.Should().Be(customerResponse);
            result.Branch.Should().Be(branchResponse);
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

            var customerResponse = _mapper.Map<GetCustomerResult>(customer);
            var branchResponse = _mapper.Map<GetBranchResult>(branch);

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
            result.Customer.Should().Be(customerResponse);
            result.Branch.Should().Be(branchResponse);
            result.Products.Should().HaveCount(1);

            var saleProductResponse = result.Products[0];
            saleProductResponse.ProductId.Should().Be(productId);

            await _saleRepository.Received(1).GetByIdAsync(saleId, Arg.Any<CancellationToken>());
            await _userRepository.Received(1).GetByIdAsync(sale.ClientId, Arg.Any<CancellationToken>());
            await _branchRepository.Received(1).GetByIdAsync(sale.BranchId, Arg.Any<CancellationToken>());
            await _productRepository.Received(1).GetByIdAsync(productId, Arg.Any<CancellationToken>());
        }
    }
}