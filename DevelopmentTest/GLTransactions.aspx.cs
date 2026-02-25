using System;
using System.Linq;
using System.Web.UI;
using DevelopmentTest.App_Start;

namespace DevelopmentTest
{
    public partial class GLTransactions : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack) return;
            var list = ServiceLocator.GLService.GetAll().ToList();
            if (list.Count == 0)
            {
                GridViewGL.Visible = false;
                PanelEmpty.Visible = true;
                return;
            }
            PanelEmpty.Visible = false;
            GridViewGL.Visible = true;
            GridViewGL.DataSource = list;
            GridViewGL.DataBind();
        }
    }
}

