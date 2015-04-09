<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="MonitoringMain.aspx.cs" Inherits="KPIWeb.StatisticsDepartment.MonitoringMain" %>
<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
   
    <h2>Отдел мониторинга и статистики</h2>
    <div>
    
    </div>
        <br />
    <br />
    <br />
    <br />
        <asp:Button ID="Button1" runat="server" Font-Bold="False" Font-Size="15pt" Height="50px" Text=" Управление справочниками" Width="400px" OnClick="Button1_Click" />
        <br />
        <br />
        <asp:Button ID="Button2" runat="server" Font-Size="15pt" Height="50px" Text="Управление пользователями" Width="400px" OnClick="Button2_Click" />
        <br />
        <br />
        <asp:Button ID="Button3" runat="server" Font-Size="15pt" Height="50px" Text="Управление индикаторами" Width="400px" OnClick="Button3_Click" />
        <br />
    <br />
    <asp:Button ID="Button7" runat="server" Height="50px" OnClick="Button7_Click" Text="Управление плановыми значениями" Width="400px" />
        <br />
        <br />
        <asp:Button ID="Button4" runat="server" Font-Size="15pt" Height="50px" Text="Управление базовыми показателями" Width="400px" OnClick="Button4_Click" />
        <br />
        <br />
        <asp:Button ID="Button5" runat="server" Font-Size="15pt" Height="50px" Text=" Управление отчётами" Width="400px" OnClick="Button5_Click" />
    <br />
    <br />
        <asp:Button ID="Button6" runat="server" Font-Size="15pt" Height="50px" Text="Прикрепление спецальностей" Width="400px" OnClick="Button6_Click" />
    <br />
    <br />
    <asp:Button ID="Button8" runat="server" Height="50px" OnClick="Button8_Click" Text="Управление документами" Width="400px" />
    <br />
    <br />
        <asp:Button ID="Button9" runat="server" Font-Size="15pt" Height="50px" Text="Редактирование структуры КФУ" Width="400px" OnClick="Button9_Click" />
        </asp:Content>