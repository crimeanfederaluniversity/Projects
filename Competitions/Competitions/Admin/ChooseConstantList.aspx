<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="ChooseConstantList.aspx.cs" Inherits="Competitions.Admin.ChooseConstantList" %>
<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
    <div>
    
        <br />
    
        <asp:Button ID="GoBackButton" runat="server" OnClick="GoBackButton_Click" Text="Назад" />
        <br />
        <asp:GridView ID="ConstantListsGV" AutoGenerateColumns="False" runat="server">
            <Columns>
                <asp:BoundField DataField="ID"   HeaderText="" Visible="false" />
                <asp:BoundField DataField="Name"   HeaderText="Название" Visible="true" />

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
        <asp:Button ID="CreateNewConstanListButton" runat="server" OnClick="CreateNewConstanListButton_Click" Text="Добавить лист констант" />
        <br />
    
    </div>
</asp:Content>