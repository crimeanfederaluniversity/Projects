<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UserRegister.aspx.cs" Inherits="KPIWeb.Account.UserRegister" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <br />
        <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>
        <br />
        <asp:Label ID="Label3" runat="server" Text="Пароль" Visible="False"></asp:Label>
        <br />
        <asp:TextBox ID="PassText" runat="server" Enabled="False" Visible="False"></asp:TextBox>
        <br />
        <asp:Label ID="Label2" runat="server" Text="Подтверждение пароля" Visible="False"></asp:Label>
        <br />
        <asp:TextBox ID="ConfText" runat="server" Enabled="False" Visible="False"></asp:TextBox>
        <br />
        <br />
        <asp:Button ID="SaveButton" runat="server" Text="Сохранить" Enabled="False" OnClick="SaveButton_Click" Visible="False" Width="170px" />
    
    </div>
    </form>
</body>
</html>
