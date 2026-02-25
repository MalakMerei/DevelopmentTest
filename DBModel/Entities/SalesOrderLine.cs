namespace DBModel.Entities
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("SalesOrderLine")]
    public partial class SalesOrderLine
    {
        public int SalesOrderId { get; set; }

        public int ItemId { get; set; }

        public int Qty { get; set; }

        [Column(TypeName = "decimal")]
        public decimal UnitPrice { get; set; }

        public virtual SalesOrder SalesOrder { get; set; }

        public virtual Item Item { get; set; }
    }
}
