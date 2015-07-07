<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="RPlannedDynamics.aspx.cs" Inherits="KPIWeb.Rector.RPlannedDynamics" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div>
    <h2> Плановая динамика Целевых показателей </h2>
        <asp:DropDownList ID="DropDownList1" runat="server" Width="1160" AutoPostBack="True" OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged">
        </asp:DropDownList>
        <br />
       <style>
              body {
             top:35px;
             }
       .chart{ opacity: 0.9;}
   </style>
                <style type="text/css">
   .button_right 
   {
       float:right
   }     
</style> 

                    <asp:Panel runat="server" ID="top_panel2" CssClass="top_panel" Height="40" Visible="true">    
        <div>    
      <asp:Button ID="GoBackButton" runat="server" OnClick="Button2_Click"  Text="Назад" Width="125px" Enabled="True" />
      <asp:Button ID="GoForwardButton" runat="server" OnClientClick="JavaScript:window.history.forward(1); return false;"  Enabled = "False" Text="Вперед" Width="125px" />
        &nbsp; &nbsp; <asp:Button ID="Button2" runat="server"  Text="На главную" Width="125px" Enabled="True" OnClick="Button2_Click" />
        &nbsp; &nbsp;       
        <asp:Button ID="Button1" runat="server" OnClientClick="showLoadPanel()"   Text="Выбор отчета" Enabled="False" Width="225px" />
       <asp:Button ID="Button5" runat="server" OnClientClick="showLoadPanel()" CssClass="button_right" OnClick="Button5_Click" Text="Нормативные документы" Width="300px" />

                 &nbsp; &nbsp; &nbsp; &nbsp;
        </div>

    </asp:Panel>
        <asp:Chart ID="Chart1" runat="server" Height="600" Width="1167" CssClass="chart">
            <Series>
                <asp:Series Name="ValueSeries" ChartType="StackedColumn">
                </asp:Series>
                <asp:Series Name="TargetSeries" ChartType="Line" BorderWidth="5">
                </asp:Series>     
            </Series>
            <ChartAreas>
                <asp:ChartArea Name="ChartArea1">
                </asp:ChartArea>
            </ChartAreas>
        </asp:Chart>
    
    </div>
</asp:Content>