using Ambev.DeveloperEvaluation.Domain.Events;
using Rebus.Handlers;

namespace Ambev.DeveloperEvaluation.Application.EventsHandlers
{
    public class SaleModifiedEventHandler : IHandleMessages<SaleModifiedEvent>
    {
        public async Task Handle(SaleModifiedEvent message)
        {
            Console.WriteLine($"Sale Modified: {message.SaleId}");
        }
    }
}
