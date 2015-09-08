﻿<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="RShowChart.aspx.cs" Inherits="KPIWeb.Rector.RShowChart" %>

<%@ Register assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" namespace="System.Web.UI.DataVisualization.Charting" tagprefix="asp" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">  
    
    
        <link rel="stylesheet" type="text/css" href="../Spinner.css">  
    <script type="text/javascript">
        function showLoadPanel() {
            document.getElementById('LoadPanel_').style.visibility = 'visible';
        }
    </script>
    <style>  
        body {
            top: 50px
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
    
            <asp:Panel runat="server" ID="top_panel2" CssClass="top_panel" Height="40" Visible="true">    
        <div>    
      <asp:Button ID="GoBackButton" runat="server" OnClientClick="JavaScript:window.history.back(1); return false; showLoadPanel();"  Text="Назад" Width="125px" Enabled="True" />
      <asp:Button ID="GoForwardButton" runat="server" OnClientClick="JavaScript:window.history.forward(1); return false; showLoadPanel();"  Text="Вперед" Width="125px" />
        &nbsp; &nbsp; <asp:Button ID="Button2" OnClientClick="showLoadPanel()" runat="server"  Text="На главную" Width="125px" Enabled="True" OnClick="Button2_Click" />
        &nbsp; &nbsp;       
        <asp:Button ID="Button1" runat="server" OnClientClick="showLoadPanel()" CssClass="button_right"  Text="Выбор отчета" Enabled="False" Width="225px" />
        &nbsp; &nbsp;
        <asp:Button ID="Button6" runat="server" OnClientClick="showLoadPanel()" CssClass="button_right"  Text="Button" Width="150px" Visible="False" />
        &nbsp; &nbsp;
        </div>

    </asp:Panel>

    &nbsp;
    <br />
    <div style="position: relative" id="owow">
       
        <div id="idh1_1">Прогресс достижения целевых показателей КФУ</div>
       <div id="asdasd" style="position: relative;">
        <div id="gwownerdiv">
             
        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="false" Width="700px" OnRowDataBound="GridView1_RowDataBound" CssClass="rrrr">
            <Columns>
                <asp:BoundField HeaderText="ID индикатора" DataField="IndicatorID" ItemStyle-Width="120px" Visible="False" />
                <asp:BoundField HeaderText="Название индикатора" DataField="IndicatorName" />
                <asp:TemplateField HeaderText="Гистограмма">
                    <ItemTemplate>
                        <div>
                            <asp:Chart ID="Chart3" runat="server" Width="950px" Height="200px">            
                                <series>
                                    <asp:Series Name="ValueSeries" ChartType="StackedBar">
                                    </asp:Series>
                                     <asp:Series Name="TargetSeries" ChartType="Bar">
                                    </asp:Series>          
                                    </series>
                                <ChartAreas>
                                    <asp:ChartArea Name="ChartArea1"></asp:ChartArea>
                                </ChartAreas>
                            </asp:Chart>
                        </div>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Подробнее">
                    <ItemTemplate>
                        <asp:Button ID="Button7" runat="server" OnClientClick="showLoadPanel()" CommandName="Select" Text='<%# Eval("Button_Text") %>' CommandArgument='<%# Eval("IndicatorID") %>' OnClick="DetailedButtonClick"/>
                        </ItemTemplate>
                    </asp:TemplateField>
            </Columns>
        </asp:GridView>

            </div>
           

       </div>
        <br />
    
        </div>
    <script>
        document.getElementById("asdasd").style.height = ((document.getElementById("gwownerdiv").getBoundingClientRect().bottom - document.getElementById("gwownerdiv").getBoundingClientRect().top).toString()+"px");
    </script>
</asp:Content>