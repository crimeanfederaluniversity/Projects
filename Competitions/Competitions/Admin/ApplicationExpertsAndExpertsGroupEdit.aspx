<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ApplicationExpertsAndExpertsGroupEdit.aspx.cs" MasterPageFile="~/Site.Master" Inherits="Competitions.Admin.ApplicationExpertsAndExpertsGroupEdit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    
    
    <br />
    <br />
    
    
    <asp:GridView ID="ExpertsGV" runat="server" AutoGenerateColumns="False" OnPreRender="ExpertsGV_PreRender">
                <Columns>
                    <asp:BoundField DataField="ExpertGroupId" HeaderText="" Visible="False" />
                    <asp:BoundField DataField="ExpertGroupName" HeaderText="" Visible="true" />
                    <asp:BoundField DataField="UserId" HeaderText="" Visible="False" />
                    <asp:BoundField DataField="UserName" HeaderText="" Visible="true" />
                    <asp:TemplateField  HeaderText="Добавить эксперта">
                        <ItemTemplate>
                            <asp:ImageButton ID="AddExpertButton" runat="server" CommandArgument='<%# Eval("UserId") %>' CommandName="Select" OnClick="AddExpertButtonClick" ImageUrl="~/Images/Add.png" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    
                    <asp:TemplateField HeaderText="Удалить экперта">
                        <ItemTemplate>
                            <asp:ImageButton ID="DeleteExpertButton" runat="server" CommandArgument='<%# Eval("UserId") %>' CommandName="Select" OnClick="DeleteExpertButtonClick" ImageUrl="~/Images/Delete.png" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    
                    <asp:TemplateField HeaderText="Добавить всех из экспертной группы">
                        <ItemTemplate>
                            <asp:ImageButton ID="AddExpertGroup" runat="server" CommandArgument='<%# Eval("ExpertGroupId") %>' CommandName="Select" AlternateText='<%# Eval("ExpertGroupId") %>' OnClick="AddExpertGroupClick" ImageUrl="~/Images/Add.png" />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>


</asp:Content>
