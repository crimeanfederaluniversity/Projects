<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ChooseApplication.aspx.cs" Inherits="Competitions.Admin.Applications" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <br />
    <br />
    Список заявок<asp:GridView ID="ApplicationGV" runat="server" AutoGenerateColumns="False">
            <Columns>
                <asp:BoundField DataField="ID"   HeaderText="" Visible="false" />
                <asp:BoundField DataField="Name"   HeaderText="Название" Visible="true" />
                <asp:BoundField DataField="Description"   HeaderText="Описание" Visible="true" />
                <asp:BoundField DataField="Competition"   HeaderText="Конкурс" Visible="true" />
                <asp:BoundField DataField="Autor"   HeaderText="Автор" Visible="true" />            
                <asp:BoundField DataField="Experts"   HeaderText="Эксперты" Visible="true" />     
                <asp:TemplateField HeaderText="Изменить состав экспертов">
                        <ItemTemplate>
                            <asp:Button ID="ExpertChangeButton" runat="server" CommandName="Select" Text="Изменить" CommandArgument='<%# Eval("ID") %>' Width="200px" OnClick="ExpertChangeButtonClick"/>
                        </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
</asp:Content>
