<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AllStudGroups.aspx.cs" Inherits="PersonalPages.AllStudGroups" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <asp:Button ID="GoBackButton" runat="server" OnClick="GoBackButton_Click" Text="Назад" Width="157px" />
    <br />
    <br />Cписок всех&nbsp; групп факультета:
    <br />
    <asp:GridView ID="StudGroupGV" runat="server" AutoGenerateColumns="False">
        <Columns>
            <asp:BoundField DataField="ID" HeaderText="" Visible="false" />
            <asp:BoundField DataField="Name" HeaderText="Код группы" Visible="true" />
            <asp:TemplateField HeaderText="Состав группы">
                <ItemTemplate>
                    <asp:Button ID="GroupButton" runat="server" CommandArgument='<%# Eval("ID") %>' OnClick="GroupButtonClick" Text="Изменить" />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Удалить группу">
                <ItemTemplate>
                    <asp:Button ID="GroupDeleteButton" runat="server" CommandArgument='<%# Eval("ID") %>' OnClick="GroupDeleteButtonClick" Text="Удалить" />
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
    <br />Cоздать новую группу:<br />
    <asp:TextBox ID="TextBox1" runat="server" Width="259px"></asp:TextBox>
    <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Сохранить" Width="121px" />
</asp:Content>
