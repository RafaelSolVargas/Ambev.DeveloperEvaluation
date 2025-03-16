namespace Ambev.DeveloperEvaluation.Application.Branchs.CreateBranch;

public sealed class CreateBranchResult
{
    public string Name { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public Guid Id { get; set; }
}
