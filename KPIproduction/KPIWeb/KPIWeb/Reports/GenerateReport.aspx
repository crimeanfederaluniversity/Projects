<%@ Page Language="C#" Title="Отчёт" MasterPageFile="~/Site.Master" AutoEventWireup="true" EnableViewStateMac="false" CodeBehind="GenerateReport.aspx.cs" Inherits="KPIWeb.Reports.GenerateReport" %>

<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">&nbsp;<h2>Отчёт</h2>
    <div>
    <asp:GridView ID="GridviewReport" runat="server" ShowFooter="true" AutoGenerateColumns="false">
                            <Columns>
                                <asp:BoundField DataField="IndicatorsTableID" Visible="false" />
                                <asp:BoundField DataField="Name" HeaderText="Название индикатора" />
                                <asp:BoundField DataField="IndicatorsValue" HeaderText="Значение индикатора" />
                            </Columns>
                        </asp:GridView>
        <br />
    </div>
        <asp:Button ID="Button1" runat="server" Text="Экспортировать данные в excel" Width="400px" />
        <br />
        <br />
        <asp:Button ID="Button2" runat="server" Text="Печать" Width="400px" />

</asp:Content>
