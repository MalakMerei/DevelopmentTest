namespace DBModel
{
    using System.Data.Entity;
    using DBModel.Entities;

    public partial class AppDbContext : DbContext
    {
        static AppDbContext()
        {
            Database.SetInitializer<AppDbContext>(null);
        }

        public AppDbContext()
            : base("name=AppDbContext")
        {
        }

        public AppDbContext(string nameOrConnectionString)
            : base(nameOrConnectionString)
        {
        }

        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<Item> Items { get; set; }
        public virtual DbSet<SalesOrder> SalesOrders { get; set; }
        public virtual DbSet<SalesOrderLine> SalesOrderLines { get; set; }
        public virtual DbSet<Invoice> Invoices { get; set; }
        public virtual DbSet<InvoiceLine> InvoiceLines { get; set; }
        public virtual DbSet<Payment> Payments { get; set; }
        public virtual DbSet<PaymentApplication> PaymentApplications { get; set; }
        public virtual DbSet<GLTransaction> GLTransactions { get; set; }
        public virtual DbSet<GLTransactionLine> GLTransactionLines { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<SalesOrderLine>()
                .HasKey(e => new { e.SalesOrderId, e.ItemId });
            modelBuilder.Entity<Item>()
                .Property(e => e.UnitPrice)
                .HasPrecision(18, 2);
            modelBuilder.Entity<Invoice>()
                .Property(e => e.NetTotal)
                .HasPrecision(18, 2);
            modelBuilder.Entity<Invoice>()
                .Property(e => e.TaxTotal)
                .HasPrecision(18, 2);
            modelBuilder.Entity<Invoice>()
                .Property(e => e.GrossTotal)
                .HasPrecision(18, 2);
            modelBuilder.Entity<InvoiceLine>()
                .Property(e => e.UnitPrice)
                .HasPrecision(18, 2);
            modelBuilder.Entity<InvoiceLine>()
                .Property(e => e.LineTotal)
                .HasPrecision(18, 2);
            modelBuilder.Entity<InvoiceLine>()
                .Property(e => e.TaxAmount)
                .HasPrecision(18, 2);
            modelBuilder.Entity<Payment>()
                .Property(e => e.Amount)
                .HasPrecision(18, 2);
            modelBuilder.Entity<PaymentApplication>()
                .Property(e => e.AmountApplied)
                .HasPrecision(18, 2);
            modelBuilder.Entity<GLTransactionLine>()
                .Property(e => e.Debit)
                .HasPrecision(18, 2);
            modelBuilder.Entity<GLTransactionLine>()
                .Property(e => e.Credit)
                .HasPrecision(18, 2);
        }
    }
}
