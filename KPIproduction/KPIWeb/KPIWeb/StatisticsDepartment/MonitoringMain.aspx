<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="MonitoringMain.aspx.cs" Inherits="KPIWeb.StatisticsDepartment.MonitoringMain" %>
<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">

    <div>
    
    </div>
        <br />
    <br />
    <br />
    <br />
        <asp:Button ID="Button1" runat="server" Font-Bold="False" Font-Size="15pt" Height="50px" Text=" Управление справочниками" Width="400px" />
        <br />
        <br />
        <asp:Button ID="Button2" runat="server" Font-Size="15pt" Height="50px" Text="Управление пользователями" Width="400px" />
        <br />
        <br />
        <asp:Button ID="Button3" runat="server" Font-Size="15pt" Height="50px" Text="Управление Индикаторами" Width="400px" OnClick="Button3_Click" />
        <br />
        <br />
        <asp:Button ID="Button4" runat="server" Font-Size="15pt" Height="50px" Text="Управление базовыми показателями" Width="400px" OnClick="Button4_Click" />
        <br />
        <br />
        <asp:Button ID="Button5" runat="server" Font-Size="15pt" Height="50px" Text=" Управление отчетами" Width="400px" OnClick="Button5_Click" />
</asp:Content>