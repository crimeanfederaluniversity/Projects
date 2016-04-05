<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" EnableEventValidation="false" AutoEventWireup="true" CodeBehind="CreateEditContract.aspx.cs" Inherits="Zakupka.Event.CreateEditContract" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
   
    <asp:Button ID="Back" runat="server" Text="Назад" CssClass="btn btn-default" OnClick="Back_Click" />
     <h2>
         <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>

     </h2>
     <script src="calendar_ru.js" type="text/javascript"> </script>
    <div id="TableDiv" runat="server">
        </div>
    <br />
    <!-- javascript:Page_ClientValidate(); if (Page_IsValid==true) { this.disabled=true; } -->
    <asp:Button ID="Button1" runat="server" Text="Сохранить" CssClass="btn btn-default"  OnClientClick="showSimpleLoadingScreen();"  OnClick="Button1_Click" />
</asp:Content>
