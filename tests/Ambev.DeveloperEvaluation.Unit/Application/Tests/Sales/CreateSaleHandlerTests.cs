using Ambev.DeveloperEvaluation.Application.Sales.CreateSale;
using Ambev.DeveloperEvaluation.Unit.Application.TestData;
using Ambev.DeveloperEvaluation.Domain.Entities.Sales;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Domain.Entities;
using FluentAssertions;
using NSubstitute;
using Xunit;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.Unit.Application.Tests.Sales
{
    public class CreateSaleHandlerTests
    {
        private readonly ISaleRepository _saleRepository;
        private readonly IUserRepository _userRepository;
        private readonly IBranchRepository _branchRepository;
        private readonly CreateSaleHandler _handler;

        public CreateSaleHandlerTests()
        {
            _saleRepository = Substitute.For<ISaleRepository>();
            _userRepository = Substitute.For<IUserRepository>();
            _branchRepository = Substitute.For<IBranchRepository>();
            _handler = new CreateSaleHandler(_saleRepository, _userRepository, _branchRepository);
        }

        [Fact(DisplayName = "Given valid sale. When creating sale. Returns success response")]
        public async Task Handle_ValidRequest_ReturnsSuccessResponse()
        {
            // Given
            var command = CreateSaleHandlerTestData.GenerateValidCommand();

            // Configuração dos mocks
            var customer = new User { Id = command.CustomerId };
            var branch = new Branch { Id = command.BranchId };
            var sale = new Sale(
                command.CustomerId,
                command.BranchId,
                command.Number,
                command.DateSold,
                command.Products.ConvertAll(p => new SaleProduct
                {
                    ProductId = p.ProductId,
                    Quantity = p.Quantity,
                    UnitPrice = p.UnitPrice
                }));

            _userRepository.GetByIdAsync(command.CustomerId, Arg.Any<CancellationToken>()).Returns(customer);
            _branchRepository.GetByIdAsync(command.BranchId, Arg.Any<CancellationToken>()).Returns(branch);
            _saleRepository.CreateAsync(Arg.Any<Sale>(), Arg.Any<CancellationToken>()).Returns(sale);

            // When
            var result = await _handler.Handle(command, CancellationToken.None);

            // Then
            result.Should().NotBeNull();
            result.Id.Should().Be(sale.Id);
            result.Number.Should().Be(sale.Number);
            result.DateSold.Should().Be(sale.DateSold);
            result.CustomerId.Should().Be(sale.ClientId);
            result.BranchId.Should().Be(sale.BranchId);
            result.TotalAmount.Should().Be(sale.TotalCost);
            result.Products.Should().HaveCount(sale.SaleProducts.Count);

            // Verifica se os métodos dos repositórios foram chamados corretamente
            await _userRepository.Received(1).GetByIdAsync(command.CustomerId, Arg.Any<CancellationToken>());
            await _branchRepository.Received(1).GetByIdAsync(command.BranchId, Arg.Any<CancellationToken>());
            await _saleRepository.Received(1).CreateAsync(Arg.Any<Sale>(), Arg.Any<CancellationToken>());
        }

        [Fact(DisplayName = "Given invalid CustomerId. When creating sale. Then throws InvalidOperationException.")]
        public async Task Handle_InvalidCustomerId_ThrowsException()
        {
            // Given
            var command = CreateSaleHandlerTestData.GenerateValidCommand();

            // Configuração dos mocks
            _userRepository.GetByIdAsync(command.CustomerId, Arg.Any<CancellationToken>()).Returns((User)null!);

            // When & Then
            Func<Task> act = async () => await _handler.Handle(command, CancellationToken.None);
            await act.Should().ThrowAsync<InvalidOperationException>().WithMessage("Invalid CustomerId passed");
        }

        [Fact(DisplayName = "Given invalid BranchId. When creating sale. Then throws InvalidOperationException.")]
        public async Task Handle_InvalidBranchId_ThrowsException()
        {
            // Given
            var command = CreateSaleHandlerTestData.GenerateValidCommand();

            // Configuração dos mocks
            var customer = new User { Id = command.CustomerId };
            _userRepository.GetByIdAsync(command.CustomerId, Arg.Any<CancellationToken>()).Returns(customer);
            _branchRepository.GetByIdAsync(command.BranchId, Arg.Any<CancellationToken>()).Returns((Branch)null!);

            // When & Then
            Func<Task> act = async () => await _handler.Handle(command, CancellationToken.None);
            await act.Should().ThrowAsync<InvalidOperationException>().WithMessage("Invalid BranchId passed");
        }

        [Fact(DisplayName = "Given product quantity > 20. When creating sale. Then throws InvalidOperationException.")]
        public async Task Handle_ProductQuantityExceedsLimit_ThrowsException()
        {
            // Given
            var command = CreateSaleHandlerTestData.GenerateValidCommand();
            command.Products[0].Quantity = 21; // Quantidade inválida

            // Configuração dos mocks
            var customer = new User { Id = command.CustomerId };
            var branch = new Branch { Id = command.BranchId };
            _userRepository.GetByIdAsync(command.CustomerId, Arg.Any<CancellationToken>()).Returns(customer);
            _branchRepository.GetByIdAsync(command.BranchId, Arg.Any<CancellationToken>()).Returns(branch);

            // When & Then
            Func<Task> act = async () => await _handler.Handle(command, CancellationToken.None);
            await act.Should().ThrowAsync<ValidationException>();
        }

        [Fact(DisplayName = "Given invalid command. When creating sale. Then throws ValidationException.")]
        public async Task Handle_InvalidCommand_ThrowsValidationException()
        {
            // Given
            var command = CreateSaleHandlerTestData.GenerateValidCommand();
            command.Number = string.Empty; // Número inválido

            // Configuração dos mocks
            var customer = new User { Id = command.CustomerId };
            var branch = new Branch { Id = command.BranchId };
            _userRepository.GetByIdAsync(command.CustomerId, Arg.Any<CancellationToken>()).Returns(customer);
            _branchRepository.GetByIdAsync(command.BranchId, Arg.Any<CancellationToken>()).Returns(branch);

            // When & Then
            Func<Task> act = async () => await _handler.Handle(command, CancellationToken.None);
            await act.Should().ThrowAsync<ValidationException>();
        }
    }
}