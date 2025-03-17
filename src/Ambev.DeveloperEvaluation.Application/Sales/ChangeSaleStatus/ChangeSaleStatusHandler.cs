using Ambev.DeveloperEvaluation.Domain.Repositories;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.ChangeSaleStatus
{
    public class ChangeSaleStatusHandler(ISaleRepository saleRepository) : IRequestHandler<ChangeSaleStatusQuery, bool>
    {
        public async Task<bool> Handle(ChangeSaleStatusQuery query, CancellationToken cancellationToken)
        {
            var sale = await saleRepository.GetByIdAsync(query.Id, cancellationToken);

            if (sale == null || sale.Status == query.NewStatus)
            {
                return false;
            }

            sale.Status = query.NewStatus;

            await saleRepository.UpdateAsync(sale, cancellationToken);

            return true;
        }
    }
}
