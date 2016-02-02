<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" EnableEventValidation="false"CodeBehind="UsersChangeAccess.aspx.cs" Inherits="KPIWeb.AutomationDepartment.UsersChangeAccess" %>


<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
   
     <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False">
            <Columns>
            <asp:TemplateField HeaderText="ID">
                                    <ItemTemplate>
                                        <asp:Label ID="ID" runat="server" Visible="True" Text='<%# Bind("ID") %>'></asp:Label>
                                    </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Почта">
                                    <ItemTemplate>
                                        <asp:Label ID="Mail" runat="server" Visible="True"  Text='<%# Bind("Mail") %>'></asp:Label>                                                                                                                   
                                    </ItemTemplate>
            </asp:TemplateField>
                <asp:TemplateField HeaderText="Подразделение">
                                    <ItemTemplate>
                                         <asp:Label ID="From" runat="server" Visible="True" Text='<%# Bind("From") %>'></asp:Label>                            
                                    </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Перейти к заявке">
                        <ItemTemplate>
                            <asp:Button ID="UserButton" runat="server" Text="Перейти" Width="200px" CommandArgument='<%# Eval("UserButton") %>' OnClick="ChangeUserButtonClick" />
                        </ItemTemplate>
                    </asp:TemplateField> 
            </Columns>
        </asp:GridView>
</asp:Content>