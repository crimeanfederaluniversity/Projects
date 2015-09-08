<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="UserRegister.aspx.cs" Inherits="KPIWeb.Account.UserRegister" %>

<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">  
     
    <div>
    
        <span style="font-size: x-large">Пожалуйста, введите пароль для вашего аккаунта</span><br />
        <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>
        <br />
        <asp:Label ID="Label3" runat="server" Text="Пароль" Visible="False"></asp:Label>
        <br />
        <asp:TextBox ID="PassText" TextMode="Password" CssClass="form-control" runat="server" Enabled="False" Visible="False"></asp:TextBox>   
                
        <asp:RegularExpressionValidator runat="server" ID="PassTextRange"  CssClass="text-danger"
        ControlToValidate="PassText" ValidationExpression="\S{6,20}"
        ErrorMessage="Длина пароля должна быть в диапазоне 6-20 символов" Display="dynamic">Длина пароля должна быть в диапазоне 6-20 символов
        </asp:RegularExpressionValidator>

        <br />
        <asp:Label ID="Label2" runat="server" Text="Подтверждение пароля" Visible="False"></asp:Label>
        <br />
        <asp:TextBox ID="ConfText"  TextMode="Password" CssClass="form-control" runat="server" Enabled="False" Visible="False"></asp:TextBox>

        <asp:RequiredFieldValidator runat="server"
                ControlToValidate="ConfText"
                CssClass="text-danger" Display="Dynamic" ErrorMessage="Введите подтверждение пароля." ID="errorNoConfirm" />
            <asp:CompareValidator runat="server" ControlToCompare="PassText" ControlToValidate="ConfText"
                CssClass="text-danger" Display="Dynamic" ErrorMessage="Пароли не совпадают." ID="ErrorWrongConfirm" />
        <br />
        <asp:Button ID="SaveButton" CssClass="form-control" runat="server" Text="Сохранить" Enabled="False" OnClick="SaveButton_Click" Visible="False" Width="170px" />
    
        <br />
    
        <br />
    </div>
</asp:Content>
