<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="RectorMain.aspx.cs" Inherits="KPIWeb.Rector.RectorMain" %>
<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">   
    <style type="text/css">
   .button_right 
   {
       float:right
   }     
</style>  
     
    <link rel="stylesheet" type="text/css" href="../Spinner.css">  
    <script type="text/javascript">
         function showLoadPanel() {
             document.getElementById('LoadPanel_').style.visibility = 'visible';
         }
    </script>
    <style>  
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


    <asp:Panel runat="server" ID="top_panel2" CssClass="top_panel" Height="40" Visible="true">    
        <div>    
      <asp:Button ID="GoBackButton" runat="server" OnClientClick="showLoadPanel()" OnClick="GoBackButton_Click" Text="Назад" Width="125px" Enabled="False" />
      <asp:Button ID="GoForwardButton" runat="server" OnClientClick="showLoadPanel()" OnClick="GoForwardButton_Click" Text="Вперед" Width="125px" />
        &nbsp; &nbsp; <asp:Button ID="Button2" OnClientClick="showLoadPanel()" runat="server" OnClick="Button4_Click" Text="На главную" Width="125px" Enabled="False" />
        &nbsp; &nbsp;       
        <asp:Button ID="Button5" runat="server" OnClientClick="showLoadPanel()" CssClass="button_right" OnClick="Button5_Click" Text="Нормативные документы" Width="225px" />
        &nbsp; &nbsp;
        <asp:Button ID="Button6" runat="server" OnClientClick="showLoadPanel()" CssClass="button_right" OnClick="Button6_Click" Text="Button" Width="150px" Visible="False" />
        &nbsp; &nbsp;
        </div>

    </asp:Panel>

    <div>
    
        <br />
    
        <br />
        <br />
        <asp:Label ID="PageName" runat="server" Font-Size="20pt" Text="Выберите действие:"></asp:Label>
        <br />
    
    </div>
    

        <asp:Button ID="Button1" runat="server" OnClientClick="showLoadPanel()" OnClick="Button1_Click" Text="Работа с целевыми показателями" Width="350px" />
&nbsp;<asp:Button ID="Button3" runat="server" OnClientClick="showLoadPanel()" OnClick="Button3_Click" Text="Работа с первичными данными" Width="350px" />
    </asp:Content>
