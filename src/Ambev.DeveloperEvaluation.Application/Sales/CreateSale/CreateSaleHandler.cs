using Ambev.DeveloperEvaluation.Domain.Entities.Sales;
using Ambev.DeveloperEvaluation.Domain.Events;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using FluentValidation;
using MediatR;
using Rebus.Bus;

namespace Ambev.DeveloperEvaluation.Application.Sales.CreateSale
{
    public class CreateSaleHandler(ISaleRepository saleRepository,
        IUserRepository userRepository,
        IBranchRepository branchRepository,
        IBus bus) : IRequestHandler<CreateSaleCommand, CreateSaleResult>
    {
        public async Task<CreateSaleResult> Handle(CreateSaleCommand command, CancellationToken cancellationToken)
        {
            var validator = new CreateSaleCommandValidator();
            var validationResult = await validator.ValidateAsync(command, cancellationToken);

            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }

            var products = command.Products.Select(p => new SaleProduct
            {
                ProductId = p.ProductId,
                Quantity = p.Quantity,
                UnitPrice = p.UnitPrice
            }).ToList();

            var sale = new Sale(
                command.CustomerId,
                command.BranchId,
                command.Number,
                command.DateSold,
                products
            );

            _ = await userRepository.GetByIdAsync(command.CustomerId, cancellationToken) ?? throw new InvalidOperationException("Invalid CustomerId passed");

            _ = await branchRepository.GetByIdAsync(command.BranchId, cancellationToken) ?? throw new InvalidOperationException("Invalid BranchId passed");

            sale = await saleRepository.CreateAsync(sale, cancellationToken);

            await bus.Publish(new SaleCreatedEvent()
            {
                SaleId = sale.Id,
                ClientId = sale.BranchId,
                DateSold = sale.DateSold,
                TotalAmount = sale.TotalCost,
            });

            return new CreateSaleResult
            {
                Id = sale.Id,
                Number = sale.Number,
                DateSold = sale.DateSold,
                CustomerId = sale.ClientId,
                BranchId = sale.BranchId,
                TotalAmount = sale.TotalCost,
                Products = sale.SaleProducts.Select(p => new CreateSaleProductResult
                {
                    ProductId = p.ProductId,
                    Quantity = p.Quantity,
                    UnitPrice = p.UnitPrice,
                }).ToList()
            };
        }
    }
}
