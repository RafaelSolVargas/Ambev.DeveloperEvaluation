using Ambev.DeveloperEvaluation.Application.Sales.CreateSale;
using Ambev.DeveloperEvaluation.Application.Sales.GetSale;
using Ambev.DeveloperEvaluation.WebApi.Features.Sales.CreateSale;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.GetSale;

public class SaleProfile : Profile
{
    public SaleProfile()
    {
        CreateMap<GetSaleByIdResult, GetSaleByIdResponse>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Number, opt => opt.MapFrom(src => src.Number))
            .ForMember(dest => dest.DateSold, opt => opt.MapFrom(src => src.DateSold))
            .ForMember(dest => dest.Customer, opt => opt.MapFrom(src => src.Customer))
            .ForMember(dest => dest.Branch, opt => opt.MapFrom(src => src.Branch))
            .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => src.CreatedAt))
            .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => src.UpdatedAt))
            .ForMember(dest => dest.Products, opt => opt.MapFrom(src => src.Products));

        CreateMap<GetCustomerResult, GetCustomerResponse>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email));

        CreateMap<GetBranchResult, GetBranchResponse>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.Address));

        CreateMap<GetSaleProductResult, GetSaleProductResponse>()
            .ForMember(dest => dest.ProductId, opt => opt.MapFrom(src => src.ProductId))
            .ForMember(dest => dest.Quantity, opt => opt.MapFrom(src => src.Quantity))
            .ForMember(dest => dest.UnitPrice, opt => opt.MapFrom(src => src.UnitPrice))
            .ForMember(dest => dest.FixedDiscount, opt => opt.MapFrom(src => src.FixedDiscount))
            .ForMember(dest => dest.PercentualDiscount, opt => opt.MapFrom(src => src.PercentualDiscount))
            .ForMember(dest => dest.TotalDiscount, opt => opt.MapFrom(src => src.TotalDiscount))
            .ForMember(dest => dest.TotalCost, opt => opt.MapFrom(src => src.TotalCost))
            .ForMember(dest => dest.TotalCostWithDiscount, opt => opt.MapFrom(src => src.TotalCostWithDiscount))
            .ForMember(dest => dest.Product, opt => opt.MapFrom(src => src.Product));

        CreateMap<GetProductResult, GetProductResponse>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name));

        CreateMap<CreateSaleResult, CreateSaleResponse>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Number, opt => opt.MapFrom(src => src.Number))
            .ForMember(dest => dest.DateSold, opt => opt.MapFrom(src => src.DateSold))
            .ForMember(dest => dest.CustomerId, opt => opt.MapFrom(src => src.CustomerId))
            .ForMember(dest => dest.BranchId, opt => opt.MapFrom(src => src.BranchId))
            .ForMember(dest => dest.TotalAmount, opt => opt.MapFrom(src => src.TotalAmount))
            .ForMember(dest => dest.Products, opt => opt.MapFrom(src => src.Products));

        CreateMap<CreateSaleProductResult, CreateSaleProductResponse>()
            .ForMember(dest => dest.ProductId, opt => opt.MapFrom(src => src.ProductId))
            .ForMember(dest => dest.Quantity, opt => opt.MapFrom(src => src.Quantity))
            .ForMember(dest => dest.UnitPrice, opt => opt.MapFrom(src => src.UnitPrice));

    }
}
