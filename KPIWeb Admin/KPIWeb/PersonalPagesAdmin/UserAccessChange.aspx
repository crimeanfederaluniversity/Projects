<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" EnableViewStateMac="false" CodeBehind="UserAccessChange.aspx.cs" Inherits="KPIWeb.PersonalPagesAdmin.UserAccessChange" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <p>
        <asp:Button ID="Button2" runat="server" OnClick="Button2_Click" Text="Назад" />
    </p>
    <p>
        <span style="font-size: 20px">База пользователей системы личных кабинетов:</span></p>
    <div>
        &nbsp;
        <asp:Label ID="Label2" runat="server" Text="Ключевое слово"></asp:Label>
        &nbsp;<asp:TextBox ID="TextBox2" runat="server" Height="21px" Width="251px"></asp:TextBox>
        <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Поиск" Width="173px" />
        &nbsp;&nbsp;&nbsp;<asp:CheckBox ID="CheckBox2" runat="server" Checked="True" Text="Предохранитель" />
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<br />
       
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
             <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle" HeaderText="Пароль" Visible="True">
                <ItemTemplate>
                    <asp:TextBox ID="Pass" runat="server" BorderWidth="0" style="text-align:center" Text='<%# Bind("Pass") %>'></asp:TextBox>
                </ItemTemplate>
                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
            </asp:TemplateField>
                  <asp:TemplateField HeaderText="Фамилия" HeaderStyle-HorizontalAlign="Center"   HeaderStyle-VerticalAlign="Middle" Visible = "True">
                        <ItemTemplate>
                            <asp:TextBox ID="Surname" style="text-align:center" BorderWidth="0" runat="server" Text='<%# Bind("Surname") %>'></asp:TextBox>
                        </ItemTemplate>

<HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle"></HeaderStyle>
                    </asp:TemplateField> 
                  <asp:TemplateField HeaderText="Имя" HeaderStyle-HorizontalAlign="Center"   HeaderStyle-VerticalAlign="Middle" Visible = "True">
                        <ItemTemplate>
                            <asp:TextBox ID="Name" style="text-align:center" BorderWidth="0" runat="server" Text='<%# Bind("Name") %>'></asp:TextBox>
                        </ItemTemplate>

<HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle"></HeaderStyle>
                    </asp:TemplateField> 
                  <asp:TemplateField HeaderText="Отчество" HeaderStyle-HorizontalAlign="Center"   HeaderStyle-VerticalAlign="Middle" Visible = "True">
                        <ItemTemplate>
                            <asp:TextBox ID="Patronimyc" style="text-align:center" BorderWidth="0" runat="server" Text='<%# Bind("Patronimyc") %>'></asp:TextBox>
                        </ItemTemplate>

<HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle"></HeaderStyle>
                    </asp:TemplateField> 
       <asp:TemplateField HeaderText="Уровень доступа" HeaderStyle-HorizontalAlign="Center"   HeaderStyle-VerticalAlign="Middle" Visible = "True">
                        <ItemTemplate>
                            <asp:TextBox ID="Acceslvl" Enabled="False" ReadOnly="True" style="text-align:center" BorderWidth="0" runat="server" Text='<%# Bind("Acceslvl") %>'></asp:TextBox>
                        </ItemTemplate>

<HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle"></HeaderStyle>
                    </asp:TemplateField>  
                     
                  <asp:TemplateField HeaderText="Первый уровень" HeaderStyle-HorizontalAlign="Center"   HeaderStyle-VerticalAlign="Middle" Visible = "True">
                        <ItemTemplate>
                            <asp:TextBox ID="Firstlvl" Enabled="False" ReadOnly="True" style="text-align:center" BorderWidth="0" runat="server" Text='<%# Bind("Firstlvl") %>'></asp:TextBox>
                        </ItemTemplate>

<HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle"></HeaderStyle>
                    </asp:TemplateField> 
                 
                  <asp:TemplateField HeaderText="Второй уровень" HeaderStyle-HorizontalAlign="Center"   HeaderStyle-VerticalAlign="Middle" Visible = "True">
                        <ItemTemplate>
                            <asp:TextBox ID="Secondlvl" Enabled="False" ReadOnly="True" style="text-align:center" BorderWidth="0" runat="server" Text='<%# Bind("Secondlvl") %>'></asp:TextBox>
                        </ItemTemplate>

<HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle"></HeaderStyle>
                    </asp:TemplateField>   
                 
                 <asp:TemplateField HeaderText="Третий уровень" HeaderStyle-HorizontalAlign="Center"   HeaderStyle-VerticalAlign="Middle" Visible = "True">
                        <ItemTemplate>
                            <asp:TextBox ID="Thirdlvl" Enabled="False" ReadOnly="True" style="text-align:center" BorderWidth="0" runat="server" Text='<%# Bind("Thirdlvl") %>'></asp:TextBox>
                        </ItemTemplate>

<HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle"></HeaderStyle>
                    </asp:TemplateField> 
 
            <asp:TemplateField HeaderText="Изменить права доступа">
                <ItemTemplate>
                    <asp:Button ID="ChangeUserButton" runat="server" CommandArgument='<%# Eval("UsersTableId") %>' CommandName="Select" OnClick="ChangeUserButtonClick" Text="Изменить" Width="150px" />
                </ItemTemplate>
            </asp:TemplateField>
             <asp:TemplateField HeaderText="Сохранить изменения">
                        <ItemTemplate>
                            <asp:Button ID="SaveUserButton" runat="server" CommandName="Select" Text="Сохранить" Width="150px" CommandArgument='<%# Eval("UsersTableId") %>' OnClick="SaveUserButtonClick" />
                        </ItemTemplate>
                    </asp:TemplateField>  
            <asp:TemplateField HeaderText="Удалить пользователя">
                <ItemTemplate>
                    <asp:Button ID="DeleteUserButton" runat="server" CommandArgument='<%# Eval("UsersTableId") %>' CommandName="Select" OnClick="DeleteUserButtonClick" Text="Удалить" Width="150px" />
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
    <p>
        &nbsp;</p>
    <p>
        &nbsp;</p>
</asp:Content>
