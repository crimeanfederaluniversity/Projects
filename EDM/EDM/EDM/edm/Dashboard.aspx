<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" EnableEventValidation="false" AutoEventWireup="true"  CodeBehind="Dashboard.aspx.cs" Inherits="EDM.edm.Dashboard" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <style type="text/css">
        
        .innerSubmitionFixed {
            width : 315px;
            height : 120px;
            position : fixed; 
            z-index : 51;
            top : 40%; 
            left : 40%;	
            border : 2px solid;
            border-radius: 10px;
            background: grey;
            padding: 5px;
            visibility: hidden;
        }
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
        #md5check{
            width : 300px;
            height : 120px;
            position : fixed; 
            z-index : 50;
            top : 20%; 
            left : 42%;	

            border : 2px solid;
            border-radius: 10px;
	
            padding: 5px;
        }
        #coomentEndP{
            width : 900px;
            height : 372px;
            position : fixed; 
            z-index : 50;
            top : 20%; 
            left : 30%;	

            border : 2px solid;
            border-radius: 10px;
	
            padding: 5px;
        }
        .button_right 
        {
            float:right
        }
    </style> 
    
    <script>
        rowId = 0;
    </script>

    <div runat="server" id="fixedPanelNewSub" class="innerSubmitionFixed">
        
        <asp:DropDownList ID="ProcessTypeDropDown" runat="server" Width="300" Height="30" AutoPostBack="False" CssClass="form-control">
                <asp:ListItem Value="parallel">Параллельное согласование</asp:ListItem>
                <asp:ListItem Value="serial">Последовательное согласование</asp:ListItem>
                <asp:ListItem Value="review">Рецензия</asp:ListItem>
            </asp:DropDownList>
        <asp:Button ID="createSubProcessButton" Width="300" Height="30" runat="server" Text="Создать" OnClientClick=" __doPostBack('ctl00$MainContent$dashGridView','SubApprove$'+rowId); return false;" />
        <asp:Button ID="cancelSubProcessButton" Width="300" Height="30" runat="server" Text="Отмена" OnClientClick="document.getElementById('MainContent_fixedPanelNewSub').style.visibility='hidden';  return false;" />
        </div>
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
