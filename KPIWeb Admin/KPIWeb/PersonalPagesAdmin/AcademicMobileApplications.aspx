<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" EnableEventValidation="false" CodeBehind="AcademicMobileApplications.aspx.cs" Inherits="KPIWeb.PersonalPagesAdmin.AcademicMobileApplications" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <p>
        <span style="font-size: 20px">Заявки пользователей на академическую мобильность:</span></p>
    <p>
        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="false">
            <Columns>
                <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle" HeaderText="Номер заявки" Visible="True">
                    <ItemTemplate>
                        <asp:Label ID="ID" runat="server" Text='<%# Bind("ID") %>' Visible="True"></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                </asp:TemplateField>
                <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle" HeaderText="Дата" Visible="True">
                    <ItemTemplate>
                        <asp:Label ID="Date" runat="server" BorderWidth="0" style="text-align:center" Text='<%# Bind("Date") %>'></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                </asp:TemplateField>
                <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle" HeaderText="От кого" Visible="True">
                    <ItemTemplate>
                        <asp:Label ID="FIO" runat="server" BorderWidth="0" style="text-align:center" Text='<%# Bind("FIO") %>'></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                </asp:TemplateField>
                  <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle" HeaderText="Персональные данные" Visible="True">
                    <ItemTemplate>
                        <asp:Label ID="Text2" runat="server" BorderWidth="0" style="text-align:center" Text='<%# Bind("Text2") %>'></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                </asp:TemplateField>
                <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle" HeaderText="Анкета" Visible="True">
                    <ItemTemplate>
                        <asp:Label ID="Text" runat="server" BorderWidth="0" style="text-align:center" Text='<%# Bind("Text") %>'></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                </asp:TemplateField>
            <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle" HeaderText="Документы" Visible="True">
                    <ItemTemplate>
                        <asp:Label ID="FileURL" runat="server" BorderWidth="0" style="text-align:center" Text='<%# Bind("FileURL") %>'></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                </asp:TemplateField>
                <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle" HeaderText="Телефон" Visible="True">
                    <ItemTemplate>
                        <asp:Label ID="TelephonNumber" runat="server" BorderWidth="0" style="text-align:center" Text='<%# Bind("TelephonNumber") %>'></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Принять">
                    <ItemTemplate>
                        <asp:Button ID="YesButton" runat="server"  CommandName="Select" OnClick="YesButtonClick" CommandArgument='<%# Eval("ID") %>' Text="Принять" Width="150px" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Отказать">
                    <ItemTemplate>
                        <asp:Button ID="NoButton" runat="server"  CommandName="Select" OnClick="NoButtonClick" CommandArgument='<%# Eval("ID") %>' Text="Отказать" Width="150px" />
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
        <span style="font-size: 30px">&nbsp;</span></p>
</asp:Content>
