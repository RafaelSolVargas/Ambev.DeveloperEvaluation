using Ambev.DeveloperEvaluation.Domain.Repositories;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.UpdateSale
{
    public class UpdateSaleHandler(ISaleRepository _saleRepository) : IRequestHandler<UpdateSaleCommand, bool>
    {
        public async Task<bool> Handle(UpdateSaleCommand request, CancellationToken cancellationToken)
        {
            var sale = await _saleRepository.GetByIdAsync(request.Id, cancellationToken);
            if (sale == null)
            {
                return false; // Venda não encontrada
            }

            foreach (var saleProduct in sale.SaleProducts)
            {
                var objectToUpdate = request.Products.FirstOrDefault(x => x.SaleProductId == saleProduct.Id);

                if (objectToUpdate != null)
                {
                    saleProduct.Quantity = objectToUpdate.Quantity ?? saleProduct.Quantity;
                    saleProduct.ProductId = objectToUpdate.ProductId ?? saleProduct.ProductId;
                    saleProduct.UnitPrice = objectToUpdate.UnitPrice ?? saleProduct.UnitPrice;

                    saleProduct.CalculateTotalCost();
                }
            }

            sale.BranchId = request.BranchId ?? sale.BranchId;
            sale.ClientId = request.ClientId ?? sale.ClientId;
            sale.Number = request.Number ?? sale.Number;
            sale.DateSold = request.DateSold ?? sale.DateSold;

            await _saleRepository.UpdateAsync(sale, cancellationToken);

            return true; 
        }
    }
}
