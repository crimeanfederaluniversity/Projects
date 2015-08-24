<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TeamUsers.aspx.cs" MasterPageFile="~/masterpage.Master" Inherits="Competition.TeamUsers" %>

 <asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="ContentPlaceHolder1" >
     <div>
    
<h2>Участники</h2>
 
        <asp:GridView ID="GridView1" AutoGenerateColumns="false" runat="server">
             <Columns>
                   <asp:TemplateField HeaderText="ФИО" HeaderStyle-HorizontalAlign="Center"  HeaderStyle-VerticalAlign="Middle" Visible = "true" >
                        <ItemTemplate> 
                            <asp:Label ID="Name" runat="server" Text='<%# Bind("Name") %>'  Visible="True"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Функция в проекте" HeaderStyle-HorizontalAlign="Center"  HeaderStyle-VerticalAlign="Middle" Visible = "true" >
                        <ItemTemplate> 
                            <asp:TextBox ID="Function" runat="server" Text='<%# Bind("Function") %>'  Visible="True"></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>
                 <asp:TemplateField HeaderText="Трудозатраты в час" HeaderStyle-HorizontalAlign="Center"  HeaderStyle-VerticalAlign="Middle" Visible = "true" >
                        <ItemTemplate> 
                            <asp:TextBox ID="PayPerHour" runat="server" Text='<%# Bind("PayPerHour") %>'  Visible="True"></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>
                 </Columns>
        </asp:GridView>
        <br />
        <asp:Button ID="Button1" runat="server" Text="Сохранить" Width="101px" OnClick="Button1_Click" />
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Button ID="Button2" runat="server" Text="Добавить строку" Width="128px" OnClick="Button2_Click" />
    
    </div>
  </asp:Content>    