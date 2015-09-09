<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="ChooseColumn.aspx.cs" Inherits="Competitions.Admin.ChooseColumn" %>
<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
    <div>
            <br />
            <asp:Button ID="GoBackButton" runat="server"  Text="Назад" Width="131px" style="height: 26px" OnClick="GoBackButton_Click" />
        <br />
        <asp:Label ID="CompetitionNameLabel" style="font-size: 20px" runat="server"></asp:Label>
        <br />
        <asp:Label ID="SectionNameLeabel" style="font-size: 20px" runat="server"></asp:Label>
        <br />
    <asp:GridView ID="ColumnGV" runat="server" AutoGenerateColumns="False" >
            <Columns>
                <asp:BoundField DataField="ID"   HeaderText="" Visible="false" />
                <asp:BoundField DataField="Name"   HeaderText="Название" Visible="true" />
                <asp:BoundField DataField="Description"   HeaderText="Описание" Visible="true" />
                <asp:BoundField DataField="DataType"   HeaderText="Тип данных" Visible="true" />

                <asp:TemplateField HeaderText="Изменить">
                        <ItemTemplate>
                            <asp:Button ID="ChangeButton" runat="server" CommandName="Select" Text="Изменить" CommandArgument='<%# Eval("ID") %>' Width="200px" OnClick="ChangeButtonClick"/>
                        </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Удалить">
                        <ItemTemplate>
                            <asp:Button ID="DeleteButton" runat="server" CommandName="Select" Text="Удалить" CommandArgument='<%# Eval("ID") %>' Width="200px"  OnClick="DeleteButtonClick"/>
                        </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
        <br />
        <asp:Button ID="NewColumn" runat="server" OnClick="NewColumn_Click" Text="Новый столбец" />
    </div>
</asp:Content>