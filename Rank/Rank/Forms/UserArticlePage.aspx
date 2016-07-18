﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" EnableEventValidation ="false" CodeBehind="UserArticlePage.aspx.cs" Inherits="Rank.Forms.UserArticlePage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
        <div>
      
              <asp:Button ID="Button3" runat="server" Text="Назад" OnClick="Button3_Click" />
    
              <h3>Список всех пунктов в показателе  <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label></h3>
              <p>
                  <asp:Label ID="Label2" runat="server" Text="Label" Visible="False"></asp:Label>
              </p>
            <p>
                <asp:TextBox ID="TextBox1" runat="server" Height="20px" Visible="False" Width="450px"></asp:TextBox>
                <asp:Button ID="Button2" runat="server" OnClick="Button2_Click" Text="Добавить" Visible="False" />
          </p>
          <asp:GridView ID="GridView1" AutoGenerateColumns="false" runat="server"  >
             <Columns>
                  <asp:TemplateField HeaderText="" HeaderStyle-HorizontalAlign="Center"   HeaderStyle-VerticalAlign="Middle" Visible = "false" >
                        <ItemTemplate> 
                            <asp:Label ID="ID" runat="server" Text='<%# Bind("ID") %>'  Visible="false"></asp:Label>
                        </ItemTemplate>
               <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle"></HeaderStyle>
                    </asp:TemplateField> 

                <asp:TemplateField HeaderText="Название" HeaderStyle-HorizontalAlign="Center"   HeaderStyle-VerticalAlign="Middle" Visible = "True" >
                        <ItemTemplate> 
                            <asp:Label ID="Name" runat="server" Text='<%# Bind("Name") %>'  Visible="True"></asp:Label>
                        </ItemTemplate>
               <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle"></HeaderStyle>
                    </asp:TemplateField> 

                  <asp:TemplateField HeaderText="Дата добавления" HeaderStyle-HorizontalAlign="Center"   HeaderStyle-VerticalAlign="Middle" Visible = "True" >
                        <ItemTemplate> 
                            <asp:Label ID="Date" runat="server" Text='<%# Bind("Date") %>'  Visible="True"></asp:Label>
                        </ItemTemplate>
               <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle"></HeaderStyle>
                    </asp:TemplateField> 

                 <asp:TemplateField HeaderText="Текущий статус" HeaderStyle-HorizontalAlign="Center"   HeaderStyle-VerticalAlign="Middle" Visible = "True" >
                        <ItemTemplate> 
                            <asp:Label ID="Status" runat="server" Text='<%# Bind("Status") %>'  Visible="True"></asp:Label>
                        </ItemTemplate>
               <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle"></HeaderStyle>
                    </asp:TemplateField> 

                      <asp:TemplateField HeaderText="Перейти">
                        <ItemTemplate>
                            <asp:Button ID="EditButton" runat="server" CommandName="Select" Text="Перейти" Width="150px" CommandArgument='<%# Eval("ID") %>' OnClick="EditButtonClik" />
                        </ItemTemplate>
                    </asp:TemplateField>      
                    <asp:TemplateField HeaderText="Удалить">
                        <ItemTemplate>
                            <asp:Button ID="DeleteButton" runat="server" CommandName="Select" Text="Удалить" Width="150px" CommandArgument='<%# Eval("ID") %>' OnClick="DeleteButtonClik" OnClientClick ="return confirm('Вы уверены что хотите удалить?');"  />
                        </ItemTemplate>
                    </asp:TemplateField>                
                 </Columns>
        </asp:GridView>

    </div>
</asp:Content>
