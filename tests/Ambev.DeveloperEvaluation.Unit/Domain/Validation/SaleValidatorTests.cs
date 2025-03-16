using Ambev.DeveloperEvaluation.Domain.Entities.Sales;
using Ambev.DeveloperEvaluation.Domain.Validation;
using FluentValidation.TestHelper;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Domain.Validation
{
    public class SaleValidatorTests
    {
        private readonly SaleValidator _validator;

        public SaleValidatorTests()
        {
            _validator = new SaleValidator();
        }

        [Fact(DisplayName = "Given empty ClientId. When validating sale. Then validation fails.")]
        public void Validate_EmptyClientId_ValidationFails()
        {
            // Arrange
            var sale = new Sale
            {
                ClientId = Guid.Empty,
                BranchId = Guid.NewGuid(),
                Number = "123",
                DateSold = DateTime.UtcNow,
                SaleProducts =
                [
                    new()
                    {
                        ProductId = Guid.NewGuid(),
                        Quantity = 1,
                        UnitPrice = 100
                    }
                ]
            };

            // Act
            var result = _validator.TestValidate(sale);

            // Assert
            result.ShouldHaveValidationErrorFor(s => s.ClientId)
                .WithErrorMessage("O ID do cliente não pode estar vazio.");
        }

        [Fact(DisplayName = "Given empty BranchId. When validating sale. Then validation fails.")]
        public void Validate_EmptyBranchId_ValidationFails()
        {
            // Arrange
            var sale = new Sale
            {
                ClientId = Guid.NewGuid(),
                BranchId = Guid.Empty,
                Number = "123",
                DateSold = DateTime.UtcNow,
                SaleProducts =
                [
                    new()
                    {
                        ProductId = Guid.NewGuid(),
                        Quantity = 1,
                        UnitPrice = 100
                    }
                ]
            };

            // Act
            var result = _validator.TestValidate(sale);

            // Assert
            result.ShouldHaveValidationErrorFor(s => s.BranchId)
                .WithErrorMessage("O ID da filial não pode estar vazio.");
        }

        [Fact(DisplayName = "Given empty Number. When validating sale. Then validation fails.")]
        public void Validate_EmptyNumber_ValidationFails()
        {
            // Arrange
            var sale = new Sale
            {
                ClientId = Guid.NewGuid(),
                BranchId = Guid.NewGuid(),
                Number = string.Empty,
                DateSold = DateTime.UtcNow,
                SaleProducts =
                [
                    new()
                    {
                        ProductId = Guid.NewGuid(),
                        Quantity = 1,
                        UnitPrice = 100
                    }
                ]
            };

            // Act
            var result = _validator.TestValidate(sale);

            // Assert
            result.ShouldHaveValidationErrorFor(s => s.Number)
                .WithErrorMessage("O número da venda não pode estar vazio.");
        }

        [Fact(DisplayName = "Given future DateSold. When validating sale. Then validation fails.")]
        public void Validate_FutureDateSold_ValidationFails()
        {
            // Arrange
            var sale = new Sale
            {
                ClientId = Guid.NewGuid(),
                BranchId = Guid.NewGuid(),
                Number = "123",
                DateSold = DateTime.UtcNow.AddDays(1),
                SaleProducts =
                [
                    new()
                    {
                        ProductId = Guid.NewGuid(),
                        Quantity = 1,
                        UnitPrice = 100
                    }
                ]
            };

            // Act
            var result = _validator.TestValidate(sale);

            // Assert
            result.ShouldHaveValidationErrorFor(s => s.DateSold)
                .WithErrorMessage("A data da venda não pode ser no futuro.");
        }

        [Fact(DisplayName = "Given empty SaleProducts. When validating sale. Then validation fails.")]
        public void Validate_EmptySaleProducts_ValidationFails()
        {
            // Arrange
            var sale = new Sale
            {
                ClientId = Guid.NewGuid(),
                BranchId = Guid.NewGuid(),
                Number = "123",
                DateSold = DateTime.UtcNow,
                SaleProducts = []
            };

            // Act
            var result = _validator.TestValidate(sale);

            // Assert
            result.ShouldHaveValidationErrorFor(s => s.SaleProducts)
                .WithErrorMessage("A venda deve conter pelo menos um produto.");
        }
    }
}