﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Subordinate.aspx.cs" Inherits="EDM.edm.Subordinate" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <script src="calendar_ru.js" type="text/javascript"> </script>

    <asp:Label ID="directionLabel" runat="server" Text="Заголовок" CssClass="header" Visible="False" ></asp:Label>
     <br />
        <div class="row centered-content">
        
        <div class="edm-main-content">
            
            <asp:Button ID="Button10" runat="server" Text="История процессов" CssClass="btn btn-default col-sm-3" OnClientClick="showSimpleLoadingScreen()" OnClick="Button10_Click" />
    
            <asp:Button ID="Button20" runat="server" Text="Статистика" CssClass="btn btn-default col-sm-3" OnClientClick="showSimpleLoadingScreen()" OnClick="Button20_Click"/>
    
        </div>
        <div runat="server" ID="searchDiv">
            <asp:TextBox ID="NameSearchBox" runat="server" placeholder="Введите имя" ></asp:TextBox> 
            <asp:TextBox ID="DateSearchBox" runat="server" onfocus="this.select();lcs(this)" onclick="event.cancelBubble=true;this.select();lcs(this)" placeholder="Выберите дату"></asp:TextBox>
            <asp:Button ID="SearchButton" runat="server" Text="Поиск" OnClientClick="showSimpleLoadingScreen()" OnClick="SearchButton_Click"/>
        </div>
            <br />
    <asp:GridView ID="subGridView" runat="server" AutoGenerateColumns="False" OnRowCommand="subGridView_RowCommand"  CssClass="table edm-table edm-history-table centered-block" AllowPaging="True" PageSize="50" OnPageIndexChanging="subGridView_PageIndexChanging" > 
    <PagerSettings Mode="Numeric" />
	<PagerStyle BackColor="#FFCC66" ForeColor="#444" HorizontalAlign="Center" />
    </asp:GridView>
</div>
</asp:Content>
