using Ambev.DeveloperEvaluation.Domain.Entities.Sales;
using Ambev.DeveloperEvaluation.Domain.Validation;
using FluentValidation.TestHelper;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Domain.Validation
{
    public class ProductValidatorTests
    {
        private readonly ProductValidator _validator;

        public ProductValidatorTests()
        {
            _validator = new ProductValidator();
        }

        [Fact(DisplayName = "Given valid product. When validating. Then no validation errors.")]
        public void Validate_ValidProduct_NoValidationErrors()
        {
            // Arrange
            var product = new Product
            {
                Name = "Valid Product",
                Description = "Valid Description",
                CreatedAt = DateTime.Today.AddDays(-1)
            };

            // Act
            var result = _validator.TestValidate(product);

            // Assert
            result.ShouldNotHaveAnyValidationErrors();
        }

        [Fact(DisplayName = "Given empty name. When validating. Then validation error for name.")]
        public void Validate_EmptyName_ValidationErrorForName()
        {
            // Arrange
            var product = new Product
            {
                Name = "", // Invalid
                Description = "Valid Description"
            };

            // Act
            var result = _validator.TestValidate(product);

            // Assert
            result.ShouldHaveValidationErrorFor(p => p.Name);
        }

        [Fact(DisplayName = "Given name exceeding maximum length. When validating. Then validation error for name.")]
        public void Validate_NameExceedsMaxLength_ValidationErrorForName()
        {
            // Arrange
            var product = new Product
            {
                Name = new string('a', 101), // Exceeds maximum length
                Description = "Valid Description"
            };

            // Act
            var result = _validator.TestValidate(product);

            // Assert
            result.ShouldHaveValidationErrorFor(p => p.Name);
        }

        [Fact(DisplayName = "Given empty description. When validating. Then validation error for description.")]
        public void Validate_EmptyDescription_ValidationErrorForDescription()
        {
            // Arrange
            var product = new Product
            {
                Name = "Valid Product",
                Description = "" // Invalid
            };

            // Act
            var result = _validator.TestValidate(product);

            // Assert
            result.ShouldHaveValidationErrorFor(p => p.Description);
        }

        [Fact(DisplayName = "Given description exceeding maximum length. When validating. Then validation error for description.")]
        public void Validate_DescriptionExceedsMaxLength_ValidationErrorForDescription()
        {
            // Arrange
            var product = new Product
            {
                Name = "Valid Product",
                Description = new string('a', 501) // Exceeds maximum length
            };

            // Act
            var result = _validator.TestValidate(product);

            // Assert
            result.ShouldHaveValidationErrorFor(p => p.Description);
        }
    }
}