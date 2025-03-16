using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using FluentValidation;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Branchs.CreateBranch
{
    public class CreateBranchHandler(IBranchRepository _branchRepository) : IRequestHandler<CreateBranchCommand, CreateBranchResult>
    {
        public async Task<CreateBranchResult> Handle(CreateBranchCommand command, CancellationToken cancellationToken)
        {
            var validator = new CreateBranchValidator();
            var validationResult = await validator.ValidateAsync(command, cancellationToken);

            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }

            var existingBranch = await _branchRepository.GetByNameAsync(command.Name, cancellationToken);
            
            if (existingBranch != null)
            {
                throw new InvalidOperationException("There is already a branch with this name");
            }

            var branch = new Branch(command.Name, command.Address);

            branch = await _branchRepository.CreateAsync(branch, cancellationToken);

            return new CreateBranchResult
            {
                Id = branch.Id,
                Name = branch.Name,
                Address = branch.Address,
            };
        }
    }
}
