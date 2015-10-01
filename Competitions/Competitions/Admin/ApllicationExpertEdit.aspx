<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ApllicationExpertEdit.aspx.cs" Inherits="Competitions.Admin.ApllicationExpertEdit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    
    <br />
    <asp:Button ID="GoBackButton" runat="server" Text="Назад" OnClick="GoBackButton_Click" Width="157px" />
    <br />

    Прикрепленные эксперты
    <asp:GridView ID="connectedExpertsGV" runat="server" AutoGenerateColumns="False">
            <Columns>
                <asp:BoundField DataField="ID"   HeaderText="" Visible="false" />
                <asp:BoundField DataField="Name"   HeaderText="Имя" Visible="true" />
                <asp:TemplateField HeaderText="Удалить">
                        <ItemTemplate>
                            <asp:Button ID="ExpertDeleteButton" runat="server" CommandName="Select" Text="Удалить" CommandArgument='<%# Eval("ID") %>' Width="200px" OnClick="ExpertDeleteButtonClick"/>
                        </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    
    
    
    Все эксперты
    <asp:GridView ID="unconnectedExpertsGV" runat="server" AutoGenerateColumns="False">
            <Columns>
                <asp:BoundField DataField="ID"   HeaderText="" Visible="false" />
                <asp:BoundField DataField="Name"   HeaderText="Имя" Visible="true" />
                <asp:TemplateField HeaderText="Удалить">
                        <ItemTemplate>
                            <asp:Button ID="ExpertAddButton" runat="server" CommandName="Select" Text="Добавить" CommandArgument='<%# Eval("ID") %>' Width="200px" OnClick="ExpertAddButtonClick"/>
                        </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>

</asp:Content>
