<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="CuratorMainPage.aspx.cs" Inherits="Competitions.Curator.CuratorMainPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <br />
    <h2><span style="font-size: 20px">Добро пожаловать в систему "Конкурсы и проекты Программы развития" </span></h2>
    <asp:Button ID="Button2" runat="server" OnClick="Button2_Click" Text="Мои конкурсы" Width="250px" Height="50px" />
    &nbsp;&nbsp;&nbsp;&nbsp;
    <asp:Button ID="Button1" runat="server" Text="Заявки к моим конкурсам" OnClick="Button1_Click" Width="250px" Height="50px" />
    <br />
    </asp:Content>

