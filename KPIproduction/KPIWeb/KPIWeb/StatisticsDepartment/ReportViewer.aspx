<%@ Page Language="C#" AutoEventWireup="true" EnableViewStateMac="false" CodeBehind="ReportViewer.aspx.cs" Inherits="KPIWeb.StatisticsDepartment.ReportViewer" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <h1>Список активных кампаний</h1>
            <asp:GridView ID="GridviewActiveCampaign" runat="server" ShowFooter="True"  AutoGenerateColumns="False" OnSelectedIndexChanged="GridviewActiveCampaign_SelectedIndexChanged">
                <Columns>

                    <asp:BoundField DataField="ReportArchiveTableID" HeaderText="ID отчета" />
                    <asp:BoundField DataField="Active" HeaderText="Активна" />
                    <asp:BoundField DataField="Name" HeaderText="Наименование отчета" />
                    <asp:BoundField DataField="StartDateTime" HeaderText="Стартовая дата отчета" />
                    <asp:BoundField DataField="EndDateTime" HeaderText="Конечная дата отчета" />
                    <asp:BoundField DataField="DateToSend" HeaderText="Запланированная дата отправки отчета" />
                    <asp:BoundField DataField="Calculeted" HeaderText="Рассчитана" />
                    <asp:BoundField DataField="Sent" HeaderText="Отчет отправлен" />
                    <asp:BoundField DataField="SentDateTime" HeaderText="Дата отправки отчета" />
                    <asp:BoundField DataField="RecipientConfirmed" HeaderText="Отчет принят получателем" />

                    <asp:TemplateField HeaderText="Редактировать">
                        <ItemTemplate>
                            <asp:Label ID="LabelReportArchiveTableID" runat="server" Text='<%# Bind("ReportArchiveTableID") %>' Visible="false"></asp:Label>
                            <asp:Button ID="ButtonEditReport" runat="server" CommandName="Select" Text="Редактировать" Width="75px" CommandArgument='<%# Eval("ReportArchiveTableID") %>' OnClick="ButtonEditReport_Click"/>
                        </ItemTemplate>
                    </asp:TemplateField>
                    
                     <asp:TemplateField HeaderText="Сгенерировать отчет">
                        <ItemTemplate>
                            <asp:Label ID="LabelReportArchiveTableID2" runat="server" Text='<%# Bind("ReportArchiveTableID") %>' Visible="false"></asp:Label>
                            <asp:Button ID="ButtonEditReport2" runat="server" CommandName="Select" Text="Сгенерировать" Width="75px" CommandArgument='<%# Eval("ReportArchiveTableID") %>' OnClick="ButtonEditReport_Click_2"/>
                        </ItemTemplate>
                    </asp:TemplateField>

                </Columns>
            </asp:GridView>

        </div>
    </form>
</body>
</html>
