<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ApplicationExpertsAndExpertsGroupEdit.aspx.cs" EnableEventValidation="false" MasterPageFile="~/Site.Master" Inherits="Competitions.Admin.ApplicationExpertsAndExpertsGroupEdit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <asp:Button ID="GoBackButton" runat="server" OnClientClick="showLoadPanel()" Text="Назад" Width="125px" OnClick="GoBackButton_Click" />
    
    <br />
    <h2><span style="font-size: 20px">Экспертные группы и привлеченные эксперты:</span></h2>
    <br />   
    <asp:GridView ID="ExpertsGV" runat="server" AutoGenerateColumns="False" OnPreRender="ExpertsGV_PreRender" OnRowDataBound="GridView1_RowDataBound">
                <Columns>
                    <asp:BoundField DataField="ExpertGroupId" HeaderText="" Visible="False" />
                    <asp:BoundField DataField="ExpertGroupName" HeaderText="Группа/Эксперт" Visible="true" />
                    <asp:BoundField DataField="UserId" HeaderText="" Visible="False" />
                    <asp:BoundField DataField="UserName" HeaderText="Имя эксперта" Visible="true" />
                    <asp:TemplateField  HeaderText="Прикрепить">
                        <ItemTemplate>
                            <asp:ImageButton ID="AddExpertButton" runat="server" CommandArgument='<%# Eval("UserId") %>' CommandName="Select" OnClick="AddExpertButtonClick" ImageUrl="~/Images/Add.png" ImageAlign="Middle" Width="30px" />
                           <asp:Label ID="StatusLabel" runat="server" Text="Прикреплен" Visible="False"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    
                    <asp:TemplateField HeaderText="Открепить">
                        <ItemTemplate>
                            <asp:ImageButton ID="DeleteExpertButton" runat="server" CommandArgument='<%# Eval("UserId") %>' CommandName="Select" OnClick="DeleteExpertButtonClick" Width="30px" ImageUrl="~/Images/Delete.png" ImageAlign="Middle" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    
                    <asp:TemplateField HeaderText="Группа">
                        <ItemTemplate>
                            <asp:ImageButton ID="AddExpertGroup" runat="server" CommandArgument='<%# Eval("ExpertGroupId") %>' CommandName="Select" AlternateText='<%# Eval("ExpertGroupId") %>' OnClick="AddExpertGroupClick" Width="30px" ImageUrl="~/Images/Add.png" ImageAlign="Middle" />
                            <asp:ImageButton ID="DeleteGroupButton" runat="server" CommandArgument='<%# Eval("ExpertGroupId") %>' CommandName="Select" AlternateText='<%# Eval("ExpertGroupId") %>' OnClick="DeleteGroupButtonClick" Width="30px" ImageUrl="~/Images/Delete.png" ImageAlign="Middle" />
                            <asp:Label ID="GroupStatusLabel" runat="server" Text="Прикреплен" Visible="False"></asp:Label>
                              </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>

    
</asp:Content>
