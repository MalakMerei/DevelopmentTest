namespace BusinessLayer.Services
{
    using System.Collections.Generic;
    using BusinessLayer.DTOs;

    public interface ILookupService
    {
        IEnumerable<CustomerListDto> GetAllCustomers();
        IEnumerable<ItemListDto> GetAllItems();
        IEnumerable<SalesOrderListDto> GetAllSalesOrders();
        IEnumerable<InvoiceListDto> GetPostedInvoices();
    }
}
