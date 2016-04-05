<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Print.aspx.cs" Inherits="Chancelerry.kanz.Print" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server"> <meta charset="utf-8"/>
    <title>Канцелярия</title>
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

&nbsp;
        <asp:DropDownList ID="DropDownList1" runat="server" Height="19px" Width="222px" AutoPostBack="True">
            <asp:ListItem Value="1000">100%</asp:ListItem>
            <asp:ListItem  Selected="True" Value="800">80%</asp:ListItem>
            <asp:ListItem Value="600">60%</asp:ListItem>
            <asp:ListItem Value="400">40%</asp:ListItem>
            <asp:ListItem Value="200">20%</asp:ListItem>
        </asp:DropDownList>

<input name="b_print" type="button" class="ipt"   onClick="printdiv('PrintMainDiv');" value=" Печать ">
        
        <asp:Button ID="Button1" class="ipt"  runat="server" Text="Назад" OnClick="Button1_Click" Width="76px" />
    <div id="PrintMainDiv" runat="server">
    
    </div>
    </form>
</body>
</html>
