<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="LearningIS.aspx.cs" Inherits="PersonalPages.LearningIS" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h3>Заявка на обучение работе с ИС:</h3>
    <asp:Label ID="Label1" runat="server" Text="Label" Visible="false"></asp:Label>
    <br />Наименование информационной системы:<br />
    <asp:TextBox ID="TextBox1" runat="server" Height="20px" TextMode="MultiLine" Width="603px"></asp:TextBox>
    <br />
    <br />Укажите Ваш номер телефона:<br />
    <asp:TextBox ID="TextBox3" runat="server" Width="190px"></asp:TextBox>
    <br />
    <br />
    <asp:Button ID="Button1" runat="server" Text="Отправить " Width="300px" OnClick="Button1_Click" />
</asp:Content>
