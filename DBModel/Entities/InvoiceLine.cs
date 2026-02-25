namespace DBModel.Entities
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("InvoiceLine")]
    public partial class InvoiceLine
    {
        public int Id { get; set; }

        public int InvoiceId { get; set; }

        public int ItemId { get; set; }

        public int Qty { get; set; }

        [Column(TypeName = "decimal")]
        public decimal UnitPrice { get; set; }

        [Column(TypeName = "decimal")]
        public decimal LineTotal { get; set; }

        [Column(TypeName = "decimal")]
        public decimal TaxAmount { get; set; }

        public virtual Invoice Invoice { get; set; }

        public virtual Item Item { get; set; }
    }
}
