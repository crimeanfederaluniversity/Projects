<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ViewDocumentsMain.aspx.cs" Inherits="KPIWeb.Rector.ViewDocumentsMain" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    
    <style type="text/css">
   .button_right 
   {
       float: right;
       margin-right: 10px;
   }     
</style>
                <asp:Panel runat="server" ID="top_panel2" CssClass="top_panel" Height="40" Visible="true">    
        <div>    
      <asp:Button ID="GoBackButton" runat="server" Text="Назад" Width="125px" OnClick="GoBackButton_Click1" />
      <asp:Button ID="Button2" runat="server" Text="На главную" Width="125px" OnClick="Button2_Click" />
        </div>

    </asp:Panel> 
    <br />
    <h2><span style="font-size: 30px">Просмотр нормативных документов</span></h2>

    <asp:GridView ID="GridView1" AutoGenerateColumns="False" runat="server" >           
             <Columns>               
                            
                 <asp:TemplateField HeaderText="">
                        <ItemTemplate>  
                            <asp:LinkButton ID="ViewDocsButton" runat="server" CommandArgument='<%# Eval("DocTypeId") %>' OnClick="ViewDocumentClick" Text='<%# Eval("DocTypeName") %>' ></asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>   
                      
                </Columns>
       </asp:GridView>

</asp:Content>
