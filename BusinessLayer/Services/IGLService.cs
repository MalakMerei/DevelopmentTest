namespace BusinessLayer.Services
{
    using System.Collections.Generic;
    using BusinessLayer.DTOs;

    public interface IGLService
    {
        IEnumerable<GLTransactionListDto> GetAll();
        GLTransactionDto GetById(int id);
    }
}

