namespace Ambev.DeveloperEvaluation.Domain.Events
{
    public class SaleCreatedEvent
    {
        public Guid SaleId { get; set; }
        public DateTime DateSold { get; set; }
        public Guid ClientId { get; set; }
        public decimal TotalAmount { get; set; }
    }
}