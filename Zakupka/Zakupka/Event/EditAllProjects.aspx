<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" EnableEventValidation="false" CodeBehind="EditAllProjects.aspx.cs" Inherits="Zakupka.Event.EditAllProjects" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h3>Cписок проектов:</h3>
    <p>
        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False">
            <Columns>
                <asp:TemplateField HeaderText="ID" Visible="false">
                    <ItemTemplate>
                        <asp:Label ID="ID" runat="server" Text='<%# Bind("ID") %>' Visible="True"></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Название проекта">
                    <ItemTemplate>
                        <asp:Label ID="Name" runat="server" Text='<%# Bind("Name") %>' Visible="True"></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Принадлежность к мероприятию">
                    <ItemTemplate>
                        <asp:Button ID="Access" runat="server" CommandName="Select"  Text="Редактировать" CommandArgument='<%# Eval("Id") %>' OnClick="EditButtonClick" />
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </p>
</asp:Content>
