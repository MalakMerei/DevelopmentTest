using System;
using System.Web.UI;
using DevelopmentTest.App_Start;

namespace DevelopmentTest
{
    public partial class GLTransactionDetail : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack) return;
            if (!int.TryParse(Request.QueryString["id"], out int id) || id <= 0)
            {
                Response.Redirect("GLTransactions.aspx");
                return;
            }
            var trx = ServiceLocator.GLService.GetById(id);
            if (trx == null)
            {
                Response.Redirect("GLTransactions.aspx");
                return;
            }
            LiteralId.Text = trx.Id.ToString();
            LiteralDate.Text = trx.Date.ToString("d");

            if (trx.Lines != null && trx.Lines.Count > 0)
            {
                GridViewLines.Visible = true;
                PanelEmptyLines.Visible = false;
                GridViewLines.DataSource = trx.Lines;
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
