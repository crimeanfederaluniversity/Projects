<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" EnableViewStateMac="false" CodeBehind="RoleMapping.aspx.cs" Inherits="KPIWeb.WebForm1" %>

<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
     <h2>Форма распределения прав на базовые показатели</h2>
        <asp:DropDownList ID="DropDownList1" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged">
        </asp:DropDownList>
    <asp:GridView ID="GridviewRoles" runat="server" ShowFooter="true" AutoGenerateColumns="false">
                            <Columns>

                                <asp:TemplateField HeaderText="Ред|Просм">
                                    <ItemTemplate>
                                        <asp:Label ID="LabelRolesTableID" runat="server" Visible="false" Text='<%# Bind("Name") %>'></asp:Label>
                                        <asp:CheckBox ID="CheckBoxCanEdit" runat="server" Checked='<%# Bind("EditChecked") %>'></asp:CheckBox>
                                        <asp:CheckBox ID="CheckBoxCanView" runat="server" Checked='<%# Bind("ViewChecked") %>'></asp:CheckBox>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:BoundField DataField="Name" HeaderText="Роль" />

                            </Columns>
                        </asp:GridView>
        <br />
        <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Сохранить" Width="235px" Visible="False" />

</asp:Content>
