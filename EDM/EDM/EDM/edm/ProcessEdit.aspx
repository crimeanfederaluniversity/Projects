<%@ Page Title="Создание и редактирование согласования" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" EnableEventValidation="false" CodeBehind="ProcessEdit.aspx.cs" Inherits="EDM.edm.ProcessEdit" %>
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
<br />
<br />
    <br />
    <script src="calendar_ru.js" type="text/javascript"> </script>
  
      <style type="text/css">
          .noFirstColumn td:first-child
          {
           display:none;
          }
          .noFirstColumn th:first-child
          {
           display:none;
          }
      </style>
    
    
    <script>

        function noTemplateValidation()
        {
            if (document.getElementById('MainContent_ProcessNameTextBox').value.length < 1)
            {
                alert('Введите название нового согласования');
                return false;
            }
            if (document.getElementById('MainContent_ParticipantsCountTextBox').value.length < 1)
            {
                alert('Не заполнено значение количества согласующих');
                return false;
            }
            return true;
        }

        function withTemplateValidation()
        {
            if (document.getElementById('MainContent_ProcessNameT').value.length < 1) {
                alert('Введите название нового согласования');
                return false;
            }
            return true;
        }

        function putValueAndClose(panelId, userNameField, userIdField,userName,userId) {
            document.getElementById(userNameField).value = userName;
            document.getElementById(userIdField).value = userId;
            document.getElementById(panelId).style.visibility = 'hidden';
        }

        function toggle_visibility(id1 ) {
            var e1 = document.getElementById(id1);
          //  var e2 = document.getElementById(id2);

            if (e1.disabled == true) {
               
                e1.disabled = false;
                
                // e2.enable = false;
            } else {
                e1.disabled = true;
                e1.value = '';
               // e2.enable = true;
            }
        }
    </script>
    
  

    <div id="createNewProcessDiv" runat="server" class="row" >
    <table class="table table-striped edm-table edm-PocessEdit-table centered-block">
        
        <tr >
		    <td colspan="4">
               Согласование с шаблоном
            </td>
        </tr>
         
        <tr>
		   <th colspan="2">Шаблон</th>
            <th>Название </th>
            <th>Создать</th>        
        </tr>
        
        <tr>
            
		   <td colspan="2">
            <asp:DropDownList ID="TemplatesDropDownList" runat="server" AutoPostBack="False" CssClass="form-control">
            </asp:DropDownList>
		       

            </td>
            <td>
                <asp:TextBox ID="ProcessNameT"  runat="server" CssClass="form-control"></asp:TextBox>
                
            </td>
            
            <td>
                <asp:Button ID="CreateNewProcessByTemplate" runat="server" OnClick="CreateNewTemplateProcess" Text="Создать" CssClass="btn btn-warning" OnClientClick="javascript:if(withTemplateValidation()){showSimpleLoadingScreen()} else return false;"/>
            </td>   
                  
        </tr>
        
        <tr >
		    <td colspan="4">
               Согласование без шаблона
            </td>
        </tr>

        <tr>
		   <th>Тип согласования</th>
            <th>Название  </th>
            <th>Кол-во согласующих
            </th>
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
                <asp:TextBox ID="ProcessNameTextBox"  runat="server" CssClass="form-control"></asp:TextBox>
                
            </td>
            <td>
                <asp:TextBox ID="ParticipantsCountTextBox" TextMode="Number" runat="server" CssClass="form-control"></asp:TextBox>
                
            </td>
            
            <td>
                <asp:Button ID="CreateProcessButton" runat="server" OnClick="CreateNewProcess" Text="Создать" CssClass="btn btn-warning" OnClientClick="javascript:if(noTemplateValidation()){showSimpleLoadingScreen()} else return false;"/>
            </td>           
        </tr>
        

        </table>
    </div>

    <br/>
    
    
     <div class="row">
         <div class="edm-proccess-edit-content centered-block">
             
        <div runat="server" id="existingProcessTitleDiv" class="header">
            <asp:Label ID="ProcessIdLabel"  runat="server" Text=""></asp:Label>
        </div>
     <br/>
             <div runat="server" id="submitterDiv" class="input-group-lg">
                   Отправлять по завершению<br />
         <asp:DropDownList ID="SubmitterDropDown" runat="server" Height="20px" Width="602px">
         </asp:DropDownList>
        </div>
        <br/>
        <div runat="server" id="commentForVersionDiv" class="input-group-lg">
             Комментарий
            <br />
            <asp:TextBox ID="commentForVersionTextBox" runat="server" Height="100px" TextMode="MultiLine" CssClass="form-control"></asp:TextBox>
             <asp:RequiredFieldValidator runat="server" SetFocusOnError="True" ControlToValidate="commentForVersionTextBox" ErrorMessage="!" ForeColor="red"/> 
        </div>
    
        <br/>

        <div runat="server" id="ParticipantsDiv"> 
    
        </div>
    
        <br/>
    
        <div runat="server" id ="documentsDiv" class="edm-process-edit-document-div">
            </div>
    
        <br/>

        <div runat="server" id="SaveAllDiv">
           <!-- javascript:Page_ClientValidate(); if (Page_IsValid==true) { this.disabled=true; } -->
            <asp:Button ID="SaveAllButton"  runat="server" Text="Сохранить" OnClick="SaveAllButton_Click" OnClientClick="javascript:Page_ClientValidate(); if (Page_IsValid==true) { showLoadingScreenWithText('Подождите, идет процесс сохранение'); }" CssClass="btn btn-success float-right" />
            </div>

        </div>

    </div>
   

</asp:Content>
