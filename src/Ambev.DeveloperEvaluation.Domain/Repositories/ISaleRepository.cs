using Ambev.DeveloperEvaluation.Domain.Entities.Sales;

namespace Ambev.DeveloperEvaluation.Domain.Repositories;

public interface ISaleRepository : IGetAllRepository<Sale>,
    ICreateRepository<Sale>,
    IGetQueryable<Sale>,
    IUpdateRepository<Sale>,
    IDeleteRepository<Sale>
{
    Task<Sale?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<bool> ExistsWithProduct(Guid productId, CancellationToken cancellationToken = default);
    Task<List<Sale>> GetAllWithProduct(Guid productId, CancellationToken cancellationToken = default);
}
