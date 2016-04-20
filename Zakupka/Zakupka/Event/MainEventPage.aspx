<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" EnableEventValidation ="false" AutoEventWireup="true" CodeBehind="MainEventPage.aspx.cs" Inherits="Zakupka.Event.MainEventPage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <br />
    <asp:Button ID="Back" runat="server" CssClass="btn btn-default" Text="Сводные данные" OnClick="Back_Click" />
    &nbsp;<asp:Button ID="CreateProject" runat="server" CssClass="btn btn-default" Text="Создать проект" OnClick="CreateProject_Click" />
    &nbsp;<asp:Button ID="CreateContract" runat="server" CssClass="btn btn-default" Text="Создать договор" OnClick="CreateContract_Click" />
    &nbsp;&nbsp;<br />
    <br />
    <p>
    
        <asp:GridView ID="GridView1" runat="server"  AutoGenerateColumns="False" Width="100%"  class="table table-striped edm-table edm-PocessEdit-table centered-block" OnRowCommand="GridView1_RowCommand" >
                    </asp:GridView>
    </p>

   
</asp:Content>
