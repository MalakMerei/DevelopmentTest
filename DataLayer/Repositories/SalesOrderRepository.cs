namespace DataLayer.Repositories
{
    using System.Data.Entity;
    using System.Linq;
    using DBModel;
    using DBModel.Entities;

    public class SalesOrderRepository : ISalesOrderRepository
    {
        private readonly AppDbContext _context;

        public SalesOrderRepository(AppDbContext context)
        {
            _context = context;
        }

        public SalesOrder GetById(int id) { 
            return _context.SalesOrders.Find(id); 
        }

        public SalesOrder GetByIdWithLines(int id)
        {
            return _context.SalesOrders
            .Include("SalesOrderLines")
            .Include("SalesOrderLines.Item")
            .Include(s => s.Customer)
            .SingleOrDefault(s => s.Id == id);
        }

        public System.Collections.Generic.IEnumerable<SalesOrder> GetAll()
        {
            return _context.SalesOrders
                .Include("Customer")
                .OrderByDescending(s => s.Date)
                .ThenByDescending(s => s.Id)
                .ToList();
        }

        public void Add(SalesOrder entity) { _context.SalesOrders.Add(entity); }

        public void AddLine(SalesOrderLine line) { _context.SalesOrderLines.Add(line); }
    }
}
