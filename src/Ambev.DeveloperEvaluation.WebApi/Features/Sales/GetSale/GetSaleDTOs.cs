using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Entities.Sales;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.GetSale;

public class GetSaleByIdResponse
{
    public Guid Id { get; set; }
    public string Number { get; set; } = string.Empty;
    public DateTime DateSold { get; set; }
    public User? Customer { get; set; }
    public Branch? Branch { get; set; }
    public int ProductsCount => Products.Count;
    public decimal TotalCost => Products.Sum(x => x.TotalCost);
    public decimal TotalCostWithDiscount => Products.Sum(x => x.TotalCostWithDiscount);
    public decimal TotalDiscount => Products.Sum(x => x.TotalDiscount);
    public List<GetSaleProductResponse> Products { get; set; } = [];
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}


public class GetSaleProductResponse
{
    public Guid ProductId { get; set; }
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
    public decimal FixedDiscount { get; set; }
    public decimal PercentualDiscount { get; set; }
    public decimal TotalDiscount { get; set; }
    public decimal TotalCost { get; set; }
    public decimal TotalCostWithDiscount { get; set; }
    public Product? Product { get; set; }
}