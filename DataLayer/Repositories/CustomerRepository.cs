namespace DataLayer.Repositories
{
    using System.Collections.Generic;
    using System.Linq;
    using DBModel;
    using DBModel.Entities;

    public class CustomerRepository : ICustomerRepository
    {
        private readonly AppDbContext _context;

        public CustomerRepository(AppDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Customer> GetAll()
        {
            return _context.Customers.OrderBy(c => c.Name).ToList();
        }
    }
}
