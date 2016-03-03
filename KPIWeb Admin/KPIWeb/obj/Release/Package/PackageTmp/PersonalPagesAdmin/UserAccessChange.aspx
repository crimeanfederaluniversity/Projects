<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="UserAccessChange.aspx.cs" Inherits="KPIWeb.PersonalPagesAdmin.UserAccessChange" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <p>
        <asp:Button ID="Button2" runat="server" OnClick="Button2_Click" Text="Назад" />
    </p>
    <p>
        <span>Редактирование прав доступа пользователей к модулям:</span></p>
    <div>
        &nbsp;
        <asp:Label ID="Label2" runat="server" Text="Ключевое слово"></asp:Label>
        &nbsp;<asp:TextBox ID="TextBox2" runat="server" Height="21px" Width="251px"></asp:TextBox>
        <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Поиск" Width="173px" />
        &nbsp;&nbsp;&nbsp;<asp:CheckBox ID="CheckBox2" runat="server" Checked="True" Text="Предохранитель" />
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<br />
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <br />
    </div>
    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="false">
        <Columns>
            <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle" HeaderText="ID пользователя" Visible="True">
                <ItemTemplate>
                    <asp:Label ID="UsersTableId" runat="server" Text='<%# Bind("UsersTableId") %>' Visible="True"></asp:Label>
                </ItemTemplate>
                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
            </asp:TemplateField>
            <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle" HeaderText="Адрес почты" Visible="True">
                <ItemTemplate>
                    <asp:TextBox ID="Email" runat="server" BorderWidth="0" style="text-align:center" Text='<%# Bind("Email") %>'></asp:TextBox>
                </ItemTemplate>
                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Изменить права доступа">
                <ItemTemplate>
                    <asp:Button ID="ChangeUserButton" runat="server" CommandArgument='<%# Eval("UsersTableId") %>' CommandName="Select" OnClick="ChangeUserButtonClick" Text="Изменить" Width="200px" />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Удалить пользователя">
                <ItemTemplate>
                    <asp:Button ID="DeleteUserButton" runat="server" CommandArgument='<%# Eval("UsersTableId") %>' CommandName="Select" OnClick="DeleteUserButtonClick" Text="Удалить" Width="200px" />
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
    <p>
        &nbsp;</p>
    <p>
        &nbsp;</p>
</asp:Content>
