<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="RegisterView.aspx.cs" Inherits="Chancelerry.kanz.RegisterView" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    
    <asp:Button ID="Button1" runat="server" Text="Добавить" />


    <br />
    <asp:Label ID="RegisterNameLabel" runat="server" Text="Label"></asp:Label>


    <asp:GridView ID="GridViewData" runat="server" AutoGenerateColumns="False"/>

</asp:Content>
