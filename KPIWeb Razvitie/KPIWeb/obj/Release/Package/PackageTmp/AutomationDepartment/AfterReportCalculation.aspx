<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="AfterReportCalculation.aspx.cs" Inherits="KPIWeb.AutomationDepartment.AfterReportCalculation" %>
<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">  
    
    <style type="text/css">
    .smallerText
    {
        font-size:12px;
    }
    </style>

    <p>
        Выберите отчет</p>
    <p>
        <asp:DropDownList ID="reportList" runat="server"  Height="18px" Width="264px">
        </asp:DropDownList>
    </p>
    <p>
        Отметьте показатли
        <asp:Button ID="Button1" runat="server" Font-Size="Smaller" Height="17px" OnClick="Button1_Click" Text="Выбрать все" Width="119px" />
    </p>
    <p>
        <asp:CheckBoxList ID="indicatorsList" runat="server" Font-Size="Smaller">
        </asp:CheckBoxList>
    </p>
    <p>
        &nbsp;</p>
    <p>
        <asp:Button ID="calculateButton" runat="server" OnClick="CalculateButton_Click" Text="Рассчет" Width="263px" />
    </p>

</asp:Content>
