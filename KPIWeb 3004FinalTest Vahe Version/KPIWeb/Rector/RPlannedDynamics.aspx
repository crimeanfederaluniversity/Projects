<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="RPlannedDynamics.aspx.cs" Inherits="KPIWeb.Rector.RPlannedDynamics" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div>
    
        <asp:DropDownList ID="DropDownList1" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged" Width="484px">
        </asp:DropDownList>
        <br />
    
        <asp:Chart ID="Chart1" runat="server" Height="463px" Width="1036px">
            <Series>
                <asp:Series Name="ValueSeries" ChartType="StackedColumn">
                </asp:Series>
                <asp:Series Name="TargetSeries" ChartType="Line" BorderWidth="5">
                </asp:Series>     
            </Series>
            <ChartAreas>
                <asp:ChartArea Name="ChartArea1">
                </asp:ChartArea>
            </ChartAreas>
        </asp:Chart>
    
    </div>
</asp:Content>