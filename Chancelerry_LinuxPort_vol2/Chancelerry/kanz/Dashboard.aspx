<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Dashboard.aspx.cs" Inherits="Chancelerry.kanz.Dashboard" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <script src="toggleLoadingScreen.js" type="text/javascript"></script>
    <asp:GridView ID="GridViewRegisters" AutoGenerateColumns="False" runat="server" OnRowCommand="GridViewRegisters_RowCommand"/>
    

    <br />
    <asp:Button ID="DictionaryEdit" runat="server" Text="Редактирование справочников" class="centered-button" OnClick="DictionaryEdit_Click"/>
    

    <br />
        
    <script>
        window.onbeforeunload = showLoadingScreen;
    </script>

</asp:Content>
