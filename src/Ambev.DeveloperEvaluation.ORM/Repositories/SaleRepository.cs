using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.ORM.Common;
using Ambev.DeveloperEvaluation.Domain.Entities.Sales;
using Microsoft.EntityFrameworkCore;

namespace Ambev.DeveloperEvaluation.ORM.Repositories;

public class SaleRepository(DefaultContext _context) : BaseRepository<Sale>(_context), 
    ISaleRepository
{
    public async Task<bool> ExistsWithProduct(Guid productId, CancellationToken cancellationToken)
    {
        var exists = await _context.Sales
            .AnyAsync(sale => sale.SaleProducts.Any(sp => sp.ProductId == productId), cancellationToken);

        return exists;
    }

    public new async Task<Sale?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _context.Sales
            .Include(s => s.SaleProducts) // Inclui os produtos da venda
            .FirstOrDefaultAsync(s => s.Id == id, cancellationToken);
    }
}
