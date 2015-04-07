<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="ViewDocument.aspx.cs" Inherits="KPIWeb.Rector.ViewDocument" %>

<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
    <h2><span style="font-size: 30px">Просмотр нормативных документов</span></h2>
  
    <div>          
        <br />
      
    <asp:GridView ID="GridView1" AutoGenerateColumns="False" runat="server" >           
             <Columns>               
                
                 <asp:TemplateField HeaderText="Имя файла" HeaderStyle-HorizontalAlign="Center"   HeaderStyle-VerticalAlign="Middle" Visible = "False" >
                        <ItemTemplate> 
                            <asp:Label ID="DocumentName" runat="server" Text='<%# Bind("DocumentName") %>'  Visible="True"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>   
                 
                  <asp:TemplateField HeaderText="Название для пользователей" HeaderStyle-HorizontalAlign="Center"   HeaderStyle-VerticalAlign="Middle" Visible = "True" >
                        <ItemTemplate> 
                            <asp:Label ID="DocumentLink" runat="server" Text='<%# Bind("DocumentLink") %>'  Visible="True"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>  
                
                 <asp:TemplateField HeaderText="Удалить">
                        <ItemTemplate>  
                           <asp:Button ID="DeleteButton" runat="server" CommandName="Select" Text="Просмотреть" Width="200px" CommandArgument='<%# Eval("DocumentLink") %>' OnClick="DeleteButtonClick"/>
                        </ItemTemplate>
                    </asp:TemplateField>   
                      
                </Columns>
       </asp:GridView>
    
 </div>
</asp:Content>