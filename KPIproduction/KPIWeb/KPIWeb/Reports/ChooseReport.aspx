<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ChooseReport.aspx.cs" Inherits="KPIWeb.Reports.ChooseReport" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        Выберите отчет для заполнения<br />
        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" OnSelectedIndexChanged="GridView1_SelectedIndexChanged">
             <Columns>
                 
                 <asp:BoundField DataField="ReportID" HeaderText="Current Report ID" Visible="false" />
                 <asp:BoundField DataField="RoleID" HeaderText="Current Report ID" Visible="false" />
                 <asp:BoundField DataField="ReportName" HeaderText="Название отчета" Visible="True" />
                 <asp:BoundField DataField="RoleName" HeaderText="Роль" Visible="True" />
                 <asp:BoundField DataField="StartDate" HeaderText="Начальная дата отчета" Visible="True" />
                 <asp:BoundField DataField="EndDate" HeaderText="Конечная дата отчета" Visible="True" />
                 <asp:BoundField DataField="Param" HeaderText="Current Report ID" Visible="false" />

                    <asp:TemplateField HeaderText="Редактировать">
                        <ItemTemplate>
                            <asp:Label ID="LabelReportArchiveTableID1" runat="server" Text='<%# Bind("ReportID") %>' Visible="false"></asp:Label>
                            <asp:Button ID="ButtonEditReport" runat="server" CommandName="Select" Text="Редактировать" Width="150px" CommandArgument='<%# Eval("Param") %>' OnClick="ButtonEditClick"/>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Просмотр">
                        <ItemTemplate>
                            <asp:Label ID="LabelReportArchiveTableID2" runat="server" Text='<%# Bind("ReportID") %>' Visible="false"></asp:Label>
                            <asp:Button ID="ButtonViewReport" runat="server" CommandName="Select" Text="Просмотреть" Width="150px" CommandArgument='<%# Eval("Param") %>' OnClick="ButtonViewClick"/>
                        </ItemTemplate>
                    </asp:TemplateField>
                   
                </Columns>
        </asp:GridView>
    </form>
</body>
</html>
