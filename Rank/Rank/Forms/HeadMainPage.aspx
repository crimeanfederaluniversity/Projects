<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="HeadMainPage.aspx.cs" Inherits="Rank.Forms.HeadMainPage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
          <h3>Информационно-аналитическая система личных кабинетов "Рейтинг научно-педагогических работников"</h3>  
          <h3><asp:Label ID="Label2" runat="server" Text="Результаты Вашего рейтинга как руководителя структурного подразделения за 2016 год:" Visible="true"></asp:Label>
&nbsp;<asp:Label ID="Label1" runat="server" Text="Label" Visible="true"></asp:Label>
          </h3>
          <br />
    <asp:Button ID="Button1" runat="server" Height="50px" OnClick="Button1_Click" Text="Мои рейтинговые данные" Width="550px" />
    &nbsp;&nbsp;&nbsp;
    <asp:Button ID="Button2" runat="server" Height="50px" OnClick="Button2_Click" Text="Просмотр и верификация данных работников подчиненного подразделения" Width="550px" />
&nbsp;<br />
    <br />
    </asp:Content>
