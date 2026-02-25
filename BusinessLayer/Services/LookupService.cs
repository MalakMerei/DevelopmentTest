namespace BusinessLayer.Services
{
    using System.Collections.Generic;
    using System.Linq;
    using BusinessLayer.DTOs;
    using DataLayer.Repositories;

    public class LookupService : ILookupService
    {
        private readonly ICustomerRepository _customers;
        private readonly IItemRepository _items;
        private readonly ISalesOrderRepository _salesOrders;
        private readonly IInvoiceRepository _invoices;

        public LookupService(
            ICustomerRepository customers,
            IItemRepository items,
            ISalesOrderRepository salesOrders,
            IInvoiceRepository invoices)
        {
            _customers = customers;
            _items = items;
            _salesOrders = salesOrders;
            _invoices = invoices;
        }

        public IEnumerable<CustomerListDto> GetAllCustomers()
        {
            return _customers.GetAll() .Select(c => new CustomerListDto {
                Id = c.Id,
                Name = c.Name
            });
        }

        public IEnumerable<ItemListDto> GetAllItems()
        {
            return _items.GetAll().Select(i => new ItemListDto
            {
                Id = i.Id,
                Name = i.Name,
                UnitPrice = i.UnitPrice
            });
        }

        public IEnumerable<SalesOrderListDto> GetAllSalesOrders()
        {
            return _salesOrders.GetAll().Select(s => new SalesOrderListDto
            {
                Id = s.Id,
                CustomerName = s.Customer?.Name,
                Date = s.Date,
                Status = s.Status
            });
        }

        public IEnumerable<InvoiceListDto> GetPostedInvoices()
        {
            return _invoices.GetPostedInvoices().Select(i => new InvoiceListDto
            {
                Id = i.Id,
                CustomerName = i.Customer?.Name,
                Date = i.Date,
                GrossTotal = i.GrossTotal,
                Status = i.Status
            });
        }
    }
}
