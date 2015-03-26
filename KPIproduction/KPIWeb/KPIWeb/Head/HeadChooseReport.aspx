<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="HeadChooseReport.aspx.cs" Inherits="KPIWeb.Head.ChooseReport" %>
<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
    <div>
        <span style="font-size: 30px">Просмотр доступных отчётов</span><br />
        <br />
    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" OnSelectedIndexChanged="GridView1_SelectedIndexChanged" style="margin-top: 0px" OnRowDataBound="GridView1_RowDataBound">
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

                 <asp:TemplateField HeaderText="Просмотр и утверждение отчёта">
                        <ItemTemplate>
                            <asp:Label ID="LabelReport" runat="server" Text='<%# Bind("ReportArchiveID") %>' Visible="false"></asp:Label>
                            <asp:Button ID="ButtonConfirmReport" runat="server" CommandName="Select" Text="Просмотреть и утвердить" Width="200px" CommandArgument='<%# Eval("ReportArchiveID") %>' OnClick="ButtonConfirmClick"/>
                        </ItemTemplate>
                    </asp:TemplateField>
                 
                 <asp:TemplateField HeaderText="Просмотреть прогресс внесения данных по структурам">
                        <ItemTemplate>
                            <asp:Label ID="LabelStruct" runat="server" Text='<%# Bind("ReportArchiveID") %>' Visible="false"></asp:Label>
                            <asp:Button ID="ButtonStruct" runat="server" CommandName="Select" Text="Просмотреть структуру" Width="200px" CommandArgument='<%# Eval("ReportArchiveID") %>' OnClick="ButtonStructClick"/>
                        </ItemTemplate>
                    </asp:TemplateField>

                 <asp:TemplateField HeaderText="Подтверждено">
                        <ItemTemplate>
                            <asp:Label ID="info0" runat="server" Text='<%# Bind("info0") %>' Visible="true"></asp:Label>
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

