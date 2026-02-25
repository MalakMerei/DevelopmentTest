namespace DBModel.Entities
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("GLTransactionLine")]
    public partial class GLTransactionLine
    {
        public int Id { get; set; }

        public int GLTransactionId { get; set; }

        [Required]
        [StringLength(100)]
        public string Account { get; set; }

        [Column(TypeName = "decimal")]
        public decimal Debit { get; set; }

        [Column(TypeName = "decimal")]
        public decimal Credit { get; set; }

        public virtual GLTransaction GLTransaction { get; set; }
    }
}
