<%@ Page Language="C#" AutoEventWireup="true"  MasterPageFile="~/Site.Master"  CodeBehind="ConnectUsers.aspx.cs" Inherits="KPIWeb.AutomationDepartment.ConnectUsers" %>
<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">  
    <div>

        <br />
        Пользователь к которому цепляем<br />
        <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
        <br />
        <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Найти" Width="191px" />
        <br />
        <asp:Label ID="LBLID0" runat="server" Text="Label"></asp:Label>
&nbsp;
        <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>
        <br />
        <br />
        Пользователь который цепляется<br />
        <asp:TextBox ID="TextBox2" runat="server"></asp:TextBox>
        <br />
        <asp:Button ID="Button2" runat="server" OnClick="Button2_Click" Text="Найти" Width="191px" />
        <br />
        <asp:Label ID="LBLID1" runat="server" Text="Label"></asp:Label>
&nbsp;
        <asp:Label ID="Label2" runat="server" Text="Label"></asp:Label>
        <br />
        <br />
        <asp:Button ID="Button3" runat="server" OnClick="Button3_Click" Text="Объединить" Width="191px" />
        <br />

    </div>
</asp:Content>
