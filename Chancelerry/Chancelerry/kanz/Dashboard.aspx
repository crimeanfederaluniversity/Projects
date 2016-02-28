<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Dashboard.aspx.cs" Inherits="Chancelerry.kanz.Dashboard" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <asp:GridView ID="GridViewRegisters" AutoGenerateColumns="False" runat="server" OnRowCommand="GridViewRegisters_RowCommand"/>
    

    <br />
    <asp:Button ID="DictionaryEdit" runat="server" Text="Редактирование справочников" Width="263px" OnClick="DictionaryEdit_Click" />
    

    <br />
        

</asp:Content>
