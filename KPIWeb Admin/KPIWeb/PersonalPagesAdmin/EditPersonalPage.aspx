<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" EnableEventValidation="false" CodeBehind="EditPersonalPage.aspx.cs" Inherits="KPIWeb.PersonalPagesAdmin.EditPersonalPage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <p>
        <asp:Button ID="Button2" runat="server" OnClick="Button2_Click" Text="Назад" />
    </p>
    <p>
        <span style="font-size: 20px">Редактирование прав доступа пользователей к модулям:</span></p>

    <div>
       &nbsp;
        <asp:Label ID="Label2" runat="server" Text="Ключевое слово"></asp:Label>
&nbsp;<asp:TextBox ID="TextBox2" runat="server" Height="21px" Width="251px"></asp:TextBox>
        <asp:Button ID="Button1" runat="server" Text="Поиск" OnClick="Button1_Click" Width="173px" />
        &nbsp;&nbsp;&nbsp;<asp:CheckBox ID="CheckBox2" runat="server" Checked="True" Text="Предохранитель" />
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<br />
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <br />
         
    
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
                            <asp:Label ID="Email" style="text-align:center"  runat="server" Text='<%# Bind("Email") %>'></asp:Label>
                        </ItemTemplate>
                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle"></HeaderStyle>
                    </asp:TemplateField> 
                 <asp:TemplateField HeaderText="Имя" HeaderStyle-HorizontalAlign="Center"   HeaderStyle-VerticalAlign="Middle" Visible = "False">
                        <ItemTemplate>
                            <asp:Label ID="Name" style="text-align:center" runat="server" Text='<%# Bind("Name") %>'></asp:Label>
                        </ItemTemplate>
                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle"></HeaderStyle>
                    </asp:TemplateField> 
                 <asp:TemplateField HeaderText="Фамилия" HeaderStyle-HorizontalAlign="Center"   HeaderStyle-VerticalAlign="Middle" Visible = "False">
                        <ItemTemplate>
                            <asp:Label ID="SecondName" style="text-align:center"  runat="server" Text='<%# Bind("SecondName") %>'></asp:Label>
                        </ItemTemplate>
                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle"></HeaderStyle>
                    </asp:TemplateField>   
                 <asp:TemplateField HeaderText="Запрос на модуль" HeaderStyle-HorizontalAlign="Center"   HeaderStyle-VerticalAlign="Middle" Visible = "True">
                        <ItemTemplate>
                            <asp:Label ID="Module" style="text-align:center" runat="server" Text='<%# Bind("Module") %>'></asp:Label>
                        </ItemTemplate>
                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle"></HeaderStyle>
                    </asp:TemplateField>                                             
                 <asp:TemplateField HeaderText="Добавить права доступа">
                        <ItemTemplate>
                            <asp:Button ID="ChangeUserButton" runat="server" CommandName="Select" Text="Подтвердить" Width="200px" CommandArgument='<%# Eval("AddUserAccess") %>' OnClick="ChangeUserButtonClick" />
                        </ItemTemplate>
                    </asp:TemplateField>                
                 <asp:TemplateField HeaderText="Отказать в доступе">
                        <ItemTemplate>
                            <asp:Button ID="DeleteUserButton" runat="server" CommandName="Select" Text="Отклонить" Width="200px" CommandArgument='<%# Eval("DeleteUserAccess") %>' OnClick="DeleteUserButtonClick" />
                        </ItemTemplate>
                    </asp:TemplateField>         
                 <asp:TemplateField HeaderText="Просмотр общих сведений">
                        <ItemTemplate>
                            <asp:Button ID="UserCheck" runat="server" CommandName="Select" Text="Права доступа" Width="200px" CommandArgument='<%# Eval("UserCheck") %>' OnClick="UserCheckButtonClick" />
                        </ItemTemplate>
                    </asp:TemplateField>                                            
                </Columns>
       </asp:GridView>
    
    <p>
        &nbsp;</p>
    <p>
        &nbsp;</p>
</asp:Content>
