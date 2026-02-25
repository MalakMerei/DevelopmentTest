using System;
using System.Web.UI;
using DevelopmentTest.App_Start;

namespace DevelopmentTest
{
    public partial class InvoiceDetail : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack) return;
            if (!int.TryParse(Request.QueryString["id"], out int id) || id <= 0)
            {
                Response.Redirect("AllInvoices.aspx");
                return;
            }
            var inv = ServiceLocator.InvoiceService.GetById(id);
            if (inv == null)
            {
                Response.Redirect("AllInvoices.aspx");
                return;
            }
            LiteralId.Text = inv.Id.ToString();
            LiteralCustomer.Text = inv.CustomerName ?? "";
            LiteralDate.Text = inv.Date.ToString("d");
            LiteralStatus.Text = inv.Status ?? "";
            LiteralNet.Text = inv.NetTotal.ToString("C");
            LiteralTax.Text = inv.TaxTotal.ToString("C");
            LiteralGross.Text = inv.GrossTotal.ToString("C");
            if (inv.Lines != null && inv.Lines.Count > 0)
            {
                GridViewLines.Visible = true;
                PanelEmptyLines.Visible = false;
                GridViewLines.DataSource = inv.Lines;
                GridViewLines.DataBind();
            }
            else
            {
                GridViewLines.Visible = false;
                PanelEmptyLines.Visible = true;
            }
        }
    }
}
