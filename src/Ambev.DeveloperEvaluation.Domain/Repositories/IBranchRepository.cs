using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Entities.Sales;

namespace Ambev.DeveloperEvaluation.Domain.Repositories;

public interface IBranchRepository : IGetByIdRepository<Sale>,
    IGetAllRepository<Sale>,
    ICreateRepository<Sale>
{
    Task<Branch?> GetByNameAsync(string name, CancellationToken cancellationToken = default);
}