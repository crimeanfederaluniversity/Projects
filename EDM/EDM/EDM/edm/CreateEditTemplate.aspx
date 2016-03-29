<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CreateEditTemplate.aspx.cs" Inherits="EDM.edm.CreateEditTamplate" %>
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
     <script>


        function putValueAndClose(panelId, userNameField, userIdField,userName,userId) {
            document.getElementById(userNameField).value = userName;
            document.getElementById(userIdField).value = userId;
            document.getElementById(panelId).style.visibility = 'hidden';
        }
    </script>
    
    
     <div runat="server" id="mainDiv">
         <br />
         <br />
         <br />
         Тип согласования
         <br />
         <asp:Label ID="AprrovalTypeLabel" runat="server" Text="Label"></asp:Label>
        <br />
         <br />
         Отправлять по завершению<br />
         <asp:DropDownList ID="SubmitterDropDown" runat="server" Height="20px" Width="602px">
         </asp:DropDownList>
         <br />
         <br />
        Название шаблона
        <br />
       <asp:TextBox ID="TemplateNameTextBox" runat="server" Width="600px"></asp:TextBox>
        <br /><br />
        Заголовок листа согласования
        <br />
       <asp:TextBox ID="TemplateTitleTextBox" runat="server" Width="600px"></asp:TextBox>
        <br /><br />
        Текст листа согласования
        <br />
       <asp:TextBox ID="TemplateContentTextBox" runat="server" TextMode="MultiLine" Width="600px" Height="100px"></asp:TextBox>
           <br />
         <br />
         <asp:CheckBox ID="AllowChangeProcessCheckBox" Text="Разрешить вносить изменения при создании процесса" runat="server" />
         <br />
         <br />
         Структурное подразделение<br />
        <asp:DropDownList ID="ChooseStructDropDown" runat="server" Height="20px" Width="602px">
         </asp:DropDownList>
         
         <br />
    </div>
    <br />
    
    <br/>
    <div runat="server" id ="participantsDiv">
    
    </div>
    <br/><br/>
   <asp:Button ID="SaveAllButton" runat="server"  Text="Сохранить" Width="211px"  OnClientClick="showSimpleLoadingScreen()" OnClick="SaveAllButton_Click" />
</asp:Content>
