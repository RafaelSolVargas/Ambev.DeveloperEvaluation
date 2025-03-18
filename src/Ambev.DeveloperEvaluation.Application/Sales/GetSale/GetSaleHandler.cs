using Ambev.DeveloperEvaluation.Domain.Cache;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using MediatR;
using Newtonsoft.Json;
using StackExchange.Redis;

namespace Ambev.DeveloperEvaluation.Application.Sales.GetSale
{
    public class GetSaleByIdHandler(
        ISaleRepository saleRepository,
        IUserRepository userRepository,
        IProductRepository productRepository,
        IBranchRepository branchRepository,
        IMapper mapper,
        IConnectionMultiplexer redis) : IRequestHandler<GetSaleByIdQuery, GetSaleByIdResult?>
    {
        private readonly IDatabase _redisDb = redis.GetDatabase();

        public async Task<GetSaleByIdResult?> Handle(GetSaleByIdQuery query, CancellationToken cancellationToken)
        {
            var cacheKey = $"{CacheKeys.Sales}{query.Id}";
            var cachedSale = await _redisDb.StringGetAsync(cacheKey);

            if (cachedSale.HasValue)
            {
                return JsonConvert.DeserializeObject<GetSaleByIdResult>(cachedSale!);
            }

            var sale = await saleRepository.GetByIdAsync(query.Id, cancellationToken);

            if (sale == null)
            {
                return null;
            }

            // Busca o cliente e a filial associados à venda
            var customer = await userRepository.GetByIdAsync(sale.ClientId, cancellationToken);
            var branch = await branchRepository.GetByIdAsync(sale.BranchId, cancellationToken);

            // Busca os produtos associados a cada SaleProduct
            var productsResponse = new List<GetSaleProductResult>();
            foreach (var saleProduct in sale.SaleProducts)
            {
                var product = await productRepository.GetByIdAsync(saleProduct.ProductId, cancellationToken);

                productsResponse.Add(new GetSaleProductResult
                {
                    Id = saleProduct.Id,
                    ProductId = saleProduct.ProductId,
                    Quantity = saleProduct.Quantity,
                    UnitPrice = saleProduct.UnitPrice,
                    FixedDiscount = saleProduct.FixedDiscount,
                    PercentualDiscount = saleProduct.PercentageDiscount,
                    TotalDiscount = saleProduct.TotalDiscount,
                    TotalCost = saleProduct.TotalCostWithDiscount,
                    TotalCostWithDiscount = saleProduct.TotalCostWithDiscount,
                    Product = mapper.Map<GetProductResult>(product)
                });
            }

            var response = new GetSaleByIdResult
            {
                Id = sale.Id,
                Number = sale.Number,
                DateSold = sale.DateSold,
                Customer = mapper.Map<GetCustomerResult>(customer),
                Branch = mapper.Map<GetBranchResult>(branch),
                Products = productsResponse,
                CreatedAt = sale.CreatedAt,
                UpdatedAt = sale.UpdatedAt
            };

            await _redisDb.StringSetAsync(cacheKey, JsonConvert.SerializeObject(response), TimeSpan.FromMinutes(10));

            return response;
        }
    }
}
