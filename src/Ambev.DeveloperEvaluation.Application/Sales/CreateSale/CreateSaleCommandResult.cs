using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.CreateSale;

public class CreateSaleCommand : IRequest<CreateSaleResult>
{
    public string Number { get; set; } = string.Empty;
    public DateTime DateSold { get; set; }
    public Guid CustomerId { get; set; }
    public Guid BranchId { get; set; }
    public List<CreateSaleProductCommand> Products { get; set; } = [];
}

public class CreateSaleProductCommand
{
    public Guid ProductId { get; set; }
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
}

public class CreateSaleResult
{
    public Guid Id { get; set; }
    public string Number { get; set; } = string.Empty;
    public DateTime DateSold { get; set; }
    public Guid CustomerId { get; set; }
    public Guid BranchId { get; set; }
    public decimal TotalAmount { get; set; }
    public List<CreateSaleProductResult> Products { get; set; } = [];
}

public class CreateSaleProductResult
{
    public Guid ProductId { get; set; }
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
}
