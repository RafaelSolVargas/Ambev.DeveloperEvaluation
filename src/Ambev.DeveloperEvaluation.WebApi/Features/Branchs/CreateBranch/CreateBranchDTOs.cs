namespace Ambev.DeveloperEvaluation.WebApi.Features.Branchs.CreateBranch;

public class CreateBranchRequest
{
    public string Name { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
}

public class CreateBranchResponse : CreateBranchRequest
{
    public Guid Id { get; set; }
}