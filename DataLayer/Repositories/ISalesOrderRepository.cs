namespace DataLayer.Repositories
{
    using System.Collections.Generic;
    using DBModel.Entities;

    public interface ISalesOrderRepository
    {
        SalesOrder GetById(int id);
        SalesOrder GetByIdWithLines(int id);
        IEnumerable<SalesOrder> GetAll();
        void Add(SalesOrder entity);
        void AddLine(SalesOrderLine line);
    }
}
