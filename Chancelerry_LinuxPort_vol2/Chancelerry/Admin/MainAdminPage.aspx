<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="MainAdminPage.aspx.cs" Inherits="Chancelerry.Admin.MainAdminPage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <br />
    <br />
    <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Управление пользователями" Width="500px" Height="60px" />
    <br />
    <br /> 
    <script src="../Scripts/jquery-1.10.2.min.js"></script>

    <asp:UpdatePanel ID="aStruct" runat="server">
       <ContentTemplate>
           Введите номер карты.<br />
           <asp:TextBox ID="CardIdTextBox" runat="server" Width="290px"></asp:TextBox>
           <br />
           <br />
        <asp:Button ID="aButton" runat="server" Text="Показать" OnClick="aButton_Click" Width="147px" />
           <br />
        <div id="findedDocsList" runat="server">
            
            </div>
       </ContentTemplate>
    </asp:UpdatePanel>

    </asp:Content>
