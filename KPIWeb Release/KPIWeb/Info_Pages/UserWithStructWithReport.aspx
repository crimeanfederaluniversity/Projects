<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="UserWithStructWithReport.aspx.cs" Inherits="KPIWeb.Info_Pages.UserWithStructWithReport" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    

    <br />
    пароль
    <asp:TextBox ID="TextBox1" runat="server" Width="262px"></asp:TextBox>
    <br />
    <br />
    отчет&nbsp;&nbsp;&nbsp;
    <asp:DropDownList ID="DropDownList1" runat="server" Height="16px" Width="275px">
    </asp:DropDownList>
    <br />
    <br />
    <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Показать" Width="329px" />
    <br />
    <br />
    <asp:TreeView ID="TreeView1" runat="server">
    </asp:TreeView>
&nbsp;
    

</asp:Content>
