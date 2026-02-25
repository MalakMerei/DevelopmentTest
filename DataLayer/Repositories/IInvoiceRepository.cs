namespace DataLayer.Repositories
{
    using System.Collections.Generic;
    using DBModel.Entities;

    public interface IInvoiceRepository
    {
        Invoice GetById(int id);
        Invoice GetBySalesOrderId(int salesOrderId);
        IEnumerable<Invoice> GetAll();
        IEnumerable<Invoice> GetOpenInvoices();
        IEnumerable<Invoice> GetPostedInvoices();
        void Add(Invoice entity);
        void AddLine(InvoiceLine line);
        void ExecutePostInvoice(int invoiceId);
        void ExecuteApplyPayment(int paymentId, int invoiceId, decimal amountApplied);
    }
}
