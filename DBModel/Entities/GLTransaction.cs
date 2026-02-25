namespace DBModel.Entities
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("GLTransaction")]
    public partial class GLTransaction
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public GLTransaction()
        {
            GLTransactionLines = new HashSet<GLTransactionLine>();
        }

        public int Id { get; set; }

        [Column(TypeName = "date")]
        public System.DateTime Date { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<GLTransactionLine> GLTransactionLines { get; set; }
    }
}
