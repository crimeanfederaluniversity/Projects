<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="UserChangePesonalPage.aspx.cs" Inherits="KPIWeb.PersonalPagesAdmin.UserChangePesonalPage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <p>
        <span style="font-size: 30px">Заявки пользователей на изменение учетных данных:</span></p>
    <div>
       &nbsp;
        <asp:Label ID="Label2" runat="server" Text="Ключевое слово"></asp:Label>
&nbsp;<asp:TextBox ID="TextBox2" runat="server" Height="21px" Width="251px"></asp:TextBox>
        <asp:Button ID="Button1" runat="server" Text="Поиск" OnClick="Button1_Click" Width="173px" />
        &nbsp;&nbsp;<br />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        </div>
    <asp:GridView ID="GridView1" AutoGenerateColumns="false" runat="server"  >
        <Columns>
            <asp:TemplateField HeaderText="ID пользователя" HeaderStyle-HorizontalAlign="Center"   HeaderStyle-VerticalAlign="Middle" Visible = "True" >
                <ItemTemplate>
                    <asp:Label ID="UsersTableId" runat="server" Text='<%# Bind("UsersTableId") %>'  Visible="True"></asp:Label>
                </ItemTemplate>
                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle"></HeaderStyle>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Адрес почты" HeaderStyle-HorizontalAlign="Center"   HeaderStyle-VerticalAlign="Middle" Visible = "True">
                <ItemTemplate>
                    <asp:TextBox ID="Email" style="text-align:center" BorderWidth="0" runat="server" Text='<%# Bind("Email") %>'></asp:TextBox>
                </ItemTemplate>
                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle"></HeaderStyle>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Изменение" HeaderStyle-HorizontalAlign="Center"   HeaderStyle-VerticalAlign="Middle" Visible = "True">
                <ItemTemplate>
                    <asp:Label ID="Name" style="text-align:center" BorderWidth="0" runat="server" Text='<%# Bind("Name") %>'></asp:Label>
                </ItemTemplate>
                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle"></HeaderStyle>
            </asp:TemplateField>
              <asp:TemplateField HeaderText="Дата" HeaderStyle-HorizontalAlign="Center"   HeaderStyle-VerticalAlign="Middle" Visible = "True">
                <ItemTemplate>
                    <asp:TextBox ID="ChangeDate" style="text-align:center" BorderWidth="0" runat="server" Text='<%# Bind("ChangeDate") %>'></asp:TextBox>
                </ItemTemplate>
                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle"></HeaderStyle>
            </asp:TemplateField>
           
            <asp:TemplateField HeaderText="Подтвердить изменения">
                <ItemTemplate>
                    <asp:Button ID="ConfirmButtonClick" runat="server" Text="Подтвердить" Width="200px" CommandArgument='<%# Eval("ParamIdToChange") %>' OnClick="ConfirmButtonClick" />
                </ItemTemplate>
            </asp:TemplateField>

             <asp:TemplateField HeaderText="Отказать в изменении">
                <ItemTemplate>
                    <asp:Button ID="DenyButton" runat="server" Text="Отказать" Width="200px" CommandArgument='<%# Eval("ParamIdToChange") %>' OnClick="DenyButtonClick" />
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
    <asp:CheckBox ID="CheckBox2" runat="server" Checked="True" Text="Предохранитель" />
    </asp:Content>
