<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CardEdit.aspx.cs" Inherits="Chancelerry.kanz.CardEdit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
  
    <script>
        function putValueAndClose(val, fieldId, panelId) {
            document.getElementById(fieldId).value = val;
            document.getElementById(panelId).style.visibility = 'hidden';
        }
    </script>

    <div id="cardMainDiv" runat="server">



    </div>
    <br />
    <asp:Button ID="CreateButton" runat="server" Text="Сохранить" OnClick="CreateButton_Click" Width="100%" />


</asp:Content>
