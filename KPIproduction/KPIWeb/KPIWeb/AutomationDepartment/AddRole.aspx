<%@ Page Language="C#" Title="Добавление роли" CodeBehind="AddRole.aspx.cs" MasterPageFile="~/Site.Master"  Inherits="KPIWeb.AutomationDepartment.AddRole"  AutoEventWireup="true" EnableViewStateMac="false" %>
<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
    <div>
    
        <span style="font-size: 30pt">Создание новой роли</span><br />
    
    </div>
        Название роли
        <asp:TextBox ID="TextBox1" runat="server" Width="139px"></asp:TextBox>
&nbsp;Активна
        <asp:CheckBox ID="CheckBox1" runat="server" />
&nbsp;&nbsp;
        <asp:Button ID="Button1" runat="server" Text="Создать" Width="63px" OnClick="Button1_Click" />
        <br />
        _________________________________________________________________<br />
        <br />
        Изменение роли<br />
        _________________________________________________________________<br />
        <br />
        Привязка показателей к роли<br />
        <asp:DropDownList ID="DropDownList1" runat="server" Height="28px" Width="328px" OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged" AutoPostBack="True">
        </asp:DropDownList>
        <asp:Button ID="Button2" runat="server" Text="Загрузить в таблицу" Width="169px" OnClick="Button2_Click" />
        <br />
        <br />
        <asp:Button ID="Button3" runat="server" OnClick="Button3_Click" Text="Сохранить изменения" Width="497px" />
        <br />
        <br />
        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False">
            <Columns>
            <asp:TemplateField HeaderText="Название">
                                    <ItemTemplate>
                                        <asp:Label ID="Label2" runat="server" Visible="False" Text='<%# Bind("BasicId") %>'></asp:Label>
                                        <asp:Label ID="Label1" runat="server" Visible="True" Text='<%# Bind("Name") %>'></asp:Label>
                                    </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Ред">
                                    <ItemTemplate>
                                        <asp:CheckBox ID="CheckBoxCanEdit" runat="server" Checked='<%# Bind("EditChecked") %>'></asp:CheckBox>                                                                                                                   
                                    </ItemTemplate>
            </asp:TemplateField>
                <asp:TemplateField HeaderText="Просм">
                                    <ItemTemplate>
                                         <asp:CheckBox ID="CheckBoxCanView" runat="server" Checked='<%# Bind("ViewChecked") %>'></asp:CheckBox>                          
                                    </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Подтв">
                                    <ItemTemplate>
                                         <asp:CheckBox ID="CheckBoxVerify" runat="server"  Checked='<%# Bind("VerifyChecked") %>'></asp:CheckBox>                        
                                    </ItemTemplate>
            </asp:TemplateField>
            </Columns>
        </asp:GridView>
</asp:Content>