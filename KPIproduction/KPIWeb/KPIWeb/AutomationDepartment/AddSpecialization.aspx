<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddSpecialization.aspx.cs" Inherits="KPIWeb.AutomationDepartment.AddSpecialization" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        вводим области знаний<br />
        активен(0,1)#название области знаний<br />
        <asp:TextBox ID="TextBox1" runat="server" Height="153px" TextMode="MultiLine" Width="265px"></asp:TextBox>
    
    </div>
        <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Добавить области знаний" Width="272px" />
        <br />
        <br />
        <br />
        <br />
        Добавление специальностей<br />
        Актив#Название#НомерСпециальности#КлючикНаОбластьЗнаний<br />
        <asp:TextBox ID="TextBox2" runat="server" Height="147px" TextMode="MultiLine" Width="264px"></asp:TextBox>
        <br />
        <asp:Button ID="Button2" runat="server" OnClick="Button2_Click" Text="Добавить специальности" Width="261px" />
        <br />
        <br />
        Добавление базовых показателей<br />
        <asp:TextBox ID="TextBox3" runat="server" Height="90px" TextMode="MultiLine" Width="428px"></asp:TextBox>
        <br />
        <br />
        <asp:Button ID="Button3" runat="server" OnClick="Button3_Click" Text="Добавить базовые показатели с параметрами" />
    </form>
</body>
</html>
