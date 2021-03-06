﻿<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" EnableEventValidation="false" CodeBehind="RShowChartDetailed.aspx.cs" Inherits="KPIWeb.Rector.RShowChartDetailed" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    
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
    
    
        <style type="text/css">
   .button_right 
   {
       float:right
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
      <asp:Button ID="GoBackButton" runat="server"  Text="Назад" Width="125px" Enabled="True" OnClick="GoBackButton_Click" />
      <asp:Button ID="GoForwardButton" runat="server" OnClientClick="JavaScript:window.history.forward(1); return false; showLoadPanel();"  Text="Вперед" Width="125px" />
        &nbsp; &nbsp; <asp:Button ID="Button2" OnClientClick="showLoadPanel()" runat="server"  Text="На главную" Width="125px" Enabled="True" OnClick="Button2_Click" />
        &nbsp; &nbsp;       
        <asp:Button ID="Button1" runat="server" OnClientClick="showLoadPanel()"  Text="Выбор отчета" Visible="False" Width="225px" />
        &nbsp;
            &nbsp;&nbsp;
            <asp:DropDownList ID="RectorChooseReportDropDown" runat="server" Height="20px" Width="550px" AutoPostBack="True" OnSelectedIndexChanged="RectorChooseReportDropDown_SelectedIndexChanged">
            </asp:DropDownList>
            &nbsp; &nbsp;
             &nbsp;
        <asp:Button ID="Button6" runat="server" OnClientClick="showLoadPanel()" Text="Button" Width="150px" Visible="False" />
        <asp:Button ID="Button5" runat="server" OnClientClick="showLoadPanel()" CssClass="button_right" OnClick="Button5_Click" Text="Нормативные документы" Width="300px" />
    
        &nbsp; &nbsp;
        </div>

    </asp:Panel>

    <h1>Вклад структурных подразделений в достижение значения целевого показателя</h1>
    
             <div id="noDataMessage" runat="server" Visible="False">           
           <h1 align="center">
            <asp:Label ID="noDataMessageLabel" runat="server" Text="Сведения внесены по совокупным данным, разложение невозможно."></asp:Label>
           </h1>               
        </div> 

    <div>
      <style>
       .chart{ opacity: 0.99;}
   </style>
        <asp:Chart ID="Chart1" runat="server" Height="700" Width="1167" CssClass="chart" OnClick="Chart1_Click">
            <ChartAreas>
                <asp:ChartArea Name="ChartArea1"></asp:ChartArea>
            </ChartAreas>
        </asp:Chart>
        <br />
        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="false" Width="100%" CssClass="Grid_view_style newcentb" OnRowDataBound="GridView1_RowDataBound" >
            <Columns>
                <asp:BoundField HeaderText="ID индикатора" DataField="IndicatorID" ItemStyle-Width="120px" Visible="False" />
                <asp:BoundField HeaderText="Рейтинг" DataField="Ratio" ItemStyle-Width="10px" Visible="true" />
                <asp:BoundField HeaderText="Название индикатора" DataField="IndicatorName" />
                <asp:BoundField HeaderText="Значение" DataField="IndicatorValue" />
                <asp:TemplateField HeaderText="Подробнее">
                    <ItemTemplate>
                        <asp:Button ID="Button7" runat="server" CommandName="Select" OnClientClick="showLoadPanel()" Text="Подробнее" CommandArgument='<%# Eval("IndicatorID") %>' OnClick="FacultyButtonClick" />
                        </ItemTemplate>
                    </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </div>
    <!-- setTimeout("document.getElementById('<%=Chart1.ClientID %>').style.width = '100%'", 500); -->
      
</asp:Content>
