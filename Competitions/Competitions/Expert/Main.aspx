<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Main.aspx.cs" Inherits="Competitions.Expert.Main" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <br />
    <h2><span style="font-size: 30px">Добро пожаловать в систему "Конкурсы и проекты Программы развития" </span></h2>
    <br />
    <asp:Button ID="Button1" runat="server" Text="Заявки, ожидающие Вашей экспертной оценки" OnClick="Button1_Click" Width="412px" />
</asp:Content>
