namespace BusinessLayer.Services
{
    using System.Linq;
    using BusinessLayer.DTOs;
    using DataLayer.Repositories;
    using DBModel.Entities;

    public class InvoiceService : IInvoiceService
    {
        private readonly IInvoiceRepository _invoices;

        public InvoiceService(IInvoiceRepository invoices)
        {
            _invoices = invoices;
        }

        public InvoiceDto GetById(int id)
        {
            var entity = _invoices.GetById(id);
            return entity == null ? null : MapToDto(entity);
        }

        public InvoiceDto GetBySalesOrderId(int salesOrderId)
        {
            var entity = _invoices.GetBySalesOrderId(salesOrderId);
            return entity == null ? null : MapToDto(entity);
        }

        public System.Collections.Generic.IEnumerable<InvoiceDto> GetOpenInvoices()
        {
            foreach (var entity in _invoices.GetOpenInvoices())
                yield return MapToDto(entity);
        }

        public System.Collections.Generic.IEnumerable<InvoiceListDto> GetPostedInvoices()
        {
            foreach (var entity in _invoices.GetPostedInvoices())
                yield return new InvoiceListDto
                {
                    Id = entity.Id,
                    CustomerName = entity.Customer?.Name,
                    Date = entity.Date,
                    GrossTotal = entity.GrossTotal,
                    Status = entity.Status
                };
        }

        public System.Collections.Generic.IEnumerable<InvoiceListDto> GetAllInvoices()
        {
            foreach (var entity in _invoices.GetAll())
                yield return new InvoiceListDto
                {
                    Id = entity.Id,
                    CustomerName = entity.Customer?.Name,
                    Date = entity.Date,
                    GrossTotal = entity.GrossTotal,
                    Status = entity.Status
                };
        }

        public void PostInvoice(int invoiceId)
        {
            _invoices.ExecutePostInvoice(invoiceId);
        }

        private static InvoiceDto MapToDto(Invoice entity)
        {
            var dto = new InvoiceDto
            {
                Id = entity.Id,
                SalesOrderId = entity.SalesOrderId,
                CustomerId = entity.CustomerId,
                CustomerName = entity.Customer?.Name,
                Date = entity.Date,
                NetTotal = entity.NetTotal,
                TaxTotal = entity.TaxTotal,
                GrossTotal = entity.GrossTotal,
                Status = entity.Status
            };
            if (entity.InvoiceLines != null)
                dto.Lines = entity.InvoiceLines.Select(l => new InvoiceLineDto
                {
                    Id = l.Id,
                    ItemId = l.ItemId,
                    ItemName = l.Item?.Name,
                    Qty = l.Qty,
                    UnitPrice = l.UnitPrice,
                    LineTotal = l.LineTotal,
                    TaxAmount = l.TaxAmount
                }).ToList();
            return dto;
        }
    }
}
