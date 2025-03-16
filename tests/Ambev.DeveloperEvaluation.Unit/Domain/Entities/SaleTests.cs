using Ambev.DeveloperEvaluation.Domain.Entities.Sales;
using FluentAssertions;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Domain.Entities
{
    public class SaleTests
    {
        [Fact(DisplayName = "Given product quantity >= 10. When applying discounts. Then 20% discount is applied.")]
        public void ApplyDiscounts_QuantityGreaterThanOrEqualTo10_Applies20PercentDiscount()
        {
            // Arrange
            var products = new List<SaleProduct>
            {
                new() {
                    ProductId = Guid.NewGuid(),
                    Quantity = 10,
                    UnitPrice = 100,
                    PercentageDiscount = 0,
                    FixedDiscount = 0
                }
            };

            // Act
            var sale = new Sale(Guid.NewGuid(), Guid.NewGuid(), "123", DateTime.UtcNow, products);

            // Assert
            sale.SaleProducts.First().PercentageDiscount.Should().Be(0.20m);
            sale.SaleProducts.First().TotalCost.Should().Be(800); // 10 * 100 * 0.8
        }

        [Fact(DisplayName = "Given product quantity >= 4 and < 10. When applying discounts. Then 10% discount is applied.")]
        public void ApplyDiscounts_QuantityBetween4And10_Applies10PercentDiscount()
        {
            // Arrange
            var products = new List<SaleProduct>
            {
                new() {
                    ProductId = Guid.NewGuid(),
                    Quantity = 5,
                    UnitPrice = 100,
                    PercentageDiscount = 0,
                    FixedDiscount = 0
                }
            };

            // Act
            var sale = new Sale(Guid.NewGuid(), Guid.NewGuid(), "123", DateTime.UtcNow, products);

            // Assert
            sale.SaleProducts.First().PercentageDiscount.Should().Be(0.10m);
            sale.SaleProducts.First().TotalCost.Should().Be(450); // 5 * 100 * 0.9
        }

        [Fact(DisplayName = "Given product quantity < 4. When applying discounts. Then no discount is applied.")]
        public void ApplyDiscounts_QuantityLessThan4_AppliesNoDiscount()
        {
            // Arrange
            var products = new List<SaleProduct>
            {
                new()
                {
                    ProductId = Guid.NewGuid(),
                    Quantity = 3,
                    UnitPrice = 100,
                    PercentageDiscount = 0,
                    FixedDiscount = 0
                }
            };

            // Act
            var sale = new Sale(Guid.NewGuid(), Guid.NewGuid(), "123", DateTime.UtcNow, products);

            // Assert
            sale.SaleProducts.First().PercentageDiscount.Should().Be(0);
            sale.SaleProducts.First().TotalCost.Should().Be(300); // 3 * 100
        }

        [Fact(DisplayName = "Given product quantity > 20. When applying discounts. Then throws InvalidOperationException.")]
        public void ApplyDiscounts_QuantityGreaterThan20_ThrowsException()
        {
            // Arrange
            var products = new List<SaleProduct>
            {
                new()
                {
                    ProductId = Guid.NewGuid(),
                    Quantity = 21,
                    UnitPrice = 100,
                    PercentageDiscount = 0,
                    FixedDiscount = 0
                }
            };

            // Act & Assert
            Action act = () => _ = new Sale(Guid.NewGuid(), Guid.NewGuid(), "123", DateTime.UtcNow, products);
            act.Should().Throw<InvalidOperationException>();
        }
    }
}