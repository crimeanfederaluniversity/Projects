<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"  CodeBehind="Dashboard.aspx.cs" Inherits="EDM.edm.Dashboard" %>
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
   #coomentEndP, #md5check{
	width : 300px;
	height : 120px;
	position : fixed; 
	z-index : 50;
	top : 20%; 
	left : 40%;	

	border : 2px solid;
	border-radius: 10px;
	
	padding: 5px;
}
         .button_right 
   {
       float:right
   } 
</style> 

<asp:Panel runat="server" ID="top_panel2" CssClass="top_panel" Height="40" Visible="True">    
    <div>          
        &nbsp;&nbsp;&nbsp;&nbsp;<asp:Button ID="goBackButton" CausesValidation="false"  runat="server" Enabled="true" CssClass="btn btn-default" Text="Назад" Width="200" Height="100%" OnClientClick="history.back ()" OnClick="goBackButton_Click" />
        &nbsp;&nbsp;&nbsp;&nbsp;<asp:Button ID="goForwardButton" Enabled="true" CausesValidation="false"  CssClass="btn btn-default" runat="server" Text="Вперед" Width="200" Height="100%" OnClientClick="history.forward ()"/>
        <asp:Button ID="GoToTemplatesButton" Enabled="true"  CausesValidation="false"  CssClass="btn btn-default button_right" runat="server" Text="Мои шаблоны" Width="200" Height="100%" OnClick="GoToTemplatesButton_Click" />
        <asp:Button ID="GoToSlavesHistory" runat="server" Text="К истории подчиненных" CausesValidation="false"  CssClass="btn btn-default button_right" Width="200" Height="100%" OnClick="GoToSlavesHistory_Click"/>
        <asp:Button ID="GoToSubmitterButton" runat="server" Text="Утвержденные документы" CausesValidation="false"  CssClass="btn btn-default button_right" Width="200" Height="100%" OnClick="GoToSubmitterButton_Click" />
        <asp:Button ID="md5Button" runat="server" Text="Проверить документ md5" CssClass="btn btn-default button_right" Width="200" Height="100%" OnClientClick="javascript: document.getElementById('md5check').style.visibility='visible'; return false;"/>
           </div>
</asp:Panel>
    <br/><br/>
    <div id="coomentEndP" style="visibility: hidden; background-color: blanchedalmond">
        <asp:Panel runat="server" ID="comment_panel" Visible="true">
            <asp:Label ID="Label2" runat="server" Text="Оставьте свой комментарий к процессу" Font-Bold="True"></asp:Label>
            <div id="loop"style="visibility: hidden; height: 0px; width: 0px">
                <asp:TextBox ID="textBoxId" runat="server"></asp:TextBox>
            </div>
            <br/>
            <br/>
            <asp:TextBox ID="commentTextBox" TextMode="MultiLine" placeholder="Комментарий..." Height="270" Width="885px" runat="server"></asp:TextBox>
            <br/>
            <div>
            <asp:Button ID="Button5" runat="server" Text="Утвердить" OnClientClick="showLoadingScreenWithText('Идет процесс утверждения. Пожалуйста дождитесь завершения!');" OnClick="Button5_Click"/>
                <asp:Button ID="Button6" runat="server" OnClientClick="document.getElementById('coomentEndP').style.visibility='hidden'; return false;" Text="Отменить" />
           </div>
        </asp:Panel>
    </div>
    
        <div id="md5check" style="visibility: hidden; background-color:lightgreen">
        <asp:Panel runat="server" ID="md5_panel" Visible="true">
            <asp:Label ID="Label3" runat="server" Text="Укажите файл" Font-Bold="True"></asp:Label>
            <br/>
            <br/>
            <asp:FileUpload ID="FileUpload1" runat="server" />
            <br/>
            <asp:Label ID="md5Label" runat="server" Text="Label" Visible="False"></asp:Label>
            <div>
            <asp:Button ID="md5CheckButton" runat="server" Text="Проверить" OnClientClick="showLoadingScreenWithText('Идет процесс, подождите');" OnClick="md5CheckButton_OnClick"/>
            <asp:Button ID="Button8" runat="server" OnClientClick="document.getElementById('md5check').style.visibility='hidden'; return false;" Text="Отменить" />
           </div>
        </asp:Panel>
    </div>

    
    
    <asp:Label ID="Label1" runat="server" Text="Label" CssClass="header" Visible="False"></asp:Label>
    <br />
        <div class="row centered-content position-rel">

        <div class="row">
            <asp:Button ID="Button4" runat="server" Text="Запустить новый процесс согласования" OnClick="Button4_Click" CssClass="btn btn-default" OnClientClick="showSimpleLoadingScreen()"/>
        </div>
        
        <div class="edm-main-content">
            
            <asp:Button ID="Button1" runat="server" Text="Инициированные согласования" OnClick="Button1_Click" CssClass="btn btn-default col-sm-3" OnClientClick="showSimpleLoadingScreen()" />
    
            <asp:Button ID="Button2" runat="server" Text="Входящие" OnClick="Button2_Click" CssClass="btn btn-default col-sm-3" OnClientClick="showSimpleLoadingScreen()"/>
    
            <asp:Button ID="Button3" runat="server" Text="Архив" OnClick="Button3_Click" CssClass="btn btn-default col-sm-3" OnClientClick="showSimpleLoadingScreen()"/>
        </div>
            <br />
    <asp:GridView ID="dashGridView" runat="server" AutoGenerateColumns="False" OnRowCommand="dashGridView_RowCommand" OnRowDataBound="dashGridView_RowDataBound" CssClass="table table-striped edm-table" AllowPaging="True" PageSize="50" OnPageIndexChanging="dashGridView_PageIndexChanging"> 
    <PagerSettings Mode="Numeric" />
	<PagerStyle BackColor="#FFCC66" ForeColor="#444" HorizontalAlign="Center" />
    </asp:GridView>
</div>
</asp:Content>
