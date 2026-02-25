namespace DataLayer.Repositories
{
    using System.Collections.Generic;
    using System.Linq;
    using DBModel;
    using DBModel.Entities;

    public class ItemRepository : IItemRepository
    {
        private readonly AppDbContext _context;

        public ItemRepository(AppDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Item> GetAll()
        {
            return _context.Items.OrderBy(i => i.Name).ToList();
        }
    }
}
