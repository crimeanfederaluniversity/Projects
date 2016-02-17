<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="LearningIS.aspx.cs" Inherits="PersonalPages.LearningIS" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h3>&nbsp;&nbsp; Заявка на обучение работе в информационных системах</h3>
    <br />&nbsp; Название информационной системы:<br />
    &nbsp;
    <asp:TextBox ID="TextBox1" runat="server" Height="22px" TextMode="MultiLine" Width="500px"></asp:TextBox>
    <br />
    <br />&nbsp; Укажите Ваш номер телефона:<br />
    &nbsp;
    <asp:TextBox ID="TextBox3" runat="server" Width="190px"></asp:TextBox>
    <br />
    <br />
    <br />
    &nbsp;
    <asp:Button ID="Button1" runat="server" Text="Отправить заявку" Width="300px" OnClick="Button1_Click" />
</asp:Content>
