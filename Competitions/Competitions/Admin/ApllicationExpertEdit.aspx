<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ApllicationExpertEdit.aspx.cs" Inherits="Competitions.Admin.ApllicationExpertEdit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    
    <br />
    <asp:Button ID="GoBackButton" runat="server" Text="Назад" OnClick="GoBackButton_Click" Width="157px" />
    <br />

    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
     
      
            <br />
            Привязанные к заявке эксперты:<asp:GridView ID="connectedExpertsGV" runat="server" AutoGenerateColumns="False">
                <Columns>
                    <asp:BoundField DataField="ID" HeaderText="" Visible="false" />
                    <asp:BoundField DataField="Name" HeaderText="Имя" Visible="true" />
                    <asp:TemplateField HeaderText="Удалить">
                        <ItemTemplate>
                            <asp:Button ID="ExpertDeleteButton" runat="server" CommandArgument='<%# Eval("ID") %>' CommandName="Select" OnClick="ExpertDeleteButtonClick" Text="Удалить" Width="200px" />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
            <br />
            Список всех экспертов:
            <asp:GridView ID="unconnectedExpertsGV" runat="server" AutoGenerateColumns="False">
                <Columns>
                    <asp:BoundField DataField="ID" HeaderText="" Visible="false" />
                    <asp:BoundField DataField="Name" HeaderText="Имя" Visible="true" />
                    <asp:TemplateField HeaderText="Удалить">
                        <ItemTemplate>
                            <asp:Button ID="ExpertAddButton" runat="server" CommandArgument='<%# Eval("ID") %>' CommandName="Select" OnClick="ExpertAddButtonClick" Text="Добавить" Width="200px" />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
     
        
         
      
</asp:Content>
