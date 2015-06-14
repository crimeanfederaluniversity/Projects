<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" EnableEventValidation="false" CodeBehind="RShowChartDetailed.aspx.cs" Inherits="KPIWeb.Rector.RShowChartDetailed" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <h1>Анализ целевого показателя в разрезе вклада каждой академии КФУ</h1>
    <div>
   
        <asp:Chart ID="Chart1" runat="server" Height="621px" Width="1022px">
            <ChartAreas>
                <asp:ChartArea Name="ChartArea1"></asp:ChartArea>
            </ChartAreas>
        </asp:Chart>
        <br />
        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="false" Width="700px" >
            <Columns>
                <asp:BoundField HeaderText="ID индикатора" DataField="IndicatorID" ItemStyle-Width="120px" Visible="False" />
                <asp:BoundField HeaderText="Название индикатора" DataField="IndicatorName" />
                <asp:BoundField HeaderText="Значение" DataField="IndicatorValue" />
                <asp:TemplateField HeaderText="Подробнее">
                    <ItemTemplate>
                        <asp:Button ID="Button7" runat="server" CommandName="Select" Text="Подробнее" CommandArgument='<%# Eval("IndicatorID") %>' OnClick="FacultyButtonClick" />
                        </ItemTemplate>
                    </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </div>
</asp:Content>
