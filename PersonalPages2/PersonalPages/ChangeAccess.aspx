<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ChangeAccess.aspx.cs" Inherits="PersonalPages.ChangeAccess" MasterPageFile="~/Site.Master"%>

<asp:Content ContentPlaceHolderID="MainContent" runat="server">
    <h3>Изменения прав доступа к модулям системы личных кабинетов</h3>
    <asp:Label ID="Label1" runat="server" Text="Label" Visible="false"></asp:Label>
    <br />
    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False">
            <Columns>
            <asp:TemplateField HeaderText="ID" Visible ="false">
                                    <ItemTemplate>
                                        <asp:Label ID="ID" runat="server" Visible="True" Text='<%# Bind("ID") %>'></asp:Label>
                                    </ItemTemplate>
            </asp:TemplateField>
                 <asp:TemplateField HeaderText="Название модуля в системе">
                                    <ItemTemplate>
                                        <asp:Label ID="Name" runat="server" Visible="True" Text='<%# Bind("Name") %>'></asp:Label>
                                    </ItemTemplate>
            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Доступ">
                                    <ItemTemplate>
                                         <asp:CheckBox ID="EarlieAccess" Text=" " runat="server" Checked='<%# Bind("Access") %>'></asp:CheckBox>                          
                                    </ItemTemplate>
            </asp:TemplateField>
                    </columns>
        </asp:GridView>
    
    <br />
    
    <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Отправить заявку" Width="282px" />
    
</asp:Content>
