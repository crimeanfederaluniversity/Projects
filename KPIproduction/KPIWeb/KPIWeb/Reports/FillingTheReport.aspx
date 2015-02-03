<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FillingTheReport.aspx.cs" Inherits="KPIWeb.Reports.FillingTheReport" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>

    <asp:GridView ID="gv1" runat="server" AutoGenerateColumns="False" 
                    OnRowEditing="gv1_RowEditing" 
                    OnRowUpdating="gv1_RowUpdating" CellPadding="4" ForeColor="#333333">
                    <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                    <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                    <Columns>
                    <asp:CommandField ShowEditButton="True" />
                        <asp:BoundField DataField="BasicParametersTableID" HeaderText="BasicParametersTableID" InsertVisible="False" 
                            ReadOnly="True" SortExpression="BasicParametersTableID" />

                        <asp:TemplateField HeaderText="sno" InsertVisible="False" SortExpression="sno">
                            <EditItemTemplate>
                                <asp:TextBox ID="TextBox6" runat="server" Visible='<%# IsInEditMode %>' Text='<%# Bind("BasicParametersTableID") %>'></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="Label6" runat="server" Visible='<%# !(bool) IsInEditMode %>' Text='<%# Bind("BasicParametersTableID") %>'></asp:Label>
                                <asp:TextBox ID="TextBox6" runat="server" Visible='<%# IsInEditMode %>' Text='<%# Bind("BasicParametersTableID") %>'></asp:TextBox>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="parameters" SortExpression="parameters">
                            <EditItemTemplate>
                                <asp:TextBox ID="TextBox2" runat="server" Visible='<%# IsInEditMode %>' Text='<%# Bind("Name") %>'></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="Label2" runat="server" Visible='<%# !(bool) IsInEditMode %>' Text='<%# Bind("Name") %>'></asp:Label>
                                <asp:TextBox ID="TextBox2" runat="server" Visible='<%# IsInEditMode %>' Text='<%# Bind("Name") %>'></asp:TextBox>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="unit" SortExpression="unit">
                            <EditItemTemplate>
                                <asp:TextBox ID="TextBox3" runat="server" Visible='<%# IsInEditMode %>' Text='<%# Bind("CollectedValue") %>'></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="Label3" runat="server" Visible='<%# !(bool) IsInEditMode %>' Text='<%# Bind("CollectedValue") %>'></asp:Label>
                                <asp:TextBox ID="TextBox3" runat="server" Visible='<%# IsInEditMode %>' Text='<%# Bind("CollectedValue") %>'></asp:TextBox>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="spval" SortExpression="spval">
                        </asp:TemplateField>
                    </Columns>
                    <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                    <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                    <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                    <EditRowStyle BackColor="#999999" />
                    <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                </asp:GridView>

    </div>
    </form>
</body>
</html>
