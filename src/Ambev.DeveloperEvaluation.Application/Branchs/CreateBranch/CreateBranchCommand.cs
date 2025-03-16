using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Branchs.CreateBranch;

public class CreateBranchCommand : IRequest<CreateBranchResult>
{
    public string Name { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
}
