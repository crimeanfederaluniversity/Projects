<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="RMain.aspx.cs" Inherits="KPIWeb.Rector.RMain" %>
<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">  

    <div>
    
        <br />
    
        <asp:Button ID="Button1" runat="server" Text="Анализ целевых показателей" OnClick="Button1_Click" Height="100px" Width="400px" />
&nbsp;<asp:Button ID="Button2" runat="server" Text="Рейтинг структурных подразделений" Height="100px" OnClick="Button2_Click" Width="400px" />
    
    </div>

</asp:Content>