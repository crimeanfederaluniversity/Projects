<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CardOrder.aspx.cs" Inherits="PersonalPages.CardOrder" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
         <h3>&nbsp;&nbsp; Для того, чтобы заказать печать визиток, пожалуйста, заполните форму:</h3>

         <br />
         &nbsp;&nbsp;
         Укажите необходимое количество:<br />
    &nbsp;&nbsp;
    <asp:TextBox ID="TextBox4" runat="server"></asp:TextBox>
         <br />
         <br />
         &nbsp;&nbsp;
         Укажите Ваш телефон:<br />
    &nbsp;&nbsp;
    <asp:TextBox ID="TextBox5" runat="server"></asp:TextBox>
         <br />
         <br />
         &nbsp;&nbsp;&nbsp;
         Прикрепите файл с дизайном Вашей визитки:<br />
         <asp:FileUpload ID="FileUpload1" runat="server" Width="312px" />
         <br />
    &nbsp;&nbsp;
    <asp:Button ID="Button1" runat="server" Text="Отправить запрос" Width="300px" OnClick="Button1_Click" />


</asp:Content>
