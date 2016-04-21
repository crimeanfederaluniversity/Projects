<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PrintList.aspx.cs" Inherits="EDM.edm.PrintList" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server"> <meta charset="utf-8"/>
    <title>Система электронного документооборота</title>
</head>
<body>
    <form id="form1" runat="server">
        
<script language="javascript">
function printdiv(printpage)
{
var headstr = "<html><head><title></title></head><body>";
var footstr = "</body>";
var newstr = document.all.item(printpage).innerHTML;
var oldstr = document.body.innerHTML;
document.body.innerHTML = headstr+newstr+footstr;
window.print();
document.body.innerHTML = oldstr;
return false;
}
</script>
        
  
        

    <input name="b_print" type="button" class="ipt"   onClick="printdiv('PrintMainDiv');" value=" Печать ">
    <br />  
    <div id="PrintMainDiv" style="width: 800px" runat="server">
        
         <style type="text/css">
   TABLE {
   
    border-collapse: collapse;
   }
   TD, TH {
    padding: 3px;
    border: 1px solid black;
   }
       .auto-style1 {
           text-align: center;
       }
  </style>

        <div id="mainDiv" runat="server" class="auto-style1">    
            <a>  ЛИСТ СОГЛАСОВАНИЯ </a>
            <br />
             <br />
            <asp:Label ID="processNameLabel" runat="server" Text=""></asp:Label>
            <br />
             <br />
            <div id="TemplateHeaderDiv" runat="server">
            </div>
            <br />
             <br />
            <asp:Label ID="idDocLabel" runat="server" Text=""></asp:Label>
            
            <br/>
          </div>  
        <br/>
         <br />
        <asp:Label ID="initiatorStructLabel" runat="server" Text=""></asp:Label>
        <br/>
        <asp:Label ID="initiatorNameLabel" runat="server" Text=""></asp:Label>
    </div>
    </form>
</body>
</html>