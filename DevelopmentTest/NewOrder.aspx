<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="NewOrder.aspx.cs" Inherits="DevelopmentTest.NewOrder" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>New Sales Order</title>
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <style>
        body { font-family: Arial, sans-serif; margin: 0; padding: 0; background: #f7f9fc; color: #222; }
        .container { max-width: 560px; margin: 0 auto; padding: 24px; }
        nav { display: flex; gap: 12px; margin-bottom: 20px; padding-bottom: 8px; border-bottom: 1px solid #dde3ee; }
        nav a { text-decoration: none; color: #0050b3; font-size: 14px; padding: 6px 10px; border-radius: 4px; }
        nav a:hover { background-color: #e6f2ff; }
        h1 { margin: 0 0 12px; font-size: 22px; }
        .card { background: #fff; border-radius: 6px; border: 1px solid #dde3ee; padding: 20px 18px; margin-bottom: 16px; }
        .form-group { margin-bottom: 14px; }
        .form-group label { display: block; margin-bottom: 4px; font-weight: 500; font-size: 13px; }
        .form-group select, .form-group input { width: 100%; max-width: 280px; padding: 7px 10px; border: 1px solid #ccd3e0; border-radius: 4px; font-size: 13px; }
        .btn { display: inline-block; padding: 6px 12px; border-radius: 4px; font-size: 13px; font-weight: 500; cursor: pointer; border: none; text-decoration: none; }
        .btn-primary { background: #2563eb; color: #fff; }
        .btn-primary:hover { background: #1d4ed8; }
        .error { color: #b91c1c; font-size: 12px; margin-top: 8px; display: block; }
    </style>
</head>
<body>
    <form id="form1" runat="server">
            <nav>
                <a href="Default.aspx">Home</a>
                <a href="SalesOrders.aspx">Sales Orders</a>
                <a href="AllInvoices.aspx">All Invoices</a>
                <a href="Invoices.aspx">Open Invoices</a>
                <a href="Payments.aspx">Payments</a>
                <a href="GLTransactions.aspx">GL Transactions</a>
            </nav>
        <div class="container">
            <h1>New Sales Order</h1>
            <div class="card">
                <div class="form-group">
                    <asp:Label ID="LabelCustomer" runat="server" AssociatedControlID="DropDownCustomer" Text="Customer" />
                    <asp:DropDownList ID="DropDownCustomer" runat="server" />
                </div>
                <div class="form-group">
                    <asp:Label ID="LabelDate" runat="server" AssociatedControlID="TextBoxDate" Text="Date" />
                    <asp:TextBox ID="TextBoxDate" runat="server" TextMode="Date" />
                </div>
                <asp:Button ID="ButtonCreate" runat="server" Text="Create order" CssClass="btn btn-primary" OnClick="ButtonCreate_Click" />
                <asp:Label ID="LabelError" runat="server" CssClass="error" Visible="False" />
            </div>
        </div>
    </form>
</body>
</html>
