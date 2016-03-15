<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Dashboard.aspx.cs" Inherits="EDM.edm.Dashboard" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <asp:Label ID="Label1" runat="server" Text="Label" CssClass="header"></asp:Label>
    <br />
        <div class="row centered-content position-rel">

        <div class="row">
            <asp:Button ID="Button4" runat="server" Text="Запустить новый процесс согласования" OnClick="Button4_Click" CssClass="btn btn-default" OnClientClick="showSimpleLoadingScreen()"/>
        </div>
        
        <div class="edm-main-content">
            
            <asp:Button ID="Button1" runat="server" Text="Инициированные согласования" OnClick="Button1_Click" CssClass="btn btn-default col-sm-3" OnClientClick="showSimpleLoadingScreen()" />
    
            <asp:Button ID="Button2" runat="server" Text="Входящие" OnClick="Button2_Click" CssClass="btn btn-default col-sm-3" OnClientClick="showSimpleLoadingScreen()"/>
    
            <asp:Button ID="Button3" runat="server" Text="Архив" OnClick="Button3_Click" CssClass="btn btn-default col-sm-3" OnClientClick="showSimpleLoadingScreen()"/>
        </div>
            <br />
    <asp:GridView ID="dashGridView" runat="server" AutoGenerateColumns="False" OnRowCommand="GridView1_RowCommand" OnRowDataBound="dashGridView_RowDataBound" CssClass="table table-striped edm-table" AllowPaging="True" PageSize="50" OnPageIndexChanging="dashGridView_PageIndexChanging"> 
    <PagerSettings Mode="Numeric" />
	<PagerStyle BackColor="#FFCC66" ForeColor="#444" HorizontalAlign="Center" />
    </asp:GridView>
</div>
</asp:Content>
