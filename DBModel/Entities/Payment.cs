namespace DBModel.Entities
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Payment")]
    public partial class Payment
    {
        public int Id { get; set; }

        public int CustomerId { get; set; }

        [Column(TypeName = "date")]
        public System.DateTime Date { get; set; }

        [Column(TypeName = "decimal")]
        public decimal Amount { get; set; }

        public virtual Customer Customer { get; set; }
    }
}
