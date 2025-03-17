using Ambev.DeveloperEvaluation.Domain.Enums;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.ChangeSaleStatus;

public class ChangeSaleStatusQuery : IRequest<bool>
{
    public Guid Id { get; set; }
    public SaleStatus NewStatus { get; set; }
}
