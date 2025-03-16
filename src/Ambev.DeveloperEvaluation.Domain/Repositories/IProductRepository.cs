using Ambev.DeveloperEvaluation.Domain.Entities.Sales;

namespace Ambev.DeveloperEvaluation.Domain.Repositories;

public interface IProductRepository : IGetByIdRepository<Product>,
    IGetAllRepository<Product>,
    ICreateRepository<Product>,
    IUpdateRepository<Product>,
    IDeleteRepository<Product>
{
    Task<Product?> GetByNameAsync(string name, CancellationToken cancellationToken = default);
}
