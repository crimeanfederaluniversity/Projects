<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CardOrder.aspx.cs" Inherits="PersonalPages.CardOrder" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
         <h3>Для того, чтобы заказать печать визиток, пожалуйста, заполните форму:</h3>

         <asp:Label ID="Label1" runat="server" Text="Label" Visible="false"></asp:Label>       
         <br />
         Укажите необходимое количество:<br />
    <asp:TextBox ID="TextBox4" runat="server" Height="25px" Width="200px"></asp:TextBox>
         <br />
         <br />
         Укажите Ваш телефон:<br />
    <asp:TextBox ID="TextBox5" runat="server" Height="25px" Width="200px"></asp:TextBox>
         <br />
         <br />
         Прикрепите файл с дизайном Вашей визитки:<br />
         <asp:FileUpload ID="FileUpload1" runat="server" Width="312px" />
         <br />
    <asp:Button ID="Button1" runat="server" Text="Отправить запрос" Width="300px" OnClick="Button1_Click" />


</asp:Content>
