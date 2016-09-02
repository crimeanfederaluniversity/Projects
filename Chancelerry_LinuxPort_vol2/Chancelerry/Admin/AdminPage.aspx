<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" EnableEventValidation ="false" CodeBehind="AdminPage.aspx.cs" Inherits="Chancelerry.Admin.AdminPage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
   
      Добавление нового пользователя:<br />
      <asp:TextBox ID="TextBox1" runat="server" Width="300px"  placeholder="ФИО" ></asp:TextBox>
&nbsp;<asp:TextBox ID="TextBox2" runat="server" placeholder="Email"></asp:TextBox>
&nbsp;<asp:TextBox ID="TextBox3" runat="server"  placeholder="Логин"></asp:TextBox>
&nbsp;<asp:TextBox ID="TextBox4" runat="server"  placeholder="Пароль"></asp:TextBox>
      &nbsp;<asp:TextBox ID="TextBox5" runat="server" Width="250px"  placeholder="Структурное подразделение"></asp:TextBox>
      &nbsp;<asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Сохранить" />
      <br />
    <br />

    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns ="false">
         <Columns>                
                     <asp:TemplateField Visible="true"   HeaderText="ID">
                        <ItemTemplate>
                            <asp:Label ID="userID" runat="server"  Visible="true" Text='<%# Bind("userID") %>'   ></asp:Label> 
                               </ItemTemplate>
                    </asp:TemplateField>  
                    <asp:TemplateField Visible="true"   HeaderText="ФИО">
                        <ItemTemplate>
                            <asp:TextBox ID="name" runat="server"  Visible="true" Text='<%# Bind("name") %>'   ></asp:TextBox> 
                               </ItemTemplate>
                    </asp:TemplateField> 
                <asp:TemplateField Visible="true"   HeaderText="Структурное подразделение">
                        <ItemTemplate>
                            <asp:TextBox ID="struct" runat="server"  Visible="true" Text='<%# Bind("struct") %>'   ></asp:TextBox> 
                               </ItemTemplate>
                    </asp:TemplateField> 
                <asp:TemplateField Visible="true"   HeaderText="Email">
                        <ItemTemplate>
                            <asp:TextBox ID="email" runat="server"  Visible="true" Text='<%# Bind("email") %>'   ></asp:TextBox> 
                               </ItemTemplate>
                    </asp:TemplateField> 
              <asp:TemplateField Visible="true"   HeaderText="Логин">
                        <ItemTemplate>
                            <asp:TextBox ID="login" runat="server"  Visible="true" Text='<%# Bind("login") %>'   ></asp:TextBox> 
                               </ItemTemplate>
                    </asp:TemplateField> 
                  <asp:TemplateField Visible="true"   HeaderText="Пароль">
                        <ItemTemplate>
                            <asp:TextBox ID="pass" runat="server"  Visible="true" Text='<%# Bind("pass") %>'   ></asp:TextBox> 
                               </ItemTemplate>
                    </asp:TemplateField> 
              
                 <asp:TemplateField HeaderText="Сохранить">
                        <ItemTemplate>
                            <asp:Button ID="SaveButton" runat="server" CommandName="Select"  Text="Сохранить" Width="150px" CommandArgument='<%# Eval("userID") %>' OnClick="SaveButton_Click"/>
                        </ItemTemplate>
                    </asp:TemplateField>
                <asp:TemplateField HeaderText="Связь">
                        <ItemTemplate>
                            <asp:Button ID="EditButton" runat="server" CommandName="Select"  Text="Изменить" Width="150px" CommandArgument='<%# Eval("userID") %>' OnClick="EditButton_Click"/>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Удалить">
                        <ItemTemplate>
                            <asp:Button ID="DeleteButton" runat="server" CommandName="Select"  Text="Удалить" Width="150px" CommandArgument='<%# Eval("userID") %>' OnClick="DeleteButton_Click"/>
                        </ItemTemplate>
                    </asp:TemplateField>
              </Columns>  
    </asp:GridView>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="TableContent" runat="server">
</asp:Content>
