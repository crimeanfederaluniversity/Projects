<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="TemplatesList.aspx.cs" Inherits="EDM.edm.TamplatesList" %>
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
        &nbsp;&nbsp;&nbsp;&nbsp;<asp:Button ID="goBackButton" CausesValidation="false"  runat="server" Enabled="true" CssClass="btn btn-default" Text="Назад" Width="150" Height="30" OnClientClick="showSimpleLoadingScreen()" OnClick="goBackButton_Click" />
        &nbsp;&nbsp;&nbsp;&nbsp;<asp:Button ID="goForwardButton" CausesValidation="false" Enabled="false" CssClass="btn btn-default" runat="server" Text="Вперед" Width="150" Height="30" OnClientClick="history.forward ()"/>
    </div>
</asp:Panel>
     <br />
    <br />
    <br />
    <div id="mainDiv" runat="server">         
    </div>
     <br />
     <div id="newTamplate">  
         
         
            
        <table class="table table-striped edm-table edm-PocessEdit-table centered-block">
        <tr>
		    <th colspan="3">Новый шаблон</th>
        </tr>
        <tr>
            <th>Тип согласования <asp:RequiredFieldValidator runat="server" SetFocusOnError="True" ControlToValidate="ProcessTypeDropDown" ErrorMessage="!" ForeColor="red"/> </th>
            <th>Название шаблона <asp:RequiredFieldValidator runat="server" SetFocusOnError="True" ControlToValidate="NewTemplateNameTextBox" ErrorMessage="!" ForeColor="red"/> </th>
            <th>Создать</th>           
        </tr>
        <tr>
		   <td>
            <asp:DropDownList ID="ProcessTypeDropDown" runat="server" AutoPostBack="False" CssClass="form-control">
                <asp:ListItem Value="parallel">Параллельное согласование</asp:ListItem>
                <asp:ListItem Value="serial">Последовательное согласование</asp:ListItem>
                <asp:ListItem Value="review">Рецензия</asp:ListItem>
            </asp:DropDownList>
            </td>
            <td>
                <asp:TextBox ID="NewTemplateNameTextBox"  runat="server" CssClass="form-control"></asp:TextBox>
                
            </td>            
            <td>
                <asp:Button ID="CreateTemplateButton" runat="server" OnClick="CreateTemplate" Text="Создать" CssClass="btn btn-warning" OnClientClick="javascript:Page_ClientValidate(); if (Page_IsValid==true) {showSimpleLoadingScreen()}"/>
            </td>           
        </tr>
        </table>
 
     </div>
</asp:Content>
