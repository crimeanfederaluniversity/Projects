<%@ Page Language="C#" Title="Активные кампании" MasterPageFile="~/Site.Master" AutoEventWireup="true" EnableViewStateMac="false" CodeBehind="ReportViewer.aspx.cs" Inherits="KPIWeb.StatisticsDepartment.ReportViewer" %>

<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
    <div>
            <h1>Список активных кампаний</h1>
            <asp:GridView ID="GridviewActiveCampaign" runat="server" ShowFooter="True"  AutoGenerateColumns="False" OnSelectedIndexChanged="GridviewActiveCampaign_SelectedIndexChanged">
                <Columns>

                    <asp:BoundField Visible="False" DataField="ReportArchiveTableID" HeaderText="ID отчета" />
                    <asp:BoundField DataField="Name" HeaderText="Наименование отчета" />
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
                            <asp:Button ID="ButtonEditReport" runat="server" CommandName="Select" Text="Редактировать" Width="200px" CommandArgument='<%# Eval("ReportArchiveTableID") %>' OnClick="ButtonEditReport_Click"/>
                        </ItemTemplate>
                    </asp:TemplateField>
                    
                     <asp:TemplateField HeaderText="Сгенерировать отчет">
                        <ItemTemplate>
                            <asp:Label ID="LabelReportArchiveTableID2" runat="server" Text='<%# Bind("ReportArchiveTableID") %>' Visible="false"></asp:Label>
                            <asp:Button ID="ButtonEditReport2" runat="server" CommandName="Select" Text="Сгенерировать" Width="150px" CommandArgument='<%# Eval("ReportArchiveTableID") %>' OnClick="ButtonEditReport_Click_2"/>
                        </ItemTemplate>
                    </asp:TemplateField>

                </Columns>
            </asp:GridView>

        </div>
        <br />
        <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Создать новую кампанию" Width="400px" />

</asp:Content>
