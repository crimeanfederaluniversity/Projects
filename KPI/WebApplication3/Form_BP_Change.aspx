<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Form_BP_Change.aspx.cs" Inherits="WebApplication3.WebForm4" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <style type="text/css">
        #form1 {
            width: 1212px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
    </div>
        <asp:Localize ID="Localize1" runat="server"></asp:Localize>
        <asp:Localize ID="Localize2" runat="server"></asp:Localize>
    <div>
    
    </div>
        &nbsp;&nbsp;
        <asp:Label ID="Label1" runat="server" Text="ID показателя   "></asp:Label>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        &nbsp;&nbsp;&nbsp;
        <asp:Label ID="Label3" runat="server" Text="Начальная дата "></asp:Label>
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Label ID="Label4" runat="server" Text="Конечная дата"></asp:Label>
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="Label5" runat="server" Text="Дата заполнения"></asp:Label>
        &nbsp;&nbsp; &nbsp;&nbsp;&nbsp;
        <asp:Label ID="Label6" runat="server" Text="Дата модификации"></asp:Label>
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="Label7" runat="server" Text="Значение"></asp:Label>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Label ID="Label8" runat="server" Text="IP последнего"></asp:Label>
&nbsp;
        <asp:Label ID="Label9" runat="server" Text="ID последнего"></asp:Label>
&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Label ID="Label10" runat="server" Text="Подтвердил"></asp:Label>
&nbsp;<p>
            <asp:TextBox ID="TextBox4" runat="server" Width="109px"></asp:TextBox>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:TextBox ID="TextBox2" runat="server" Height="16px" Width="112px"></asp:TextBox>
&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:TextBox ID="TextBox3" runat="server" Width="117px" Height="16px"></asp:TextBox>
&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:TextBox ID="TextBox6" runat="server" Width="137px" Height="16px"></asp:TextBox>
&nbsp;&nbsp;&nbsp;
            <asp:TextBox ID="TextBox7" runat="server" Width="131px" Height="16px"></asp:TextBox>
&nbsp;&nbsp;&nbsp;&nbsp;<asp:TextBox ID="TextBox8" runat="server" Width="66px" Height="16px"></asp:TextBox>
&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:TextBox ID="TextBox9" runat="server" Width="84px" Height="16px"></asp:TextBox>
&nbsp;&nbsp;
            <asp:TextBox ID="TextBox10" runat="server" Width="84px" Height="16px"></asp:TextBox>
&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:TextBox ID="TextBox11" runat="server" Width="129px" Height="16px"></asp:TextBox>
        </p>
        <p>
            <asp:Button ID="Button4" runat="server" Text="Добавить" Height="24px" Width="85px" />
&nbsp;&nbsp;
            <asp:Button ID="Button5" runat="server" Text="Изменить" Height="25px" Width="85px" />
&nbsp;&nbsp;
            <asp:Button ID="Button6" runat="server" Text="Удалить" Width="85px" Height="25px" />
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        </p>
    </form>
</body>
</html>
