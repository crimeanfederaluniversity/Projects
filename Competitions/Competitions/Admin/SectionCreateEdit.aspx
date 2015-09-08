<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="SectionCreateEdit.aspx.cs" Inherits="Competitions.Admin.SectionCreateEdit" %>
<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
    <div>
            <br />
            <asp:Button ID="GoBackButton" runat="server"  Text="Назад" Width="131px" style="height: 26px" OnClick="GoBackButton_Click" />
        <br />
        <br />
    Название
        <br />
        <asp:TextBox ID="NameTextBox" runat="server"></asp:TextBox>
        <br />
        <br />
        Описание<br />
        <asp:TextBox ID="DescriptionTextBox" runat="server" style="margin-bottom: 0px"></asp:TextBox>
        <br />
            <br />
            Максимальное кол-во строк<br />
            <asp:TextBox ID="MaxRowCountTextBox" runat="server">1</asp:TextBox>
            <br />
        <br />
        Сохранить<br />
        <asp:Button ID="CreateSaveButton" runat="server" OnClick="CreateSaveButtonClick" Text="Сохранить" Width="131px" style="height: 26px" />
        <br />
    
    </div>
</asp:Content>