<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Main.aspx.cs" Inherits="Competitions.Expert.Main" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <br />
    <h2><span style="font-size: 20px">Добро пожаловать в систему экспертной оценки модуля "Конкурсы и проекты Программы развития" </span></h2>
    <p>&nbsp;</p>
    <asp:Button ID="Button2" runat="server" OnClick="Button2_Click" Text="Обработанные заявки" Width="400px" Height="50px" />
    &nbsp;&nbsp;
    <asp:Button ID="Button1" runat="server" Text="Заявки, ожидающие Вашей экспертной оценки" OnClick="Button1_Click" Width="400px" Height="50px" />
    <br />
    </asp:Content>
