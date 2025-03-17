using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Common.Extensions;
using Microsoft.EntityFrameworkCore;
using MediatR;
using Ambev.DeveloperEvaluation.Application.Sales.GetSale;

namespace Ambev.DeveloperEvaluation.Application.Sales.GetAllSales
{
    public class GetAllSalesHandler(
      ISaleRepository saleRepository,
      IUserRepository userRepository,
      IBranchRepository branchRepository,
      IProductRepository productRepository) : IRequestHandler<GetAllSalesQuery, List<GetSaleByIdResult>>
    {
        public async Task<List<GetSaleByIdResult>> Handle(GetAllSalesQuery query, CancellationToken cancellationToken)
        {
            // Consulta base
            var salesQuery = saleRepository.GetAll()
                .Include(s => s.SaleProducts) // Inclui os produtos da venda
                .AsQueryable();

            // Aplica filtros
            if (query.StartDate.HasValue)
            {
                salesQuery = salesQuery.Where(s => s.DateSold >= query.StartDate.Value);
            }

            if (query.EndDate.HasValue)
            {
                salesQuery = salesQuery.Where(s => s.DateSold <= query.EndDate.Value);
            }

            if (query.BranchId.HasValue)
            {
                salesQuery = salesQuery.Where(s => s.BranchId == query.BranchId.Value);
            }

            if (query.CustomerId.HasValue)
            {
                salesQuery = salesQuery.Where(s => s.ClientId == query.CustomerId.Value);
            }

            // Aplica ordenação
            if (!string.IsNullOrEmpty(query.SortBy))
            {
                salesQuery = query.Ascending
                    ? salesQuery.OrderBy(query.SortBy, ascending: true)
                    : salesQuery.OrderBy(query.SortBy, ascending: false);
            }

            // Aplica paginação
            var sales = await salesQuery
                .Skip((query.Page - 1) * query.PageSize)
                .Take(query.PageSize)
                .ToListAsync(cancellationToken);

            // Mapeia os resultados
            var result = new List<GetSaleByIdResult>();
            foreach (var sale in sales)
            {
                var customer = await userRepository.GetByIdAsync(sale.ClientId, cancellationToken);
                var branch = await branchRepository.GetByIdAsync(sale.BranchId, cancellationToken);

                var productsResponse = new List<GetSaleProductResult>();
                foreach (var saleProduct in sale.SaleProducts)
                {
                    var product = await productRepository.GetByIdAsync(saleProduct.ProductId, cancellationToken);

                    productsResponse.Add(new GetSaleProductResult
                    {
                        ProductId = saleProduct.ProductId,
                        Quantity = saleProduct.Quantity,
                        UnitPrice = saleProduct.UnitPrice,
                        FixedDiscount = saleProduct.FixedDiscount,
                        PercentualDiscount = saleProduct.PercentageDiscount,
                        TotalDiscount = saleProduct.TotalDiscount,
                        TotalCost = saleProduct.TotalCostWithDiscount,
                        TotalCostWithDiscount = saleProduct.TotalCostWithDiscount,
                        Product = product
                    });
                }

                result.Add(new GetSaleByIdResult
                {
                    Id = sale.Id,
                    Number = sale.Number,
                    DateSold = sale.DateSold,
                    Customer = customer,
                    Branch = branch,
                    Products = productsResponse,
                    CreatedAt = sale.CreatedAt,
                    UpdatedAt = sale.UpdatedAt
                });
            }

            return result;
        }
    }
}
