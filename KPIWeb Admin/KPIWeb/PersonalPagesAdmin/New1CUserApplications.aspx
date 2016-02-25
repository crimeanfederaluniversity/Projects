<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="New1CUserApplications.aspx.cs" Inherits="KPIWeb.PersonalPagesAdmin.New1CUserApplications" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <p>
        <span style="font-size: 20px">Заявки пользователей на заведение нового пользователя в 1С:</span></p>
    <p>
        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="false">
            <Columns>
                <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle" HeaderText="Номер заявки" Visible="True">
                    <ItemTemplate>
                        <asp:Label ID="UsersTableId" runat="server" Text='<%# Bind("ID") %>' Visible="True"></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                </asp:TemplateField>
                <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle" HeaderText="Дата" Visible="True">
                    <ItemTemplate>
                        <asp:TextBox ID="Date" runat="server" BorderWidth="0" style="text-align:center" Text='<%# Bind("Date") %>'></asp:TextBox>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                </asp:TemplateField>
                <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle" HeaderText="От кого" Visible="True">
                    <ItemTemplate>
                        <asp:TextBox ID="FIO" runat="server" BorderWidth="0" style="text-align:center" Text='<%# Bind("FIO") %>'></asp:TextBox>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                </asp:TemplateField>
                <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle" HeaderText="Роль пользователя в системе" Visible="True">
                    <ItemTemplate>
                        <asp:TextBox ID="Text" runat="server" BorderWidth="0" style="text-align:center" Text='<%# Bind("Text") %>'></asp:TextBox>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                </asp:TemplateField>            
                <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle" HeaderText="Телефон" Visible="True">
                    <ItemTemplate>
                        <asp:TextBox ID="TelephonNumber" runat="server" BorderWidth="0" style="text-align:center" Text='<%# Bind("TelephonNumber") %>'></asp:TextBox>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Принять">
                    <ItemTemplate>
                        <asp:Button ID="YesButton" runat="server" CommandArgument='<%# Eval("ID") %>' CommandName="Select" OnClick="DeleteButtonClick" Text="Принять" Width="200px" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Отказать">
                    <ItemTemplate>
                        <asp:Button ID="NoButton" runat="server" CommandArgument='<%# Eval("ID") %>' CommandName="Select" OnClick="DeleteButtonClick" Text="Отказать" Width="200px" />
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
        <span style="font-size: 30px">&nbsp;</span></p>
</asp:Content>
