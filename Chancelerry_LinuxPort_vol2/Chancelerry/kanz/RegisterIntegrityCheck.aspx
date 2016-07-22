<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="RegisterIntegrityCheck.aspx.cs" Inherits="Chancelerry.kanz.RegisterIntegrityCheck" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <br>
    <br>
     <input type="button" onclick="location.href = 'RegisterView.aspx'" value="Назад к реестру" style="width: 150px" />
    <br>
    <br>
    <asp:Table ID="ResultTable" runat="server"></asp:Table> 
</asp:Content>