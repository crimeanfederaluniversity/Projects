<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="OtdelChooseReport.aspx.cs" Inherits="KPIWeb.FinKadr.OtdelChooseReport" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">  
    
    <style type="text/css">
   .button_right 
   {
       float: right;
       margin-right: 10px;
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
      <asp:Button ID="GoBackButton" runat="server" OnClientClick="showLoadPanel()" OnClick="GoBackButton_Click" Text="Назад" Width="125px" />
      <asp:Button ID="GoForwardButton" runat="server" OnClientClick="showLoadPanel()" OnClick="GoForwardButton_Click" Text="Вперед" Width="125px" />
        &nbsp; &nbsp; <asp:Button ID="Button2" runat="server" OnClientClick="showLoadPanel()" OnClick="Button4_Click" Text="На главную" Width="125px" />
            &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
        </div>

    </asp:Panel> 
    <div>
        <br />
        <br />


        <br />


        <asp:Label ID="PageName" runat="server" Text="PageName" Font-Size="20pt"></asp:Label>
        <br />
        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" style="margin-top: 0px">
             <Columns>                
                 <asp:BoundField DataField="ReportArchiveID"   HeaderText="Current Report ID" Visible="false" />    
                 <asp:BoundField DataField="ReportName" HeaderText="Название отчёта" Visible="True" />          
                 <asp:BoundField DataField="StartDate" HeaderText="Начальная дата отчёта" Visible="True" />
                 <asp:BoundField DataField="EndDate" HeaderText="Конечная дата отчёта" Visible="True" />

                    <asp:TemplateField HeaderText="Просмотр отчёта в текущем состоянии">
                        <ItemTemplate>
                            <asp:Label ID="LabelReportArchiveTableID2" runat="server" Text='<%# Bind("ReportArchiveID") %>' Visible="false"></asp:Label>
                            <asp:Button ID="ButtonViewReport" OnClientClick="showLoadPanel()" runat="server" CommandName="Select" Text="Просмотреть" Width="200px" CommandArgument='<%# Eval("ReportArchiveID") %>' OnClick="ButtonViewClick"/>
                        </ItemTemplate>
                    </asp:TemplateField>   

                </Columns>
        </asp:GridView>
    </div>
</asp:Content>
