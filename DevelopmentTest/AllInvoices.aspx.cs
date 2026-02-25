using System;
using System.Linq;
using System.Web.UI;
using DevelopmentTest.App_Start;

namespace DevelopmentTest
{
    public partial class AllInvoices : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack) return;
            var list = ServiceLocator.InvoiceService.GetAllInvoices().ToList();
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
    }
}
