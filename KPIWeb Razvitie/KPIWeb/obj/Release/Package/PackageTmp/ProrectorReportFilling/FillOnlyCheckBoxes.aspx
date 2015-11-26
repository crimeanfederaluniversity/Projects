<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="FillOnlyCheckBoxes.aspx.cs" Inherits="KPIWeb.ProrectorReportFilling.FillOnlyCheckBoxes" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    
    
     
        <link rel="stylesheet" type="text/css" href="../Spinner.css">  
    <script type="text/javascript">
        function showLoadPanel() {
            document.getElementById('LoadPanel_').style.visibility = 'visible';
        }
    </script>
    <style>  
        body {
        top: 50px;
    }
        .LoadPanel 
   {
          position: fixed;
          z-index: 10;
          background-color: whitesmoke;
          top: 0;
          left: 0;
          width: 100%;
          height: 100%;
          opacity: 0.9;
          visibility: hidden;
   }
</style>     
    <div id="LoadPanel_" class='LoadPanel'>               
            <div id="floatingCirclesG">
            <div class="f_circleG" id="frotateG_01">
            </div>
            <div class="f_circleG" id="frotateG_02">
            </div>
            <div class="f_circleG" id="frotateG_03">
            </div>
            <div class="f_circleG" id="frotateG_04">
            </div>
            <div class="f_circleG" id="frotateG_05">
            </div>
            <div class="f_circleG" id="frotateG_06">
            </div>
            <div class="f_circleG" id="frotateG_07">
            </div>
            <div class="f_circleG" id="frotateG_08">
            </div>
            </div>
        </div>


    <br />
    
     <style type="text/css">
   .button_right 
   {
       float: right;
       margin-right: 10px;
   }      
</style>
<asp:Panel runat="server" ID="top_panel2" CssClass="top_panel" Height="40" Visible="true">    
    <div>    
        <asp:Button ID="GoBackButton" runat="server" OnClientClick="showLoadPanel()" Text="Назад" Width="125px" OnClick="GoBackButton_Click" />
        <asp:Button ID="Button2" runat="server" OnClientClick="showLoadPanel()" Text="На главную" Width="125px" OnClick="Button2_Click" />

        <asp:Label ID="DataStatusLabel" runat="server" Text=""></asp:Label>

        <asp:Button ID="Button5" runat="server" CssClass="button_right" OnClientClick="showLoadPanel()"  Text="Нормативные документы" Width="250px" OnClick="Button5_Click" />
    </div> 
</asp:Panel> 

    <h4>
        &nbsp;</h4>
    <h4>
        <asp:Label ID="ReportNameLabel" runat="server" Text="reportName"></asp:Label>
    </h4>

    <h4>
        <asp:Label ID="BasicParamName" runat="server" Text="Выберите направления подготовки, на которых реализуются сетевые программы."></asp:Label>
    </h4>
    <h4>
    <asp:Button ID="ExpandAllButton" runat="server" Text="Раскрыть все"  Width="400px" OnClick="ExpandAllButton_Click"/>
    </h4>
    <p>
        &nbsp;</p>

    <asp:TreeView ID="TreeView1" runat="server"  ShowCheckBoxes="Leaf"></asp:TreeView>
    

    <br />
    <asp:Button ID="ClearAllCheckedButton" runat="server" Text="Убрать все галочки" Width="400px" OnClick="ClearAllCheckedButton_Click" Enabled="False" Visible="False"/>
    <br />
    <asp:Button ID="SaveChangesButton" runat="server" OnClientClick="showLoadPanel()" OnClick="Button1_Click" Text="Сохранить изменения" Width="400px" />
    

</asp:Content>
