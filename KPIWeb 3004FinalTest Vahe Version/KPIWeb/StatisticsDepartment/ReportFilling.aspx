<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="ReportFilling.aspx.cs" Inherits="KPIWeb.StatisticsDepartment.ReportFilling" %>

<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
    <div>
    <asp:GridView ID="GridWhoOws" runat="server" AutoGenerateColumns="False" style="margin-top: 0px">
             <Columns>          
                 <asp:BoundField DataField="LV_1" HeaderText="Академия" Visible="True" />          
                 <asp:BoundField DataField="LV_2" HeaderText="Факультет" Visible="True" />
                 <asp:BoundField DataField="LV_3" HeaderText="Кафедра" Visible="True" />
                 <asp:BoundField DataField="Status" HeaderText="Стату данных" Visible="True" />
                 <asp:BoundField DataField="EmailEdit" HeaderText="Email заполняющего" Visible="True" />
                 <asp:BoundField DataField="EmailConfirm" HeaderText="Email проверяющего" Visible="True" />
                </Columns>
        </asp:GridView>
    </div>  
</asp:Content>

