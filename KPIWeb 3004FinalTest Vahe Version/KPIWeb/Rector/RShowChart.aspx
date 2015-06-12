<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="RShowChart.aspx.cs" Inherits="KPIWeb.Rector.RShowChart" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">  

    <asp:TextBox ID="TextBox1" runat="server" Height="108px" TextMode="MultiLine" Width="300px"></asp:TextBox>

&nbsp;
    <br />
    <br />
    <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Посчитать для выбранных показателей" />
    <br />
    <br />
    <br />
    <hr />
    <br />
    тут ID показателя
    <asp:TextBox ID="TextBox2" runat="server" Width="283px">1029 </asp:TextBox>
    <br />
    <asp:Button ID="Button2" runat="server" OnClick="Button2_Click" Text="Посчитать для ID показателя по академиям" Width="556px" />
    <br />
    <br />
    <hr />
    <br />
    тут ID показателя
    <asp:TextBox ID="TextBox3" runat="server" Width="283px">1029 </asp:TextBox>
    <br />
    <asp:Button ID="Button3" runat="server" OnClick="Button3_Click" Text="Посчитать для ID показателя по факультетам" Width="556px" />
    <br />
        <br />
    <hr />
    <br />
    тут ID академии
    <asp:TextBox ID="TextBox4" runat="server" Width="283px">1016 </asp:TextBox>
    <br />
    <asp:Button ID="Button4" runat="server" OnClick="Button4_Click" Text="Посчитать для ID Академии все показатели" Width="556px" />
    <br />
</asp:Content>