<%@ Page Title="Основная форма администратора" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AdminMain.aspx.cs" Inherits="EDM.edmAdmin.AdminMain" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    Основная страница администратора электронного докмуентооборота.<br />
<br />
    <br />
    <asp:Button ID="NewUserButton" runat="server" Height="30px" Text="Создать нового пользователя" Width="300px" OnClick="NewUserButton_Click" />
    <br />
    <br />
    <asp:Button ID="WatchUsers" runat="server" Height="30px" Text="Просмотр пользователей" Width="300px" OnClick="WatchUsers_Click" />
    <br />
    <br />
    <asp:Button ID="WatchStructure" runat="server" Height="30px" Text="Редактирование структуры" Width="300px" OnClick="WatchStructure_Click" />
    <br />
    <br />
    <asp:Button ID="WatchSubmiters" runat="server" Height="30px" Text="Доступ к отчетам о распечатке" Width="300px" OnClick="WatchSubmitersClick" />
    <br />
    <br />
</asp:Content>
