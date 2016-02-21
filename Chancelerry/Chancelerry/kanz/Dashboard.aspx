<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Dashboard.aspx.cs" Inherits="Chancelerry.kanz.Dashboard" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <asp:GridView ID="GridViewRegisters" AutoGenerateColumns="False" runat="server" OnRowCommand="GridViewRegisters_RowCommand"/>

</asp:Content>
