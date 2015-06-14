<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="RShowChart.aspx.cs" Inherits="KPIWeb.Rector.RShowChart" %>

<%@ Register assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" namespace="System.Web.UI.DataVisualization.Charting" tagprefix="asp" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">  
    <h1>Анализ целевых показателей</h1>
    <h2>Анализ целевых показателей</h2>

&nbsp;
    <br />
    <div>
        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="false" Width="700px" OnRowDataBound="GridView1_RowDataBound">
            <Columns>
                <asp:BoundField HeaderText="ID индикатора" DataField="IndicatorID" ItemStyle-Width="120px" Visible="False" />
                <asp:BoundField HeaderText="Название индикатора" DataField="IndicatorName" />
                <asp:TemplateField HeaderText="Гистограмма">
                    <ItemTemplate>
                        <div>
                            <asp:Chart ID="Chart3" runat="server" Width="950px" Height="200px">            
                                <series>
                                    <asp:Series Name="ValueSeries" ChartType="StackedBar">
                                    </asp:Series>
                                     <asp:Series Name="TargetSeries" ChartType="Bar">
                                    </asp:Series>          
                                    </series>
                                <ChartAreas>
                                    <asp:ChartArea Name="ChartArea1"></asp:ChartArea>
                                </ChartAreas>
                            </asp:Chart>
                        </div>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Подробнее">
                    <ItemTemplate>
                        <asp:Button ID="Button7" runat="server" CommandName="Select" Text="Подробнее" CommandArgument='<%# Eval("IndicatorID") %>' OnClick="DetailedButtonClick"/>
                        </ItemTemplate>
                    </asp:TemplateField>
            </Columns>
        </asp:GridView>
        <br />
    </div>
</asp:Content>