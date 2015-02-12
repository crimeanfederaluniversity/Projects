<%@ Page Language="C#" AutoEventWireup="true" EnableViewStateMac="false" CodeBehind="GenerateReport.aspx.cs" Inherits="KPIWeb.Reports.GenerateReport" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <asp:GridView ID="GridviewReport" runat="server" ShowFooter="true" AutoGenerateColumns="false">
                            <Columns>
                                <asp:BoundField DataField="IndicatorsTableID" Visible="false" />
                                <asp:BoundField DataField="Name" HeaderText="Название индикатора" />
                                <asp:BoundField DataField="IndicatorsValue" HeaderText="Значение индикатора" />
                            </Columns>
                        </asp:GridView>
        <br />
    </div>
        <asp:Button ID="Button1" runat="server" Text="Экспортировать данные в excel" Width="400px" />
        <br />
        <br />
        <asp:Button ID="Button2" runat="server" Text="Печать" Width="400px" />
    </form>
</body>
</html>
