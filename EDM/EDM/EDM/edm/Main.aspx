<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Main.aspx.cs" Inherits="EDM.edm.Main" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <asp:Button ID="Button4" runat="server" Text="Запустить новый процесс согласования" OnClick="Button4_Click" />
    <br />
    <asp:Button ID="Button1" runat="server" Text="Инициированные согласования" OnClick="Button1_Click" />
    <br />
    <asp:Button ID="Button2" runat="server" Text="Входящие" OnClick="Button2_Click" />
    <br />
    <asp:Button ID="Button3" runat="server" Text="Архив" OnClick="Button3_Click" />
</asp:Content>
