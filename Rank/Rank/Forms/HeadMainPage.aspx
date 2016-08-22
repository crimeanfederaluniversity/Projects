<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="HeadMainPage.aspx.cs" Inherits="Rank.Forms.HeadMainPage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
          <h3>Добро пожаловать в систему "Рейтинги"</h3>  
          <h3><asp:Label ID="Label2" runat="server" Text="Результаты Вашего рейтинга как руководителя структурного подразделения за 2016 год:" Visible="true"></asp:Label>
&nbsp;<asp:Label ID="Label1" runat="server" Text="Label" Visible="true"></asp:Label>
          </h3>
          <br />
    <asp:Button ID="Button1" runat="server" Height="50px" OnClick="Button1_Click" Text="Индивидуальный рейтинг" Width="400px" />
    &nbsp;&nbsp;&nbsp;
    <asp:Button ID="Button2" runat="server" Height="50px" OnClick="Button2_Click" Text="Рейтинг структурного подразделения" Width="400px" />
&nbsp;<br />
    <br />
    </asp:Content>
