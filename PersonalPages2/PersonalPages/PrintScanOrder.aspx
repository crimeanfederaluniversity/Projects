<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="PrintScanOrder.aspx.cs" Inherits="PersonalPages.PrintScanOrder" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h3>&nbsp;&nbsp; Для того, чтобы заказать печать файлов, пожалуйста, заполните форму:</h3>
    <br />
    &nbsp;&nbsp;
    Укажите необходимое количество копий:<br />
    &nbsp;&nbsp;
    <asp:TextBox ID="TextBox4" runat="server"></asp:TextBox>
    <br />
    &nbsp;&nbsp;
    Укажите формат печати:<br />
    &nbsp;
    <asp:TextBox ID="TextBox5" runat="server"></asp:TextBox>
    <br />
    &nbsp;&nbsp;
    Укажите необходимые страницы:<br />
    &nbsp;
    <asp:TextBox ID="TextBox6" runat="server"></asp:TextBox>
    <br />
    <br />
    &nbsp;&nbsp;
    Прикрепите файл:<br />
    <asp:FileUpload ID="FileUpload1" runat="server" Width="295px" />
    <br />
    &nbsp;
    <asp:Button ID="Button1" runat="server" Text="Отправить на печать" Width="300px" OnClick="Button1_Click" />
</asp:Content>
