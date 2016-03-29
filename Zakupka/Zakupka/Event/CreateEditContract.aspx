<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" EnableEventValidation="false" AutoEventWireup="true" CodeBehind="CreateEditContract.aspx.cs" Inherits="Zakupka.Event.CreateEditContract" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
     <br />
    <asp:Button ID="Back" runat="server" Text="Назад" OnClick="Back_Click" />
     <br />
    <div id="TableDiv" runat="server">
        </div>
    <br />
    <asp:Button ID="Button1" runat="server" Text="Сохранить" OnClick="Button1_Click" />
</asp:Content>
