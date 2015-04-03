<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="RectorChooseReport.aspx.cs" Inherits="KPIWeb.Rector.RectorChooseReport" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">   
    <div>
        <br />
        <br />
        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" style="margin-top: 0px">
             <Columns>                
                 <asp:BoundField DataField="ReportArchiveID"   HeaderText="Current Report ID" Visible="false" />    
                 <asp:BoundField DataField="ReportName" HeaderText="Название отчёта" Visible="True" />          
                 <asp:BoundField DataField="StartDate" HeaderText="Начальная дата отчёта" Visible="True" />
                 <asp:BoundField DataField="EndDate" HeaderText="Конечная дата отчёта" Visible="True" />

                    <asp:TemplateField HeaderText="Просмотр результатов отчёта">
                        <ItemTemplate>
                            <asp:Label ID="LabelReportArchiveTableID2" runat="server" Text='<%# Bind("ReportArchiveID") %>' Visible="false"></asp:Label>
                            <asp:Button ID="ButtonViewReport" runat="server" CommandName="Select" Text="Просмотреть" Width="200px" CommandArgument='<%# Eval("ReportArchiveID") %>' OnClick="ButtonViewClick"/>
                        </ItemTemplate>
                    </asp:TemplateField>   

                </Columns>
        </asp:GridView>
    </div>
</asp:Content>
