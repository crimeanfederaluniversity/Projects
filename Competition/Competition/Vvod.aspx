<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Vvod.aspx.cs" Inherits="Competition.Vvod" %>

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
        <asp:Label ID="Label1" runat="server" Text="Название конкурса"></asp:Label>
        <br />
        <asp:TextBox ID="TextBox1" runat="server" Width="293px"></asp:TextBox>
        <br />
        <asp:Label ID="Label2" runat="server" Text="Шифр конкурса"></asp:Label>
        <br />
        <asp:TextBox ID="TextBox2" runat="server" Width="141px"></asp:TextBox>
        <br />
        <asp:Label ID="Label3" runat="server" Text="Куратор"></asp:Label>
        <br />
        <asp:TextBox ID="TextBox3" runat="server"></asp:TextBox>
        <br />
        <br />
        <asp:Label ID="Label4" runat="server" Text="Начальная дата"></asp:Label>
        <asp:Calendar ID="Calendar1" runat="server"></asp:Calendar>
        <br />
        <asp:Label ID="Label5" runat="server" Text="Конечная дата"></asp:Label>
        <asp:Calendar ID="Calendar2" runat="server"></asp:Calendar>
        <p>
            <asp:Button ID="Button1" runat="server" Text="Cохранить" OnClick="Button1_Click" />
        </p>
    </form>
</body>
</html>
