 <%@ Page  Title="Форма редактирования базы пользователей" Language="C#" EnableViewStateMac="false" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="EditUser.aspx.cs" Inherits="KPIWeb.AutomationDepartment.EditUser" %>

<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
        <h2><%: Title %></h2>
    <div>
       <asp:Label ID="Label1" runat="server" Text="Пароль доступа"></asp:Label>

 &nbsp;

 <asp:TextBox runat="server" ID="TextBox1" TextMode="Password"  Width="325px" />
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Label ID="Label2" runat="server" Text="Ключевое слово"></asp:Label>
&nbsp;<asp:TextBox ID="TextBox2" runat="server" Height="21px" Width="251px"></asp:TextBox>
        &nbsp;&nbsp;<br />
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:CheckBox ID="CheckBox1" runat="server" AutoPostBack="True" OnCheckedChanged="CheckBox1_CheckedChanged1" Text="Показать пароли и email пользователей" />
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Button ID="Button1" runat="server" Text="Поиск" OnClick="Button1_Click" Width="173px" />
        <br />
        <br />
         
    
        <asp:GridView ID="GridView1" AutoGenerateColumns="False" runat="server">           
             <Columns>               
                           
                 <asp:TemplateField HeaderText="ID пользователя" HeaderStyle-HorizontalAlign="Center"   HeaderStyle-VerticalAlign="Middle" Visible = "True" >
                        <ItemTemplate> 
                            <asp:Label ID="FourthlvlId" runat="server" Text='<%# Bind("FourthlvlId") %>'  Visible="True"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>   
                 
                 

                   <asp:TemplateField HeaderText="Login" HeaderStyle-HorizontalAlign="Center"   HeaderStyle-VerticalAlign="Middle" Visible = "True" >
                        <ItemTemplate> 
                           <asp:TextBox ID="Login" style="text-align:center" BorderWidth="0" runat="server" Text='<%# Bind("Login") %>'></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>     

                     <asp:TemplateField HeaderText="Password"  HeaderStyle-HorizontalAlign="Center"   HeaderStyle-VerticalAlign="Middle" Visible = "True">
                        <ItemTemplate>
                            <asp:TextBox ID="Password" style="text-align:center" BorderWidth="0" runat="server" Text='<%# Bind("Password") %>'></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>   

                    <asp:TemplateField HeaderText="Email" HeaderStyle-HorizontalAlign="Center"   HeaderStyle-VerticalAlign="Middle" Visible = "True">
                        <ItemTemplate>
                            <asp:TextBox ID="Email" style="text-align:center" BorderWidth="0" runat="server" Text='<%# Bind("Email") %>'></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField> 
                     
                  <asp:TemplateField HeaderText="First_LVL" HeaderStyle-HorizontalAlign="Center"   HeaderStyle-VerticalAlign="Middle" Visible = "True">
                        <ItemTemplate>
                            <asp:TextBox ID="Firstlvl" style="text-align:center" BorderWidth="0" runat="server" Text='<%# Bind("Firstlvl") %>'></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField> 
                 
                  <asp:TemplateField HeaderText="Second_LVL" HeaderStyle-HorizontalAlign="Center"   HeaderStyle-VerticalAlign="Middle" Visible = "True">
                        <ItemTemplate>
                            <asp:TextBox ID="Secondlvl" style="text-align:center" BorderWidth="0" runat="server" Text='<%# Bind("Secondlvl") %>'></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>   
                 
                 <asp:TemplateField HeaderText="Third_LVL" HeaderStyle-HorizontalAlign="Center"   HeaderStyle-VerticalAlign="Middle" Visible = "True">
                        <ItemTemplate>
                            <asp:TextBox ID="Thirdlvl" style="text-align:center" BorderWidth="0" runat="server" Text='<%# Bind("Thirdlvl") %>'></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField> 
                 
                 <asp:TemplateField HeaderText="Fourth_LVL" HeaderStyle-HorizontalAlign="Center"   HeaderStyle-VerticalAlign="Middle" Visible = "True">
                        <ItemTemplate>
                            <asp:TextBox ID="Fourthlvl" style="text-align:center" BorderWidth="0" runat="server" Text='<%# Bind("Fourthlvl") %>'></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField> 
                 
                     <asp:TemplateField HeaderText="Fifth_LVL" HeaderStyle-HorizontalAlign="Center"   HeaderStyle-VerticalAlign="Middle" Visible = "True">
                        <ItemTemplate>
                            <asp:TextBox ID="Fifthlvl" style="text-align:center" BorderWidth="0" runat="server" Text='<%# Bind("Fifthlvl") %>'></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField> 
                 
                 <asp:TemplateField HeaderText="Acces_lvl" HeaderStyle-HorizontalAlign="Center"   HeaderStyle-VerticalAlign="Middle" Visible = "True">
                        <ItemTemplate>
                            <asp:TextBox ID="Acceslvl" style="text-align:center" BorderWidth="0" runat="server" Text='<%# Bind("Acceslvl") %>'></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField> 

                 <asp:TemplateField HeaderText="Zero_lvl" HeaderStyle-HorizontalAlign="Center"   HeaderStyle-VerticalAlign="Middle" Visible = "True">
                        <ItemTemplate>
                            <asp:TextBox ID="Zerolvl" style="text-align:center" BorderWidth="0" runat="server" Text='<%# Bind("Zerolvl") %>'></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField> 

                 <asp:TemplateField HeaderText="Удалить пользователя">
                        <ItemTemplate>
                            <asp:Button ID="DeleteUserButton" runat="server" CommandName="Select" Text="Удалить" Width="200px" CommandArgument='<%# Eval("FourthlvlId") %>' OnClick="DeleteUserButtonClick" />
                        </ItemTemplate>
                    </asp:TemplateField>   
                 
                 <asp:TemplateField HeaderText="Сохранить изменения">
                        <ItemTemplate>
                            <asp:Button ID="SaveUserButton" runat="server" CommandName="Select" Text="Сохранить" Width="200px" CommandArgument='<%# Eval("FourthlvlId") %>' OnClick="SaveUserButtonClick" />
                        </ItemTemplate>
                    </asp:TemplateField>  
                                           
       
                </Columns>
       </asp:GridView>
    
    </div>
</asp:Content>