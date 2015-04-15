<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="WatchingParamtrsState.aspx.cs" Inherits="KPIWeb.AutomationDepartment.WatchingParamtrsState" %>

<asp:Content runat="server" ID="Content1" ContentPlaceHolderID="MainContent">
    <h2><span style="font-size: 30px">Просмотр и отправка на доработку значений по структурным подразделениям</span></h2>

    <asp:Label ID="Label4" runat="server" Text="Выберите отчет"></asp:Label>
    <br />
    <asp:DropDownList ID="DropDownList4" runat="server" OnSelectedIndexChanged="DropDownList4_SelectedIndexChanged">
    </asp:DropDownList>
    <br />

 <asp:Label ID="Label1" runat="server" Text="Выберите университет" ></asp:Label>
            <br />
    <asp:DropDownList ID="DropDownList1" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged">
    </asp:DropDownList>
    <br />
    <asp:Label ID="Label2" runat="server" Text="Выберите факультет"></asp:Label>
            <br />
    <asp:DropDownList ID="DropDownList2" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DropDownList2_SelectedIndexChanged">
    </asp:DropDownList>
            <br />
            <asp:Label ID="Label3" runat="server" Text="Выберите кафедру"></asp:Label>
            <br />
            <asp:DropDownList ID="DropDownList3" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DropDownList3_SelectedIndexChanged">
            </asp:DropDownList>
            <br />
    <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Сформировать" Width="218px" />
    <br />
    <br />
    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False">
        <Columns>
            <asp:TemplateField HeaderText="Название">
                <ItemTemplate>
                    <asp:Label ID="Label5" runat="server" Text='<%# Bind("BasicId") %>' Visible="False"></asp:Label>
                    <asp:Label ID="Label6" runat="server" Text='<%# Bind("Name") %>' Visible="True"></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Значение">
                <ItemTemplate>
                    <asp:Label ID="Label7" runat="server" Text='<%# Bind("Value") %>' Visible="True"></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Статус">
                <ItemTemplate>
                    <asp:Label ID="Label8" runat="server" Text='<%# Bind("State") %>' Visible="True"></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            
        </Columns>
    </asp:GridView>
            <br />
    </asp:Content>