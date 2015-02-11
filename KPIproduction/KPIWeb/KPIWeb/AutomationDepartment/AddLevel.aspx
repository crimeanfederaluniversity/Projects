<%@ Page Language="C#" AutoEventWireup="true" EnableViewStateMac="false" CodeBehind="AddLevel.aspx.cs" Inherits="KPIWeb.AutomationDepartment.AddLevel" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        Форма добавления подразделений<br />
        <br />
        <asp:Label ID="Label1" runat="server" Text="Академия"></asp:Label>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Label ID="Label2" runat="server" Text="Факультет"></asp:Label>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Label ID="Label3" runat="server" Text="Кафедра"></asp:Label>
    
    </div>
        <asp:DropDownList ID="DropDownList1" runat="server" AutoPostBack="True" Height="25px" OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged" Width="210px">
        </asp:DropDownList>
        <asp:DropDownList ID="DropDownList2" runat="server" AutoPostBack="True" Height="25px" OnSelectedIndexChanged="DropDownList2_SelectedIndexChanged" Width="210px">
        </asp:DropDownList>
        <asp:DropDownList ID="DropDownList3" runat="server" AutoPostBack="True" Height="25px" OnSelectedIndexChanged="DropDownList3_SelectedIndexChanged" Width="210px">
        </asp:DropDownList>
        <br />
        <br />
        <asp:TextBox ID="TextBox1" runat="server" Height="200px" TextMode="MultiLine" Width="205px"></asp:TextBox>
        <asp:TextBox ID="TextBox2" runat="server" Height="200px" TextMode="MultiLine" Width="205px"></asp:TextBox>
        <asp:TextBox ID="TextBox3" runat="server" Height="200px" TextMode="MultiLine" Width="205px"></asp:TextBox>
        <br />
        <br />
        <asp:Button ID="Button1" runat="server" Height="25px" OnClick="Button1_Click" Text="Добавить академию" Width="210px" />
        <asp:Button ID="Button2" runat="server" Height="25px" OnClick="Button2_Click" Text="Добавить факультет" Width="210px" />
        <asp:Button ID="Button3" runat="server" Height="25px" OnClick="Button3_Click" Text="Добавить кафедру" Width="210px" />
        <br />
        <br />
        <asp:Button ID="Button4" runat="server" Height="25px" OnClick="Button4_Click" Text="Обновить страницу" Width="630px" />
    </form>
</body>
</html>
