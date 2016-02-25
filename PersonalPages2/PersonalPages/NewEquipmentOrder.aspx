<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="NewEquipmentOrder.aspx.cs" Inherits="PersonalPages.NewEquipmentOrder" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h3>Заявка на приобретение техники и расходных материалов:</h3>
    Необходимая техника или расходные материалы:<br />
    <asp:TextBox ID="TextBox4" runat="server" Width="400px"></asp:TextBox>
    <br />Цель использования:<br />
    <asp:TextBox ID="TextBox5" runat="server" Width="400px"></asp:TextBox>
    <br />
    Кто будет использовать:<br />
    <asp:TextBox ID="TextBox6" runat="server" Width="400px"></asp:TextBox>
    <br />
    Получатель (ответственный):<br />
    <asp:TextBox ID="TextBox7" runat="server" Width="400px"></asp:TextBox>
    <br />
    <br />
    <asp:Button ID="Button1" runat="server" Text="Отправить запрос" Width="300px" OnClick="Button1_Click" />
</asp:Content>
