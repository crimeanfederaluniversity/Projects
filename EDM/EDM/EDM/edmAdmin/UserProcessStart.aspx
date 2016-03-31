<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UserProcessStart.aspx.cs" MasterPageFile="~/Site.Master" Inherits="EDM.edmAdmin.UserProcessStart" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
   
        <br />
   
        <asp:Button ID="Button2" runat="server" Height="25px" OnClick="Button2_Click" Text="На главную" Width="203px" />
    
        <br />
    
        <br />
    
     <asp:GridView ID="GridView1" AutoGenerateColumns="false" runat="server" CssClass="table edm-table edm-history-table centered-block" Height="215px" Width="1427px">           
            
         <Columns>               
                           
                 <asp:TemplateField HeaderText="ID пользователя" HeaderStyle-HorizontalAlign="Center"   HeaderStyle-VerticalAlign="Middle" Visible = "True" >
                        <ItemTemplate> 
                            <asp:Label ID="ID" runat="server" Text='<%# Bind("ID") %>'  Visible="True"></asp:Label>
                        </ItemTemplate>

<HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle"></HeaderStyle>
                    </asp:TemplateField>   
                                   
                    <asp:TemplateField HeaderText="Адрес почты" HeaderStyle-HorizontalAlign="Center"   HeaderStyle-VerticalAlign="Middle" Visible = "True">
                        <ItemTemplate>
                            <asp:Label ID="Email" style="text-align:center" BorderWidth="0" runat="server" Text='<%# Bind("Email") %>'></asp:Label>
                        </ItemTemplate>
<HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle"></HeaderStyle>
                    </asp:TemplateField> 
             
             <asp:TemplateField HeaderText="Структура" HeaderStyle-HorizontalAlign="Center"   HeaderStyle-VerticalAlign="Middle" Visible = "True">
                        <ItemTemplate>
                            <asp:Label ID="Structure" style="text-align:center" BorderWidth="0" runat="server" Text='<%# Bind("Structure") %>'></asp:Label>
                        </ItemTemplate>
<HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle"></HeaderStyle>
                    </asp:TemplateField> 

                 <asp:TemplateField HeaderText="Удалить пользователя">
                        <ItemTemplate>
                            <asp:Button ID="DeleteUserButton" runat="server" CommandName="Select" Text="Удалить" Width="200px" CommandArgument='<%# Eval("DeleteUser") %>' OnClick="DeleteUserButtonClick" />
                        </ItemTemplate>
                    </asp:TemplateField>   
             </Columns>
         </asp:GridView>
    Добавление нового пользователя:
     <br />
     <br />
     Выберите структурное подразделение<br />
&nbsp;<asp:DropDownList ID="DropDownList1" runat="server" Height="22px" Width="276px">
    </asp:DropDownList>
     <br />
     <br />
    Укажите e-mail адрес
     <br />
     <asp:TextBox ID="TextBox1" runat="server" Height="24px" Width="274px"></asp:TextBox>
     <br />
     <br />
     <asp:Button ID="Button1" runat="server" Height="34px" OnClick="Button1_Click" Text="Добавить" Width="198px" />
</asp:Content>