<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CardHistory.aspx.cs" Inherits="Chancelerry.kanz.CardHistory" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <br>
    <br>
     <input type="button" onclick="location.href = 'RegisterView.aspx'" value="Назад к реестру" style="width: 150px" />
    <br>
    <br>

    <asp:Table ID="ResultTable" CellSpacing="2" BorderWidth="1"  runat="server"></asp:Table>
</asp:Content>

