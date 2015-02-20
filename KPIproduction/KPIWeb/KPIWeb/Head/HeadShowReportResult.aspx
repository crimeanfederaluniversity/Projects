<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="HeadShowReportResult.aspx.cs" Inherits="KPIWeb.Head.HeadShowReportResult" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        Резульаты для вашей академии<br />
    <asp:GridView ID="IndicatorsTable" runat="server" ShowFooter="true" AutoGenerateColumns="false" Width="1000px">
                            <Columns>
                                <asp:BoundField DataField="IndicatorName" HeaderText="Индикатор" />
                                <asp:BoundField DataField="IndicatorResult" HeaderText="Результат" />
                            </Columns>
            </asp:GridView>
        <br />
            
             <asp:GridView ID="CalculatedParametrsTable" runat="server" ShowFooter="true" AutoGenerateColumns="false" Width="1000px">
                            <Columns>
                                <asp:BoundField DataField="CalculatedParametrsName" HeaderText="Расчетный параметр" />
                                <asp:BoundField DataField="CalculatedParametrsResult" HeaderText="Результат" />                 
                            </Columns>
            </asp:GridView>

            <br />

            <asp:GridView ID="BasicParametrsTable" runat="server" ShowFooter="true" AutoGenerateColumns="false" Width="1000px">
                            <Columns>
                                <asp:BoundField DataField="BasicParametrsName" HeaderText="Базовый параметр" />
                                <asp:BoundField DataField="BasicParametrsResult" HeaderText="Результат(сумма)" />                          
                            </Columns>
            </asp:GridView>
    </div>
    </form>
</body>
</html>
