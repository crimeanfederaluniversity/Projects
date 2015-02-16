<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddRole.aspx.cs" Inherits="KPIWeb.AutomationDepartment.AddRole" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        Создание новой роли<br />
    
    </div>
        Название роли
        <asp:TextBox ID="TextBox1" runat="server" Width="139px"></asp:TextBox>
&nbsp;Активна
        <asp:CheckBox ID="CheckBox1" runat="server" />
&nbsp;&nbsp;
        <asp:Button ID="Button1" runat="server" Text="Создать" Width="63px" OnClick="Button1_Click" />
        <br />
        _________________________________________________________________<br />
        <br />
        Изменение роли<br />
        _________________________________________________________________<br />
        <br />
        Привязка показателей к роли<br />
        <asp:DropDownList ID="DropDownList1" runat="server" Height="28px" Width="328px">
        </asp:DropDownList>
        <asp:Button ID="Button2" runat="server" Text="Загрузить в таблицу" Width="169px" OnClick="Button2_Click" />
        <br />
        <br />
        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False">
            <Columns>
                <asp:CheckBoxField DataField="ch1"  HeaderText="Чт." runat="server"/>
                <asp:CheckBoxField DataField="ch2"  HeaderText="Ред." runat="server"/>
                <asp:CheckBoxField DataField="ch3"  HeaderText="Под." runat="server"/>
                <asp:BoundField DataField="name"   runat="server"/>
            </Columns>
        </asp:GridView>
    </form>
</body>
</html>
