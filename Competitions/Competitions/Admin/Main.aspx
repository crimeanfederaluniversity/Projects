<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Main.aspx.cs" Inherits="Competitions.Admin.Main" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    
    <br />
    <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Работа с конкурсами" Width="330px" />
    <br />
    <br />
    <asp:Button ID="Button2" runat="server" OnClick="Button2_Click" Text="Необработанные заявки" Width="332px" />
    
    <br />
    <br />
    <asp:Button ID="Button3" runat="server" OnClick="Button3_Click" Text="Готовые заявки" Width="331px" />
    
</asp:Content>
