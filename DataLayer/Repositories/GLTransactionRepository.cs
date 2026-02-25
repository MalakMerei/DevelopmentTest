namespace DataLayer.Repositories
{
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using DBModel;
    using DBModel.Entities;

    public class GLTransactionRepository : IGLTransactionRepository
    {
        private readonly AppDbContext _context;

        public GLTransactionRepository(AppDbContext context)
        {
            _context = context;
        }

        public IEnumerable<GLTransaction> GetAll()
        {
            return _context.GLTransactions
                .OrderByDescending(t => t.Date)
                .ThenByDescending(t => t.Id)
                .ToList();
        }

        public GLTransaction GetByIdWithLines(int id)
        {
            return _context.GLTransactions
                .Include("GLTransactionLines")
                .SingleOrDefault(t => t.Id == id);
        }
    }
}

