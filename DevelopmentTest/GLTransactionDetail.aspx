<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GLTransactionDetail.aspx.cs" Inherits="DevelopmentTest.GLTransactionDetail" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>GL Transaction detail</title>
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <style>
        body { font-family: Arial, sans-serif; margin: 0; padding: 0; background: #f7f9fc; color: #222; }
        .container { max-width: 960px; margin: 0 auto; padding: 24px; }
        nav { display: flex; gap: 12px; margin-bottom: 20px; padding-bottom: 8px; border-bottom: 1px solid #dde3ee; }
        nav a { text-decoration: none; color: #0050b3; font-size: 14px; padding: 6px 10px; border-radius: 4px; }
        nav a:hover { background-color: #e6f2ff; }
        h1 { margin: 0 0 8px; font-size: 22px; }
        .card { background: #fff; border-radius: 6px; border: 1px solid #dde3ee; overflow: hidden; margin-bottom: 14px; }
        .section { padding: 14px 16px; }
        table { width: 100%; border-collapse: collapse; }
        th, td { padding: 10px 14px; text-align: left; border-bottom: 1px solid #eef1f7; font-size: 13px; }
        th { background: #f5f7fb; font-weight: 600; color: #555; }
        tr:last-child td { border-bottom: 0; }
        .num { text-align: right; }
        .empty-lines { padding: 20px; color: #888; text-align: center; font-size: 13px; }
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
            <h1>GL Transaction #<asp:Literal ID="LiteralId" runat="server" /></h1>
            <div class="card">
                <div class="section">
                    <strong>Date:</strong> <asp:Literal ID="LiteralDate" runat="server" />
                </div>
            </div>
            <div class="card">
                <div class="section">
                    <strong>Lines</strong>
                    <asp:GridView ID="GridViewLines" runat="server" AutoGenerateColumns="False" ShowHeader="True" GridLines="None">
                        <Columns>
                            <asp:BoundField DataField="Account" HeaderText="Account" />
                            <asp:BoundField DataField="Debit" HeaderText="Debit" DataFormatString="{0:C}" ItemStyle-CssClass="num" />
                            <asp:BoundField DataField="Credit" HeaderText="Credit" DataFormatString="{0:C}" ItemStyle-CssClass="num" />
                        </Columns>
                    </asp:GridView>
                    <asp:Panel ID="PanelEmptyLines" runat="server" CssClass="empty-lines" Visible="False">No GL lines.</asp:Panel>
                </div>
            </div>
        </div>
    </form>
</body>
</html>

