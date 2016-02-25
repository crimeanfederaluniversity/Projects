<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ITOffice.aspx.cs" Inherits="PersonalPages.ITOffice" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h3>Задайте Ваш вопрос:</h3>
    <br />Вопрос:<br />
    <asp:TextBox ID="TextBox1" runat="server" Height="45px" TextMode="MultiLine" Width="800px"></asp:TextBox>
    <br />
    <br />Укажите Ваш номер телефона:<br />
    <asp:TextBox ID="TextBox3" runat="server" Width="190px"></asp:TextBox>
    <br />
    <br />
    <asp:Button ID="Button1" runat="server" Text="Отправить " Width="300px" OnClick="Button1_Click" />
</asp:Content>
