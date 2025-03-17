using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.UpdateSale;

public class UpdateSaleCommand : IRequest<bool>
{
    public Guid Id { get; set; }
    public Guid? BranchId { get; set; }
    public Guid? ClientId { get; set; }
    public string? Number { get; set; } = string.Empty;
    public DateTime? DateSold { get; set; }
    public List<UpdateSaleProductCommand> Products { get; set; } = [];
}

public class UpdateSaleProductCommand
{
    public Guid SaleProductId { get; set; }
    public Guid? ProductId { get; set; }
    public int? Quantity { get; set; }
    public decimal? UnitPrice { get; set; }
}