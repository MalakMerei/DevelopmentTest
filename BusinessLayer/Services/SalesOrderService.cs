namespace BusinessLayer.Services
{
    using System;
    using System.Linq;
    using BusinessLayer.DTOs;
    using DataLayer.Repositories;
    using DBModel;
    using DBModel.Entities;

    public class SalesOrderService : ISalesOrderService
    {
        private const decimal rate = 0.11m;
        private readonly AppDbContext _context;
        private readonly ISalesOrderRepository _salesOrders;
        private readonly IInvoiceRepository _invoices;

        public SalesOrderService(
            AppDbContext context,
            ISalesOrderRepository salesOrders,
            IInvoiceRepository invoices)
        {
            _context = context;
            _salesOrders = salesOrders;
            _invoices = invoices;
        }

        public SalesOrderDto GetById(int id)
        {
            var so = _salesOrders.GetByIdWithLines(id);
            return so == null ? null : MapToDto(so);
        }

        public System.Collections.Generic.IEnumerable<SalesOrderListDto> GetAll()
        {
            foreach (var s in _salesOrders.GetAll())
                yield return new SalesOrderListDto
                {
                    Id = s.Id,
                    CustomerName = s.Customer?.Name,
                    Date = s.Date,
                    Status = s.Status
                };
        }

        public int CreateSalesOrder(SalesOrderCreateDto dto)
        {
            var so = new SalesOrder
            {
                CustomerId = dto.CustomerId,
                Date = dto.Date.Date,
                Status = "Open"
            };
            _salesOrders.Add(so);
            _context.SaveChanges();
            return so.Id;
        }

        public void AddLine(int salesOrderId, SalesOrderLineDto dto)
        {
            if (dto.Qty <= 0)
                throw new Exception("Qty must be greater than 0.");

            var so = _salesOrders.GetById(salesOrderId);
            if (so == null)
                throw new Exception("Sales order not found.");
            if (so.Status != "Open")
                throw new Exception("Can only add lines to an Open sales order.");
            
            var line = new SalesOrderLine
            {
                SalesOrderId = salesOrderId,
                ItemId = dto.ItemId,
                Qty = dto.Qty,
                UnitPrice = dto.UnitPrice
            };
            _salesOrders.AddLine(line);
            _context.SaveChanges();
        }

        public ReleaseToInvoiceResultDto ReleaseToInvoice(int salesOrderId)
        {
            var so = _salesOrders.GetByIdWithLines(salesOrderId);
            if (so == null)
                throw new Exception("Sales order not found.");

            if (so.Status != "Open")
                throw new Exception("Only Open sales orders can be released to invoice.");

            if (so.SalesOrderLines == null || !so.SalesOrderLines.Any())
                throw new Exception("Sales order has no lines.");

            decimal netTotal = 0;
            foreach (var sol in so.SalesOrderLines)
            {
                if (sol.Qty <= 0)
                    throw new InvalidOperationException("All line quantities must be greater than 0.");
                netTotal += sol.Qty * sol.UnitPrice;
            }
            decimal taxTotal = netTotal * rate;
            decimal grossTotal = netTotal + taxTotal;

            var inv = new Invoice
            {
                SalesOrderId = so.Id,
                CustomerId = so.CustomerId,
                Date = DateTime.Today,
                NetTotal = netTotal,
                TaxTotal = taxTotal,
                GrossTotal = grossTotal,
                Status = "Open"
            };
            _invoices.Add(inv);
            _context.SaveChanges();

            foreach (var sol in so.SalesOrderLines)
            {
                decimal lineTotal = sol.Qty * sol.UnitPrice;
                decimal taxAmount = lineTotal * rate;
                var il = new InvoiceLine
                {
                    InvoiceId = inv.Id,
                    ItemId = sol.ItemId,
                    Qty = sol.Qty,
                    UnitPrice = sol.UnitPrice,
                    LineTotal = lineTotal,
                    TaxAmount = taxAmount
                };
                _invoices.AddLine(il);
            }
            so.Status = "Invoiced"; 
            _context.SaveChanges();

            return new ReleaseToInvoiceResultDto
            {
                InvoiceId = inv.Id,
                NetTotal = netTotal,
                TaxTotal = taxTotal,
                GrossTotal = grossTotal
            };
        }

        private static SalesOrderDto MapToDto(SalesOrder so)
        {
            var dto = new SalesOrderDto
            {
                Id = so.Id,
                CustomerId = so.CustomerId,
                CustomerName = so.Customer?.Name,
                Date = so.Date,
                Status = so.Status
            };
            if (so.SalesOrderLines != null)
                dto.Lines = so.SalesOrderLines.Select(l => new SalesOrderLineDto
                {
                    ItemId = l.ItemId,
                    ItemName = l.Item?.Name,
                    Qty = l.Qty,
                    UnitPrice = l.UnitPrice
                }).ToList();
            return dto;
        }
    }
}
