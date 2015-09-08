<%@ Page Language="C#" MasterPageFile="~/Site.Master" EnableEventValidation="false" AutoEventWireup="true" CodeBehind="RShowChartFaculty.aspx.cs" Inherits="KPIWeb.Rector.RShowChartFaculty" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <style>
        body {
              top: 60px;
          }
    </style>
            <asp:Panel runat="server" ID="top_panel2" CssClass="top_panel" Height="40" Visible="true">    
        <div>    
      <asp:Button ID="GoBackButton" runat="server" OnClientClick="JavaScript:window.history.back(1); return false;"  Text="Назад" Width="125px" Enabled="True" />
      <asp:Button ID="GoForwardButton" runat="server" OnClientClick="JavaScript:window.history.forward(1); return false;"  Enabled = "False" Text="Вперед" Width="125px" />
        &nbsp; &nbsp; <asp:Button ID="Button2" OnClientClick="showLoadPanel()" runat="server"  Text="На главную" Width="125px" Enabled="True" OnClick="Button2_Click" />
        &nbsp; &nbsp;       
        <asp:Button ID="Button1" runat="server" OnClientClick="showLoadPanel()" CssClass="button_right"  Text="Выбор отчета" Enabled="False" Width="225px" />
        &nbsp; &nbsp;
        <asp:Button ID="Button6" runat="server" OnClientClick="showLoadPanel()" CssClass="button_right"  Text="Button" Width="150px" Visible="False" />
        &nbsp; &nbsp;
        </div>

    </asp:Panel>

    <h1 style="text-align: center">Анализ целевого показателя в разрезе факультетов академии КФУ</h1>
    <div>
   <style>
       .chart{ opacity: 0.9;}
   </style>
        <asp:Chart ID="Chart1" runat="server" Height="700" Width="1167" CssClass="chart">
            <ChartAreas>
                <asp:ChartArea Name="ChartArea1"></asp:ChartArea>
            </ChartAreas>
        </asp:Chart>
        <br />
        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="false" Width="100%" CssClass="Grid_view_style newcentb" >
            <Columns>
                <asp:BoundField HeaderText="ID индикатора" DataField="IndicatorID" ItemStyle-Width="120px" Visible="False" />
                <asp:BoundField HeaderText="Рейтинг" DataField="Ratio" ItemStyle-Width="10px" Visible="true" />
                <asp:BoundField HeaderText="Название индикатора" DataField="IndicatorName" />
                <asp:BoundField HeaderText="Значение" DataField="IndicatorValue" />
            </Columns>
        </asp:GridView>
    </div>
</asp:Content>
