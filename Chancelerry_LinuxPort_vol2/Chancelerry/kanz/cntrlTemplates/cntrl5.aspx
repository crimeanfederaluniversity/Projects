<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="cntrl5.aspx.cs" Inherits="Chancelerry.kanz.cntrlTemplates.cntrl5" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">


 <script src="../toggleLoadingScreen.js" type="text/javascript"></script>
    <script src="../calendar_ru.js" type="text/javascript"></script>
    <link rel="stylesheet" type="text/css" href="cntrlStyle.css">
    <div>
        <br />
        <br />
        Количество резолюций (по дате передачи)<br />
        <br />
        <asp:DropDownList ID="T5ListOfPrikazDropDownList" runat="server" Height="20px" Width="400px"></asp:DropDownList>
        <br />
        <br />
        <asp:TextBox ID="T5StartDateTextBox" runat="server" Height="20px" Width="400px" onfocus="this.select();lcs(this)" onclick="event.cancelBubble=true;this.select();lcs(this)" placeholder="Начальная дата (включительно)"></asp:TextBox>
        <br />
        <br />
        <asp:TextBox ID="T5EndDateTextBox" runat="server" Height="20px" Width="400px" onfocus="this.select();lcs(this)" onclick="event.cancelBubble=true;this.select();lcs(this)" placeholder="Конечная дата (включительно)"></asp:TextBox>
        <br />
        <br />
        <asp:LinkButton ID="T5CreateTableButton" runat="server" Text="Показать" Height="20px" Width="400px" OnClick="T5CreateTableButton_Click"    />
        <br />
        <br />
        <div id="T5ResultDiv" runat="server">
        </div>
    </div>
    
    </asp:Content>