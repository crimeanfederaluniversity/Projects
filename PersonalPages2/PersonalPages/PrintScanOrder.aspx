<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="PrintScanOrder.aspx.cs" Inherits="PersonalPages.PrintScanOrder" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h3>Для того, чтобы заказать печать файлов, пожалуйста, заполните форму:</h3>
    <br />
    Укажите необходимое количество копий:<br />
    <asp:TextBox ID="TextBox4" runat="server"></asp:TextBox>
    <br />
    <br />
    Прикрепите файл:<br />
    <asp:FileUpload ID="FileUpload1" runat="server" />
    <br />
    <asp:Button ID="Button1" runat="server" Text="Отправить на печать" Width="300px" />
</asp:Content>
