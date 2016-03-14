<%@ Page Title="Создание и редактирование согласования" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" EnableEventValidation="false" CodeBehind="ProcessEdit.aspx.cs" Inherits="EDM.edm.ProcessEdit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
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

    <div id="createNewProcessDiv" runat="server" class="row" >
    <table class="table table-striped edm-table edm-PocessEdit-table centered-block">
        <tr>
		   <th>Тип согласования</th>
            <th>Название <asp:RequiredFieldValidator runat="server" ControlToValidate="ProcessNameTextBox" ErrorMessage="!" ForeColor="red"/> </th>
            <th>Кол-во согласующих
                <asp:RangeValidator runat="server" ControlToValidate="ParticipantsCountTextBox" ErrorMessage="!" ForeColor="red" Type="Integer" MinimumValue="1" MaximumValue="10"/>
                <asp:RequiredFieldValidator runat="server" ControlToValidate="ParticipantsCountTextBox" ErrorMessage="!" ForeColor="red"/></th>
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
                <asp:Button ID="CreateProcessButton" runat="server" OnClick="CreateNewProcess" Text="Создать" CssClass="btn btn-warning" />
            </td>           
        </tr>
        </table>
    </div>

    <br/>
    
    
     <div class="row">
         <div class="edm-proccess-edit-content centered-block">

        <div runat="server" id="existingProcessTitleDiv" class="header">
            <asp:Label ID="ProcessIdLabel" runat="server" Text=""></asp:Label>
        </div>
    
        <br/>
        <div runat="server" id="commentForVersionDiv" class="input-group-lg">
             Комментарий
            <br />
            <asp:TextBox ID="commentForVersionTextBox" runat="server" Height="100px" TextMode="MultiLine" CssClass="form-control"></asp:TextBox>
             <asp:RequiredFieldValidator runat="server" ControlToValidate="commentForVersionTextBox" ErrorMessage="!" ForeColor="red"/> 
        </div>
    
        <br/>

        <div runat="server" id="ParticipantsDiv"> 
    
        </div>
    
        <br/>
    
        <div runat="server" id ="documentsDiv" class="edm-process-edit-document-div">
            </div>
    
        <br/>

        <div runat="server" id="SaveAllDiv">
            <asp:Button ID="SaveAllButton"  runat="server" Text="Сохранить" OnClick="SaveAllButton_Click" CssClass="btn btn-success float-right" />
            </div>

        </div>

    </div>
   

</asp:Content>
