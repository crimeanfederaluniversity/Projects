<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="OMRMainPage.aspx.cs" Inherits="Rank.Forms.OMRMainPage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <br />
    <asp:Button ID="Button1" runat="server" Height="50px" OnClick="Button1_Click" Text="Ввести данные" Width="500px" />
    <br />
    <br />
    <asp:Button ID="Button2" runat="server" Height="50px" OnClick="Button2_Click" Text="Верифицировать данные" Width="500px" />
    <br />
    <br />
    <asp:Button ID="Button3" runat="server" Height="50px" OnClick="Button3_Click" Text="Расчет рейтинга" Width="500px" />
</asp:Content>
