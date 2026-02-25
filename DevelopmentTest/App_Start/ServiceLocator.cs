using System.Configuration;
using BusinessLayer.Services;
using DataLayer.Repositories;
using DBModel;

namespace DevelopmentTest.App_Start
{
    public static class ServiceLocator
    {
        public static IPaymentService PaymentService
        {
            get
            {
                var context = CreateContext();
                var payments = new PaymentRepository(context);
                var invoices = new InvoiceRepository(context);
                return new PaymentService(context, payments, invoices);
            }
        }

        public static IInvoiceService InvoiceService
        {
            get
            {
                var context = CreateContext();
                var invoices = new InvoiceRepository(context);
                return new InvoiceService(invoices);
            }
        }

        public static ISalesOrderService SalesOrderService
        {
            get
            {
                var context = CreateContext();
                var salesOrders = new SalesOrderRepository(context);
                var invoices = new InvoiceRepository(context);
                return new SalesOrderService(context, salesOrders, invoices);
            }
        }

        public static ILookupService LookupService
        {
            get
            {
                var context = CreateContext();
                var customers = new CustomerRepository(context);
                var items = new ItemRepository(context);
                var salesOrders = new SalesOrderRepository(context);
                var invoices = new InvoiceRepository(context);
                return new LookupService(customers, items, salesOrders, invoices);
            }
        }

        public static IGLService GLService
        {
            get
            {
                var context = CreateContext();
                var glRepo = new GLTransactionRepository(context);
                return new GLService(glRepo);
            }
        }

        private static AppDbContext CreateContext()
        {
            var conn = ConfigurationManager.ConnectionStrings["AppDbContext"]?.ConnectionString;
            return string.IsNullOrEmpty(conn) ? new AppDbContext() : new AppDbContext(conn);
        }
    }
}
