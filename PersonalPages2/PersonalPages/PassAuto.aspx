<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="PassAuto.aspx.cs" Inherits="PersonalPages.PassAuto" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
  
     <h3>Для того, чтобы заказать автомобильный пропуск, пожалуйста, заполните данную форму:</h3>


     Укажите госномер Вашего транспортного средства:<br />
    <asp:TextBox ID="TextBox4" runat="server" Width="300px" Height="25px"></asp:TextBox>
     <br />
     Укажите Ваш номер телефона:<br />
    <asp:TextBox ID="TextBox3" runat="server" Width="300px" Height="25px"></asp:TextBox>
    <br />
    <br />
     &nbsp;<asp:Button ID="Button1" runat="server" Text="Отправить заявку" Width="300px" OnClick="Button1_Click" />


</asp:Content>
