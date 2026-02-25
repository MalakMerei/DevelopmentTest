namespace BusinessLayer.Services
{
    using BusinessLayer.DTOs;
    using DataLayer.Repositories;
    using DBModel;
    using DBModel.Entities;

    public class PaymentService : IPaymentService
    {
        private readonly AppDbContext _context;
        private readonly IPaymentRepository _payments;
        private readonly IInvoiceRepository _invoices;

        public PaymentService(
            AppDbContext context,
            IPaymentRepository payments,
            IInvoiceRepository invoices)
        {
            _context = context;
            _payments = payments;
            _invoices = invoices;
        }

        public PaymentDto GetById(int id)
        {
            var entity = _payments.GetById(id);
            return entity == null ? null : MapToDto(entity);
        }

        public System.Collections.Generic.IEnumerable<PaymentDto> GetAll()
        {
            foreach (var entity in _payments.GetAll())
                yield return MapToDto(entity);
        }

        public int CreatePayment(PaymentCreateDto dto)
        {
            if (dto.Amount <= 0)
                throw new System.Exception("Amount must be greater than 0.");

            var p = new Payment
            {
                CustomerId = dto.CustomerId,
                Date = dto.Date.Date,
                Amount = dto.Amount
            };
            _payments.Add(p);
            _context.SaveChanges();
            return p.Id;
        }

        public void ApplyToInvoice(int paymentId, int invoiceId, decimal amountApplied)
        {
            _invoices.ExecuteApplyPayment(paymentId, invoiceId, amountApplied);
        }

        public void ReceivePaymentAgainstInvoice(int invoiceId, decimal amount, System.DateTime paymentDate)
        {
            if (amount <= 0)
                throw new System.Exception("Amount must be greater than 0.");

            var invoice = _invoices.GetById(invoiceId);
            if (invoice == null)
                throw new System.Exception("Invoice not found.");

            if (invoice.Status != "Posted" && invoice.Status != "PartiallyPaid")
                throw new System.Exception("Only Posted or PartiallyPaid invoices can be paid.");

            var p = new Payment
            {
                CustomerId = invoice.CustomerId,
                Date = paymentDate.Date,
                Amount = amount
            };
            _payments.Add(p);
            _context.SaveChanges();
            _invoices.ExecuteApplyPayment(p.Id, invoiceId, amount);
        }

        private static PaymentDto MapToDto(Payment entity)
        {
            return new PaymentDto
            {
                Id = entity.Id,
                CustomerId = entity.CustomerId,
                CustomerName = entity.Customer?.Name,
                Date = entity.Date,
                Amount = entity.Amount
            };
        }
    }
}
