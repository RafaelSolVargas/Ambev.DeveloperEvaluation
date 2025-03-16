using Ambev.DeveloperEvaluation.Common.Validation;
using Ambev.DeveloperEvaluation.Domain.Common;
using Ambev.DeveloperEvaluation.Domain.Enums;
using Ambev.DeveloperEvaluation.Domain.Validation;

namespace Ambev.DeveloperEvaluation.Domain.Entities.Sales
{
    /// <summary>
    /// Represents a sale in the system.
    /// </summary>
    public class Sale : BaseEntity
    {
        /// <summary>
        /// Gets the ID of the client associated with the sale.
        /// </summary>
        public Guid ClientId { get; set; }

        /// <summary>
        /// Gets the ID of the branch where the sale was made.
        /// </summary>
        public Guid BranchId { get; set; }

        /// <summary>
        /// Gets the list of products sold in the sale.
        /// </summary>
        public ICollection<SaleProduct> SaleProducts { get; set; } = [];

        /// <summary>
        /// Gets the status of the sale (e.g., NotCancelled, Cancelled).
        /// </summary>
        public SaleStatus Status { get; set; } = SaleStatus.NotCancelled;

        /// <summary>
        /// Gets the sale number.
        /// </summary>
        public string Number { get; set; } = string.Empty;

        /// <summary>
        /// Gets the date and time when the sale was made.
        /// </summary>
        public DateTime DateSold { get; set; } = DateTime.UtcNow;

        /// <summary>
        /// Gets the date and time when the sale was created.
        /// </summary>
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        /// <summary>
        /// Gets the date and time of the last update to the sale's information.
        /// </summary>
        public DateTime? UpdatedAt { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Sale"/> class.
        /// </summary>
        /// <param name="clientId">The ID of the client.</param>
        /// <param name="branchId">The ID of the branch.</param>
        /// <param name="number">The sale number.</param>
        /// <param name="dateSold">The date and time when the sale was made.</param>
        /// <param name="products">The list of products sold in the sale.</param>
        public Sale(
            Guid clientId,
            Guid branchId,
            string number,
            DateTime dateSold,
            List<SaleProduct> products)
        {
            ClientId = clientId;
            BranchId = branchId;
            Number = number;
            DateSold = dateSold;
            Status = SaleStatus.NotCancelled;
            CreatedAt = DateTime.UtcNow;
            SaleProducts = ApplyDiscounts(products ?? throw new ArgumentNullException(nameof(products)));
        }

        public Sale() { }

        /// <summary>
        /// Applies discounts to the products based on the business rules.
        /// </summary>
        /// <param name="products">The list of products to apply discounts to.</param>
        /// <returns>The list of products with discounts applied.</returns>
        private static List<SaleProduct> ApplyDiscounts(List<SaleProduct> products)
        {
            foreach (var product in products)
            {
                if (product.Quantity > 20)
                {
                    throw new InvalidOperationException($"Não é possível vender mais de 20 unidades do produto {product.ProductId}.");
                }

                if (product.Quantity >= 10)
                {
                    product.PercentageDiscount = 0.20m; // 20% discount
                }
                else if (product.Quantity >= 4)
                {
                    product.PercentageDiscount = 0.10m; // 10% discount
                }
                else
                {
                    product.PercentageDiscount = 0; // No discount
                }

                // Recalculate the total cost after applying the discount
                product.CalculateTotalCost();
            }

            return products;
        }

        /// <summary>
        /// Cancels the sale.
        /// Changes the sale's status to Cancelled and updates the UpdatedAt property.
        /// </summary>
        public void Cancel()
        {
            Status = SaleStatus.Cancelled;
            UpdatedAt = DateTime.UtcNow;
        }

        /// <summary>
        /// Validates the sale entity using the SaleValidator rules.
        /// </summary>
        /// <returns>
        /// A <see cref="ValidationResultDetail"/> containing:
        /// - IsValid: Indicates whether all validation rules passed.
        /// - Errors: Collection of validation errors if any rules failed.
        /// </returns>
        /// <remarks>
        /// <listheader>The validation includes checking:</listheader>
        /// <list type="bullet">ClientId and BranchId are not empty.</list>
        /// <list type="bullet">Sale number is not empty and has a valid format.</list>
        /// <list type="bullet">DateSold is not in the future.</list>
        /// <list type="bullet">Products list is not empty.</list>
        /// <list type="bullet">Each product in the list is valid.</list>
        /// </remarks>
        public ValidationResultDetail Validate()
        {
            var validator = new SaleValidator();
            var result = validator.Validate(this);
            return new ValidationResultDetail
            {
                IsValid = result.IsValid,
                Errors = result.Errors.Select(o => (ValidationErrorDetail)o)
            };
        }
    }
}