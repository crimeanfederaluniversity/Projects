﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="PassAuto.aspx.cs" Inherits="PersonalPages.PassAuto" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
  
     <h3>Для того, чтобы заказать автомобильный пропуск,пожалуйста, заполните форму:</h3>


    Укажите госномер Вашего транспортного средства:<br />
    <asp:TextBox ID="TextBox4" runat="server" Width="193px"></asp:TextBox>
     <br />
    Укажите Ваш номер телефона:<br />
    <asp:TextBox ID="TextBox3" runat="server" Width="192px"></asp:TextBox>
    <br />
    <br />
    <asp:Button ID="Button1" runat="server" Text="Отправить заявку" Width="300px" />


</asp:Content>
