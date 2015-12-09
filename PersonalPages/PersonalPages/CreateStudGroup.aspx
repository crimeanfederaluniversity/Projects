<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CreateStudGroup.aspx.cs" Inherits="PersonalPages.CreateStudGroup" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <asp:Button ID="GoBackButton" runat="server" OnClick="GoBackButton_Click" Text="Назад" Width="152px" />
    <br />
    <br />
    Cтуденты в группе:<br />
    <asp:GridView ID="StudInGroupGV" runat="server" AutoGenerateColumns="False">
        <Columns>
            <asp:BoundField DataField="ID" HeaderText="" Visible="false" />
            <asp:BoundField DataField="Surname" HeaderText="Фамилия" Visible="true" />
            <asp:BoundField DataField="Name" HeaderText="Имя" Visible="true" />
            <asp:BoundField DataField="Patronimyc" HeaderText="Отчество" Visible="true" />
            <asp:BoundField DataField="Kurs" HeaderText="Курс" Visible="true" />
            <asp:TemplateField HeaderText="Удалить из группы">
                <ItemTemplate>
                    <asp:Button ID="StudentDeleteButton" runat="server" CommandArgument='<%# Eval("ID") %>' CommandName="Select" OnClick="StudentDeleteButtonClick" Text="Удалить" Width="200px" />
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
    <br />
    Оставшиеся студенты (без группы):<br />
    <asp:GridView ID="StudOutGroupGV" runat="server" AutoGenerateColumns="False">
        <Columns>
            <asp:BoundField DataField="ID" HeaderText="" Visible="false" />
            <asp:BoundField DataField="Surname" HeaderText="Фамилия" Visible="true" />
            <asp:BoundField DataField="Name" HeaderText="Имя" Visible="true" />
            <asp:BoundField DataField="Patronimyc" HeaderText="Отчество" Visible="true" />
            <asp:BoundField DataField="Kurs" HeaderText="Курс" Visible="true" />
            <asp:TemplateField HeaderText="Добавить студента в группу">
                <ItemTemplate>
                    <asp:Button ID="StudentAddButton" runat="server" CommandArgument='<%# Eval("ID") %>' CommandName="Select" OnClick="StudentAddButtonClick" Text="Добавить" Width="200px" />
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
</asp:Content>
