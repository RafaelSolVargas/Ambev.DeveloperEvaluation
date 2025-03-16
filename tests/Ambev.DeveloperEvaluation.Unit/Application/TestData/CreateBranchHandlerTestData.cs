using Ambev.DeveloperEvaluation.Application.Branchs.CreateBranch;
using Bogus;

namespace Ambev.DeveloperEvaluation.Unit.Application.TestData;

public static class CreateBranchHandlerTestData
{
    private static readonly Faker<CreateBranchCommand> createBranchHandlerFaker = new Faker<CreateBranchCommand>()
        .RuleFor(u => u.Name, f => f.Internet.UserName())
        .RuleFor(u => u.Address, f => f.Address.ZipCode());

    public static CreateBranchCommand GenerateValidCommand()
    {
        return createBranchHandlerFaker.Generate();
    }
}
