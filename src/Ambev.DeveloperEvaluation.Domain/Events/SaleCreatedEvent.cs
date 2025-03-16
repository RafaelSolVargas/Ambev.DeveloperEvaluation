namespace Ambev.DeveloperEvaluation.Domain.Events
{
    public class SaleCreatedEvent
    {
        public Guid SaleId { get; }
        public DateTime DateSold { get; }
        public Guid ClientId { get; }
        public decimal TotalAmount { get; }

        public SaleCreatedEvent(Guid saleId, DateTime dateSold, Guid clientId, decimal totalAmount)
        {
            SaleId = saleId;
            DateSold = dateSold;
            ClientId = clientId;
            TotalAmount = totalAmount;
        }
    }
}