<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Form_BLP_Report.aspx.cs" Inherits="WebApplication3.WebForm1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body style="height: 281px">
    <form id="form1" runat="server">
    <div>
    
        <asp:TextBox ID="TextBox1" runat="server" Height="16px" Width="300px">Выберите подразделение для просмотра отчета</asp:TextBox>
    
    </div>
        <p>
            <asp:ListBox ID="ListBox1" runat="server" Height="23px" Width="143px"></asp:ListBox>
        </p>
        <p>
            <asp:ListBox ID="ListBox2" runat="server" Height="24px" Width="143px"></asp:ListBox>
        </p>
        <p>
            <asp:ListBox ID="ListBox4" runat="server" Height="21px" Width="143px"></asp:ListBox>
        </p>
        <asp:ListBox ID="ListBox3" runat="server" Height="22px" Width="143px"></asp:ListBox>
        <p>
            <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Выбрать" />
        </p>
    </form>
</body>
</html>
