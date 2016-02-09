<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="LearningIS.aspx.cs" Inherits="PersonalPages.LearningIS" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h3>Заявка на обучение работе в информационных системах</h3>
    <br />Название информационной системы:<br />
    <asp:TextBox ID="TextBox1" runat="server" Height="22px" TextMode="MultiLine" Width="500px"></asp:TextBox>
    <br />
    <br />Укажите Ваш номер телефона:<br />
    <asp:TextBox ID="TextBox3" runat="server"></asp:TextBox>
    <br />
    <br />
    <br />
    <asp:Button ID="Button1" runat="server" Text="Отправить заявку" Width="300px" OnClick="Button1_Click" />
</asp:Content>
