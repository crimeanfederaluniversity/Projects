<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ReturnReportForUser.aspx.cs" Inherits="KPIWeb.AutomationDepartment.ReturnReportForUser" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <asp:Label ID="Label1" runat="server" Text="Выбрать отчет"></asp:Label>
        <br />
        <asp:DropDownList ID="DropDownList1" runat="server" Height="28px" Width="315px">
        </asp:DropDownList>
        <br />
        <br />
        <asp:Label ID="Label2" runat="server" Text="Пользователь"></asp:Label>
        <br />
        <asp:TextBox ID="TextBox1" runat="server" Width="302px"></asp:TextBox>
        <br />
        <br />
        <asp:DropDownList ID="DropDownList2" runat="server" Height="16px" Width="249px">
        </asp:DropDownList>
        <br />
        <br />
        <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Выполнить" />
    
    </div>
    </form>
</body>
</html>
