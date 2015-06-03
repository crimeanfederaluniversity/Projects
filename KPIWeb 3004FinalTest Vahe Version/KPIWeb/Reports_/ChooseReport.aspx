<%@ Page Language="C#" Title="Выбор отчёта" MasterPageFile="~/Site.Master" AutoEventWireup="true" EnableViewStateMac="false" CodeBehind="ChooseReport.aspx.cs" Inherits="KPIWeb.Reports.ChooseReport" %>


       <asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
           <h2>Список активных отчётов</h2>
           <asp:Label ID="Label1" runat="server" Font-Bold="True" Font-Size="X-Large" Text="Label" Visible="False"></asp:Label>
           <br />
           <br />
           <asp:Label ID="Label2" runat="server" Font-Bold="True" Font-Size="Large" Text="Label" Visible="False"></asp:Label>
        <asp:GridView ID="GridView1" runat="server" 
            AutoGenerateColumns="False"
            style="margin-top: 0px" OnRowDataBound="GridView1_RowDataBound">
             <Columns>
                 
                 <asp:BoundField DataField="ReportArchiveID"   HeaderText="Current Report ID" Visible="false" />    
                 <asp:BoundField DataField="ReportName" HeaderText="Название отчёта" Visible="True" />          
                 <asp:BoundField DataField="StartDate" HeaderText="Начальная дата отчёта" Visible="True" />
                 <asp:BoundField DataField="EndDate" HeaderText="Конечная дата отчёта" Visible="True" />

                    <asp:TemplateField HeaderText="Ввод данных">
                        <ItemTemplate>
                            <asp:Button ID="ButtonEditReport" runat="server" CommandName="Select" Text="Редактировать" Width="200px" CommandArgument='<%# Eval("ReportArchiveID") %>' OnClick="ButtonEditClick"/>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Просмотр введенных данных">
                        <ItemTemplate>
                            <asp:Button ID="ButtonViewReport" runat="server" CommandName="Select" Text="Просмотреть" Width="200px" CommandArgument='<%# Eval("ReportArchiveID") %>' OnClick="ButtonViewClick"/>
                        </ItemTemplate>
                    </asp:TemplateField>

                  <asp:TemplateField HeaderText="Просмотр и утверждение данных">
                        <ItemTemplate>
                            <asp:Button ID="ButtonConfirmReport" runat="server" CommandName="Select" Text="Просмотреть и утвердить" Width="200px" CommandArgument='<%# Eval("ReportArchiveID") %>' OnClick="ButtonConfirmClick"/>
                        </ItemTemplate>
                    </asp:TemplateField>

                   <asp:BoundField DataField="Status" HeaderText="Статус данных" Visible="True" />
                </Columns>
        </asp:GridView>
           <br />
       </asp:Content>
