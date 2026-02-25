namespace DBModel.Entities
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Invoice")]
    public partial class Invoice
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Invoice()
        {
            InvoiceLines = new HashSet<InvoiceLine>();
        }

        public int Id { get; set; }

        public int SalesOrderId { get; set; }

        public int CustomerId { get; set; }

        [Column(TypeName = "date")]
        public System.DateTime Date { get; set; }

        [Column(TypeName = "decimal")]
        public decimal NetTotal { get; set; }

        [Column(TypeName = "decimal")]
        public decimal TaxTotal { get; set; }

        [Column(TypeName = "decimal")]
        public decimal GrossTotal { get; set; }

        [Required]
        [StringLength(50)]
        public string Status { get; set; }

        public virtual SalesOrder SalesOrder { get; set; }

        public virtual Customer Customer { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<InvoiceLine> InvoiceLines { get; set; }
    }
}
