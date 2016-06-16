<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="cntrl4.aspx.cs" Inherits="Chancelerry.kanz.cntrlTemplates.cntrl4" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <script src="../toggleLoadingScreen.js" type="text/javascript"></script>
    <script src="../calendar_ru.js" type="text/javascript"></script>
    <link rel="stylesheet" type="text/css" href="cntrlStyle.css">
    <div>
        <br />
        <br />
        Количество резолюций (по дате передачи)<br />
        <br />
        <asp:DropDownList ID="T4ListOfIncomingDropDownList" runat="server" Height="20px" Width="400px"></asp:DropDownList>
        <br />
        <br />
        <asp:TextBox ID="T4StartDateTextBox" runat="server" Height="20px" Width="400px" onfocus="this.select();lcs(this)" onclick="event.cancelBubble=true;this.select();lcs(this)" placeholder="Начальная дата передачи документа (включительно)"></asp:TextBox>
        <br />
        <br />
        <asp:TextBox ID="T4EndDateTextBox" runat="server" Height="20px" Width="400px" onfocus="this.select();lcs(this)" onclick="event.cancelBubble=true;this.select();lcs(this)" placeholder="Конечная дата передачи документа(включительно)"></asp:TextBox>
        <br />
        <br />
        <asp:LinkButton ID="T4CreateTableButton" runat="server" Text="Показать" Height="20px" Width="400px" OnClick="T4CreateTableButton_Click"   />
        <br />
        <br />
        <div id="T4ResultDiv" runat="server">
        </div>
    </div>
</asp:Content>
