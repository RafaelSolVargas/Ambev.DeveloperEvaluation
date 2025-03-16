using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Branchs.CreateBranch
{
    public class CreateBranchValidator : AbstractValidator<CreateBranchCommand>
    {
        public CreateBranchValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty();

            RuleFor(x => x.Address)
                .NotEmpty();
        }
    }
}
