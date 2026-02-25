using System;
using System.Linq;
using System.Web.UI;
using DevelopmentTest.App_Start;

namespace DevelopmentTest
{
    public partial class SalesOrders : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack) return;
            var list = ServiceLocator.SalesOrderService.GetAll().ToList();
            if (list.Count == 0)
            {
                GridViewOrders.Visible = false;
                PanelEmpty.Visible = true;
                return;
            }
            PanelEmpty.Visible = false;
            GridViewOrders.Visible = true;
            GridViewOrders.DataSource = list;
            GridViewOrders.DataBind();
        }
    }
}
