<%@ Page Title="Log in" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="Rank.Account.Login" Async="true" %>


<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
    <br />
        <asp:Label ID="Label3" runat="server" Font-Size="18pt" Text="Введите Ваш логин и пароль для входа в систему"></asp:Label>
        <br />
        <br />
        <asp:Label ID="Label1" runat="server" Text="Адрес электронной почты"></asp:Label>
  
        <br />
                        <asp:PlaceHolder runat="server" ID="ErrorMessage" Visible="false">
                        <p class="text-danger">
                            <asp:Literal runat="server" ID="FailureText" />
                        </p>
                        </asp:PlaceHolder>
       <asp:TextBox ID="UserName" CssClass="form-control" runat="server" Height="40px" Width="300px"></asp:TextBox>
       <asp:RequiredFieldValidator runat="server" ControlToValidate="UserName"
                                CssClass="text-danger" ErrorMessage="Поле &quot;Адрес электронной почты&quot; обязательное." />
        <br />
        <asp:Label ID="Label2" runat="server" Text="Пароль"></asp:Label>
        <br />

        <asp:TextBox ID="Password" CssClass="form-control" TextMode="Password" runat="server" Height="40px" Width="300px"></asp:TextBox>
         <asp:RequiredFieldValidator runat="server" ControlToValidate="Password" CssClass="text-danger" ErrorMessage="Поле &quot;Пароль&quot; обязательное." />
        <br />
        <asp:Button ID="Button1" CssClass="form-control" runat="server" Text="Войти" Height="40px" OnClick="Button1_Click" Width="300px" />
        <br />
</asp:Content>
