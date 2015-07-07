<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ZapolnenieForm.aspx.cs" Inherits="Competition.ZapolnenieForm" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
     <h2><span style="font-size: 30px">Заполнение заявки на конкурс</span></h2>
    <form id="form1" runat="server">
    <div>
    
        <asp:DropDownList ID="DropDownList1" runat="server" Height="25px" OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged" Width="400px">
        </asp:DropDownList>
    
        <br />
        <br />
        <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Загрузить" />
    
        <br />
    
    </div>
        <br />
        <asp:GridView ID="GridView1"   runat="server" 
             Visible="False">
             
                 </asp:GridView>
        <br />
    </form>
</body>
</html>
