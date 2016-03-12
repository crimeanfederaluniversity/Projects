<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="DocumentView.aspx.cs" Inherits="EDM.edm.DocumentView" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    
 

    <asp:Label ID="Label1" runat="server" Text="Документы"></asp:Label>
    <br />
   
    
    <asp:GridView ID="docGridView" runat="server" AutoGenerateColumns="False">
    </asp:GridView>
    
    <br />
    
    <asp:Label ID="LabelComment" runat="server" Text="Label"></asp:Label>

    <br />

    <asp:Button ID="ApproveButton" runat="server" Text="Согласовать" OnClick="ApproveButton_Click" />
        <br />
        <br />
    <asp:TextBox ID="CommentTextBox" runat="server" TextMode="MultiLine" Height="54px" Width="160px"></asp:TextBox>
    <br />
    <br />
    <asp:Button ID="RejectButton" runat="server" Text="Отправить на доработку" OnClick="RejectButton_Click" />
    
</asp:Content>
