namespace DataLayer.Repositories
{
    using System.Collections.Generic;
    using DBModel.Entities;

    public interface ICustomerRepository
    {
        IEnumerable<Customer> GetAll();
    }
}
