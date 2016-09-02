<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Dashboard.aspx.cs" Inherits="Chancelerry.kanz.Dashboard" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <script src="toggleLoadingScreen.js" type="text/javascript"></script>
    <asp:GridView ID="GridViewRegisters" AutoGenerateColumns="False" runat="server" OnRowCommand="GridViewRegisters_RowCommand"/>
        <br />
    <asp:Button ID="DictionaryEdit" runat="server" Text="Редактирование справочников" class="centered-button" OnClick="DictionaryEdit_Click"/>
        <br />
    <asp:Button ID="StructEdit" runat="server" Text="Редактирование справочника структуры" class="centered-button" OnClick="StructEdit_Click"/>
        <br />
    <asp:Button ID="GoToStatistics" runat="server" Text="Статистика 1" class="centered-button" OnClick="GoToStatistics_Click"/>
        <br />
    <asp:Button ID="GoToControl" runat="server" Text="Статистика 2" class="centered-button" OnClick="GoToControl_Click" />
        <br />
    <asp:Button ID="GoToShowResterAndPrint" runat="server" Visible="False" Text="Вывести весь ресстр" class="centered-button" OnClick="GoToShowResterAndPrint_Click" />
        <br />
    <asp:Button ID="GoToAdmin" runat="server" Visible="False" Text="Администрирование" class="centered-button" OnClick="GoToAdmin_Click" />
        <br />    
    <script>
        window.onbeforeunload = showLoadingScreen;
    </script>
</asp:Content>
