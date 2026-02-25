namespace BusinessLayer.DTOs
{
    using System.Collections.Generic;

    public class SalesOrderCreateDto
    {
        public int CustomerId { get; set; }
        public System.DateTime Date { get; set; }
    }

    public class SalesOrderLineDto
    {
        public int ItemId { get; set; }
        public string ItemName { get; set; }
        public int Qty { get; set; }
        public decimal UnitPrice { get; set; }
    }

    public class SalesOrderDto
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public string CustomerName { get; set; }
        public System.DateTime Date { get; set; }
        public string Status { get; set; }
        public List<SalesOrderLineDto> Lines { get; set; }
    }

    public class ReleaseToInvoiceResultDto
    {
        public int InvoiceId { get; set; }
        public decimal NetTotal { get; set; }
        public decimal TaxTotal { get; set; }
        public decimal GrossTotal { get; set; }
    }
}
