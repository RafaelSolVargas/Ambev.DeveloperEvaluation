using Ambev.DeveloperEvaluation.Domain.Cache;
using Ambev.DeveloperEvaluation.Domain.Events;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using MediatR;
using Rebus.Bus;
using StackExchange.Redis;

namespace Ambev.DeveloperEvaluation.Application.Sales.UpdateSale
{
    public class UpdateSaleHandler(ISaleRepository _saleRepository,
        IBus bus,
        IConnectionMultiplexer redis) : IRequestHandler<UpdateSaleCommand, bool>
    {
        private readonly IDatabase _redisDb = redis.GetDatabase();
     
        public async Task<bool> Handle(UpdateSaleCommand command, CancellationToken cancellationToken)
        {
            var sale = await _saleRepository.GetByIdAsync(command.Id, cancellationToken);
            if (sale == null)
            {
                return false; // Venda não encontrada
            }

            foreach (var saleProduct in sale.SaleProducts)
            {
                var objectToUpdate = command.Products.FirstOrDefault(x => x.SaleProductId == saleProduct.Id);

                if (objectToUpdate != null)
                {
                    saleProduct.Quantity = objectToUpdate.Quantity ?? saleProduct.Quantity;
                    saleProduct.ProductId = objectToUpdate.ProductId ?? saleProduct.ProductId;
                    saleProduct.UnitPrice = objectToUpdate.UnitPrice ?? saleProduct.UnitPrice;

                    saleProduct.CalculateTotalCost();
                }
            }

            sale.BranchId = command.BranchId ?? sale.BranchId;
            sale.ClientId = command.ClientId ?? sale.ClientId;
            sale.Number = command.Number ?? sale.Number;
            sale.DateSold = command.DateSold ?? sale.DateSold;

            await _saleRepository.UpdateAsync(sale, cancellationToken);

            await bus.Publish(new SaleModifiedEvent()
            {
                SaleId = sale.Id,
            });

            var cacheKey = $"{CacheKeys.Sales}{command.Id}";
            await _redisDb.KeyDeleteAsync(cacheKey);

            return true; 
        }
    }
}
