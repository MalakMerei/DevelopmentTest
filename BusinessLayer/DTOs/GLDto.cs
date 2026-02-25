namespace BusinessLayer.DTOs
{
    using System.Collections.Generic;

    public class GLTransactionListDto
    {
        public int Id { get; set; }
        public System.DateTime Date { get; set; }
    }

    public class GLTransactionLineDto
    {
        public string Account { get; set; }
        public decimal Debit { get; set; }
        public decimal Credit { get; set; }
    }

    public class GLTransactionDto
    {
        public int Id { get; set; }
        public System.DateTime Date { get; set; }
        public List<GLTransactionLineDto> Lines { get; set; }
    }
}

