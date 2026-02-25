namespace DataLayer.Repositories
{
    using System.Collections.Generic;
    using DBModel.Entities;

    public interface IGLTransactionRepository
    {
        IEnumerable<GLTransaction> GetAll();
        GLTransaction GetByIdWithLines(int id);
    }
}
