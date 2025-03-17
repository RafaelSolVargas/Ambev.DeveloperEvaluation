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
            var saleProduct = sale.SaleProducts.First();
            saleProduct.PercentageDiscount.Should().Be(0.20m);
            saleProduct.TotalCost.Should().Be(1000); // 10 * 100
            saleProduct.TotalDiscount.Should().Be(200); // 10 * 100 * 0.20
            saleProduct.TotalCostWithDiscount.Should().Be(800); // 10 * 100 * 0.8
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
            var saleProduct = sale.SaleProducts.First();
            saleProduct.PercentageDiscount.Should().Be(0.10m);
            saleProduct.TotalCost.Should().Be(500); // 5 * 100
            saleProduct.TotalDiscount.Should().Be(50); // 5 * 100 * 0.10
            saleProduct.TotalCostWithDiscount.Should().Be(450); // 5 * 100 * 0.9
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
            var saleProduct = sale.SaleProducts.First();
            saleProduct.PercentageDiscount.Should().Be(0);
            saleProduct.TotalCost.Should().Be(300); // 3 * 100
            saleProduct.TotalDiscount.Should().Be(0); // Sem desconto
            saleProduct.TotalCostWithDiscount.Should().Be(300); // 3 * 100
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

        [Fact(DisplayName = "Given more than 20 units of the same product across multiple SaleProducts. When creating sale. Then throws InvalidOperationException.")]
        public void CreateSale_MoreThan20UnitsOfSameProduct_ThrowsException()
        {
            // Arrange
            var productId = Guid.NewGuid();
            var products = new List<SaleProduct>
            {
                new()
                {
                    ProductId = productId,
                    Quantity = 15, // 15 unidades
                    UnitPrice = 100
                },
                new()
                {
                    ProductId = productId,
                    Quantity = 10, // 10 unidades (totalizando 25)
                    UnitPrice = 100
                }
            };

            // Act & Assert
            Action act = () =>
            {
                _ = new Sale(
                    Guid.NewGuid(), // ClientId
                    Guid.NewGuid(), // BranchId
                    "123",         // Number
                    DateTime.UtcNow, // DateSold
                    products        // Lista de produtos
                );
            };

            act.Should().Throw<InvalidOperationException>()
                .WithMessage($"Não é possível vender mais de 20 unidades do produto {productId}.");
        }

        [Fact(DisplayName = "Given product with both fixed and percentage discounts. When applying discounts. Then discounts are applied correctly.")]
        public void ApplyDiscounts_ProductWithFixedAndPercentageDiscounts_AppliesDiscountsCorrectly()
        {
            // Arrange
            var products = new List<SaleProduct>
            {
                new()
                {
                    ProductId = Guid.NewGuid(),
                    Quantity = 5,
                    UnitPrice = 100,
                    PercentageDiscount = 0.10m, // 10% de desconto
                    FixedDiscount = 20 // Desconto fixo de 20
                }
            };

            // Act
            var sale = new Sale(Guid.NewGuid(), Guid.NewGuid(), "123", DateTime.UtcNow, products);

            // Assert
            var saleProduct = sale.SaleProducts.First();
            saleProduct.TotalCost.Should().Be(500); // 5 * 100
            saleProduct.TotalDiscount.Should().Be(70); // (5 * 100 * 0.10) + 20
            saleProduct.TotalCostWithDiscount.Should().Be(430); // 500 - 70
        }

        [Fact(DisplayName = "Given product with fixed discount only. When applying discounts. Then fixed discount is applied correctly.")]
        public void ApplyDiscounts_ProductWithFixedDiscountOnly_AppliesFixedDiscountCorrectly()
        {
            // Arrange
            var products = new List<SaleProduct>
            {
                new()
                {
                    ProductId = Guid.NewGuid(),
                    Quantity = 3,
                    UnitPrice = 100,
                    FixedDiscount = 20 // Desconto fixo de 20
                }
            };

            // Act
            var sale = new Sale(Guid.NewGuid(), Guid.NewGuid(), "123", DateTime.UtcNow, products);

            // Assert
            var saleProduct = sale.SaleProducts.First();
            saleProduct.TotalCost.Should().Be(300); // 3 * 100
            saleProduct.TotalDiscount.Should().Be(20); // Desconto fixo de 20
            saleProduct.TotalCostWithDiscount.Should().Be(280); // 300 - 20
        }
    }
}