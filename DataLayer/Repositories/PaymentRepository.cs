namespace DataLayer.Repositories
{
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using DBModel;
    using DBModel.Entities;

    public class PaymentRepository : IPaymentRepository
    {
        private readonly AppDbContext _context;

        public PaymentRepository(AppDbContext context)
        {
            _context = context;
        }

        public Payment GetById(int id)
        {
            return _context.Payments
            .Include("Customer")
            .FirstOrDefault(p => p.Id == id);

        }

        public System.Collections.Generic.IEnumerable<Payment> GetAll()
        {
            return _context.Payments.Include("Customer").OrderBy(p => p.Date).ToList();
        }

        public void Add(Payment entity)
        {
            _context.Payments.Add(entity);
        }
    }
}
