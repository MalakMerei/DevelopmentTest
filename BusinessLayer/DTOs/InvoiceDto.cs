namespace BusinessLayer.DTOs
{
    using System.Collections.Generic;

    public class InvoiceDto
    {
        public int Id { get; set; }
        public int SalesOrderId { get; set; }
        public int CustomerId { get; set; }
        public string CustomerName { get; set; }
        public System.DateTime Date { get; set; }
        public decimal NetTotal { get; set; }
        public decimal TaxTotal { get; set; }
        public decimal GrossTotal { get; set; }
        public string Status { get; set; }
        public List<InvoiceLineDto> Lines { get; set; }
    }

    public class InvoiceLineDto
    {
        public int Id { get; set; }
        public int ItemId { get; set; }
        public string ItemName { get; set; }
        public int Qty { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal LineTotal { get; set; }
        public decimal TaxAmount { get; set; }
    }
}
