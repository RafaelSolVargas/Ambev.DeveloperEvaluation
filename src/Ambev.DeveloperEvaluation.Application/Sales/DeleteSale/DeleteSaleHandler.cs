using Ambev.DeveloperEvaluation.Domain.Cache;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using MediatR;
using StackExchange.Redis;

namespace Ambev.DeveloperEvaluation.Application.Sales.DeleteSale
{
    public class DeleteSaleHandler : IRequestHandler<DeleteSaleCommand, bool>
    {
        private readonly ISaleRepository _saleRepository;
        private readonly IDatabase _redisDb;

        public DeleteSaleHandler(ISaleRepository saleRepository,
        IConnectionMultiplexer redis)
        {
            _saleRepository = saleRepository;
            _redisDb = redis.GetDatabase();
        }

        public async Task<bool> Handle(DeleteSaleCommand request, CancellationToken cancellationToken)
        {
            var sale = await _saleRepository.GetByIdAsync(request.Id, cancellationToken);
            if (sale == null)
            {
                return false;
            }

            await _saleRepository.DeleteAsync(sale.Id, cancellationToken);

            var cacheKey = $"{CacheKeys.Sales}{sale.Id}";
            await _redisDb.KeyDeleteAsync(cacheKey);

            return true;
        }
    }
}
