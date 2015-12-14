<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CreatePersonalPage.aspx.cs" Inherits="KPIWeb.PersonalPagesAdmin.CreatePersonalPage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div>
        <span style="font-size: 30px">Регистрация нового пользователя:</span><br />
        <br />
        <asp:Label ID="EmailLabel" runat="server" Text="Адрес электронной почты "></asp:Label>
         <br />
         <asp:TextBox ID="EmailText" CssClass="form-control" runat="server" Height="40px" Width="400px"></asp:TextBox>
         <asp:RadioButtonList ID="RadioButtonList1" runat="server" AutoPostBack="True">
             <asp:ListItem Value="0">Студент</asp:ListItem>
             <asp:ListItem Selected="True" Value="1">Сотрудник</asp:ListItem>
        </asp:RadioButtonList>
        <asp:Label ID="Label1" runat="server" Text="Выберите необходимые модули для пользователя:"></asp:Label>
        <br />
        <div>
            <asp:CheckBoxList ID="CheckBoxList1" runat="server" Visible="False">
        </asp:CheckBoxList>
       </div>
        
        <br />
             <asp:CheckBox ID="CheckBox1" runat="server" Checked="True"   Text="Отправить email?" />
             <br />
             <br />
             <asp:Button ID="Button1" runat="server" CssClass="form-control" OnClientClick="showLoadPanel()" Text="Создать пользователя" Height="40px" Width="400px" OnClick="Button1_Click" />
         </div>
&nbsp;
</asp:Content>
