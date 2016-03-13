<%@ Page Title="Основная форма администратора" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AdminMain.aspx.cs" Inherits="EDM.edmAdmin.AdminMain" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    Основная страница администратора электронного докмуентооборота.<br />
    <br />
    <asp:Button ID="NewUserButton" runat="server" Height="30px" Text="Создать нового пользователя" Width="300px" OnClick="NewUserButton_Click" />
</asp:Content>
