<%@ Page Title="Log in" Language="C#" MasterPageFile="~/BlankPage.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="Chancelerry.Account.Login" Async="true" %>

<%@ Register Src="~/Account/OpenAuthProviders.ascx" TagPrefix="uc" TagName="OpenAuthProviders" %>

<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">

    <table class="full-page">
        <tr>
            <td>
            <section id="loginForm">
                <div class="form-horizontal">

                    <div class="cfu-logo-container">
                        <asp:Image AlternateText="cfu logo" ImageUrl="~/kanz/icons/cfuLoginLogo.png" runat="server"/>
                    </div>

                    <asp:PlaceHolder runat="server" ID="ErrorMessage" Visible="false">
                        <p class="text-danger">
                            <asp:Literal runat="server" ID="FailureText" />
                        </p>
                    </asp:PlaceHolder>
                    <div class="form-group">
                        <asp:Label runat="server" AssociatedControlID="Email" CssClass="col-md-2 control-label alf-label">Электронный адрес</asp:Label>
                        <div class="col-md-10">
                            <asp:TextBox runat="server" ID="Email" CssClass="form-control" TextMode="SingleLine" />
                            <asp:RequiredFieldValidator runat="server" ControlToValidate="Email"
                                CssClass="text-danger" ErrorMessage="The email field is required." />
                        </div>
                    </div>
                    <div class="form-group">
                        <asp:Label runat="server" AssociatedControlID="Password" CssClass="col-md-2 control-label alf-label">Пароль</asp:Label>
                        <div class="col-md-10">
                            <asp:TextBox runat="server" ID="Password" TextMode="Password" CssClass="form-control" />
                            <asp:RequiredFieldValidator runat="server" ControlToValidate="Password" CssClass="text-danger" ErrorMessage="The password field is required." />
                        </div>
                    </div>
                    <div class="form-group">
                        <div class="col-md-offset-2 col-md-10">
                            <div class="checkbox">
                                <asp:Button runat="server" OnClick="LogIn" Text="Войти" CssClass="btn btn-default" />

                                <asp:CheckBox runat="server" ID="RememberMe" />
                                <asp:Label runat="server" AssociatedControlID="RememberMe">Запомнить?</asp:Label>
                            </div>
                        </div>
                    </div>
                    <%--<div class="form-group">
                        <div class="col-md-offset-2 col-md-10">
                            
                        </div>
                    </div>--%>
                </div>
                <%--<p>
                    <asp:HyperLink runat="server" ID="RegisterHyperLink" ViewStateMode="Disabled">Забыли пароль?</asp:HyperLink>
                </p>--%>
                <p>
                    <%-- Enable this once you have account confirmation enabled for password reset functionality
                    <asp:HyperLink runat="server" ID="ForgotPasswordHyperLink" ViewStateMode="Disabled">Forgot your password?</asp:HyperLink>
                    --%>
                </p>
            </section>
            </td>
        </tr>
    </table>

    <script src="<%= ResolveUrl("~/Scripts/toggleLoadingScreen.js") %>"></script>
    <script>
        window.onbeforeunload = showLoadingScreen;
    </script>
</asp:Content>
