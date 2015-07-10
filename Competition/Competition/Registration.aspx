<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Registration.aspx.cs" Inherits="Competition.Registration" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <h2><span style="font-size: 30px">Регистрация</span></h2>
    <form id="form1" runat="server">
    <div>
    
    </div>
        <asp:Label ID="Label1" runat="server" Text="ФИО :        "></asp:Label>
        <br />
        <asp:TextBox ID="TextBox1" runat="server" Width="300px"></asp:TextBox>
        <br />
        <br />
        <asp:Label ID="Label2" runat="server" Text="Должность :"></asp:Label>
        <br />
        <asp:TextBox ID="TextBox2" runat="server" Width="300px"></asp:TextBox>
        <br />
        <br />
        <asp:Label ID="Label3" runat="server" Text="Место работы"></asp:Label>
        <br />
        <asp:TextBox ID="TextBox3" runat="server" Width="300px"></asp:TextBox>
        <br />
        <br />
        <asp:Label ID="Label4" runat="server" Text="E-mail"></asp:Label>
        <br />
        <asp:TextBox ID="TextBox4" runat="server" Width="100px"></asp:TextBox>
        <br />
        <asp:Label ID="Label5" runat="server" Text="Пароль"></asp:Label>
        <br />
        <asp:TextBox ID="TextBox5" runat="server"></asp:TextBox>
        <br />
        <br />
        <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Готово" />
        <br />
    </form>
</body>
</html>
