<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="RShowChart.aspx.cs" Inherits="KPIWeb.Rector.RShowChart" %>

<%@ Register assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" namespace="System.Web.UI.DataVisualization.Charting" tagprefix="asp" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">  
    <h1>Анализ целевых показателей</h1>
    <asp:TextBox ID="TextBox1" runat="server" Height="108px" TextMode="MultiLine" Width="300px"></asp:TextBox>

&nbsp;
    <br />
    <br />
    <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Посчитать для выбранных показателей" />
    <br />
    <br />
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
    <hr />
    <br />
    тут ID показателя
    <asp:TextBox ID="TextBox2" runat="server" Width="283px">1029 </asp:TextBox>
    <br />
    <asp:Button ID="Button2" runat="server" OnClick="Button2_Click" Text="Посчитать для ID показателя по академиям" Width="556px" />
    <br />
    <br />
    <hr />
    <br />
    тут ID показателя
    <asp:TextBox ID="TextBox3" runat="server" Width="283px">1029 </asp:TextBox>
    <br />
    <asp:Button ID="Button3" runat="server" OnClick="Button3_Click" Text="Посчитать для ID показателя по факультетам" Width="556px" />
    <br />
        <br />
    <hr />
    <br />
    тут ID академии
    <asp:TextBox ID="TextBox4" runat="server" Width="283px">1016 </asp:TextBox>
    <br />
    <asp:Button ID="Button4" runat="server" OnClick="Button4_Click" Text="Посчитать для ID Академии все показатели" Width="556px" />
    <br />
</asp:Content>