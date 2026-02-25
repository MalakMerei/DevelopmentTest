<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AllInvoices.aspx.cs" Inherits="DevelopmentTest.AllInvoices" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>All Invoices</title>
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <style>
        body { font-family: Arial, sans-serif; margin: 0; padding: 0; background: #f7f9fc; color: #222; }
        .container { max-width: 960px; margin: 0 auto; padding: 24px; }
        nav { display: flex; gap: 12px; margin-bottom: 20px; padding-bottom: 8px; border-bottom: 1px solid #dde3ee; }
        nav a { text-decoration: none; color: #0050b3; font-size: 14px; padding: 6px 10px; border-radius: 4px; }
        nav a:hover { background-color: #e6f2ff; }
        h1 { margin: 0 0 8px; font-size: 22px; }
        .subtitle { margin: 0 0 16px; font-size: 13px; color: #667; }
        .card { background: #fff; border-radius: 6px; border: 1px solid #dde3ee; overflow: hidden; }
        table { width: 100%; border-collapse: collapse; }
        th, td { padding: 10px 14px; text-align: left; border-bottom: 1px solid #eef1f7; font-size: 13px; }
        th { background: #f5f7fb; font-weight: 600; color: #555; }
        tr:last-child td { border-bottom: 0; }
        tr:hover { background: #fafbfc; }
        .num { text-align: right; }
        .link-view { color: #0050b3; text-decoration: none; font-size: 13px; }
        .link-view:hover { text-decoration: underline; }
        .empty { padding: 24px; text-align: center; color: #888; font-size: 13px; }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="container">
            <nav>
                <a href="Default.aspx">Home</a>
                <a href="SalesOrders.aspx">Sales Orders</a>
                <a href="AllInvoices.aspx">All Invoices</a>
                <a href="Invoices.aspx">Open Invoices</a>
                <a href="Payments.aspx">Payments</a>
            </nav>
            <h1>All Invoices</h1>
            <p class="subtitle">View all invoices by status. Click View to see details and line items.</p>
            <div class="card">
                <asp:GridView ID="GridViewInvoices" runat="server" AutoGenerateColumns="False" ShowHeader="True" GridLines="None">
                    <Columns>
                        <asp:BoundField DataField="Id" HeaderText="Id" ItemStyle-CssClass="num" />
                        <asp:BoundField DataField="CustomerName" HeaderText="Customer" />
                        <asp:BoundField DataField="Date" HeaderText="Date" DataFormatString="{0:d}" />
                        <asp:BoundField DataField="GrossTotal" HeaderText="Gross" DataFormatString="{0:C}" ItemStyle-CssClass="num" />
                        <asp:BoundField DataField="Status" HeaderText="Status" />
                        <asp:TemplateField HeaderText="Action">
                            <ItemTemplate>
                                <a href="InvoiceDetail.aspx?id=<%# Eval("Id") %>" class="link-view">View</a>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
                <asp:Panel ID="PanelEmpty" runat="server" CssClass="empty" Visible="False">No invoices.</asp:Panel>
            </div>
        </div>
    </form>
</body>
</html>
