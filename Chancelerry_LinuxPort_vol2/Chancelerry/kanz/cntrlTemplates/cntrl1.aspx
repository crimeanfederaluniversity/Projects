<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="cntrl1.aspx.cs" Inherits="Chancelerry.kanz.cntrlTemplates.cntrl1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <script src="../toggleLoadingScreen.js" type="text/javascript"></script>
    <script src="../calendar_ru.js" type="text/javascript"></script>
    <link rel="stylesheet" type="text/css" href="cntrlStyle.css">
    <div>
        <br />
        <br />
        <asp:DropDownList ID="T2ListOfIncomingDropDownList" runat="server" Height="20px" Width="400px"></asp:DropDownList>
        <br />
        <br />
        <asp:TextBox ID="T2CntlFilterStartDateTextBox" runat="server" Height="20px" Width="400px" onfocus="this.select();lcs(this)" onclick="event.cancelBubble=true;this.select();lcs(this)" placeholder="Начальная дата контроля (включительно)"></asp:TextBox>
        <br />
        <br />
        <asp:TextBox ID="T2CntrlFilterEndDateTextBox" runat="server" Height="20px" Width="400px" onfocus="this.select();lcs(this)" onclick="event.cancelBubble=true;this.select();lcs(this)" placeholder="Конечнпая дата контроля (включительно)"></asp:TextBox>
        <br />
        <br />
        <asp:TextBox ID="T2CompareDateTextBox" runat="server" Height="20px" Width="400px" onfocus="this.select();lcs(this)" onclick="event.cancelBubble=true;this.select();lcs(this)" placeholder="Дата для сравнения"></asp:TextBox>
        <br />
        <br />
        <asp:LinkButton ID="T2CreateTableButton" runat="server" Text="Показать" Height="20px" Width="400px" OnClick="T2CreateTableButton_Click"  />
        <br />
        <br />
        <div id="T2ResultDiv" runat="server">
        </div>
    </div>
</asp:Content>
