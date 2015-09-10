<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CompetitionExpertEdit.aspx.cs" Inherits="Competitions.Admin.CompetitionExpertEdit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">  
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
