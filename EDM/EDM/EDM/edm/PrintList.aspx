<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PrintList.aspx.cs" Inherits="EDM.edm.PrintList" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Документооборот</title>
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


    <div id="PrintMainDiv" runat="server">
            <div id="TemplateHeaderDiv" runat="server">
    
    </div>
        <br/>
    </div>
    </form>
</body>
</html>