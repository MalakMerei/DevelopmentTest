namespace BusinessLayer.Services
{
    using BusinessLayer.DTOs;

    public interface ISalesOrderService
    {
        SalesOrderDto GetById(int id);
        System.Collections.Generic.IEnumerable<SalesOrderListDto> GetAll();
        int CreateSalesOrder(SalesOrderCreateDto dto);
        void AddLine(int salesOrderId, SalesOrderLineDto dto);
        ReleaseToInvoiceResultDto ReleaseToInvoice(int salesOrderId);
    }
}
