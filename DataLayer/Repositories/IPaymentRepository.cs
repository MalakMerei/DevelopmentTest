namespace DataLayer.Repositories
{
    using System.Collections.Generic;
    using DBModel.Entities;

    public interface IPaymentRepository
    {
        Payment GetById(int id);
        IEnumerable<Payment> GetAll();
        void Add(Payment entity);
    }
}
