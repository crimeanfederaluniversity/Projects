<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="HeadMainPage.aspx.cs" Inherits="Rank.Forms.HeadMainPage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <br />
    <asp:Button ID="Button1" runat="server" Height="50px" OnClick="Button1_Click" Text="Индивидуальный рейтинг" Width="500px" />
    <br />
    <br />
    <asp:Button ID="Button2" runat="server" Height="50px" OnClick="Button2_Click" Text="Рейтинг структурного подразделения" Width="500px" />
</asp:Content>
