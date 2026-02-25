namespace DBModel.Entities
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("PaymentApplication")]
    public partial class PaymentApplication
    {
        public int Id { get; set; }

        public int PaymentId { get; set; }

        public int InvoiceId { get; set; }

        [Column(TypeName = "decimal")]
        public decimal AmountApplied { get; set; }

        public virtual Payment Payment { get; set; }

        public virtual Invoice Invoice { get; set; }
    }
}
