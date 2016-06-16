<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="cntrl2.aspx.cs" Inherits="Chancelerry.kanz.cntrlTemplates.cntrl2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <script src="../toggleLoadingScreen.js" type="text/javascript"></script>
    <script src="../calendar_ru.js" type="text/javascript"></script>
    <link rel="stylesheet" type="text/css" href="cntrlStyle.css">
    <div>
        <br />
        <br />
        <asp:DropDownList ID="T1ListOfIncomingDropDownList" runat="server" Height="20px" Width="400px"></asp:DropDownList>
        <br />
        <br />
        <asp:TextBox ID="T1StartDateTextBox" runat="server" Height="20px" Width="400px" onfocus="this.select();lcs(this)" onclick="event.cancelBubble=true;this.select();lcs(this)" placeholder="Начальная дата поступления документа (включительно)"></asp:TextBox>
        <br />
        <br />
        <asp:TextBox ID="T1EndDateTextBox" runat="server" Height="20px" Width="400px" onfocus="this.select();lcs(this)" onclick="event.cancelBubble=true;this.select();lcs(this)" placeholder="Конечнпая дата поступления документа(включительно)"></asp:TextBox>
        <br />
        <br />
        <asp:TextBox ID="T1CompareDateTextBox" runat="server" Height="20px" Width="400px" onfocus="this.select();lcs(this)" onclick="event.cancelBubble=true;this.select();lcs(this)" placeholder="Дата для сравнения"></asp:TextBox>
        <br />
        <br />
        <asp:LinkButton ID="T1CreateTableButton" runat="server" Text="Показать" Height="20px" Width="400px" OnClick="T1CreateTableButton_Click"  />
        <br />
        <br />
        <div id="T1ResultDiv" runat="server">
        </div>
    </div>
</asp:Content>
