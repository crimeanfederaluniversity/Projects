<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="RegisterView.aspx.cs" Inherits="Chancelerry.kanz.RegisterView" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    
    
    <asp:Button ID="Button1" runat="server" Text="Добавить" Width="362px" OnClick="Button1_Click" OnClientClick="showLoadingScreen"/>

    <br />
    <asp:Button ID="Button3" runat="server" OnClick="Button3_Click" Text="Настройка страницы" />

    <br />
    <asp:Button ID="Button2" runat="server" OnClick="Button2_Click" Text="Поиск" OnClientClick="showLoadingScreen"/>

    <br />
    <asp:Label ID="RegisterNameLabel" runat="server" Text="Label"></asp:Label>
    
    <asp:Table ID="dataTable" runat="server" Width="100%" >


         
</asp:Table>

</asp:Content>
