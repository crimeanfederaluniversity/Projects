<%@ Page Language="C#" Title="Активные кампании" MasterPageFile="~/Site.Master" AutoEventWireup="true" EnableViewStateMac="false" CodeBehind="ReportViewer.aspx.cs" Inherits="KPIWeb.StatisticsDepartment.ReportViewer" %>
 
 

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
    

    <div>
            <h1>Список активных кампаний</h1>
            <h1>
        <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Создать новую кампанию" Width="400px" />

            </h1>
            <asp:GridView ID="GridviewActiveCampaign" runat="server" ShowFooter="True"  AutoGenerateColumns="False" OnSelectedIndexChanged="GridviewActiveCampaign_SelectedIndexChanged">
                <Columns>

                    <asp:BoundField Visible="False" DataField="ReportArchiveTableID" HeaderText="ID отчёта" />
                    <asp:BoundField DataField="Name" HeaderText="Наименование отчёта" />
                    <asp:BoundField DataField="Active" HeaderText="Активна" />
                   
                    <asp:BoundField DataField="StartDateTime" HeaderText="Стартовая дата" />
                    <asp:BoundField DataField="EndDateTime" HeaderText="Конечная дата" />
                    <asp:BoundField DataField="DateToSend" HeaderText="Планируемая дата отправки" />
                    <asp:BoundField DataField="Calculeted" HeaderText="Рассчитан" />
                    <asp:BoundField DataField="Sent" HeaderText="Отправлен" />
                    <asp:BoundField DataField="SentDateTime" HeaderText="Дата отправки" />
                    <asp:BoundField DataField="RecipientConfirmed" HeaderText="Принят получателем" />

                    <asp:TemplateField HeaderText="Редактировать параметры кампании">
                        <ItemTemplate>
                            <asp:Label ID="LabelReportArchiveTableID" runat="server" Text='<%# Bind("ReportArchiveTableID") %>' Visible="false"></asp:Label>
                            <asp:Button ID="ButtonEditReport" runat="server" CommandName="Select" OnClientClick="showLoadPanel()" Text="Редактировать" Width="200px" CommandArgument='<%# Eval("ReportArchiveTableID") %>' OnClick="ButtonEditReport_Click"/>
                        </ItemTemplate>
                    </asp:TemplateField>
                    
                   <asp:TemplateField HeaderText="Рассылка уведомлений">
                        <ItemTemplate>
                            <asp:Label ID="LabelReportArchiveTableID22" runat="server" Text='<%# Bind("ReportArchiveTableID") %>' Visible="false"></asp:Label>
                            <asp:Button ID="ButtonMailSending" runat="server" OnClientClick="showLoadPanel()" CommandName="Select" Text="Разослать всем" Width="200px" CommandArgument='<%# Eval("ReportArchiveTableID") %>' OnClick="ButtonMailSending_Click"/>
                            <asp:Button ID="ButtonMailSending2" runat="server" OnClientClick="showLoadPanel()" CommandName="Select" Text="Только должникам" Width="200px" CommandArgument='<%# Eval("ReportArchiveTableID") %>' OnClick="ButtonMailSending2_Click"/>
                        </ItemTemplate>
                    </asp:TemplateField>

                     <asp:TemplateField Visible="false" HeaderText="Сгенерировать отчёт">
                        <ItemTemplate>
                            <asp:Label ID="LabelReportArchiveTableID2" runat="server" Text='<%# Bind("ReportArchiveTableID") %>' Visible="false"></asp:Label>
                            <asp:Button ID="ButtonEditReport2" runat="server" OnClientClick="showLoadPanel()" CommandName="Select" Text="Сгенерировать" Width="150px" CommandArgument='<%# Eval("ReportArchiveTableID") %>' OnClick="ButtonEditReport_Click_2"/>
                        </ItemTemplate>
                    </asp:TemplateField>
                    
                    <asp:TemplateField Visible="false" HeaderText="Просмотреть отчёт">
                        <ItemTemplate>
                            <asp:Button ID="ButtonViewReport" runat="server" CommandName="Select" Text="Просмотреть" Width="150px" CommandArgument='<%# Eval("ReportArchiveTableID") %>' OnClick="ButtonViewReportClick"/>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField Visible="false" HeaderText="Просмотреть уровень заполненности">
                        <ItemTemplate>
                            <asp:Button ID="ButtonViewStruct" runat="server" CommandName="Select" Text="Просмотреть" Width="150px" CommandArgument='<%# Eval("ReportArchiveTableID") %>' OnClick="ButtonViewStruct"/>
                        </ItemTemplate>
                    </asp:TemplateField>

                </Columns>
            </asp:GridView>

        </div>
        <br />

    <br />
    <br />
    <br />
    
</asp:Content>
