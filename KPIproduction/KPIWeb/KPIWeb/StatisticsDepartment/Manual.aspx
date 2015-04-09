<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="Manual.aspx.cs" Inherits="KPIWeb.StatisticsDepartment.Manual" %>

 
 <asp:Content runat="server" ID="Content1" ContentPlaceHolderID="MainContent">
    <h2><span style="font-size: 30px">Редактирование cправочников</span></h2>
  
    <div>          
        <br />
        <asp:GridView ID="GridView1" AutoGenerateColumns="False" runat="server" >           
             <Columns>               
                
                 <asp:TemplateField HeaderText="Имя файла" HeaderStyle-HorizontalAlign="Center"   HeaderStyle-VerticalAlign="Middle" Visible = "True" >
                        <ItemTemplate> 
                            <asp:Label ID="ManualName" runat="server" Text='<%# Bind("ManualName") %>'  Visible="True"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>   
                 
                  <asp:TemplateField HeaderText="Полное название" HeaderStyle-HorizontalAlign="Center"   HeaderStyle-VerticalAlign="Middle" Visible = "True" >
                        <ItemTemplate> 
                            <asp:Label ID="ManualLink" runat="server" Text='<%# Bind("ManualLink") %>'  Visible="True"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>  
                
                 <asp:TemplateField HeaderText="Удалить">
                        <ItemTemplate>  
                           <asp:Button ID="DeleteButton" runat="server" CommandName="Select" Text="Удалить" Width="200px" CommandArgument='<%# Eval("ManualID") %>' OnClick="DeleteButtonClick"/>
                        </ItemTemplate>
                    </asp:TemplateField>   
                      
                </Columns>
       </asp:GridView>
        <br />
        <br />
        <h3><span style="font-size: 30px">Добавление нового справочника</span></h3>
        <br />
  <asp:Label ID="Label1" runat="server" Text="Точное имя файла"></asp:Label>
        <br />
        <br />
        <asp:TextBox ID="TextBox1" runat="server" Width="258px"></asp:TextBox>
        <br />
        <br />
        <asp:Label ID="Label2" runat="server" Text="Полное название справочника"></asp:Label>
        <br />
        <br />
        <asp:TextBox ID="TextBox2" runat="server" Width="254px"></asp:TextBox>
        <br />
        <br />
        <asp:Button ID="Button1" runat="server" Text="Сохранить" OnClick="Button1_Click" />
    </div>
</asp:Content>