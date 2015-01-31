<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Form_Add_Stage.aspx.cs" Inherits="WebApplication3.WebForm2" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div style="height: 20px; width: 967px">
    
        <asp:TextBox ID="Pass_in" runat="server"></asp:TextBox>
    
    </div>
        <p>
            <asp:TextBox ID="TextBox2" runat="server" Height="17px" Width="143px">Академия/Предприятие</asp:TextBox>
            <asp:DropDownList ID="DropDownList1" runat="server" Height="24px" Width="232px" OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged" AutoPostBack="True">
            </asp:DropDownList>
        </p>
        <p>
            <asp:TextBox ID="TextBox3" runat="server" Height="17px" Width="143px">Деканат/Департамент</asp:TextBox>
            <asp:DropDownList ID="DropDownList2" runat="server" Height="24px" Width="232px" AutoPostBack="True" OnSelectedIndexChanged="DropDownList2_SelectedIndexChanged">
            </asp:DropDownList>
        </p>
        <p>
            <asp:TextBox ID="TextBox4" runat="server" Height="17px" Width="143px">Управление/Кафедра</asp:TextBox>
            <asp:DropDownList ID="DropDownList3" runat="server" Height="24px" Width="232px" AutoPostBack="True">
            </asp:DropDownList>
        </p>
        <p>
            <asp:TextBox ID="TextBox5" runat="server" Height="17px" Width="143px">Отделение</asp:TextBox>
            <asp:DropDownList ID="DropDownList4" runat="server" Height="24px" Width="232px">
            </asp:DropDownList>
        </p>
        <p>
            <asp:TextBox ID="TextBox6" runat="server"></asp:TextBox>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:TextBox ID="TextBox7" runat="server"></asp:TextBox>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </p>
        <p>
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:Button ID="Button1" runat="server" Text="Добавить" Width="72px" OnClick="Button1_Click" />
            </p>
        <p>
            &nbsp;</p>
    </form>
</body>
</html>
