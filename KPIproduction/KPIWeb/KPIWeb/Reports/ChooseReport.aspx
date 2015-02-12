<%@ Page Language="C#" AutoEventWireup="true" EnableViewStateMac="false" CodeBehind="ChooseReport.aspx.cs" Inherits="KPIWeb.Reports.ChooseReport" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <h2>Список активных отчетов</h2><br />
        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" OnSelectedIndexChanged="GridView1_SelectedIndexChanged" style="margin-top: 0px">
             <Columns>
                 
                 <asp:BoundField DataField="ReportID"   HeaderText="Current Report ID" Visible="false" />
                 <asp:BoundField DataField="RoleID" HeaderText="Current Report ID" Visible="false" />
                 <asp:BoundField DataField="ReportName" HeaderText="Название отчета" Visible="True" />
                 <asp:BoundField DataField="RoleName" HeaderText="Роль" Visible="True" />
                 <asp:BoundField DataField="StartDate" HeaderText="Начальная дата отчета" Visible="True" />
                 <asp:BoundField DataField="EndDate" HeaderText="Конечная дата отчета" Visible="True" />
                 <asp:BoundField DataField="Param" HeaderText="Current Report ID" Visible="false" />

                    <asp:TemplateField HeaderText="Ввод данных">
                        <ItemTemplate>
                            <asp:Label ID="LabelReportArchiveTableID1" runat="server" Text='<%# Bind("ReportID") %>' Visible="false"></asp:Label>
                            <asp:Button ID="ButtonEditReport" runat="server" CommandName="Select" Text="Редактировать" Width="250px" CommandArgument='<%# Eval("Param") %>' OnClick="ButtonEditClick"/>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Просмотр введенных данных">
                        <ItemTemplate>
                            <asp:Label ID="LabelReportArchiveTableID2" runat="server" Text='<%# Bind("ReportID") %>' Visible="false"></asp:Label>
                            <asp:Button ID="ButtonViewReport" runat="server" CommandName="Select" Text="Просмотреть" Width="200px" CommandArgument='<%# Eval("Param") %>' OnClick="ButtonViewClick"/>
                        </ItemTemplate>
                    </asp:TemplateField>
                   
                </Columns>
        </asp:GridView>
    </form>
</body>
</html>
