<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ViewStruct.aspx.cs" Inherits="KPIWeb.Rector.ViewStruct" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

        <asp:Panel runat="server" ID="top_panel2" CssClass="top_panel" Height="40" Visible="true">    
        <div>    
      <asp:Button ID="GoBackButton" runat="server" Text="Назад" Width="125px" OnClick="GoBackButton_Click" />
      <asp:Button ID="Button2" runat="server" Text="На главную" Width="125px" OnClick="Button2_Click" />

        </div>

    </asp:Panel> 
    
    <br />
    <h2>Штатное расписание</h2>
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


     table {
         border-collapse:collapse;
         table-layout:fixed;
         width:560px;
         border: 0px solid white;
     }
    td, th {
        border:1px solid gray;
        padding: 0px;
        width:100px;
    }
    th {background:#ccc;}
    input{
        display:none;
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
        display:none;
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

    <br />
    <br />

    <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>
</asp:Content>
