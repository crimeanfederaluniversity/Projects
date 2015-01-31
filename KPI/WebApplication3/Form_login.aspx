<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Form_login.aspx.cs" Inherits="WebApplication3.Form_login" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        #form1
        {
            height: 276px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div style="height: 140px;border-style:solid; border-width:4px; border-color:#00ff00;  margin-left: auto;margin-right: auto; width: 300px; right: auto; bottom: auto; left: auto; top: auto; margin-top: 78px;" 
        align="center">
    <div style="margin-top:10px;margin-bottom:15px">
    
    <asp:Label ID="Label1" runat="server" Text="ЛОГИН" style="margin-right: 19px;" ></asp:Label>
    <asp:TextBox ID="TextBox1" runat="server" 
       style="margin-left: 0px auto; margin-top: 0px auto;"   Width="140px"></asp:TextBox></div>
    
    <div style="margin-top:10px;margin-bottom:5px">
    <asp:Label ID="Label2" runat="server" Text="ПАРОЛЬ" style="margin-right: 10px;margin-top: 5px"></asp:Label>
    <asp:TextBox ID="TextBox2" runat="server" 
        style="margin-left: 0px auto; margin-right: 0px auto; margin-top: 10px" 
        Width="140px"></asp:TextBox></div>
    <div style="margin-top:10px;margin-bottom:10px">
    <asp:Button ID="Button1" runat="server" Text="Войти" 
         BackColor="#00ff00" Width="140px"  
        style="margin-left: 0px auto; margin-right: 0px auto; margin-top: 10px" 
            Font-Bold="True"  Font-Names="Arial Black" ForeColor="White" OnClick="Button1_Click"/></div>
        </div>
        
   
    </form>
</body>
</html>
