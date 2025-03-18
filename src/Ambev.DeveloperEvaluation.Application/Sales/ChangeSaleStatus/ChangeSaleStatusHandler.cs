using Ambev.DeveloperEvaluation.Domain.Cache;
using Ambev.DeveloperEvaluation.Domain.Events;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using MediatR;
using Rebus.Bus;
using StackExchange.Redis;

namespace Ambev.DeveloperEvaluation.Application.Sales.ChangeSaleStatus
{
    public class ChangeSaleStatusHandler(ISaleRepository saleRepository,
        IBus bus,
        IConnectionMultiplexer redis) : IRequestHandler<ChangeSaleStatusQuery, bool>
    {
        private readonly IDatabase _redisDb = redis.GetDatabase();

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

            var cacheKey = $"{CacheKeys.Sales}{query.Id}";
            await _redisDb.KeyDeleteAsync(cacheKey);

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
