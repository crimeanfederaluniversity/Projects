<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="WatchUniqueMarksInCompetition.aspx.cs" Inherits="Competitions.Admin.WatchUniqueMarksInCompetition" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    
    
    
    
    <br />
    <br />
    <br />
    <br />
    
    
    
    
    <asp:TextBox ID="UniqueMarksTextBox" TextMode="MultiLine" Width="600px" ReadOnly="True" runat="server"></asp:TextBox>
    <br />
    <asp:Button ID="GetUniqueMarksButton" runat="server" Text="Получить список уникальных меток в конкурсе" OnClick="GetUniqueMarksButton_Click" Width="603px" />

</asp:Content>
