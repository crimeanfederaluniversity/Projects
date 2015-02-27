<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm3.aspx.cs" Inherits="KPIWeb.WebForm3" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>
        <asp:Label ID="FourthlvlId" runat="server" Text='<%# Bind("FourthlvlId") %>'  Visible="True"></asp:Label>
    
        <asp:TextBox ID="TextBox1" runat="server" AutoPostBack="True"></asp:TextBox>
        <asp:RangeValidator runat="server" ID="ValidateDayOff2" ControlToValidate="TextBox1" 
                            MinimumValue="0" Type="Double" 
                            ErrorMessage="Ошибка" ForeColor="Red" Display="dynamic" 
                            SetFocusOnError="True">Ошибка!!1
                        </asp:RangeValidator>
    
        <br />
        <asp:TextBox ID="TextBox2" runat="server"></asp:TextBox>
        <asp:RangeValidator runat="server" ID="RangeValidator2" ControlToValidate="TextBox2" 
                            MinimumValue="0" Type="Double" 
                            ErrorMessage="Ошибка" ForeColor="Red" Display="dynamic" 
                            SetFocusOnError="True">Ошибка!!2
                        </asp:RangeValidator>
        <br />
        <asp:TextBox ID="TextBox3" runat="server"></asp:TextBox>
        <asp:RangeValidator runat="server" ID="RangeValidator3" ControlToValidate="TextBox3" 
                            MinimumValue="0" Type="Double" 
                            ErrorMessage="Ошибка" ForeColor="Red" Display="dynamic" 
                            SetFocusOnError="True">Ошибка!!3
                        </asp:RangeValidator>
        <br />
        <br />
        <asp:Button ID="Button1" runat="server" OnClick="Button1_Click1" Text="Button" />
        <asp:Button ID="Button2" runat="server" OnClick="Button2_Click" Text="Button" />
        <br />
    <asp:ValidationSummary runat="server" ID="Summary" DisplayMode="BulletList" 
        HeaderText="<b>Пожалуйста, исправьте следующие ошибки: </b>" ShowSummary="true" ShowMessageBox="true" />
    </div>
    </form>
</body>
</html>
