<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="DocumentView.aspx.cs" Inherits="EDM.edm.DocumentView" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    
 

    <asp:Label ID="Label1" runat="server" Text="Документы"></asp:Label>
    <br />
   
    
    <asp:GridView ID="docGridView" runat="server" AutoGenerateColumns="False">
    </asp:GridView>
    
    <asp:Label ID="LabelComment" runat="server" Text="Label"></asp:Label>

    <br />

    <asp:Button ID="ApproveButton" runat="server" Text="Согласовать" OnClick="ApproveButton_Click" />
        <br />
    <asp:TextBox ID="CommentTextBox" runat="server" TextMode="MultiLine"></asp:TextBox>
    <br />
    <asp:Button ID="RejectButton" runat="server"  Text="Отправить на доработку" OnClick="RejectButton_Click" />
    
</asp:Content>
