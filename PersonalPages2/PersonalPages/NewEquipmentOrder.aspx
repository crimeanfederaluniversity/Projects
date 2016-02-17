<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="NewEquipmentOrder.aspx.cs" Inherits="PersonalPages.NewEquipmentOrder" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h3>&nbsp;&nbsp; Заявка на приобретение техники и расходных материалов:</h3>
    <br />&nbsp;&nbsp; Необходимая техника или расходные материалы:<br />
    &nbsp;
    <asp:TextBox ID="TextBox4" runat="server" Width="400px"></asp:TextBox>
    <br />&nbsp;&nbsp; Цель использования:<br />
    &nbsp;
    <asp:TextBox ID="TextBox5" runat="server" Width="400px"></asp:TextBox>
    <br />
    &nbsp;&nbsp;
    Кто будет использовать:<br />
    &nbsp;
    <asp:TextBox ID="TextBox6" runat="server" Width="400px"></asp:TextBox>
    <br />
    &nbsp;&nbsp;
    Получатель (ответственный):<br />
    &nbsp;
    <asp:TextBox ID="TextBox7" runat="server" Width="400px"></asp:TextBox>
    <br />
    <br />
    &nbsp;&nbsp;
    <asp:Button ID="Button1" runat="server" Text="Отправить запрос" Width="300px" OnClick="Button1_Click" />
</asp:Content>
