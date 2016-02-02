<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="ChangeAccess.aspx.cs" Inherits="KPIWeb.AutomationDepartment.ChangeAccess" %>


<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False">
            <Columns>
            <asp:TemplateField HeaderText="ID">
                                    <ItemTemplate>
                                        <asp:Label ID="ID" runat="server" Visible="True" Text='<%# Bind("ID") %>'></asp:Label>
                                    </ItemTemplate>
            </asp:TemplateField>
                 <asp:TemplateField HeaderText="Name">
                                    <ItemTemplate>
                                        <asp:Label ID="Name" runat="server" Visible="True" Text='<%# Bind("Name") %>'></asp:Label>
                                    </ItemTemplate>
            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Права доступа в данный момент">
                                    <ItemTemplate>
                                         <asp:CheckBox ID="EarlieAccess" Text=" " runat="server" Checked='<%# Bind("EarlieAccess") %>'></asp:CheckBox>                          
                                    </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Желаемые права доступа">
                                    <ItemTemplate>
                                         <asp:CheckBox ID="AfterAccess" Text=" " runat="server"  Checked='<%# Bind("AfterAccess") %>'></asp:CheckBox>                        
                                    </ItemTemplate>
            </asp:TemplateField>
                    </columns>
        </asp:GridView>
    <br />
    <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Подтвердить изменениния" Width="281px" />
</asp:Content>
