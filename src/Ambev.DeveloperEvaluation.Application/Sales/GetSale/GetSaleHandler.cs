using Ambev.DeveloperEvaluation.Domain.Repositories;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.GetSale
{
    public class GetSaleByIdHandler(
        ISaleRepository saleRepository,
        IUserRepository userRepository,
        IProductRepository productRepository,
        IBranchRepository branchRepository) : IRequestHandler<GetSaleByIdQuery, GetSaleByIdResult?>
    {
        public async Task<GetSaleByIdResult?> Handle(GetSaleByIdQuery query, CancellationToken cancellationToken)
        {
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
                    ProductId = saleProduct.ProductId,
                    Quantity = saleProduct.Quantity,
                    UnitPrice = saleProduct.UnitPrice,
                    FixedDiscount = saleProduct.FixedDiscount,
                    PercentualDiscount = saleProduct.PercentageDiscount,
                    TotalDiscount = saleProduct.TotalDiscount,
                    TotalCost = saleProduct.TotalCostWithDiscount,
                    TotalCostWithDiscount = saleProduct.TotalCostWithDiscount,
                    Product = product // Adicionado
                });
            }

            // Retorna a resposta
            return new GetSaleByIdResult
            {
                Id = sale.Id,
                Number = sale.Number,
                DateSold = sale.DateSold,
                Customer = customer,
                Branch = branch,
                Products = productsResponse,
                CreatedAt = sale.CreatedAt,
                UpdatedAt = sale.UpdatedAt
            };
        }
    }
}
