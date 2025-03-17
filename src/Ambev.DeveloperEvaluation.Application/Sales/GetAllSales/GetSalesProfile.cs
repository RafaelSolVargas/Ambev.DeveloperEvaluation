using AutoMapper;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Application.Sales.GetSale;
using Ambev.DeveloperEvaluation.Domain.Entities.Sales;

namespace Ambev.DeveloperEvaluation.Application.Sales.GetAllSales;

public class GetSalesProfile : Profile
{
    public GetSalesProfile()
    {
        CreateMap<User, GetCustomerResult>();
        CreateMap<Branch, GetBranchResult>();
        CreateMap<Product, GetProductResult>();
    }
}
