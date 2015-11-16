<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="WatchProrectorSubmit.aspx.cs" Inherits="KPIWeb.AutomationDepartment.WebForm1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <asp:Panel runat="server" ID="top_panel2" CssClass="top_panel" Height="40" Visible="true">    
    <div>    
        <asp:Button ID="Button2" runat="server" OnClientClick="showLoadPanel()" Text="На главную" Width="125px" OnClick="Button2_Click" />     
    </div> 
</asp:Panel> 
    <br />
     <br />
     <br />
    Отчет&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
    <asp:DropDownList ID="ReportsDropDown" runat="server"></asp:DropDownList>

&nbsp;
    <br />
    <br />
    Тип показателей&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
    <asp:DropDownList ID="ParamTypeDropDown" runat="server">
        <asp:ListItem Value="1">Целевые показатели</asp:ListItem>
        <asp:ListItem Value="2">Расчетные показатели</asp:ListItem>
    </asp:DropDownList>
&nbsp;<br />
    <br />
    Фильтр 1&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
    <asp:DropDownList ID="Filter1DropDown" runat="server">
        <asp:ListItem Value="1">Все показатели</asp:ListItem>
        <asp:ListItem Value="2">Только утвержденные</asp:ListItem>
        <asp:ListItem Value="3">Только не утвержденные</asp:ListItem>
    </asp:DropDownList>
&nbsp;<br />
    <br />
    <asp:Button ID="Button1" runat="server" Text="Показать" Width="314px" OnClick="Button1_Click" />
    <br />
    <br />
    <asp:GridView ID="GridView1"  runat="server" AutoGenerateColumns="False" Width="100%">
        <Columns>
            <asp:BoundField DataField="Name" HeaderText="Название показателя" />
            <asp:BoundField DataField="Value" HeaderText="Значение показателя" />
            <asp:BoundField DataField="Status" HeaderText="Статус показателя" />
            <asp:BoundField DataField="ResponsibleProrector" HeaderText="Ответственный проректор" />
            <asp:BoundField DataField="SubmitDate" HeaderText="Дата утверждения" />
            <asp:BoundField DataField="SubmitComment" HeaderText="Комментарий при утверждении" />          
            <asp:TemplateField HeaderText="Изменить статус" Visible="False">
                <ItemTemplate>
                    <asp:Button ID="ButtonStatusChange" runat="server" CommandName="Select" Text="Изменить" Width="200px" CommandArgument='<%# Eval("CollectedID") %>' OnClick="ButtonStatusChangeClick"/>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Удалить значение" Visible="False">
                <ItemTemplate>
                    <asp:Button ID="ButtonDeleteRow" runat="server" CommandName="Select" Text="Удалить" Width="200px" CommandArgument='<%# Eval("CollectedID") %>' OnClick="ButtonDeleteRowClick"/>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Удалить строку из базы" Visible="False">
                <ItemTemplate>
                    <asp:Button ID="ButtobDeleteRowFromDb" runat="server" CommandName="Select" Text="Удалить" Width="200px" CommandArgument='<%# Eval("CollectedID") %>' OnClick="ButtobDeleteRowFromDbClick"/>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
    <br />
    <br />

</asp:Content>
