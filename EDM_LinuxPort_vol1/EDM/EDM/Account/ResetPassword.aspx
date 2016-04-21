<%@ Page Title="Reset Password" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ResetPassword.aspx.cs" Inherits="EDM.Account.ResetPassword" Async="true" %>

<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">

    <div style="margin-top:5%">
    <br />
        <br />
    Введите Ваш старый пароль
    <asp:TextBox runat="server" ID="OldPassword" CssClass="form-control" /><br />    
    Введите Ваш новый пароль<br />
    <asp:TextBox runat="server" ID="Password" TextMode="Password" CssClass="form-control" />
    Повторите пароль<br />
    <asp:TextBox runat="server" ID="ConfirmPassword" TextMode="Password" CssClass="form-control" />
                <asp:RequiredFieldValidator runat="server" ControlToValidate="ConfirmPassword" ID ="DisplayMessage"
                    CssClass="text-danger" Display="Dynamic" ErrorMessage="Пароли не совпадают!" Visible="false" />
    <br />
    <asp:Button ID="Button1" runat="server" Height="27px" Text="Применить" Width="201px" OnClick="Button1_Click" />
    </div>
    </asp:Content>