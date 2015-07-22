<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ReturnReportForUser.aspx.cs" Inherits="KPIWeb.AutomationDepartment.ReturnReportForUser" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">   
    <h2>Работа со статусом данных</h2>
 <div>
    
        <asp:Label ID="Label1" runat="server" Text="Выбрать отчет"></asp:Label>
        <br />
        <asp:DropDownList ID="DropDownList1" runat="server" Height="28px" Width="315px">
        </asp:DropDownList>
        <br />
        <br />
        <asp:Label ID="Label2" runat="server" Text="Пользователь"></asp:Label>
        <br />
        <asp:TextBox ID="TextBox1" runat="server" Width="302px"></asp:TextBox>
        <br />
        <br />
        <asp:Label ID="Label3" runat="server" Text="Выбрать действие"></asp:Label>
        <br />
        <asp:DropDownList ID="DropDownList2" runat="server" Height="44px" Width="249px">
        </asp:DropDownList>
        <br />
        <br />
        <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Выполнить" />
    
    </div>
 </asp:Content>