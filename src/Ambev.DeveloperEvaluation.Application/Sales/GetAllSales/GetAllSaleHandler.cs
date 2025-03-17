using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Common.Extensions;
using Microsoft.EntityFrameworkCore;
using MediatR;
using Ambev.DeveloperEvaluation.Application.Sales.GetSale;
using Ambev.DeveloperEvaluation.Common.Pagination;

namespace Ambev.DeveloperEvaluation.Application.Sales.GetAllSales
{
    public class GetAllSalesHandler(
      ISaleRepository saleRepository,
      IUserRepository userRepository,
      IBranchRepository branchRepository,
      IProductRepository productRepository) : IRequestHandler<GetAllSalesQuery, PaginatedList<GetSaleByIdResult>>
    {
        public async Task<PaginatedList<GetSaleByIdResult>> Handle(GetAllSalesQuery query, CancellationToken cancellationToken)
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

            // Mapeia os resultados para GetSaleByIdResult
            var resultQuery = salesQuery.Select(s => new GetSaleByIdResult
            {
                Id = s.Id,
                Number = s.Number,
                DateSold = s.DateSold,
                Customer = userRepository.GetByIdAsync(s.ClientId, cancellationToken).Result,
                Branch = branchRepository.GetByIdAsync(s.BranchId, cancellationToken).Result,
                Products = s.SaleProducts.Select(sp => new GetSaleProductResult
                {
                    ProductId = sp.ProductId,
                    Quantity = sp.Quantity,
                    UnitPrice = sp.UnitPrice,
                    FixedDiscount = sp.FixedDiscount,
                    PercentualDiscount = sp.PercentageDiscount,
                    TotalDiscount = sp.TotalDiscount,
                    TotalCost = sp.TotalCostWithDiscount,
                    TotalCostWithDiscount = sp.TotalCostWithDiscount,
                    Product = productRepository.GetByIdAsync(sp.ProductId, cancellationToken).Result
                }).ToList(),
                CreatedAt = s.CreatedAt,
                UpdatedAt = s.UpdatedAt
            });

            // Aplica paginação e retorna a lista paginada
            var paginatedResult = await PaginatedList<GetSaleByIdResult>.CreateAsync(resultQuery, query.Page, query.PageSize);

            return paginatedResult;
        }
    }
}