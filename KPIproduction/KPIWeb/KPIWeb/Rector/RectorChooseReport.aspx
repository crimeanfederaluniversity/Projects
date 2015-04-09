<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="RectorChooseReport.aspx.cs" Inherits="KPIWeb.Rector.RectorChooseReport" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">  
    
    <style type="text/css">
   .button_right 
   {
       float:right
   }     
</style>
                <asp:Panel runat="server" ID="top_panel2" CssClass="top_panel" Height="40" Visible="true">    
        <div>    
      <asp:Button ID="GoBackButton" runat="server" OnClick="GoBackButton_Click" Text="Назад" Width="125px" />
      <asp:Button ID="GoForwardButton" runat="server" OnClick="GoForwardButton_Click" Text="Вперед" Width="125px" />
        &nbsp; &nbsp; <asp:Button ID="Button2" runat="server" OnClick="Button4_Click" Text="На главную" Width="125px" />
        &nbsp; &nbsp;       
        <asp:Button ID="Button5" runat="server" CssClass="button_right" OnClick="Button5_Click" Text="Нормативные документы" Width="225px" />
        &nbsp; &nbsp;
        <asp:Button ID="Button6" runat="server" CssClass="button_right" OnClick="Button6_Click" Text="Button" Width="150px" Visible="False" />
        &nbsp; &nbsp;
        <asp:Button ID="Help" runat="server" CssClass="button_right"  Text="Помощь" Width="150px" />
        </div>

    </asp:Panel> 
    <div>
        <br />


        <asp:Label ID="PageName" runat="server" Text="PageName"></asp:Label>
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
