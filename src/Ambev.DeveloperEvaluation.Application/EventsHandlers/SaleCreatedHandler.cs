using Ambev.DeveloperEvaluation.Domain.Events;
using Rebus.Handlers;

namespace Ambev.DeveloperEvaluation.Application.EventsHandlers
{
    public class SaleCreatedEventHandler : IHandleMessages<SaleCreatedEvent>
    {
        public async Task Handle(SaleCreatedEvent message)
        {
            Console.WriteLine($"Sale Created: {message.SaleId}, Total Amount: {message.TotalAmount}");
            // Lógica para processar o evento de venda criada
        }
    }
}
