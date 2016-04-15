<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Admin.aspx.cs" Inherits="Chancelerry.kanz.Admin" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <link href="../Content/Site.css" rel="stylesheet" />

    <br />
    <asp:Button ID="Button1" runat="server" Text="Button" OnClick="Button1_Click" />
    <br />
    <asp:TextBox ID="TextBox1" TextMode="MultiLine" runat="server" Height="143px" Width="757px"></asp:TextBox>
    <br />
    <br />
    <asp:Button ID="Button2" runat="server" Text="DOIT" Width="227px" OnClick="Button2_Click" />
<br />
</asp:Content>

