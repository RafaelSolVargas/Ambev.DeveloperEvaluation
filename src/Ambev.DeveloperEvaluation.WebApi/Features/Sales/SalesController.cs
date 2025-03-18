using Ambev.DeveloperEvaluation.WebApi.Features.Sales.CreateSale;
using Ambev.DeveloperEvaluation.Application.Sales.GetAllSales;
using Ambev.DeveloperEvaluation.WebApi.Features.Sales.GetSale;
using Ambev.DeveloperEvaluation.Application.Sales.CreateSale;
using Ambev.DeveloperEvaluation.Application.Sales.GetSale;
using Ambev.DeveloperEvaluation.WebApi.Common;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using MediatR;
using Ambev.DeveloperEvaluation.Common.Pagination;
using Ambev.DeveloperEvaluation.Domain.Enums;
using Ambev.DeveloperEvaluation.Application.Sales.ChangeSaleStatus;
using Ambev.DeveloperEvaluation.Application.Sales.DeleteSale;
using Ambev.DeveloperEvaluation.WebApi.Features.Sales.UpdateSale;
using Ambev.DeveloperEvaluation.Application.Sales.UpdateSale;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales;

[ApiController]
[Route("api/[controller]")]
public class SalesController(IMediator mediator, IMapper mapper) : BaseController
{
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(ApiResponseWithData<GetSaleByIdResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetSaleById(Guid id, CancellationToken cancellationToken)
    {
        var query = new GetSaleByIdQuery { Id = id };
        var result = await mediator.Send(query, cancellationToken);

        if (result == null)
        {
            return NotFound();
        }

        var response = mapper.Map<GetSaleByIdResponse>(result);

        return Ok(response);
    }

    [HttpGet]
    [ProducesResponseType(typeof(ApiResponseWithData<ApiResponseWithData<PaginatedList<GetSaleByIdResponse>>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetAllSales(
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 10,
        [FromQuery] string? sortBy = null,
        [FromQuery] bool ascending = true,
        [FromQuery] DateTime? startDate = null,
        [FromQuery] DateTime? endDate = null,
        [FromQuery] Guid? branchId = null,
        [FromQuery] Guid? customerId = null,
        CancellationToken cancellationToken = default)
    {
        var query = new GetAllSalesQuery
        {
            Page = page,
            PageSize = pageSize,
            SortBy = sortBy,
            Ascending = ascending,
            StartDate = startDate,
            EndDate = endDate,
            BranchId = branchId,
            CustomerId = customerId
        };

        var result = await mediator.Send(query, cancellationToken);

        var itens = mapper.Map<List<GetSaleByIdResponse>>(result.ToList());

        var response = result.ConvertToType(itens);

        return OkPaginated(response);
    }

    [HttpGet("branch/{branchId}")]
    [ProducesResponseType(typeof(ApiResponseWithData<ApiResponseWithData<PaginatedList<GetSaleByIdResponse>>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetAllSalesByBranch(
    [FromRoute] Guid branchId,
    [FromQuery] string? sortBy = null,
    [FromQuery] bool ascending = true,
    CancellationToken cancellationToken = default)
    {
        var query = new GetAllSalesQuery
        {
            Page = 1,
            PageSize = int.MaxValue,
            SortBy = sortBy,
            Ascending = ascending,
            BranchId = branchId,
        };

        var result = await mediator.Send(query, cancellationToken);

        var itens = mapper.Map<List<GetSaleByIdResponse>>(result);

        return OkPaginated(result.ConvertToType(itens));
    }

    [HttpGet("customer/{customerId}")]
    [ProducesResponseType(typeof(ApiResponseWithData<ApiResponseWithData<PaginatedList<GetSaleByIdResponse>>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetAllSalesByCostumer(
    [FromRoute] Guid customerId,
    [FromQuery] string? sortBy = null,
    [FromQuery] bool ascending = true,
    CancellationToken cancellationToken = default)
    {
        var query = new GetAllSalesQuery
        {
            Page = 1,
            PageSize = int.MaxValue,
            SortBy = sortBy,
            Ascending = ascending,
            CustomerId = customerId
        };

        var result = await mediator.Send(query, cancellationToken);

        var itens = mapper.Map<List<GetSaleByIdResponse>>(result);

        return OkPaginated(result.ConvertToType(itens));
    }

    [HttpPost]
    [ProducesResponseType(typeof(ApiResponseWithData<CreateSaleResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateSale([FromBody] CreateSaleRequest request, CancellationToken cancellationToken)
    {
        var validator = new CreateSaleRequestValidator();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
        {
            return BadRequest(validationResult.Errors);
        }

        var command = new CreateSaleCommand
        {
            Number = request.Number,
            DateSold = request.DateSold,
            CustomerId = request.CustomerId,
            BranchId = request.BranchId,
            Products = request.Products.Select(x => new CreateSaleProductCommand()
            {
                ProductId = x.ProductId,
                Quantity = x.Quantity,
                UnitPrice = x.UnitPrice,
            }).ToList()
        };

        var result = await mediator.Send(command, cancellationToken);
        var response = mapper.Map<CreateSaleResponse>(result);

        return Ok(response);
    }

    [HttpPatch("ChangeStatus/{saleId}/{status}")]
    [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> ChangeStaus(
    [FromRoute] Guid saleId,
    [FromRoute] SaleStatus status,
    CancellationToken cancellationToken = default)
    {
        var query = new ChangeSaleStatusQuery
        {
            Id = saleId,
            NewStatus = status,
        };

        var result = await mediator.Send(query, cancellationToken);

        if (!result)
        {
            return NotFound();
        }

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteSale(
        [FromRoute] Guid id, 
        CancellationToken cancellationToken = default)
    {
        var result = await mediator.Send(new DeleteSaleCommand { Id = id }, cancellationToken);

        return result ? NoContent() : NotFound();
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateSale(
        [FromRoute] Guid id, 
        [FromBody] UpdateSaleRequest request, 
        CancellationToken cancellationToken = default)
    {
        var validator = new UpdateSaleRequestValidator();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
        {
            return BadRequest(validationResult.Errors);
        }

        if (id != request.Id)
        {
            return BadRequest("ID da venda não corresponde ao ID na requisição.");
        }

        var result = await mediator.Send(new UpdateSaleCommand
        {
            Id = request.Id,
            BranchId = request.BranchId,
            ClientId = request.ClientId,
            Number = request.Number,
            DateSold = request.DateSold,
            Products = request.Products.Select(x => new UpdateSaleProductCommand()
            {
                ProductId = x.ProductId,
                Quantity = x.Quantity,
                SaleProductId = x.SaleProductId,
                UnitPrice = x.UnitPrice 
            }).ToList()
        }, cancellationToken);

        return result ? NoContent() : NotFound();
    }
}
