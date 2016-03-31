<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" EnableEventValidation="false" AutoEventWireup="true" CodeBehind="CreateEditContract.aspx.cs" Inherits="Zakupka.Event.CreateEditContract" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
   
     <br />
    <asp:Button ID="Back" runat="server" Text="Назад" CssClass="btn btn-default" OnClick="Back_Click" />
     <br />
    <script src="calendar_ru.js" type="text/javascript"></script>
    <div id="TableDiv" runat="server">
        </div>
    <br />
    <asp:Button ID="Button1" runat="server" Text="Сохранить" CssClass="btn btn-default" OnClick="Button1_Click" />
</asp:Content>
