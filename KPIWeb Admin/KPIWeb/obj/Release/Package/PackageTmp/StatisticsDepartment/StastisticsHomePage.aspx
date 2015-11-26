<%@ Page Language="C#" Title="Отдел статистики" MasterPageFile="~/Site.Master" AutoEventWireup="true" EnableViewStateMac="false" CodeBehind="StastisticsHomePage.aspx.cs" Inherits="KPIWeb.StatisticsDepartment.StastisticsHomePage" %>

<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
    <h2>Отдел статистики</h2>
    <div>
    
    </div>
        <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Открыть список активных кампаний" Width="356px" />
        <br />
        <br />
        <asp:Button ID="Button2" runat="server" OnClick="Button2_Click" Text="Создание новой" Width="250px" />
        <br />
</asp:Content>
