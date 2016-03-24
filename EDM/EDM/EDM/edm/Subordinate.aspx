<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Subordinate.aspx.cs" Inherits="EDM.edm.Subordinate" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <asp:Label ID="directionLabel" runat="server" Text="Заголовок" CssClass="header" ></asp:Label>
    <asp:GridView runat="server" ID="subGridView" CssClass="able edm-table edm-history-table centered-block" AutoGenerateColumns="False" OnRowCommand="subGridView_RowCommand"></asp:GridView>
</asp:Content>
