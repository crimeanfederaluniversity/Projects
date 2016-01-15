<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ViewDocumentsSub.aspx.cs" Inherits="KPIWeb.Rector.ViewDocumentsSub" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">    
    <asp:Panel runat="server" ID="top_panel2" CssClass="top_panel" Height="40" Visible="true">    
        <div>    
      <asp:Button ID="GoBackButton" runat="server" Text="Назад" Width="125px" OnClick="GoBackButton_Click" />
      <asp:Button ID="Button2" runat="server" Text="На главную" Width="125px" OnClick="Button2_Click" />

        </div>

    </asp:Panel> 
      <br />
    <h2>
        <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>
    </h2>
  
        
        <br />
      
    <asp:GridView ID="GridView1" AutoGenerateColumns="False" runat="server" >           
             <Columns>               
                
                 <asp:TemplateField HeaderText="Название файла" HeaderStyle-HorizontalAlign="Center"   HeaderStyle-VerticalAlign="Middle" Visible = "True" >
                        <ItemTemplate> 
                            <asp:Label ID="DocumentName" runat="server" Text='<%# Bind("DocumentName") %>'  Visible="True"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>   
                 
                  <asp:TemplateField HeaderText="Название документа" HeaderStyle-HorizontalAlign="Center"   HeaderStyle-VerticalAlign="Middle" Visible = "False" >
                        <ItemTemplate> 
                            <asp:Label ID="DocumentLink" runat="server" Text='<%# Bind("DocumentLink") %>'  Visible="True"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>  
                
                 <asp:TemplateField HeaderText="Просмотреть">
                        <ItemTemplate>  
                           <asp:Button ID="OpenDoc" runat="server" CommandName="Select" Text="Просмотреть" Width="200px"  CommandArgument='<%# Eval("DocumentLink") %>' OnClick="OpenDocButtonClick"  />
                        </ItemTemplate>
                    </asp:TemplateField>   
                      
                </Columns>
       </asp:GridView>
</asp:Content>
