<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="ApplicationCreateEdit.aspx.cs" Inherits="Competitions.User.ApplicationCreateEdit" %>
<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
    <div>
    
        <asp:Button ID="GoBackButton" runat="server" OnClick="GoBackButton_Click" Text="Назад" />
        <br />
                <h2><span style="font-size: 30px">Создание новой заявки  </span></h2>
        <br />
        Название Вашего проекта <br />
        <asp:TextBox ID="ApplicationNameTextBox" runat="server" Height="38px" Width="484px"></asp:TextBox>
        <br />
        <br />
        <br />
        <asp:Button ID="CreateEditButton" runat="server" OnClick="CreateEditButton_Click" Text="Сохранить" />
    
    </div>
</asp:Content>