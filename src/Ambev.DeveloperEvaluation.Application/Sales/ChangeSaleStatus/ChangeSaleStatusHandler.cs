using Ambev.DeveloperEvaluation.Domain.Events;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using MediatR;
using Rebus.Bus;

namespace Ambev.DeveloperEvaluation.Application.Sales.ChangeSaleStatus
{
    public class ChangeSaleStatusHandler(ISaleRepository saleRepository,
        IBus bus) : IRequestHandler<ChangeSaleStatusQuery, bool>
    {
        public async Task<bool> Handle(ChangeSaleStatusQuery query, CancellationToken cancellationToken)
        {
            var sale = await saleRepository.GetByIdAsync(query.Id, cancellationToken);

            if (sale == null)
            {
                return false;
            }

            if (sale.Status == query.NewStatus)
            {
                return true;
            }

            sale.Status = query.NewStatus;

            await saleRepository.UpdateAsync(sale, cancellationToken);

            if (sale.Status == Domain.Enums.SaleStatus.Cancelled)
            {
                await bus.Publish(new SaleCancelledEvent()
                {
                    Id = sale.Id,
                });
            }
            else
            {
                await bus.Publish(new SaleUncancelledEvent()
                {
                    Id = sale.Id,
                });
            }

            return true;
        }
    }
}
