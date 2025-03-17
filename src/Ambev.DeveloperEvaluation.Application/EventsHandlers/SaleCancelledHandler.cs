using Ambev.DeveloperEvaluation.Domain.Events;
using Rebus.Handlers;

namespace Ambev.DeveloperEvaluation.Application.EventsHandlers
{
    public class SaleCancelledEventHandler : IHandleMessages<SaleCancelledEvent>
    {
        public async Task Handle(SaleCancelledEvent message)
        {
            Console.WriteLine($"Sale Cancelled: {message.Id}");
            // Lógica para processar o evento de venda cancelada
        }
    }
}
