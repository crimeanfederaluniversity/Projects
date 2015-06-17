<%@ Page Language="C#" AutoEventWireup="true"  MasterPageFile="~/Site.Master" CodeBehind="RCalculate.aspx.cs" Inherits="KPIWeb.Rector.RCalculate" %>
<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">  
    <asp:Button ID="Button1" runat="server" Text="Рассчет для ректора (очень долго)" OnClick="Button1_Click" Width="716px" />
    <br />
    <br />
    <asp:Button ID="Button2" runat="server" OnClick="Button2_Click" Text="Не нажимать (пересчитывае все рассчетные показатели)" Width="716px" />
    <br />
<br />
<asp:Button ID="Button3" runat="server" OnClick="Button3_Click" Text="Утвердить все недоутвержденные" Width="716px" />
<br />
    <br />
    <asp:TextBox ID="TextBox1" runat="server" Height="157px" TextMode="MultiLine" Width="701px"></asp:TextBox>
    <br />
</asp:Content>
