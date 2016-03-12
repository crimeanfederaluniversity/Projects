<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Dashboard.aspx.cs" Inherits="EDM.edm.Dashboard" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>
    <br />
    <asp:GridView ID="dashGridView" runat="server" AutoGenerateColumns="False" OnRowCommand="GridView1_RowCommand">
    </asp:GridView>
</asp:Content>
