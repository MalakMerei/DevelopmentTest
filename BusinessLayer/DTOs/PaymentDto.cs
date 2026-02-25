namespace BusinessLayer.DTOs
{
    public class PaymentDto
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public string CustomerName { get; set; }
        public System.DateTime Date { get; set; }
        public decimal Amount { get; set; }
    }
}
