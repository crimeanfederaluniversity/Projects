<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Vvod form.aspx.cs" Inherits="Competition.Vvod_form" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
    </div>
        <asp:Label ID="Label1" runat="server" Text="Название поля таблицы"></asp:Label>
        <br />
        <asp:TextBox ID="TextBox1" runat="server" Height="25px" Width="603px"></asp:TextBox>
        <br />
        <br />

        <br />
        <br />
        <asp:CheckBoxList ID="CheckBoxList1" runat="server" AutoPostBack="true">
        </asp:CheckBoxList>
        <br />
        <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Сохранить" />
    </form>
</body>
</html>
