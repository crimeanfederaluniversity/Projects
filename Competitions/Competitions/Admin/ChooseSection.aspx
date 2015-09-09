<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="ChooseSection.aspx.cs" Inherits="Competitions.Admin.ChooseSection" %>
<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
    <div>
            <br />
            <asp:Button ID="GoBackButton" runat="server"  Text="Назад" Width="131px" style="height: 26px" OnClick="GoBackButton_Click" />
        <br />   
        <h2><span style="font-size: 20px">Формы для конкурса: </span></h2>
        <asp:Label ID="CompetitionNameLabel" style="font-size: 20px"  runat="server" Text=""> </asp:Label>
            <br />
            <br />
        <asp:Button ID="NewSection" runat="server" Text="Создать новый пункт" OnClick="NewSection_Click" />
    &nbsp;&nbsp;
            <asp:Button ID="GoToConstListManagmentButton" runat="server" OnClick="GoToConstListManagmentButton_Click" Text="Списки констант" />
            <br />
        <br />
        <asp:GridView ID="SectionGV" runat="server" AutoGenerateColumns="False">
            <Columns>
                <asp:BoundField DataField="ID"   HeaderText="" Visible="false" />
                <asp:BoundField DataField="Name"   HeaderText="Название" Visible="true" />
                <asp:BoundField DataField="Description"   HeaderText="Описание" Visible="true" />
                 <asp:TemplateField HeaderText="Открыть">
                        <ItemTemplate>
                            <asp:Button ID="OpenButton" runat="server" CommandName="Select" Text="Открыть" CommandArgument='<%# Eval("ID") %>' Width="200px" OnClick="OpenButtonClick"/>
                        </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Изменить">
                        <ItemTemplate>
                            <asp:Button ID="ChangeButton" runat="server" CommandName="Select" Text="Изменить" CommandArgument='<%# Eval("ID") %>' Width="200px" OnClick="ChangeButtonClick"/>
                        </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Удалить">
                        <ItemTemplate>
                            <asp:Button ID="DeleteButton" runat="server" CommandName="Select" Text="Удалить" CommandArgument='<%# Eval("ID") %>' Width="200px" OnClick="DeleteButtonClick"/>
                        </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
            <br />
    &nbsp;
            </div>
</asp:Content>