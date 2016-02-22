<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CardEdit.aspx.cs" Inherits="Chancelerry.kanz.CardEdit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
  
    cardId&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; registerId&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; version<br />
  
    <asp:TextBox ID="TextBox1" runat="server">6</asp:TextBox>
    <asp:TextBox ID="TextBox2" runat="server">1</asp:TextBox>
    <asp:TextBox ID="TextBox3" runat="server">100</asp:TextBox>
      <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Button" />
    <br />
      <div id="cardMainDiv" runat="server">



    </div>
    <asp:Button ID="CreateButton" runat="server" Text="SaveCreate" OnClick="CreateButton_Click" />


</asp:Content>
