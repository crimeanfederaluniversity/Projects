<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TeamUsers.aspx.cs" MasterPageFile="~/Site.Master" Inherits="Competition.TeamUsers" %>

 <asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent" >
     <div>
    
<h2>Участники</h2>
 
        <asp:GridView ID="GridView1" AutoGenerateColumns="false" runat="server">
             <Columns>
                  
                  <asp:TemplateField HeaderText="ФИО" HeaderStyle-HorizontalAlign="Center"  HeaderStyle-VerticalAlign="Middle" Visible = "true" >
                        <ItemTemplate> 
                            <asp:TextBox ID="PartnerName" runat="server" Text='<%# Bind("PartnerName") %>'  Visible="True"></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Функция в проекте" HeaderStyle-HorizontalAlign="Center"  HeaderStyle-VerticalAlign="Middle" Visible = "true" >
                        <ItemTemplate> 
                            <asp:TextBox ID="Functions" runat="server" Text='<%# Bind("Functions") %>'  Visible="True"></asp:TextBox>
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
    
    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
         <asp:Button ID="Button5" runat="server" OnClick="Button5_Click" Text="Далее" />
    
    </div>
  </asp:Content>    