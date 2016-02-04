<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CardOrder.aspx.cs" Inherits="PersonalPages.CardOrder" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
         <h3>Для того, чтобы заказать печать визиток, пожалуйста, заполните форму:</h3>

         <br />
         Укажите необходимое количество:<br />
    <asp:TextBox ID="TextBox4" runat="server"></asp:TextBox>
         <br />
         <br />
         Прикрепите файл с дизайном Вашей визитки:<br />
         <asp:FileUpload ID="FileUpload1" runat="server" />
         <br />
    <asp:Button ID="Button1" runat="server" Text="Отправить заказ" Width="300px" />


</asp:Content>
