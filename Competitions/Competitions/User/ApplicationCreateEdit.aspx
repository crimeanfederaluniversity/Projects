<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="ApplicationCreateEdit.aspx.cs" Inherits="Competitions.User.ApplicationCreateEdit" %>
<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
    <div>
    
        <asp:Button ID="GoBackButton" runat="server" OnClick="GoBackButton_Click" Text="Назад" />
        <br />
        <br />
        Название заявки<br />
        <asp:TextBox ID="ApplicationNameTextBox" runat="server"></asp:TextBox>
        <br />
        <br />
        Конкурс<br />
        <asp:DropDownList ID="ChooseCompetitionDropDownList" runat="server">
        </asp:DropDownList>
        <br />
        <br />
        <asp:Button ID="CreateEditButton" runat="server" OnClick="CreateEditButton_Click" Text="Сохранить" />
    
    </div>
</asp:Content>