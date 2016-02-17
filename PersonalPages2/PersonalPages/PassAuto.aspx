<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="PassAuto.aspx.cs" Inherits="PersonalPages.PassAuto" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
  
     <h3>&nbsp;&nbsp; Для того, чтобы заказать автомобильный пропуск,пожалуйста, заполните форму:</h3>


    &nbsp;&nbsp;&nbsp;


    Укажите госномер Вашего транспортного средства:<br />
    &nbsp;
    <asp:TextBox ID="TextBox4" runat="server" Width="193px"></asp:TextBox>
     <br />
    &nbsp;&nbsp;
    Укажите Ваш номер телефона:<br />
    &nbsp;
    <asp:TextBox ID="TextBox3" runat="server" Width="192px"></asp:TextBox>
    <br />
    <br />
    &nbsp;&nbsp;
    <asp:Button ID="Button1" runat="server" Text="Отправить заявку" Width="300px" OnClick="Button1_Click" />


</asp:Content>
