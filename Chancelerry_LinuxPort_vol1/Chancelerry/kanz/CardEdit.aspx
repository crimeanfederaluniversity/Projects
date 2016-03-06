<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CardEdit.aspx.cs" Inherits="Chancelerry.kanz.CardEdit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <link href="../Content/Site.css" rel="stylesheet" />
    <script src="calendar_ru.js" type="text/javascript"></script>
    <script src="toggleLoadingScreen.js" type="text/javascript"></script>
    <script>
        function putValueAndClose(val, fieldId, panelId) {
            document.getElementById(fieldId).value = val;
            document.getElementById(panelId).style.visibility = 'hidden';
        }
    </script>
    <div id="cardMainDiv" runat="server">
    <asp:LinkButton ID="LinkButton1" runat="server" OnClick="LinkButton1_Click" Visible="False">Версия для печати</asp:LinkButton>
    </div>
    <br />
    <asp:Button ID="CreateButton" runat="server" Text="Сохранить" OnClick="CreateButton_Click" Width="100%" />
</asp:Content>
