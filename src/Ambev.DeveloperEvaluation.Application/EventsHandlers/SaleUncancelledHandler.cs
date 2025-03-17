using Ambev.DeveloperEvaluation.Domain.Events;
using Rebus.Handlers;

namespace Ambev.DeveloperEvaluation.Application.EventsHandlers
{
    public class SaleUncancelledEventHandler : IHandleMessages<SaleUncancelledEvent>
    {
        public async Task Handle(SaleUncancelledEvent message)
        {
            Console.WriteLine($"Sale Uncancelled: {message.Id}");
            // Lógica para processar o evento de venda não cancelada
        }
    }
}
