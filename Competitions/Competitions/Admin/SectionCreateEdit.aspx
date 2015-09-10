<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="SectionCreateEdit.aspx.cs" Inherits="Competitions.Admin.SectionCreateEdit" %>
<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
    <div>
            <br />
            <asp:Button ID="GoBackButton" runat="server"  Text="Назад" Width="131px" style="height: 26px" OnClick="GoBackButton_Click" />
        <br />
        <br />
    Название
        <br />
        <asp:TextBox ID="NameTextBox" runat="server" Width="236px"></asp:TextBox>
        <br />
        Описание<br />
        <asp:TextBox ID="DescriptionTextBox" runat="server" style="margin-bottom: 0px" Width="232px"></asp:TextBox>
            <br />
            <br />
            Форма относится к<br />
            <asp:DropDownList ID="DropDownList1" runat="server" Height="16px" Width="240px">
            </asp:DropDownList>
            <br />
            Максимальное кол-во строк<br />
            <asp:TextBox ID="MaxRowCountTextBox" runat="server">1</asp:TextBox>
            <br />
            <br />
        <asp:Button ID="CreateSaveButton" runat="server" OnClick="CreateSaveButtonClick" Text="Сохранить" Width="131px" style="height: 26px" />
        <br />
    
    </div>
</asp:Content>