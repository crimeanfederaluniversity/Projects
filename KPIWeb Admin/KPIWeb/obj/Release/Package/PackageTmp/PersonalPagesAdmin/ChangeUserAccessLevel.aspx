<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ChangeUserAccessLevel.aspx.cs" Inherits="KPIWeb.PersonalPagesAdmin.ChangeUserAccessLevel" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <asp:Button ID="Button2" runat="server" OnClick="Button2_Click" Text="Назад" />
    <br />
    <br />
    <span style="font-size: 20px">Редактирование прав доступа пользователя:</span><br />
    <br />Выберите необходимые модули для пользователя:<br />
    <div>
        <asp:CheckBoxList ID="CheckBoxList1" runat="server">
        </asp:CheckBoxList>
    </div>
&nbsp;<br />
    <asp:Button ID="Button1" runat="server" CssClass="form-control" OnClientClick="showLoadPanel()" Text="Сохранить" Height="40px" Width="400px" OnClick="Button1_Click" />
</asp:Content>
