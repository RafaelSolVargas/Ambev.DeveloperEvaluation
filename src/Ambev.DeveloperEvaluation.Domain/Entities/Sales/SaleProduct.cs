using Ambev.DeveloperEvaluation.Common.Validation;
using Ambev.DeveloperEvaluation.Domain.Common;
using Ambev.DeveloperEvaluation.Domain.Validation;

namespace Ambev.DeveloperEvaluation.Domain.Entities.Sales;

/// <summary>
/// Represents a product sold in a sale.
/// This entity follows domain-driven design principles and includes business rules validation.
/// </summary>
public class SaleProduct : BaseEntity
{
    /// <summary>
    /// Gets the ID of the sale associated with this product.
    /// </summary>
    public Guid SaleId { get; set; }

    /// <summary>
    /// Gets the ID of the product.
    /// </summary>
    public Guid ProductId { get; set; }

    public Sale Sale { get; set; } = null!;
    
    public Product Product { get; set; } = null!;

    /// <summary>
    /// Gets the quantity of the product sold.
    /// Must be greater than zero and cannot exceed 20.
    /// </summary>
    public int Quantity { get; set; }

    /// <summary>
    /// Gets the unit price of the product.
    /// Must be greater than zero.
    /// </summary>
    public decimal UnitPrice { get; set; }

    /// <summary>
    /// Gets the percentage discount applied to the product.
    /// Must be between 0 and 1 (0% to 100%).
    /// </summary>
    public decimal PercentageDiscount { get; set; }

    /// <summary>
    /// Gets the fixed discount applied to the product.
    /// Cannot be negative.
    /// </summary>
    public decimal FixedDiscount { get; set; }

    /// <summary>
    /// Gets the total discounts.
    /// This value is calculated automatically.
    /// </summary>
    public decimal TotalDiscount { get; set; }

    /// <summary>
    /// Gets the total cost of the product without discounts.
    /// This value is calculated automatically.
    /// </summary>
    public decimal TotalCost { get; set; }

    /// <summary>
    /// Gets the total cost of the product after applying discounts.
    /// This value is calculated automatically.
    /// </summary>
    public decimal TotalCostWithDiscount { get; set; }

    /// <summary>
    /// Gets the date and time when the product was sold.
    /// Cannot be in the future.
    /// </summary>
    public DateTime DateSold { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// Gets the date and time when the sale product was created.
    /// </summary>
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// Gets the date and time of the last update to the sale product's information.
    /// </summary>
    public DateTime? UpdatedAt { get; set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="SaleProduct"/> class.
    /// </summary>
    /// <param name="saleId">The ID of the sale.</param>
    /// <param name="productId">The ID of the product.</param>
    /// <param name="quantity">The quantity of the product sold.</param>
    /// <param name="unitPrice">The unit price of the product.</param>
    /// <param name="percentageDiscount">The percentage discount applied to the product.</param>
    /// <param name="fixedDiscount">The fixed discount applied to the product.</param>
    /// <param name="dateSold">The date and time when the product was sold.</param>
    public SaleProduct(
        Guid id,
        Guid saleId,
        Guid productId,
        int quantity,
        decimal unitPrice,
        decimal percentageDiscount,
        decimal fixedDiscount,
        DateTime dateSold,
        Product? product)
    {
        Id = id;
        SaleId = saleId;
        ProductId = productId;
        Quantity = quantity;
        UnitPrice = unitPrice;
        PercentageDiscount = percentageDiscount;
        FixedDiscount = fixedDiscount;
        DateSold = dateSold;
        CreatedAt = DateTime.UtcNow;

        CalculateTotalCost();
        Product = product!;
    }

    public SaleProduct() { }

    /// <summary>
    /// Calculates the total cost of the product after applying discounts.
    /// </summary>
    public void CalculateTotalCost()
    {
        TotalCost = Quantity * UnitPrice;
        
        TotalDiscount = (TotalCost * PercentageDiscount) + FixedDiscount;

        TotalCostWithDiscount = TotalCost - TotalDiscount;
    }

    /// <summary>
    /// Updates the sale product's information.
    /// </summary>
    /// <param name="quantity">The new quantity of the product sold.</param>
    /// <param name="unitPrice">The new unit price of the product.</param>
    /// <param name="percentageDiscount">The new percentage discount applied to the product.</param>
    /// <param name="fixedDiscount">The new fixed discount applied to the product.</param>
    /// <param name="dateSold">The new date and time when the product was sold.</param>
    public void Update(
        int quantity,
        decimal unitPrice,
        decimal percentageDiscount,
        decimal fixedDiscount,
        DateTime dateSold)
    {
        Quantity = quantity;
        UnitPrice = unitPrice;
        PercentageDiscount = percentageDiscount;
        FixedDiscount = fixedDiscount;
        DateSold = dateSold;
        UpdatedAt = DateTime.UtcNow;

        CalculateTotalCost();
    }

    /// <summary>
    /// Validates the sale product entity using the SaleProductValidator rules.
    /// </summary>
    /// <returns>
    /// A <see cref="ValidationResultDetail"/> containing:
    /// - IsValid: Indicates whether all validation rules passed.
    /// - Errors: Collection of validation errors if any rules failed.
    /// </returns>
    /// <remarks>
    /// <listheader>The validation includes checking:</listheader>
    /// <list type="bullet">SaleId and ProductId are not empty.</list>
    /// <list type="bullet">Quantity is greater than zero and does not exceed 20.</list>
    /// <list type="bullet">UnitPrice is greater than zero.</list>
    /// <list type="bullet">PercentageDiscount is between 0 and 1.</list>
    /// <list type="bullet">FixedDiscount is not negative.</list>
    /// <list type="bullet">DateSold is not in the future.</list>
    /// </remarks>
    public ValidationResultDetail Validate()
    {
        var validator = new SaleProductValidator();
        var result = validator.Validate(this);
        return new ValidationResultDetail
        {
            IsValid = result.IsValid,
            Errors = result.Errors.Select(o => (ValidationErrorDetail)o)
        };
    }
}
