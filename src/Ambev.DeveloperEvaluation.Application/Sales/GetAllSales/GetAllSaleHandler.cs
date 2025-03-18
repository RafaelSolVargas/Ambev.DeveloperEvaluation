using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Common.Extensions;
using Microsoft.EntityFrameworkCore;
using MediatR;
using Ambev.DeveloperEvaluation.Application.Sales.GetSale;
using Ambev.DeveloperEvaluation.Common.Pagination;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.Application.Sales.GetAllSales
{
    public class GetAllSalesHandler(
      ISaleRepository saleRepository,
      IUserRepository userRepository,
      IBranchRepository branchRepository,
      IProductRepository productRepository,
      IMapper mapper) : IRequestHandler<GetAllSalesQuery, PaginatedList<GetSaleByIdResult>>
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

            // Primeiramente, obtenha a lista de vendas com paginação
            var salesQueryResult = await salesQuery
                .Skip((query.Page - 1) * query.PageSize)  // Aplica a paginação
                .Take(query.PageSize)  // Limita o número de itens
                .ToListAsync(cancellationToken);  // Obtém a lista de vendas de forma assíncrona

            // Agora, preenche as propriedades 'Branch' e 'Customer' para cada venda
            var saleResults = new List<GetSaleByIdResult>();

            foreach (var sale in salesQueryResult)
            {
                // Obter as propriedades associadas de forma assíncrona
                var customer = await userRepository.GetByIdAsync(sale.ClientId, cancellationToken);
                var branch = await branchRepository.GetByIdAsync(sale.BranchId, cancellationToken);

                // Mapeia os produtos
                var products = new List<GetSaleProductResult>();
                foreach (var saleProduct in sale.SaleProducts)
                {
                    var product = await productRepository.GetByIdAsync(saleProduct.ProductId, cancellationToken);

                    products.Add(new GetSaleProductResult
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

                // Adiciona o resultado à lista
                saleResults.Add(new GetSaleByIdResult
                {
                    Id = sale.Id,
                    Number = sale.Number,
                    DateSold = sale.DateSold,
                    Customer = mapper.Map<GetCustomerResult>(customer),
                    Branch = mapper.Map<GetBranchResult>(branch),
                    Products = products,
                    CreatedAt = sale.CreatedAt,
                    UpdatedAt = sale.UpdatedAt
                });
            }

            // Agora, obtenha o total de itens para a páginação
            var totalItems = await salesQuery.CountAsync(cancellationToken);

            // Crie a lista paginada manualmente
            var paginatedResult = new PaginatedList<GetSaleByIdResult>(
                saleResults, totalItems, query.Page, query.PageSize
            );

            // Retorne o resultado
            return paginatedResult;

        }
    }
}