<%@ Page Language="C#" AutoEventWireup="true"  MasterPageFile="~/Site.Master" CodeBehind="RCalculate.aspx.cs" Inherits="KPIWeb.Rector.RCalculate" %>
<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">  
<asp:Button ID="Button1" runat="server" Text="Рассчет (очень долго)" OnClick="Button1_Click" Width="332px" />
    <br />
    <br />
    <asp:Button ID="Button2" runat="server" OnClick="Button2_Click" Text="Не нажимать" Width="329px" />
    <br />
    <br />
</asp:Content>
