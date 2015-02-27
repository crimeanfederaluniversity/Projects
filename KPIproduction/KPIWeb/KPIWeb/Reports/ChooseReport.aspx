<%@ Page Language="C#" Title="Выбор отчета" MasterPageFile="~/Site.Master" AutoEventWireup="true" EnableViewStateMac="false" CodeBehind="ChooseReport.aspx.cs" Inherits="KPIWeb.Reports.ChooseReport" %>


       <asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
        <h2>Список активных отчетов</h2><br />
        <asp:GridView ID="GridView1" runat="server" 
            AutoGenerateColumns="False" OnSelectedIndexChanged="GridView1_SelectedIndexChanged" 
            style="margin-top: 0px">
             <Columns>
                 
                 <asp:BoundField DataField="ReportArchiveID"   HeaderText="Current Report ID" Visible="false" />    
                 <asp:BoundField DataField="ReportName" HeaderText="Название отчета" Visible="True" />          
                 <asp:BoundField DataField="StartDate" HeaderText="Начальная дата отчета" Visible="True" />
                 <asp:BoundField DataField="EndDate" HeaderText="Конечная дата отчета" Visible="True" />

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

                  <asp:TemplateField HeaderText="Просмотр и подтверждение данных">
                        <ItemTemplate>
                            <asp:Button ID="ButtonConfirmReport" runat="server" CommandName="Select" Text="Просмотреть и подтвердить" Width="200px" CommandArgument='<%# Eval("ReportArchiveID") %>' OnClick="ButtonConfirmClick"/>
                        </ItemTemplate>
                    </asp:TemplateField>
                   
                </Columns>
        </asp:GridView>
           <br />
           <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Форма выбора специальностей и их параметров" Width="903px" />
           <br />
       </asp:Content>
