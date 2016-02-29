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
    <asp:Button ID="Button3" runat="server" OnClick="Button3_Click" Text="Заявки на изменение учетных данных" Width="500px" Height="50px" />
    <br />
    <br />
    <asp:Button ID="Button4" runat="server" Height="50px" Text="Редактирование модулей" Width="500px" OnClick="Button4_Click" />
    <br />
    <br />
    <asp:Button ID="Button5" runat="server" Height="50px" OnClick="Button5_Click" Text="академ мобильность" Width="263px" />
    <br />
    <asp:Button ID="Button6" runat="server" OnClick="Button6_Click" Text="автопропуск" Width="263px" />
    <br />
    <asp:Button ID="Button7" runat="server" OnClick="Button7_Click" Text="печать визиток" />
    <br />
    <asp:Button ID="Button8" runat="server" OnClick="Button8_Click" Text="списание оборудования" />
    <br />
    <asp:Button ID="Button9" runat="server" OnClick="Button9_Click" Text="вопрос в диспетчерскую" />
    <br />
    <asp:Button ID="Button10" runat="server" OnClick="Button10_Click" Text="1С пользователь" />
    <br />
    <asp:Button ID="Button11" runat="server" OnClick="Button11_Click" Text="приобрет оборуд" />
    <br />
    <asp:Button ID="Button12" runat="server" OnClick="Button12_Click" Text="курс мудл" />
    <br />
    <asp:Button ID="Button13" runat="server" OnClick="Button13_Click" Text="печать" />
    <br />
    <asp:Button ID="Button14" runat="server" OnClick="Button14_Click" Text="ректору" />
    <br />
</asp:Content>
