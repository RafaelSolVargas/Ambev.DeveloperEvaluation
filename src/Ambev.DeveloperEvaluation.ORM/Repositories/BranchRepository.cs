using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.ORM.Common;
using Microsoft.EntityFrameworkCore;

namespace Ambev.DeveloperEvaluation.ORM.Repositories;

public class BranchRepository(DefaultContext _context) : BaseRepository<Branch>(_context), 
    IBranchRepository
{
    public async Task<Branch?> GetByNameAsync(string name, CancellationToken cancellationToken = default)
    {
        return await _context.Branches.FirstOrDefaultAsync(o => o.Name == name, cancellationToken);
    }
}
