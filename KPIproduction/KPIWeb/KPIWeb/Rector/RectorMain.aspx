<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="RectorMain.aspx.cs" Inherits="KPIWeb.Rector.RectorMain" %>
<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">   
    <div>
    
        <br />
    
    </div>
        <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Целевые показатели" Width="350px" />
&nbsp;<asp:Button ID="Button3" runat="server" OnClick="Button1_Click" Text="Расчетные показатели" Width="350px" />
        <asp:Button ID="Button2" runat="server" Text="Нормативные документы" Width="350px" OnClick="Button2_Click" />
</asp:Content>
