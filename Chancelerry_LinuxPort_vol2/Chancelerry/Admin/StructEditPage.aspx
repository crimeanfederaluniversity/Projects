<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" EnableEventValidation ="false"  AutoEventWireup="true" CodeBehind="StructEditPage.aspx.cs" Inherits="Chancelerry.Admin.StructEditPage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
<script type="text/javascript" src="fortable/jquery-1.7.2.js"></script> 
<script>
    function showChildren(childrenTableName) {
        if (document.getElementById(childrenTableName).style.display == 'table') {
            (document.getElementById(childrenTableName).style.display = 'none');
        } else {
            (document.getElementById(childrenTableName).style.display = 'table');
        }

    }
    function sh(el) {
        var idx = el.parent().parent().parent().next().find('input');
        if (idx.attr('checked') == "checked")
            idx.removeAttr('checked');
        else
            idx.attr('checked', 'checked');
    }
</script>
<style>

    #MainContent_nameTextBox0 {
        width: 1000px;
        max-width: 1000px;
        
    }
     #ctl00_MainContent_nameTextBox0 {
        width: 1000px;
        max-width: 1000px;
        
    }
     table {
         border-collapse:collapse;
         table-layout:fixed;
         width:1000px;
         margin-left: 25px;
         border: 0px solid white;
     }
    td, th {
        
        border-bottom: 1px solid black;
        border-top: 1px solid black;
        border-left: 1px solid black;
        padding: 1px;
        width:100px;
    }

    th {background:#ccc;}
    input{
       /* display:none; */
    }
    input + a {
        background:url(data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAAAsAAAALCAIAAAAmzuBxAAAACXBIWXMAAAsSAAALEgHS3X78AAAAkElEQVQYlXWOvRWDQAyDv/DYK2wQSro8OkpGuRFcUjJCRmEE0TldCpsjPy9qzj7Jki62Pgh4vnqbbbEWuN+use/PlArwHccWGg780psENGFY6W4YgxZIAM339WmT3m397YYxxn6aASslFfVotYLTT3NwcuTKlFpNR2sdEak4acdKeafPlE2SZ7sw/1BEtX94AXYTVmyR94mPAAAAAElFTkSuQmCC)
                   no-repeat 0px 4px;
    }
    input:checked + a {
        background:url(data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAAAsAAAALCAIAAAAmzuBxAAAACXBIWXMAAAsSAAALEgHS3X78AAAAeklEQVQYlX2PsRGDMAxFX3zeK9mAlHRcupSM4hFUUjJCRpI70VHIJr7D8BtJ977+SQ9Zf7isVG16WSQC0/D0OW/FqoBlDFkIVJ2xAhA8sI/NHbcYiFrPfI0fGklKagDx2F4ltdtaM0J9L3dxcVxi+zv62E+MwPs7c60dClRP6iug7wUAAAAASUVORK5CYII=)
                   no-repeat 0px 4px;
    }
    label a	{
        cursor:pointer;
        padding-left:16px;
    }
    input ~ table {
       /* display:none;*/
    }
    input:checked ~ table {
        display:table;
        margin-top:-1px;
        margin-left:-1px;
    }
    .node {
        padding:0;
        border-bottom:0;
    }
    td+td+td+td {text-align:left;}
</style>
    <div id="mainDiv" runat="server">   
    
    <br />
    <br />
      <h3>Редактирование справочника структурных подразделений</h3>
     <p>Для того чтобы добавить новое структурное подразделение:<p>
     <p>1) Введите название новоого структурного подразделения соответствующее поле<p>
     <p>2) Нажмите на кнопку "Добавить" в строке структурного подразделения за которым закреплено новое подразделение<p>
     <p>ВНИМАНИЕ! При удалении структурного подразделения, все относящиеся к нему структурные подразделения также будут удалены!<p>
    
    </div>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="TableContent" runat="server">
</asp:Content>
