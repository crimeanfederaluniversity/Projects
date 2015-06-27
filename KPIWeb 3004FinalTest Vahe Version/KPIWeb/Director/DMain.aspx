<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="DMain.aspx.cs" Inherits="KPIWeb.Director.DMain" %>
<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">  
<h2>Список активных отчётов</h2>

    <asp:GridView ID="GridView1" runat="server" 
            AutoGenerateColumns="False"
            style="margin-top: 0px" >
             <Columns>  
                 <asp:BoundField DataField="ReportName" HeaderText="Название отчёта" Visible="True" />          
                 <asp:BoundField DataField="StartDate" HeaderText="Начальная дата отчёта" Visible="True" />
                 <asp:BoundField DataField="EndDate" HeaderText="Конечная дата отчёта" Visible="True" />

                    <asp:TemplateField HeaderText="Подробнее">
                        <ItemTemplate>
                            <asp:Button ID="ButtonEditReport" runat="server" CommandName="Select" Text="Просмотреть" Width="200px" CommandArgument='<%# Eval("ReportArchiveID") %>' OnClick="ButtonViewClick"/>
                        </ItemTemplate>
                    </asp:TemplateField>

                   <asp:BoundField DataField="Status" HeaderText="Статус данных" Visible="True" />
                </Columns>
        </asp:GridView>


</asp:Content>
