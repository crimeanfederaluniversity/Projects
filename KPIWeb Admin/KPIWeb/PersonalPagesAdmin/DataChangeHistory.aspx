<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" EnableEventValidation ="false" CodeBehind="DataChangeHistory.aspx.cs" Inherits="KPIWeb.PersonalPagesAdmin.DataChangeHistory" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <p>
        <span style="font-size: 30px">История изменения учетных данных пользователя:</span></p>
    <p>
        <asp:GridView ID="GridView1" AutoGenerateColumns="false" runat="server">
             <Columns>
            <asp:TemplateField HeaderText="ID" HeaderStyle-HorizontalAlign="Center"   HeaderStyle-VerticalAlign="Middle" Visible = "False" >
                <ItemTemplate>
                    <asp:Label ID="UserChangeDataID" runat="server" Text='<%# Bind("UserChangeDataID") %>'  Visible="True"></asp:Label>
                </ItemTemplate>
                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle"></HeaderStyle>
            </asp:TemplateField>          
            <asp:TemplateField HeaderText="Изменившееся поле" HeaderStyle-HorizontalAlign="Center"   HeaderStyle-VerticalAlign="Middle" Visible = "True">
                <ItemTemplate>
                    <asp:TextBox ID="ID_Param_ToChange" style="text-align:center" BorderWidth="0" runat="server" Text='<%# Bind("ID_Param_ToChange") %>'></asp:TextBox>
                </ItemTemplate>
                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle"></HeaderStyle>
            </asp:TemplateField>
             <asp:TemplateField HeaderText="Значение" HeaderStyle-HorizontalAlign="Center"   HeaderStyle-VerticalAlign="Middle" Visible = "True">
                <ItemTemplate>
                    <asp:TextBox ID="Name" style="text-align:center" BorderWidth="0" runat="server" Text='<%# Bind("Name") %>'></asp:TextBox>
                </ItemTemplate>
                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle"></HeaderStyle>
            </asp:TemplateField>
                   <asp:TemplateField HeaderText="Дата изменения" HeaderStyle-HorizontalAlign="Center"   HeaderStyle-VerticalAlign="Middle" Visible = "True">
                <ItemTemplate>
                    <asp:TextBox ID="ChangeDate" style="text-align:center" BorderWidth="0" runat="server" Text='<%# Bind("ChangeDate") %>'></asp:TextBox>
                </ItemTemplate>
                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle"></HeaderStyle>
            </asp:TemplateField>
                   <asp:TemplateField HeaderText="Статус" HeaderStyle-HorizontalAlign="Center"   HeaderStyle-VerticalAlign="Middle" Visible = "True">
                <ItemTemplate>
                    <asp:TextBox ID="Status" style="text-align:center" BorderWidth="0" runat="server" Text='<%# Bind("Status") %>'></asp:TextBox>
                </ItemTemplate>
                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle"></HeaderStyle>
            </asp:TemplateField>
                   </Columns>
        </asp:GridView>
    </p>
</asp:Content>
