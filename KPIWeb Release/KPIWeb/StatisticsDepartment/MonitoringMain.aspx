<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="MonitoringMain.aspx.cs" Inherits="KPIWeb.StatisticsDepartment.MonitoringMain" %>
<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
   
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
    
    

    <h2>Отдел развитие и статистики</h2>
    <div>
    
    </div>
        <br />
    <asp:Button ID="Button10" runat="server" Height="50px" OnClientClick="showLoadPanel()" OnClick="Button10_Click" Text="Информация о подтверждениях" Width="400px" />
    <br />
    <br />
        <asp:Button ID="Button1" runat="server" Font-Bold="False" Font-Size="15pt" Height="50px" Text=" Управление справочниками" Width="400px" OnClientClick="showLoadPanel()" OnClick="Button1_Click" />
        <br />
        <br />
        <asp:Button ID="Button2" runat="server" Font-Size="15pt" Height="50px" Text="Управление пользователями" Width="400px" OnClientClick="showLoadPanel()" OnClick="Button2_Click" />
        <br />
        <br />
        <asp:Button ID="Button3" runat="server" Font-Size="15pt" Height="50px" Text="Управление целевыми показателями" Width="400px" OnClientClick="showLoadPanel()" OnClick="Button3_Click" />
        <br />
    <br />
    <asp:Button ID="Button7" runat="server" Height="50px" OnClientClick="showLoadPanel()" OnClick="Button7_Click" Text="Управление плановыми значениями" Width="400px" />
        <br />
        <br />
        <asp:Button ID="Button4" runat="server" Font-Size="15pt" Height="50px" Text="Управление базовыми показателями" Width="400px" OnClientClick="showLoadPanel()" OnClick="Button4_Click" />
        <br />
        <br />
        <asp:Button ID="Button5" runat="server" Font-Size="15pt" Height="50px" Text=" Управление отчётами" Width="400px" OnClientClick="showLoadPanel()" OnClick="Button5_Click" />
    <br />
    <br />
        <asp:Button ID="Button6" runat="server" Font-Size="15pt" Height="50px" Text="Прикрепление спецальностей" Width="400px" OnClientClick="showLoadPanel()" OnClick="Button6_Click" />
    <br />
    <br />
    <asp:Button ID="Button8" runat="server" Height="50px" OnClientClick="showLoadPanel()" OnClick="Button8_Click" Text="Управление документами" Width="400px" />
      <br />
      <br />
        <asp:Button ID="Button22" runat="server" Font-Size="15pt" Height="50px" Text="Просмотреть структуру университета" Width="400px" OnClientClick="showLoadPanel()" OnClick="Button22_Click" />
    <br />
    <br />
        <asp:Button ID="Button9" runat="server" Font-Size="15pt" Height="50px" Text="Редактирование структуры КФУ" Width="400px" OnClientClick="showLoadPanel()" OnClick="Button9_Click" />
        </asp:Content>