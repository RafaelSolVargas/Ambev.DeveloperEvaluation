namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.CreateSale;

public class CreateSaleRequest
{
    public string Number { get; set; } = string.Empty;
    public DateTime DateSold { get; set; }
    public Guid CustomerId { get; set; }
    public Guid BranchId { get; set; }
    public List<SaleProductRequest> Products { get; set; } = [];
}

public class SaleProductRequest
{
    public Guid ProductId { get; set; }
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
}

public class CreateSaleResponse
{
    public Guid Id { get; set; }
    public string Number { get; set; } = string.Empty;
    public DateTime DateSold { get; set; }
    public Guid CustomerId { get; set; }
    public Guid BranchId { get; set; }
    public decimal TotalAmount { get; set; }
    public List<CreateSaleProductResponse> Products { get; set; } = [];
}

public class CreateSaleProductResponse
{
    public Guid ProductId { get; set; }
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
    public decimal TotalCost { get; set; }
}