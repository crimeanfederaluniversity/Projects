<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" EnableEventValidation ="false" CodeBehind="UserAccessPage.aspx.cs" Inherits="Chancelerry.Admin.UserAccessPage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
   
            <asp:Button ID="Button2" runat="server" Text="Назад" OnClick="Button2_Click" />
 
&nbsp;<br />
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
                                         <asp:CheckBox ID="Access" Text=" " runat="server" Checked='<%# Bind("Access") %>'></asp:CheckBox>                          
                                    </ItemTemplate>
            </asp:TemplateField>
                                 <asp:TemplateField HeaderText="Редактирование">
                                    <ItemTemplate>
                                         <asp:CheckBox ID="Edit" Text=" " runat="server" Checked='<%# Bind("Edit") %>'></asp:CheckBox>                          
                                    </ItemTemplate>
            </asp:TemplateField>
                    </columns>
        </asp:GridView>
        <br />
        <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Сохранить" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="TableContent" runat="server">
</asp:Content>
