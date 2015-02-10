<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RoleMapping.aspx.cs" Inherits="KPIWeb.WebForm1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
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
    </div>
    </form>
</body>
</html>
