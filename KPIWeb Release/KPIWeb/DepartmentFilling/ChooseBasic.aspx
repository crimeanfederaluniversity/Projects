<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="ChooseBasic.aspx.cs" Inherits="KPIWeb.DepartmentFilling.ChooseBasic" %>
<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <asp:GridView ID="GridView1" AutoGenerateColumns="false" runat="server">
         <Columns>
        <asp:BoundField DataField="id"   HeaderText="Код показателя" Visible="true" />
        <asp:BoundField DataField="basicName"   HeaderText="Название показателя" Visible="true" />
        
        <asp:TemplateField HeaderText="Ввод данных">
                        <ItemTemplate>
                            <asp:Button ID="ButtonEditReport" runat="server" CommandName="Select" Text="Редактировать" Width="200px" CommandArgument='<%# Eval("id") %>' OnClick="ButtonEditClick"/>
                        </ItemTemplate>
        </asp:TemplateField>
         <asp:BoundField DataField="status"   HeaderText="Статус данных" Visible="true" />
              </Columns>
    </asp:GridView>
</asp:Content>