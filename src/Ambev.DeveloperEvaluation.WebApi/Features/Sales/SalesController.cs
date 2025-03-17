using Ambev.DeveloperEvaluation.WebApi.Features.Sales.CreateSale;
using Ambev.DeveloperEvaluation.Application.Sales.GetAllSales;
using Ambev.DeveloperEvaluation.WebApi.Features.Sales.GetSale;
using Ambev.DeveloperEvaluation.Application.Sales.CreateSale;
using Ambev.DeveloperEvaluation.Application.Sales.GetSale;
using Ambev.DeveloperEvaluation.WebApi.Common;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using MediatR;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales;

[ApiController]
[Route("api/[controller]")]
public class SalesController(IMediator mediator, IMapper mapper) : BaseController
{
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
    [ProducesResponseType(typeof(ApiResponseWithData<List<GetSaleByIdResponse>>), StatusCodes.Status200OK)]
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

        var response = mapper.Map<List<GetSaleByIdResponse>>(result);

        return Ok(response);
    }
}
