<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="cntrl3.aspx.cs" Inherits="Chancelerry.kanz.cntrlTemplates.cntrl3" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
<script src="../toggleLoadingScreen.js" type="text/javascript"></script>
    <script src="../calendar_ru.js" type="text/javascript"></script>
    <link rel="stylesheet" type="text/css" href="cntrlStyle.css">
    <div>
        <br />
        <br />
        Количество поступивших документов<br />
        <br />
        <asp:DropDownList ID="T3ListOfIncomingDropDownList" runat="server" Height="20px" Width="400px"></asp:DropDownList>
        <br />
        <br />
        <asp:TextBox ID="T3StartDateTextBox" runat="server" Height="20px" Width="400px" onfocus="this.select();lcs(this)" onclick="event.cancelBubble=true;this.select();lcs(this)" placeholder="Начальная дата поступления документа (включительно)"></asp:TextBox>
        <br />
        <br />
        <asp:TextBox ID="T3EndDateTextBox" runat="server" Height="20px" Width="400px" onfocus="this.select();lcs(this)" onclick="event.cancelBubble=true;this.select();lcs(this)" placeholder="Конечная дата поступления документа(включительно)"></asp:TextBox>
        <br />
        <br />
        <asp:LinkButton ID="T3CreateTableButton" runat="server" Text="Показать" Height="20px" Width="400px" OnClick="T3CreateTableButton_Click"   />
        <br />
        <br />
        <div id="T3ResultDiv" runat="server">
        </div>
    </div>
</asp:Content>