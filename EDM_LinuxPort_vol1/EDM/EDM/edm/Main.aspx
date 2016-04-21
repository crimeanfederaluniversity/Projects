<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Main.aspx.cs" Inherits="EDM.edm.Main" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
  
    <style type="text/css">
    .top_panel {
    position:fixed;
    left:0;
    top:3.5em;
    width:100%;
    height:30px;
    background-color:#70463A !important;
    z-index:10;
    color:#05ff01;  
    padding-top:5px;
    font-weight:bold;
}
   .button_right 
   {
       float:right
   }     
</style> 
<asp:Panel runat="server" ID="top_panel2" CssClass="top_panel" Height="40" Visible="true">    
    <div>          
        &nbsp;&nbsp;&nbsp;&nbsp;<asp:Button ID="goBackButton" CausesValidation="false"  runat="server" Enabled="true" CssClass="btn btn-default" Text="Назад" Width="150" Height="30" />
        &nbsp;&nbsp;&nbsp;&nbsp;<asp:Button ID="goForwardButton" CausesValidation="false" Enabled="false" CssClass="btn btn-default" runat="server" Text="Вперед" Width="150" Height="30" OnClientClick="history.forward ()"/>
    </div>
</asp:Panel>
<br />
<br />
    
      <div class="row centered-content position-rel">

        <div class="row">
            <asp:Button ID="Button4" runat="server" Text="Запустить новый процесс согласования" OnClick="Button4_Click" CssClass="btn btn-default float-right" />
        </div>
        
        <div class="edm-main-content">
            
            <asp:Button ID="Button1" runat="server" Text="Инициированные согласования" OnClick="Button1_Click" CssClass="btn btn-default col-sm-3" />
    
            <asp:Button ID="Button2" runat="server" Text="Входящие" OnClick="Button2_Click" CssClass="btn btn-default col-sm-3" />
    
            <asp:Button ID="Button3" runat="server" Text="Архив" OnClick="Button3_Click" CssClass="btn btn-default col-sm-3" />
        </div>
    </div>
</asp:Content>
