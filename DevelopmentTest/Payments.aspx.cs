using System;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevelopmentTest.App_Start;

namespace DevelopmentTest
{
    public partial class Payments : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindPayments();
                BindInvoiceDropdown();
                if (string.IsNullOrEmpty(TextBoxPaymentDate.Text))
                    TextBoxPaymentDate.Text = DateTime.Today.ToString("yyyy-MM-dd");
            }
        }

        private void BindPayments()
        {
            var list = ServiceLocator.PaymentService.GetAll().ToList();
            if (list.Count == 0)
            {
                GridViewPayments.Visible = false;
                PanelEmpty.Visible = true;
                return;
            }
            PanelEmpty.Visible = false;
            GridViewPayments.Visible = true;
            GridViewPayments.DataSource = list;
            GridViewPayments.DataBind();
        }

        private void BindInvoiceDropdown()
        {
            var invoices = ServiceLocator.InvoiceService.GetPostedInvoices().ToList();
            DropDownInvoice.Items.Clear();
            DropDownInvoice.Items.Add(new ListItem("-- Select invoice to pay --", ""));
            foreach (var inv in invoices)
                DropDownInvoice.Items.Add(new ListItem(
                    string.Format("Invoice #{0} - {1} - {2:C} ({3})", inv.Id, inv.CustomerName ?? "", inv.GrossTotal, inv.Status ?? ""),
                    inv.Id.ToString()));
        }

        protected void ButtonReceivePayment_Click(object sender, EventArgs e)
        {
            LabelError.Visible = false;
            if (!int.TryParse(DropDownInvoice.SelectedValue, out int invoiceId) || invoiceId <= 0)
            {
                LabelError.Text = "Select an invoice.";
                LabelError.Visible = true;
                return;
            }
            if (!decimal.TryParse(TextBoxAmount.Text, out decimal amount) || amount <= 0)
            {
                LabelError.Text = "Amount must be greater than 0.";
                LabelError.Visible = true;
                return;
            }
            if (!DateTime.TryParse(TextBoxPaymentDate.Text, out DateTime paymentDate))
            {
                LabelError.Text = "Invalid date.";
                LabelError.Visible = true;
                return;
            }
            try
            {
                ServiceLocator.PaymentService.ReceivePaymentAgainstInvoice(invoiceId, amount, paymentDate);
                BindPayments();
                BindInvoiceDropdown();
                TextBoxAmount.Text = "";
            }
            catch (Exception ex)
            {
                LabelError.Text = ex.Message;
                LabelError.Visible = true;
            }
        }
    }
}
