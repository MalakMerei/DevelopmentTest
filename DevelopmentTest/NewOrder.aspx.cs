using System;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessLayer.DTOs;
using DevelopmentTest.App_Start;

namespace DevelopmentTest
{
    public partial class NewOrder : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack) return;
            var customers = ServiceLocator.LookupService.GetAllCustomers().ToList();
            DropDownCustomer.DataSource = customers;
            DropDownCustomer.DataValueField = "Id";
            DropDownCustomer.DataTextField = "Name";
            DropDownCustomer.DataBind();
            DropDownCustomer.Items.Insert(0, new ListItem("-- Select customer --", ""));
            if (string.IsNullOrEmpty(TextBoxDate.Text))
                TextBoxDate.Text = DateTime.Today.ToString("yyyy-MM-dd");
        }

        protected void ButtonCreate_Click(object sender, EventArgs e)
        {
            LabelError.Visible = false;
            if (!int.TryParse(DropDownCustomer.SelectedValue, out int customerId) || customerId <= 0)
            {
                LabelError.Text = "Please select a customer.";
                LabelError.Visible = true;
                return;
            }
            if (!DateTime.TryParse(TextBoxDate.Text, out DateTime orderDate))
            {
                LabelError.Text = "Invalid date.";
                LabelError.Visible = true;
                return;
            }
            try
            {
                int id = ServiceLocator.SalesOrderService.CreateSalesOrder(new SalesOrderCreateDto
                {
                    CustomerId = customerId,
                    Date = orderDate.Date
                });
                Response.Redirect("OrderDetail.aspx?id=" + id);
            }
            catch (Exception ex)
            {
                LabelError.Text = ex.Message;
                LabelError.Visible = true;
            }
        }
    }
}
