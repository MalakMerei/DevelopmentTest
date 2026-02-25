<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Payments.aspx.cs" Inherits="DevelopmentTest.Payments" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Payments</title>
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
        .link-api { color: #0050b3; text-decoration: none; font-size: 12px; }
        .link-api:hover { text-decoration: underline; }
        .empty { padding: 24px; text-align: center; color: #888; font-size: 13px; }
        .num { text-align: right; }
        .section { padding: 14px 16px; border-bottom: 1px solid #eef1f7; }
        .section:last-of-type { border-bottom: 0; }
        .form-row { margin-bottom: 10px; font-size: 13px; }
        .form-row label { display: inline-block; width: 90px; }
        .form-row select, .form-row input { padding: 6px 8px; border: 1px solid #ccd3e0; border-radius: 4px; width: 170px; font-size: 13px; }
        .btn { display: inline-block; padding: 6px 12px; border-radius: 4px; font-size: 13px; font-weight: 500; cursor: pointer; border: none; text-decoration: none; }
        .btn-primary { background: #2563eb; color: #fff; }
        .btn-primary:hover { background: #1d4ed8; }
        .err { color: #b91c1c; font-size: 12px; margin-top: 4px; display: block; }
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
                <a href="GLTransactions.aspx">GL Transactions</a>
            </nav>
            <h1>Receive payment</h1>
            <div class="card" style="margin-bottom: 16px;">
                <div class="section">
                    <strong>Receive payment against invoice</strong>
                    <div class="form-row" style="margin-top: 10px;">
                        <asp:Label ID="LabelInvoice" runat="server" Text="Invoice" AssociatedControlID="DropDownInvoice" />
                        <asp:DropDownList ID="DropDownInvoice" runat="server" Width="320px" />
                        <asp:Label ID="LabelAmount" runat="server" Text="Amount" AssociatedControlID="TextBoxAmount" style="margin-left: 12px;" />
                        <asp:TextBox ID="TextBoxAmount" runat="server" />
                        <asp:Label ID="LabelDate" runat="server" Text="Date" AssociatedControlID="TextBoxPaymentDate" style="margin-left: 12px;" />
                        <asp:TextBox ID="TextBoxPaymentDate" runat="server" TextMode="Date" />
                        <asp:Button ID="ButtonReceivePayment" runat="server" Text="Receive payment" CssClass="btn btn-primary" style="margin-left: 12px;" OnClick="ButtonReceivePayment_Click" />
                    </div>
                    <asp:Label ID="LabelError" runat="server" CssClass="err" Visible="False" />
                </div>
            </div>
            <div class="card">
                <div class="section">
                    <strong>Payment history</strong>
                </div>
                <asp:GridView ID="GridViewPayments" runat="server" AutoGenerateColumns="False" ShowHeader="True" GridLines="None">
                    <Columns>
                        <asp:TemplateField HeaderText="Id">
                            <ItemTemplate>
                                <a href="api/payments/<%# Eval("Id") %>" class="link-api" target="_blank"><%# Eval("Id") %></a>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="CustomerName" HeaderText="Customer" />
                        <asp:BoundField DataField="Date" HeaderText="Date" DataFormatString="{0:d}" />
                        <asp:BoundField DataField="Amount" HeaderText="Amount" DataFormatString="{0:C}" ItemStyle-CssClass="num" />
                    </Columns>
                </asp:GridView>
                <asp:Panel ID="PanelEmpty" runat="server" CssClass="empty" Visible="False">No payments.</asp:Panel>
            </div>
        </div>
    </form>
</body>
</html>
