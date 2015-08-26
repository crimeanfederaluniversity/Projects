<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UserLogin.aspx.cs" Inherits="Competition.UserLogin" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <h2><span style="font-size: 30px">Авторизация в систему "Конкурсы"</span></h2>
    <form id="form1" runat="server">
    <div>
    
        <asp:Label ID="Label1" runat="server" Text="Логин"></asp:Label>
    
    </div>
        <asp:TextBox ID="TextBox1" runat="server" Width="150px"></asp:TextBox>
        <br />
        <asp:Label ID="Label2" runat="server" Text="Пароль"></asp:Label>
        <br />
        <asp:TextBox ID="TextBox2" runat="server" Width="150px"></asp:TextBox>
        <br />
        <br />
        <asp:Button ID="Button1" runat="server" Text="Войти" OnClick="Button1_Click" />
    </form>
</body>
</html>
