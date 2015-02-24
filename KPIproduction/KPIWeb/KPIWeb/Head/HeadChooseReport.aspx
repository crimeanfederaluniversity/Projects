<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="HeadChooseReport.aspx.cs" Inherits="KPIWeb.Head.ChooseReport" %>
<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
    <div>
    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" OnSelectedIndexChanged="GridView1_SelectedIndexChanged" style="margin-top: 0px">
             <Columns>
                 
                 <asp:BoundField DataField="ReportArchiveID"   HeaderText="Current Report ID" Visible="false" />    
                 <asp:BoundField DataField="ReportName" HeaderText="Название отчета" Visible="True" />          
                 <asp:BoundField DataField="StartDate" HeaderText="Начальная дата отчета" Visible="True" />
                 <asp:BoundField DataField="EndDate" HeaderText="Конечная дата отчета" Visible="True" />


                    <asp:TemplateField HeaderText="Просмотр результатов отчета">
                        <ItemTemplate>
                            <asp:Label ID="LabelReportArchiveTableID2" runat="server" Text='<%# Bind("ReportArchiveID") %>' Visible="false"></asp:Label>
                            <asp:Button ID="ButtonViewReport" runat="server" CommandName="Select" Text="Просмотреть" Width="200px" CommandArgument='<%# Eval("ReportArchiveID") %>' OnClick="ButtonViewClick"/>
                        </ItemTemplate>
                    </asp:TemplateField>
                   
                </Columns>
        </asp:GridView>
    </div>
</asp:Content>

