namespace BusinessLayer.DTOs
{
    public class CustomerListDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class ItemListDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal UnitPrice { get; set; }
    }

    public class SalesOrderListDto
    {
        public int Id { get; set; }
        public string CustomerName { get; set; }
        public System.DateTime Date { get; set; }
        public string Status { get; set; }
    }

    public class InvoiceListDto
    {
        public int Id { get; set; }
        public string CustomerName { get; set; }
        public System.DateTime Date { get; set; }
        public decimal GrossTotal { get; set; }
        public string Status { get; set; }
    }
}
