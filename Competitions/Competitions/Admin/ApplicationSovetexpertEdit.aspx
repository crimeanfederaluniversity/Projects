<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ApplicationSovetexpertEdit.aspx.cs" Inherits="Competitions.Admin.ApplicationSovetexpertEdit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
       <asp:Button ID="GoBackButton" runat="server" Text="Назад" OnClick="GoBackButton_Click" Width="157px" />
       <br />
    <br />
       Cписок всех экспертных групп:
            <br />
            <br />
            <asp:GridView ID="sovetExpertsGV" runat="server" AutoGenerateColumns="False" >
                <Columns>
                    <asp:BoundField DataField="ID" HeaderText="" Visible="false" />
                    <asp:BoundField DataField="Name" HeaderText="Имя" Visible="true" />

            <asp:TemplateField HeaderText="Редактировать">
                        <ItemTemplate>
                        <asp:Button ID="GroupButton" runat="server" CommandArgument='<%# Eval("ID") %>' OnClick="GroupButtonClick" Text="Изменить" />
                              </ItemTemplate>
                </asp:TemplateField>
                          </Columns>
            </asp:GridView>
       <br />
       Cоздать новую группу:<br />
       <asp:TextBox ID="TextBox1" runat="server" Width="259px"></asp:TextBox>
       <asp:Button ID="Button1" runat="server" Text="Сохранить" Width="121px" OnClick="Button1_Click" />
       <br />
</asp:Content>
