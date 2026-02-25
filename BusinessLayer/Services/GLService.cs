namespace BusinessLayer.Services
{
    using System.Collections.Generic;
    using System.Linq;
    using BusinessLayer.DTOs;
    using DataLayer.Repositories;

    public class GLService : IGLService
    {
        private readonly IGLTransactionRepository _transactions;

        public GLService(IGLTransactionRepository transactions)
        {
            _transactions = transactions;
        }

        public IEnumerable<GLTransactionListDto> GetAll()
        {
            foreach (var t in _transactions.GetAll())
                yield return new GLTransactionListDto
                {
                    Id = t.Id,
                    Date = t.Date
                };
        }

        public GLTransactionDto GetById(int id)
        {
            var t = _transactions.GetByIdWithLines(id);
            if (t == null) return null;
            return new GLTransactionDto
            {
                Id = t.Id,
                Date = t.Date,
                Lines = t.GLTransactionLines == null
                    ? new List<GLTransactionLineDto>()
                    : t.GLTransactionLines.Select(l => new GLTransactionLineDto
                    {
                        Account = l.Account,
                        Debit = l.Debit,
                        Credit = l.Credit
                    }).ToList()
            };
        }
    }
}

