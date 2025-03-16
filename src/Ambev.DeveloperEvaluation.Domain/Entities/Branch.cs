using Ambev.DeveloperEvaluation.Common.Validation;
using Ambev.DeveloperEvaluation.Domain.Common;
using Ambev.DeveloperEvaluation.Domain.Validation;

namespace Ambev.DeveloperEvaluation.Domain.Entities
{
    /// <summary>
    /// Represents a branch in the system.
    /// This entity follows domain-driven design principles and includes business rules validation.
    /// </summary>
    public class Branch : BaseEntity
    {
        /// <summary>
        /// Gets the name of the branch.
        /// Must not be null or empty and should have a minimum length of 3 characters.
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Gets the address of the branch.
        /// Must not be null or empty and should have a maximum length of 200 characters.
        /// </summary>
        public string Address { get; private set; }

        /// <summary>
        /// Gets the date and time when the branch was created.
        /// </summary>
        public DateTime CreatedAt { get; private set; }

        /// <summary>
        /// Gets the date and time of the last update to the branch's information.
        /// </summary>
        public DateTime? UpdatedAt { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Branch"/> class.
        /// </summary>
        /// <param name="name">The name of the branch.</param>
        /// <param name="address">The address of the branch.</param>
        public Branch(string name, string address)
        {
            Name = name;
            Address = address;
            CreatedAt = DateTime.UtcNow;
        }

        /// <summary>
        /// Updates the branch's information.
        /// </summary>
        /// <param name="name">The new name of the branch.</param>
        /// <param name="address">The new address of the branch.</param>
        public void Update(string name, string address)
        {
            Name = name;
            Address = address;
            UpdatedAt = DateTime.UtcNow;
        }

        /// <summary>
        /// Validates the branch entity using the BranchValidator rules.
        /// </summary>
        /// <returns>
        /// A <see cref="ValidationResultDetail"/> containing:
        /// - IsValid: Indicates whether all validation rules passed.
        /// - Errors: Collection of validation errors if any rules failed.
        /// </returns>
        /// <remarks>
        /// <listheader>The validation includes checking:</listheader>
        /// <list type="bullet">Name is not empty and has a minimum length of 3 characters.</list>
        /// <list type="bullet">Address is not empty and has a maximum length of 200 characters.</list>
        /// </remarks>
        public ValidationResultDetail Validate()
        {
            var validator = new BranchValidator();
            var result = validator.Validate(this);
            return new ValidationResultDetail
            {
                IsValid = result.IsValid,
                Errors = result.Errors.Select(o => (ValidationErrorDetail)o)
            };
        }
    }
}