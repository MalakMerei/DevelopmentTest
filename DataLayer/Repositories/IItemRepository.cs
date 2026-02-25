namespace DataLayer.Repositories
{
    using System.Collections.Generic;
    using DBModel.Entities;

    public interface IItemRepository
    {
        IEnumerable<Item> GetAll();
    }
}
