namespace BusinessLayer.Services
{
    using System.Collections.Generic;
    using BusinessLayer.DTOs;

    public interface IInvoiceService
    {
        InvoiceDto GetById(int id);
        InvoiceDto GetBySalesOrderId(int salesOrderId);
        IEnumerable<InvoiceDto> GetOpenInvoices();
        IEnumerable<InvoiceListDto> GetPostedInvoices();
        IEnumerable<InvoiceListDto> GetAllInvoices();
        void PostInvoice(int invoiceId);
    }
}
