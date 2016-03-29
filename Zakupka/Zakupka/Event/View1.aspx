<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="View1.aspx.cs" Inherits="Zakupka.Event.View1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <br />
    <asp:DropDownList ID="DropDownList1" runat="server" AutoPostBack ="true" Height="16px" Width="200px">
        <asp:ListItem Selected="True" Value="1">Вид1</asp:ListItem>
        <asp:ListItem Value="2">Вид 2</asp:ListItem>
    </asp:DropDownList>
    <br />
    <style>
        table {
    border-collapse: collapse;
}

table, th, td {
    border: 1px solid black;
}
        </style>
      <div id="TableDiv" runat="server">
        </div>
</asp:Content>
