namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.UpdateSale;

public class UpdateSaleRequest
{
    public Guid Id { get; set; }
    public Guid? BranchId { get; set; }
    public Guid? ClientId { get; set; }
    public string? Number { get; set; } = string.Empty;
    public DateTime? DateSold { get; set; }
    public List<UpdateSaleProductRequest> Products { get; set; } = [];
}

public class UpdateSaleProductRequest
{
    public Guid SaleProductId { get; set; }
    public Guid? ProductId { get; set; }
    public int? Quantity { get; set; }
    public decimal? UnitPrice { get; set; }
}