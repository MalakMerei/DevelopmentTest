namespace BusinessLayer.DTOs
{
    public class PaymentCreateDto
    {
        public int CustomerId { get; set; }
        public System.DateTime Date { get; set; }
        public decimal Amount { get; set; }
    }

    public class ApplyPaymentDto
    {
        public int InvoiceId { get; set; }
        public decimal AmountApplied { get; set; }
    }
}
