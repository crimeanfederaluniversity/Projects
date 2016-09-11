<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AdminMainPage.aspx.cs" Inherits="Registration.Account.AdminMainPage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <asp:Button ID="Button1" runat="server" Height="50px" OnClick="Button1_Click" Text="Заявки на регистрацию" Width="300px" />
&nbsp;&nbsp;
    <asp:Button ID="Button2" runat="server" Height="50px" OnClick="Button2_Click" Text="Подтвержденные пользователи" Width="300px" />
</asp:Content>
