<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="RegisterView.aspx.cs" Inherits="Chancelerry.kanz.RegisterView" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    
    
    <asp:Button ID="Button1" runat="server" Text="Добавить" Width="362px" OnClick="Button1_Click" OnClientClick="showLoadingScreen"/>

    <br />
    <asp:Button ID="Button3" runat="server" OnClick="Button3_Click" Text="Настройка страницы" />

    <br />
    <asp:Button ID="Button2" runat="server" OnClick="Button2_Click" Text="Поиск" OnClientClick="showLoadingScreen"/>

    <asp:Button ID="Button4" runat="server" OnClick="Button4_Click" Text="Очистить поиск" />

    <br />
    <asp:Button ID="Button5" runat="server" OnClick="Button5_Click" Text="Назад" Width="61px" />
    <asp:Button ID="Button6" runat="server" OnClick="Button6_Click" Text="Вперёд" />
    <asp:Label ID="PageNumberLabel" runat="server" Text="Label"></asp:Label>

    <br />
    <asp:Label ID="RegisterNameLabel" runat="server" Text="Label"></asp:Label>
    
    <asp:Table ID="dataTable" runat="server" Width="100%" >


         
</asp:Table>

</asp:Content>
