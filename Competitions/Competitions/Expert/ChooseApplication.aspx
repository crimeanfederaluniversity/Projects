<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ChooseApplication.aspx.cs" Inherits="Competitions.Expert.Applications" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <br />
    <asp:Button ID="GoBackButton" runat="server" OnClick="GoBackButton_Click" Text="Назад" />
    <br />
    <br />
    Список заявок<asp:GridView ID="ApplicationGV" runat="server" AutoGenerateColumns="False">
            <Columns>
                <asp:BoundField DataField="ID"   HeaderText="Код заявки" Visible="true" />
                <asp:BoundField DataField="Name"   HeaderText="Название" Visible="true" />
                <asp:BoundField DataField="Description"   HeaderText="Описание" Visible="false" />
                <asp:BoundField DataField="Competition"   HeaderText="Конкурс" Visible="true" />
                <asp:BoundField DataField="Autor"   HeaderText="Автор" Visible="true" />                
            </Columns>
        </asp:GridView>
</asp:Content>
