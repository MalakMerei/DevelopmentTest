namespace DataLayer.Repositories
{
    using System.Data.Entity;
    using System.Linq;
    using DBModel;
    using DBModel.Entities;

    public class InvoiceRepository : IInvoiceRepository
    {
        private readonly AppDbContext _context;

        public InvoiceRepository(AppDbContext context)
        {
            _context = context;
        }

        public Invoice GetById(int id)
        {
            return _context.Invoices
            .Include(i => i.Customer)
            .Include("InvoiceLines.Item")
            .FirstOrDefault(i => i.Id == id);
        }

        public Invoice GetBySalesOrderId(int salesOrderId)
        {
            return _context.Invoices
            .Include(i => i.Customer)
            .FirstOrDefault(i => i.SalesOrderId == salesOrderId);
        }

        public System.Collections.Generic.IEnumerable<Invoice> GetAll()
        {
            return _context.Invoices
                .Include(i => i.Customer)
                .OrderByDescending(i => i.Date)
                .ThenByDescending(i => i.Id)
                .ToList();
        }

        public System.Collections.Generic.IEnumerable<Invoice> GetOpenInvoices()
        {
            return _context.Invoices
                .Include(i => i.Customer)
                .Where(i => i.Status == "Open")
                .OrderBy(i => i.Customer.Name)
                .ThenBy(i => i.Date)
                .ToList();
        }

        public System.Collections.Generic.IEnumerable<Invoice> GetPostedInvoices()
        {
            return _context.Invoices
                .Include(i => i.Customer)
                .Where(i => i.Status == "Posted" || i.Status == "PartiallyPaid")
                .OrderBy(i => i.Date)
                .ToList();
        }

        public void Add(Invoice entity)
        {
            _context.Invoices.Add(entity);
        }

        public void AddLine(InvoiceLine line){ 
            _context.InvoiceLines.Add(line);
        }

        public void ExecutePostInvoice(int invoiceId)
        {
            _context.Database.ExecuteSqlCommand("EXEC dbo.sp_PostInvoice @p0", invoiceId);
        }

        public void ExecuteApplyPayment(int paymentId, int invoiceId, decimal amountApplied)
        {
            _context.Database.ExecuteSqlCommand(
                "EXEC dbo.sp_ApplyPayment @p0, @p1, @p2",
                paymentId,
                invoiceId,
                amountApplied);
        }
    }
}
