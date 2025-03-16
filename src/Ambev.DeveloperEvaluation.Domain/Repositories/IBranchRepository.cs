using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Domain.Repositories;

public interface IBranchRepository : IGetByIdRepository<Branch>,
    IGetAllRepository<Branch>,
    ICreateRepository<Branch>
{
    Task<Branch?> GetByNameAsync(string name, CancellationToken cancellationToken = default);
}