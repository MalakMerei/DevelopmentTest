using System;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessLayer.DTOs;
using DevelopmentTest.App_Start;

namespace DevelopmentTest
{
    public partial class OrderDetail : Page
    {
        private int _orderId;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!int.TryParse(Request.QueryString["id"], out _orderId) || _orderId <= 0)
            {
                Response.Redirect("SalesOrders.aspx");
                return;
            }
            if (IsPostBack) return;
            LoadOrder();
        }

        private void LoadOrder()
        {
            var so = ServiceLocator.SalesOrderService.GetById(_orderId);
            if (so == null)
            {
                Response.Redirect("SalesOrders.aspx");
                return;
            }
            LiteralId.Text = so.Id.ToString();
            LiteralCustomer.Text = so.CustomerName ?? "";
            LiteralDate.Text = so.Date.ToString("d");
            LiteralStatus.Text = so.Status ?? "";

            if (so.Lines != null && so.Lines.Count > 0)
            {
                GridViewLines.Visible = true;
                PanelEmptyLines.Visible = false;
                GridViewLines.DataSource = so.Lines;
                GridViewLines.DataBind();
            }
            else
            {
                GridViewLines.Visible = false;
                PanelEmptyLines.Visible = true;
            }

            bool isOpen = so.Status == "Open";
            PanelAddLine.Visible = isOpen;
            if (isOpen)
            {
                var items = ServiceLocator.LookupService.GetAllItems().ToList();
                DropDownItem.DataSource = items;
                DropDownItem.DataValueField = "Id";
                DropDownItem.DataTextField = "Name";
                DropDownItem.DataBind();
                DropDownItem.Items.Insert(0, new ListItem("-- Select item --", ""));
            }
            else if (so.Status == "Invoiced")
            {
                var inv = ServiceLocator.InvoiceService.GetBySalesOrderId(so.Id);
                if (inv != null)
                {
                    PanelInvoiceLink.Visible = true;
                    LinkInvoice.NavigateUrl = "Invoices.aspx";
                    LinkInvoice.Text = "View invoice #" + inv.Id;
                }
            }
        }

        protected void ButtonAddLine_Click(object sender, EventArgs e)
        {
            LabelLineError.Visible = false;
            if (!int.TryParse(DropDownItem.SelectedValue, out int itemId) || itemId <= 0)
            {
                LabelLineError.Text = "Select an item.";
                LabelLineError.Visible = true;
                return;
            }
            if (!int.TryParse(TextBoxQty.Text, out int qty) || qty <= 0)
            {
                LabelLineError.Text = "Qty must be greater than 0.";
                LabelLineError.Visible = true;
                return;
            }
            if (!decimal.TryParse(TextBoxUnitPrice.Text, out decimal unitPrice))
                unitPrice = ServiceLocator.LookupService.GetAllItems().FirstOrDefault(i => i.Id == itemId)?.UnitPrice ?? 0;
            if (unitPrice < 0)
            {
                LabelLineError.Text = "Unit price cannot be negative.";
                LabelLineError.Visible = true;
                return;
            }
            try
            {
                ServiceLocator.SalesOrderService.AddLine(_orderId, new SalesOrderLineDto
                {
                    ItemId = itemId,
                    Qty = qty,
                    UnitPrice = unitPrice
                });
                LoadOrder();
                TextBoxQty.Text = "1";
                TextBoxUnitPrice.Text = "";
            }
            catch (Exception ex)
            {
                LabelLineError.Text = ex.Message;
                LabelLineError.Visible = true;
            }
        }

        protected void ButtonRelease_Click(object sender, EventArgs e)
        {
            LabelReleaseError.Visible = false;
            var so = ServiceLocator.SalesOrderService.GetById(_orderId);
            if (so?.Lines == null || so.Lines.Count == 0)
            {
                LabelReleaseError.Text = "Add at least one line before releasing.";
                LabelReleaseError.Visible = true;
                return;
            }
            try
            {
                var result = ServiceLocator.SalesOrderService.ReleaseToInvoice(_orderId);
                Response.Redirect("Invoices.aspx?posted=" + result.InvoiceId);
            }
            catch (Exception ex)
            {
                LabelReleaseError.Text = ex.Message;
                LabelReleaseError.Visible = true;
            }
        }
    }
}
