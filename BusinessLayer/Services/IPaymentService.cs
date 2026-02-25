namespace BusinessLayer.Services
{
    using System.Collections.Generic;
    using BusinessLayer.DTOs;

    public interface IPaymentService
    {
        PaymentDto GetById(int id);
        IEnumerable<PaymentDto> GetAll();
        int CreatePayment(PaymentCreateDto dto);
        void ApplyToInvoice(int paymentId, int invoiceId, decimal amountApplied);
        void ReceivePaymentAgainstInvoice(int invoiceId, decimal amount, System.DateTime paymentDate);
    }
}
