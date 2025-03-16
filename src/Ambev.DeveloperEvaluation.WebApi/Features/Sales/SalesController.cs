using Ambev.DeveloperEvaluation.WebApi.Features.Sales.CreateSale;
using Ambev.DeveloperEvaluation.Application.Sales.CreateSale;
using Ambev.DeveloperEvaluation.WebApi.Common;
using Microsoft.AspNetCore.Mvc;
using MediatR;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales;

[ApiController]
[Route("api/[controller]")]
public class SalesController(IMediator mediator) : BaseController
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

        var createSaleResponse = new CreateSaleResponse()
        {
            BranchId = result.BranchId,
            CustomerId = result.CustomerId,
            DateSold = result.DateSold,
            Id = result.Id,
            Number = result.Number,
            TotalAmount = result.TotalAmount,
            Products = result.Products.Select(x => new CreateSaleProductResponse
            {
                ProductId = x.ProductId,
                Quantity = x.Quantity,
                UnitPrice = x.UnitPrice,
            }).ToList(),
        };

        return Ok(createSaleResponse);
    }
}