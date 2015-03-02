﻿<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="HeadChooseReport.aspx.cs" Inherits="KPIWeb.Head.ChooseReport" %>
<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
    <div>
        <br />
        <br />
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

                 <asp:TemplateField HeaderText="Просмотр и подтверждение отчета">
                        <ItemTemplate>
                            <asp:Label ID="LabelReport" runat="server" Text='<%# Bind("ReportArchiveID") %>' Visible="false"></asp:Label>
                            <asp:Button ID="ButtonConfirmReport" runat="server" CommandName="Select" Text="Просмотреть и подтвердить" Width="200px" CommandArgument='<%# Eval("ReportArchiveID") %>' OnClick="ButtonConfirmClick"/>
                        </ItemTemplate>
                    </asp:TemplateField>
                   
                </Columns>
        </asp:GridView>
        <br />
        <br />
        <asp:CheckBox ID="CheckBox1" runat="server" AutoPostBack="True" OnCheckedChanged="CheckBox1_CheckedChanged" Text="Рассчет для определенного подразделения" />
        <br />
        <br />
        <asp:Label ID="Label4" runat="server" Text="Академия"></asp:Label>
        <br />
        <asp:DropDownList ID="DropDownList1" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged">
        </asp:DropDownList>
        <br />
        <br />
        <asp:Label ID="Label3" runat="server" Text="Факультет"></asp:Label>
        <br />
        <asp:DropDownList ID="DropDownList2" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DropDownList2_SelectedIndexChanged">
        </asp:DropDownList>
        <br />
        <br />
        <asp:Label ID="Label2" runat="server" Text="Кафедра"></asp:Label>
        <br />
        <asp:DropDownList ID="DropDownList3" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DropDownList3_SelectedIndexChanged">
        </asp:DropDownList>
    </div>
</asp:Content>

