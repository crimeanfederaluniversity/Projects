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
    <asp:TextBox ID="TextBox1" runat="server" Height="39px" TextMode="MultiLine" Width="705px"></asp:TextBox>
    <br />
    <br />
    <asp:Button ID="Button4" runat="server" Visible="False" OnClick="Button4_Click" Text="Создание связи для новых показателей (не нажимай а то плохо будет)" Width="710px" />
    <br />
    <br />
    <asp:Button ID="Button5" runat="server" OnClick="Button5_Click" Text="Button" />
    <br />
    <br />
    <br />
    <asp:Button ID="Button6" runat="server" OnClick="Button6_Click" Text="Button" />
    <br />
    <br />
    <asp:Button ID="Button7" runat="server" OnClick="Button7_Click" Text="Button" />
    <br />
    <br />
    <asp:GridView ID="GridView1" runat="server">
    </asp:GridView>
    <br />
</asp:Content>
