<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PurchasePlan.aspx.cs" MasterPageFile="~/Site.Master" Inherits="Competition.PurchasePlan1" %>
 
<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent" >
 
    <h2><span style="font-size: 30px">План закупок </span></h2>
 
        <asp:GridView ID="GridView1" AutoGenerateColumns="false" runat="server">
             <Columns>
                   <asp:TemplateField HeaderText="Номер" HeaderStyle-HorizontalAlign="Center"  HeaderStyle-VerticalAlign="Middle" Visible = "true" >
                        <ItemTemplate> 
                            <asp:Label ID="ID_Purchase" runat="server" Text='<%# Bind("ID_Purchase") %>'  Visible="True"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Статья расходов" HeaderStyle-HorizontalAlign="Center"  HeaderStyle-VerticalAlign="Middle" Visible = "true" >
                        <ItemTemplate> 
                            <asp:TextBox ID="Purchase" runat="server" Text='<%# Bind("Purchase") %>'  Visible="True"></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>
                 <asp:TemplateField HeaderText="Единица измерения" HeaderStyle-HorizontalAlign="Center"  HeaderStyle-VerticalAlign="Middle" Visible = "true" >
                        <ItemTemplate> 
                            <asp:TextBox ID="Unit" runat="server" Text='<%# Bind("Unit") %>'  Visible="True"></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>
                  <asp:TemplateField HeaderText="Количество" HeaderStyle-HorizontalAlign="Center"  HeaderStyle-VerticalAlign="Middle" Visible = "true" >
                        <ItemTemplate> 
                            <asp:TextBox ID="Amount" runat="server" Text='<%# Bind("Amount") %>'  Visible="True"></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>
                  <asp:TemplateField HeaderText="Ориентировочная цена(руб.)" HeaderStyle-HorizontalAlign="Center"  HeaderStyle-VerticalAlign="Middle" Visible = "true" >
                        <ItemTemplate> 
                            <asp:TextBox ID="Price" runat="server" Text='<%# Bind("Price") %>'  Visible="True"></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>
                  <asp:TemplateField HeaderText="Ориентировочная сумма(руб.)" HeaderStyle-HorizontalAlign="Center"  HeaderStyle-VerticalAlign="Middle" Visible = "true" >
                        <ItemTemplate> 
                            <asp:TextBox ID="Sum" runat="server" Text='<%# Bind("Sum") %>'  Visible="True"></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>
                   
                 </Columns>
        </asp:GridView>
        <br />
    <br />
        <br />
        <asp:Button ID="Button1" runat="server" Text="Сохранить" Width="101px" OnClick="Button1_Click" />
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Button ID="Button2" runat="server" Text="Добавить строку" Width="128px" OnClick="Button2_Click" />
  &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
    <asp:Button ID="Button6" runat="server" OnClick="Button6_Click" Text="Далее" />
  </asp:Content>    
