<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Main.aspx.cs" Inherits="Competitions.Admin.Main" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
       <h2><span style="font-size: 20px">Добро пожаловать в систему администрирования модуля "Конкурсы и проекты Программы развития" </span></h2>
    <br />
    <asp:Button ID="Button4" runat="server" OnClick="Button4_Click" Text="Работа со справочниками" Width="300px" Height="50px" />
    <br />
    <br />
    <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Работа с конкурсами" Width="300px" Height="50px" />
    <br />
    <br />
    <asp:Button ID="Button2" runat="server" OnClick="Button2_Click" Text="Необработанные заявки" Width="300px" Height="50px" />
    
    <br />
    <br />
    <asp:Button ID="Button3" runat="server" OnClick="Button3_Click" Text="Готовые заявки" Width="300px" Height="50px" />
    
</asp:Content>
