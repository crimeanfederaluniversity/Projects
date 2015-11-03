<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ApplicationExpertsAndExpertsGroupEdit.aspx.cs" EnableEventValidation="false" MasterPageFile="~/Site.Master" Inherits="Competitions.Admin.ApplicationExpertsAndExpertsGroupEdit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <asp:Button ID="GoBackButton" runat="server" OnClientClick="showLoadPanel()" Text="Назад" Width="125px" OnClick="GoBackButton_Click" />
    
    <br />
    <h2><span style="font-size: 20px">Экспертные группы и привлеченные эксперты:</span></h2>
    <br />   
    <asp:GridView ID="ExpertsGV" runat="server" AutoGenerateColumns="False" OnPreRender="ExpertsGV_PreRender" OnRowDataBound="GridView1_RowDataBound">
                <Columns>
                    <asp:BoundField DataField="ExpertGroupId" HeaderText="" Visible="False" />
                    <asp:BoundField DataField="ExpertGroupName" HeaderText="Принадлежность к группе" Visible="true" />
                    <asp:BoundField DataField="UserId" HeaderText="" Visible="False" />
                    <asp:BoundField DataField="UserName" HeaderText="Имя эксперта" Visible="true" />
                    <asp:TemplateField  HeaderText="Добавить эксперта">
                        <ItemTemplate>
                            <asp:ImageButton ID="AddExpertButton" runat="server" CommandArgument='<%# Eval("UserId") %>' CommandName="Select" OnClick="AddExpertButtonClick" ImageUrl="~/Images/Add.png" />
                           <asp:Label ID="StatusLabel" runat="server" Text="Прикреплен" Visible="False"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    
                    <asp:TemplateField HeaderText="Удалить экперта">
                        <ItemTemplate>
                            <asp:ImageButton ID="DeleteExpertButton" runat="server" CommandArgument='<%# Eval("UserId") %>' CommandName="Select" OnClick="DeleteExpertButtonClick" ImageUrl="~/Images/Delete.png" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    
                    <asp:TemplateField HeaderText="Добавить группу">
                        <ItemTemplate>
                            <asp:ImageButton ID="AddExpertGroup" runat="server" CommandArgument='<%# Eval("ExpertGroupId") %>' CommandName="Select" AlternateText='<%# Eval("ExpertGroupId") %>' OnClick="AddExpertGroupClick" ImageUrl="~/Images/Add.png" />
                            <asp:Label ID="GroupStatusLabel" runat="server" Text="Прикреплен" Visible="False"></asp:Label>
                              </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>

    
</asp:Content>
