<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="RestorePassword.aspx.cs" Inherits="KPIWeb.Account.RestorePassword" %>

<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
    <div>
    
    </div>
        <asp:Label ID="Label3" runat="server" Font-Size="XX-Large" Text="Восстановление пароля" Font-Bold="True"></asp:Label>
        <br />
        <br />
        <asp:Label ID="Label4" runat="server" Font-Size="Medium" Text="Введите свой Email"></asp:Label>
        &nbsp;<asp:TextBox ID="TextBox1" runat="server" TextMode="Email" Width="300px"></asp:TextBox>
        <br />
        <br />
        <asp:Button ID="Button1" runat="server" CssClass="first_div_on_login" OnClick="Button1_Click" Text="Отправить" />
</asp:Content>
