<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SmetaForm.aspx.cs" MasterPageFile="~/Site.Master" Inherits="Competition.SmetaForm" %>
<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent" >
 
    <h2><span style="font-size: 30px">Смета</span></h2>
 
        <asp:GridView ID="GridView1" AutoGenerateColumns="false" runat="server">
             <Columns>
                   <asp:TemplateField HeaderText="Номер" HeaderStyle-HorizontalAlign="Center"  HeaderStyle-VerticalAlign="Middle" Visible = "true" >
                        <ItemTemplate> 
                            <asp:Label ID="ID_Value" runat="server" Text='<%# Bind("ID_Value") %>'  Visible="True"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Наименование" HeaderStyle-HorizontalAlign="Center"  HeaderStyle-VerticalAlign="Middle" Visible = "true" >
                        <ItemTemplate> 
                            <asp:TextBox ID="Name_state" runat="server" Text='<%# Bind("Name_state") %>'  Visible="True"></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>
                 <asp:TemplateField HeaderText="Значение" HeaderStyle-HorizontalAlign="Center"  HeaderStyle-VerticalAlign="Middle" Visible = "true" >
                        <ItemTemplate> 
                            <asp:TextBox ID="Value" runat="server" Text='<%# Bind("Value") %>'  Visible="True"></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>
                 </Columns>
        </asp:GridView>
        <br />
        <asp:Button ID="Button1" runat="server" Text="Сохранить" Width="101px" OnClick="Button1_Click" />
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Button ID="Button2" runat="server" Text="Добавить строку" Width="128px" OnClick="Button2_Click" />
  &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
    <asp:Button ID="Button6" runat="server" OnClick="Button6_Click" Text="Далее" />
  </asp:Content>    
