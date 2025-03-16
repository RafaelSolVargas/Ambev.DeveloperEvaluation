using Ambev.DeveloperEvaluation.Application.Branchs.CreateBranch;
using Ambev.DeveloperEvaluation.Unit.Application.TestData;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Domain.Entities;
using FluentAssertions;
using NSubstitute;
using AutoMapper;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application;

/// <summary>
/// Contains unit tests for the <see cref="CreateBranchHandler"/> class.
/// </summary>
public class CreateBranchHandlerTests
{
    private readonly IBranchRepository _branchRepository;
    private readonly CreateBranchHandler _handler;
    private readonly IMapper _mapper;

    public CreateBranchHandlerTests()
    {
        _branchRepository = Substitute.For<IBranchRepository>();
        _handler = new CreateBranchHandler(_branchRepository);
        _mapper = Substitute.For<IMapper>();
    }

    [Fact(DisplayName = "Given valid branch data When creating branch Then returns success response")]
    public async Task Handle_ValidRequest_ReturnsSuccessResponse()
    {
        // Given
        var command = CreateBranchHandlerTestData.GenerateValidCommand();
        var branch = new Branch
        {
            Id = Guid.NewGuid(),
            Name = command.Name,
            Address = command.Address,
        };

        var result = new CreateBranchResult
        {
            Id = branch.Id,
        };

        _mapper.Map<Branch>(command).Returns(branch);
        _mapper.Map<CreateBranchResult>(branch).Returns(result);

        _branchRepository.CreateAsync(Arg.Any<Branch>(), Arg.Any<CancellationToken>()).Returns(branch);

        // When
        var createBranchResult = await _handler.Handle(command, CancellationToken.None);

        // Then
        createBranchResult.Should().NotBeNull();
        createBranchResult.Id.Should().Be(branch.Id);
        await _branchRepository.Received(1).CreateAsync(Arg.Any<Branch>(), Arg.Any<CancellationToken>());
    }

    [Fact(DisplayName = "Given invalid branch data When creating branch Then throws validation exception")]
    public async Task Handle_InvalidRequest_ThrowsValidationException()
    {
        // Given
        var command = new CreateBranchCommand(); // Empty command will fail validation

        // When
        var act = () => _handler.Handle(command, CancellationToken.None);

        // Then
        await act.Should().ThrowAsync<FluentValidation.ValidationException>();
    }
}
