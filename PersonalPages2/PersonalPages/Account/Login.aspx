<%@ Page Title="Выполнить вход" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="PersonalPages.Account.Login" Async="true" %>


<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
      <asp:Panel runat="server" ID="top_panel2" CssClass="top_panel" Visible="false">

    </asp:Panel>
    <div id="left_content">
        </div>
    <div id="page_wrapper">    
     <div class="body-content">
    <div class="first_div_on_login">
        <div class="login_image_gerb"></div>
        <br />
        <asp:Label ID="Label3" runat="server" Font-Size="18pt" Text="Введите Ваш логин и пароль для входа в систему личных кабинетов КФУ"></asp:Label>
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
        
 </div></div></div>
 
</asp:Content>
