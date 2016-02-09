<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="EquipmentWriteOff.aspx.cs" Inherits="PersonalPages.EquipmentWriteOff" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h3>Заявка на cписание оборудования:</h3>
    <br />Название оборудования:<br />
    <asp:TextBox ID="TextBox4" runat="server" Width="400px"></asp:TextBox>
    <br />Инвентарный номер:<br />
    <asp:TextBox ID="TextBox5" runat="server" Width="400px"></asp:TextBox>
    <br />
    Ответственный:<br />
    <asp:TextBox ID="TextBox6" runat="server" Width="400px"></asp:TextBox>
    <br />
    Причина списания:<br />
    <asp:TextBox ID="TextBox7" runat="server" Width="400px"></asp:TextBox>
    <br />
    <br />
    <asp:Button ID="Button1" runat="server" Text="Отправить запрос" Width="300px" OnClick="Button1_Click" />
</asp:Content>
