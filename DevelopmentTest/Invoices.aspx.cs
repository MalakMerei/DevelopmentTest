using System;
using System.Linq;
using System.Web.UI;
using DevelopmentTest.App_Start;

namespace DevelopmentTest
{
    public partial class Invoices : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack) return;
            BindInvoices();
        }

        private void BindInvoices()
        {
            var list = ServiceLocator.InvoiceService.GetOpenInvoices().ToList();
            if (list.Count == 0)
            {
                GridViewInvoices.Visible = false;
                PanelEmpty.Visible = true;
                return;
            }
            PanelEmpty.Visible = false;
            GridViewInvoices.Visible = true;
            GridViewInvoices.DataSource = list;
            GridViewInvoices.DataBind();
        }

        protected void BtnPost_Click(object sender, EventArgs e)
        {
            var btn = (System.Web.UI.WebControls.Button)sender;
            if (int.TryParse(btn.CommandArgument, out int id))
            {
                try
                {
                    ServiceLocator.InvoiceService.PostInvoice(id);
                    BindInvoices();
                }
                catch (Exception)
                {
                }
            }
        }
    }
}
