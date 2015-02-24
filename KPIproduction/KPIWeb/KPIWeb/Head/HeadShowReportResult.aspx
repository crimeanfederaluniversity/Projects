<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="HeadShowReportResult.aspx.cs" Inherits="KPIWeb.Head.HeadShowReportResult" %>
<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
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
</asp:Content>

