<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="BasicParametrs.aspx.cs" Inherits="KPIWeb.StatisticsDepartment.BasicParametrs" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Найти одинаковые аббревиатуры" Width="375px" />
    
        <br />
    
    </div>
        <asp:TextBox ID="TextBox1" runat="server" Height="238px" TextMode="MultiLine" Width="368px"></asp:TextBox>
    </form>
</body>
</html>
