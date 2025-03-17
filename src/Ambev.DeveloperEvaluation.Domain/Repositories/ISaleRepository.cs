using Ambev.DeveloperEvaluation.Domain.Entities.Sales;

namespace Ambev.DeveloperEvaluation.Domain.Repositories;

public interface ISaleRepository : IGetAllRepository<Sale>,
    ICreateRepository<Sale>,
    IGetQueryable<Sale>
{
    Task<Sale?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
}
