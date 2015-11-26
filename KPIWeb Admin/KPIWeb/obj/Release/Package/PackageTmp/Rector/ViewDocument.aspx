<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="ViewDocument.aspx.cs" Inherits="KPIWeb.Rector.ViewDocument" %>

<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
    
        <style type="text/css">
   .button_right 
   {
       float: right;
       margin-right: 10px;
   }     
</style>
                <asp:Panel runat="server" ID="top_panel2" CssClass="top_panel" Height="40" Visible="true">    
        <div>    
      <asp:Button ID="GoBackButton" runat="server" Enabled="False" Text="Назад" Width="125px" />
      <asp:Button ID="GoForwardButton" runat="server" Enabled="False" Text="Вперед" Width="125px" />
        &nbsp; &nbsp; <asp:Button ID="Button2" runat="server" Text="На главную" Width="125px" OnClick="Button2_Click" />
        &nbsp; &nbsp;       
        <asp:Button ID="Button5" runat="server" CssClass="button_right" Enabled="False" Text="Нормативные документы" Width="250px" />
            &nbsp; &nbsp; &nbsp; &nbsp;
        </div>

    </asp:Panel> 


        <br />


    <h2><span style="font-size: 30px">Просмотр нормативных документов</span></h2>
  
    <div>          
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
                           <asp:Button ID="DeleteButton" runat="server" CommandName="Select" Text="Просмотреть" Width="200px"  CommandArgument='<%# Eval("DocumentLink") %>' OnClick="DeleteButtonClick"  />
                        </ItemTemplate>
                    </asp:TemplateField>   
                      
                </Columns>
       </asp:GridView>
    
 </div>
</asp:Content>