<%@ Page Language="C#" Title="Выбор отчета" MasterPageFile="~/Site.Master" AutoEventWireup="true" EnableViewStateMac="false" CodeBehind="ChooseReport.aspx.cs" Inherits="KPIWeb.Reports.ChooseReport" %>


       <asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
        <h2>Список активных отчетов</h2><br />
        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" OnSelectedIndexChanged="GridView1_SelectedIndexChanged" style="margin-top: 0px">
             <Columns>
                 
                 <asp:BoundField DataField="ReportArchiveID"   HeaderText="Current Report ID" Visible="false" />    
                 <asp:BoundField DataField="ReportName" HeaderText="Название отчета" Visible="True" />          
                 <asp:BoundField DataField="StartDate" HeaderText="Начальная дата отчета" Visible="True" />
                 <asp:BoundField DataField="EndDate" HeaderText="Конечная дата отчета" Visible="True" />

                    <asp:TemplateField HeaderText="Ввод данных">
                        <ItemTemplate>
                            <asp:Label ID="LabelReportArchiveTableID1" runat="server" Text='<%# Bind("ReportArchiveID") %>' Visible="false"></asp:Label>
                            <asp:Button ID="ButtonEditReport" runat="server" CommandName="Select" Text="Редактировать" Width="250px" CommandArgument='<%# Eval("ReportArchiveID") %>' OnClick="ButtonEditClick"/>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Просмотр введенных данных">
                        <ItemTemplate>
                            <asp:Label ID="LabelReportArchiveTableID2" runat="server" Text='<%# Bind("ReportArchiveID") %>' Visible="false"></asp:Label>
                            <asp:Button ID="ButtonViewReport" runat="server" CommandName="Select" Text="Просмотреть" Width="200px" CommandArgument='<%# Eval("ReportArchiveID") %>' OnClick="ButtonViewClick"/>
                        </ItemTemplate>
                    </asp:TemplateField>
                   
                </Columns>
        </asp:GridView>
           <br />
           <asp:DropDownList ID="DropDownList1" runat="server" AutoPostBack="True" Height="50px" OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged" Width="500px">
           </asp:DropDownList>
           <br />
           <br />
           <asp:CheckBoxList ID="CheckBoxList1" runat="server" Height="33px" Width="500px">
           </asp:CheckBoxList>
           <br />
           <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Сохранить внесенные изменения" Width="500px" />
           <br />
       </asp:Content>
