<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="PersonalMainPage.aspx.cs" Inherits="KPIWeb.PersonalPagesAdmin.PersonalMainPage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <br />
       <h2>Администрирование персональных кабинетов пользователей</h2>
    <br />
    <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Регистрация пользователей" Width="500px" Height="50px" />
    <br />
    <br />
    <asp:Button ID="Button2" runat="server" OnClick="Button2_Click" Text="Редактирование прав доступа" Height="50px" Width="500px" />
    <br />
    <br />
    <asp:Button ID="Button5" runat="server" Height="50px" Text="Заявки пользователей на  доступ к модулям" Width="500px" OnClick="Button5_Click" />
    <br />
    <br />
    <asp:Button ID="Button3" runat="server" OnClick="Button3_Click" Text="Заявки на изменение учетных данных" Width="500px" Height="50px" />
    <br />
    <br />
    <asp:Button ID="Button4" runat="server" Height="50px" Text="Редактирование модулей" Width="500px" OnClick="Button4_Click" />
    <br />
    <br />
    <br />
</asp:Content>
