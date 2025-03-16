using Ambev.DeveloperEvaluation.Domain.Entities.Sales;

namespace Ambev.DeveloperEvaluation.Domain.Repositories;

public interface ISaleRepository : IGetByIdRepository<Sale>,
    IGetAllRepository<Sale>,
    ICreateRepository<Sale>
{ }
