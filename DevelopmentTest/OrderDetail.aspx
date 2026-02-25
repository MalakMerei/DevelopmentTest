<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="OrderDetail.aspx.cs" Inherits="DevelopmentTest.OrderDetail" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Order detail</title>
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <style>
        body { font-family: Arial, sans-serif; margin: 0; padding: 0; background: #f7f9fc; color: #222; }
        .container { max-width: 960px; margin: 0 auto; padding: 24px; }
        nav { display: flex; gap: 12px; margin-bottom: 20px; padding-bottom: 8px; border-bottom: 1px solid #dde3ee; }
        nav a { text-decoration: none; color: #0050b3; font-size: 14px; padding: 6px 10px; border-radius: 4px; }
        nav a:hover { background-color: #e6f2ff; }
        h1 { margin: 0 0 6px; font-size: 22px; }
        .info { color: #667; font-size: 13px; margin: 0 0 16px; }
        .card { background: #fff; border-radius: 6px; border: 1px solid #dde3ee; overflow: hidden; margin-bottom: 14px; }
        .section { padding: 14px 16px; }
        table { width: 100%; border-collapse: collapse; }
        th, td { padding: 10px 14px; text-align: left; border-bottom: 1px solid #eef1f7; font-size: 13px; }
        th { background: #f5f7fb; font-weight: 600; color: #555; }
        tr:last-child td { border-bottom: 0; }
        .num { text-align: right; }
        .form-inline .form-group { display: inline-block; margin-right: 10px; margin-bottom: 8px; }
        .form-inline label { margin-right: 4px; font-size: 13px; }
        .form-inline input, .form-inline select { padding: 6px 8px; border: 1px solid #ccd3e0; border-radius: 4px; width: 110px; font-size: 13px; }
        .btn { display: inline-block; padding: 6px 12px; border-radius: 4px; font-size: 13px; font-weight: 500; cursor: pointer; border: none; text-decoration: none; }
        .btn-primary { background: #2563eb; color: #fff; }
        .btn-primary:hover { background: #1d4ed8; }
        .empty-lines { padding: 20px; color: #888; text-align: center; font-size: 13px; }
        .error { color: #b91c1c; font-size: 12px; margin-top: 8px; display: block; }
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
            <h1>Order #<asp:Literal ID="LiteralId" runat="server" /></h1>
            <div class="card">
                <div class="section">
                    <strong>Customer:</strong> <asp:Literal ID="LiteralCustomer" runat="server" /> &nbsp;|&nbsp;
                    <strong>Date:</strong> <asp:Literal ID="LiteralDate" runat="server" /> &nbsp;|&nbsp;
                    <strong>Status:</strong> <asp:Literal ID="LiteralStatus" runat="server" />
                    <asp:Panel ID="PanelInvoiceLink" runat="server" Visible="False">
                        &nbsp;|&nbsp; <asp:HyperLink ID="LinkInvoice" runat="server" Text="View invoice" />
                    </asp:Panel>
                </div>
            </div>
            <div class="card">
                <div class="section">
                    <strong>Lines</strong>
                    <asp:GridView ID="GridViewLines" runat="server" AutoGenerateColumns="False" ShowHeader="True" GridLines="None">
                        <Columns>
                            <asp:BoundField DataField="ItemName" HeaderText="Item" />
                            <asp:BoundField DataField="Qty" HeaderText="Qty" ItemStyle-CssClass="num" />
                            <asp:BoundField DataField="UnitPrice" HeaderText="Unit price" DataFormatString="{0:C}" ItemStyle-CssClass="num" />
                            <asp:TemplateField HeaderText="Line total" ItemStyle-CssClass="num">
                                <ItemTemplate><%# string.Format("{0:C}", (int)Eval("Qty") * (decimal)Eval("UnitPrice")) %></ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                    <asp:Panel ID="PanelEmptyLines" runat="server" CssClass="empty-lines" Visible="False">No lines. Add a line below (order must be Open).</asp:Panel>
                </div>
            </div>
            <asp:Panel ID="PanelAddLine" runat="server" Visible="False">
                <div class="card">
                    <div class="section">
                        <strong>Add line</strong>
                        <div class="form-inline" style="margin-top: 12px;">
                            <div class="form-group">
                                <asp:Label ID="LabelItem" runat="server" Text="Item" />
                                <asp:DropDownList ID="DropDownItem" runat="server" Width="180px" />
                            </div>
                            <div class="form-group">
                                <asp:Label ID="LabelQty" runat="server" Text="Qty" />
                                <asp:TextBox ID="TextBoxQty" runat="server" Text="1" />
                            </div>
                            <div class="form-group">
                                <asp:Label ID="LabelUnitPrice" runat="server" Text="Unit price" />
                                <asp:TextBox ID="TextBoxUnitPrice" runat="server" />
                            </div>
                            <asp:Button ID="ButtonAddLine" runat="server" Text="Add line" CssClass="btn btn-primary" OnClick="ButtonAddLine_Click" />
                        </div>
                        <asp:Label ID="LabelLineError" runat="server" CssClass="error" Visible="False" />
                    </div>
                </div>
                <div class="card">
                    <div class="section">
                        <asp:Button ID="ButtonRelease" runat="server" Text="Release to invoice" CssClass="btn btn-primary" OnClick="ButtonRelease_Click" />
                        <asp:Label ID="LabelReleaseError" runat="server" CssClass="error" Visible="False" />
                    </div>
                </div>
            </asp:Panel>
        </div>
    </form>
</body>
</html>
