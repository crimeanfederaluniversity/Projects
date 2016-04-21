<%@ Page Language="C#" Title="Редактирование пользователей" AutoEventWireup="true"  MasterPageFile="~/Site.Master" EnableEventValidation="false" CodeBehind="EditUser.aspx.cs" Inherits="EDM.edmAdmin.ChangeUser" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div style="margin-top:auto">
            <br />
            <br />
            <asp:Button ID="Button2" runat="server" Height="25px" OnClick="Button2_Click" Text="На главную" Width="198px" />
    
            <br />
    
            <br />
    
    <asp:GridView ID="GridView1" AutoGenerateColumns="false" runat="server" CssClass="table edm-table edm-history-table centered-block" AllowPaging="true">           
            
         <Columns>               
                           
                 <asp:TemplateField HeaderText="ID пользователя" HeaderStyle-HorizontalAlign="Center"   HeaderStyle-VerticalAlign="Middle" Visible = "True" >
                        <ItemTemplate> 
                            <asp:Label ID="ID" runat="server" Text='<%# Bind("ID") %>'  Visible="True"></asp:Label>
                        </ItemTemplate>

<HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle"></HeaderStyle>
                    </asp:TemplateField>   
                                 
                   <asp:TemplateField HeaderText="Логин" HeaderStyle-HorizontalAlign="Center"   HeaderStyle-VerticalAlign="Middle" Visible = "True" >
                        <ItemTemplate> 
                           <asp:TextBox ID="Login" style="text-align:center" BorderWidth="0" runat="server" Text='<%# Bind("Login") %>'></asp:TextBox>
                        </ItemTemplate>

<HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle"></HeaderStyle>
                    </asp:TemplateField>     

                     <asp:TemplateField HeaderText="Пароль"  HeaderStyle-HorizontalAlign="Center"   HeaderStyle-VerticalAlign="Middle" Visible = "True">
                        <ItemTemplate>
                            <asp:TextBox ID="Password" style="text-align:center" BorderWidth="0" runat="server" Text='<%# Bind("Password") %>'></asp:TextBox>
                        </ItemTemplate>

<HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle"></HeaderStyle>
                    </asp:TemplateField>   

                    <asp:TemplateField HeaderText="Адрес почты" HeaderStyle-HorizontalAlign="Center"   HeaderStyle-VerticalAlign="Middle" Visible = "True">
                        <ItemTemplate>
                            <asp:TextBox ID="Email" style="text-align:center" BorderWidth="0" runat="server" Text='<%# Bind("Email") %>'></asp:TextBox>
                        </ItemTemplate>

<HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle"></HeaderStyle>
                    </asp:TemplateField> 
                     
               <asp:TemplateField HeaderText="Подразделение" HeaderStyle-HorizontalAlign="Center"   HeaderStyle-VerticalAlign="Middle" Visible = "True">
                        <ItemTemplate>
                            <asp:label ID="SecondLevel" BorderWidth="0" runat="server" Text='<%# Bind("SecondLevel") %>'></asp:label>
                        </ItemTemplate>

<HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle"></HeaderStyle>
                    </asp:TemplateField> 

                  <asp:TemplateField HeaderText="Структура" HeaderStyle-HorizontalAlign="Center"   HeaderStyle-VerticalAlign="Middle" Visible = "True">
                        <ItemTemplate>
                            <asp:label ID="Structure" BorderWidth="0" runat="server" Text='<%# Bind("Structure") %>'></asp:label>
                        </ItemTemplate>

<HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle"></HeaderStyle>
                    </asp:TemplateField> 
               
             <asp:TemplateField HeaderText="Может инициировать">
                        <ItemTemplate>
                            <asp:CheckBox ID="CanIniateCheck" runat="server" CommandName="Select" Text="" Width="200px" Checked='<%# Bind("CanIniate") %>'/>
                        </ItemTemplate>
                    </asp:TemplateField> 
             <asp:TemplateField HeaderText="Может создавать шаблоны">
                        <ItemTemplate>
                            <asp:CheckBox ID="CanCreateTemple" runat="server" CommandName="Select" Text="" Width="200px" Checked='<%# Bind("CanCreate") %>' />
                        </ItemTemplate>
                    </asp:TemplateField> 
             <asp:TemplateField HeaderText="Может создавать нешаблонные процессы">
                        <ItemTemplate>
                            <asp:CheckBox ID="CanIniateCustom" runat="server" CommandName="Select" Text="" Width="200px" Checked='<%# Bind("CanIniateCustom") %>'/>
                        </ItemTemplate>
                    </asp:TemplateField> 
             <asp:TemplateField HeaderText="Может печатать результат">
                        <ItemTemplate>
                            <asp:CheckBox ID="CanPrint" runat="server" CommandName="Select" Text="" Width="200px" Checked='<%# Bind("CanPrint") %>' />
                        </ItemTemplate>
                    </asp:TemplateField> 
                 <asp:TemplateField HeaderText="Удалить пользователя">
                        <ItemTemplate>
                            <asp:Button ID="DeleteUserButton" runat="server" CommandName="Select" Text="Удалить" Width="200px" CommandArgument='<%# Eval("DeleteUser") %>' OnClick="DeleteUserButtonClick" />
                        </ItemTemplate>
                    </asp:TemplateField>   
                 
                 <asp:TemplateField HeaderText="Сохранить изменения">
                        <ItemTemplate>
                            <asp:Button ID="SaveUserButton" runat="server" CommandName="Select" Text="Сохранить" Width="200px" CommandArgument='<%# Eval("SaveUser") %>' OnClick="SaveUserButtonClick" />
                        </ItemTemplate>
                    </asp:TemplateField>                  
                    
             <asp:TemplateField HeaderText="Изменить структуру">
                        <ItemTemplate>
                            <asp:Button ID="ChangeUserButton" runat="server" CommandName="Select" Text="Изменить" Width="200px" CommandArgument='<%# Eval("ChangeUser") %>' OnClick="ChangeUserButtonClick" />
                        </ItemTemplate>
                    </asp:TemplateField>                     
                </Columns>
       </asp:GridView>
        </div>
</asp:Content>