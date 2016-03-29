<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="NewPassword.aspx.cs" MasterPageFile="~/Site.Master" Inherits="EDM.Account.NewPassword" %>

<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">

    Введите Ваш новый пароль<br />
    <asp:TextBox runat="server" ID="Password" TextMode="Password" CssClass="form-control" />

    Повторите пароль<br />
    <asp:TextBox runat="server" ID="ConfirmPassword" TextMode="Password" CssClass="form-control" />
                <asp:RequiredFieldValidator runat="server" ControlToValidate="ConfirmPassword" ID ="DisplayMessage"
                    CssClass="text-danger" Display="Dynamic" ErrorMessage="Пароли не совпадают!" Visible="false" />
    <br />
    <asp:Button ID="Button1" runat="server" Height="27px" Text="Применить" Width="201px" OnClick="Button1_Click" />
 </asp:Content>