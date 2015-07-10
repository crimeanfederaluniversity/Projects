<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Questions.aspx.cs" Inherits="Competition.Questions" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
     <h2><span style="font-size: 30px">Внесение вопросов в базу</span></h2>
    <form id="form1" runat="server">
    <div>
    
        <br />
    
    </div>
        <asp:TextBox ID="TextBox1" runat="server" Width="1011px"></asp:TextBox>
        <br />
        <br />
        <br />
        <asp:CheckBoxList ID="CheckBoxList1" runat="server">
        </asp:CheckBoxList>
        <br />
        <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" style="height: 25px" Text="Сохранить" />
    </form>
</body>
</html>
