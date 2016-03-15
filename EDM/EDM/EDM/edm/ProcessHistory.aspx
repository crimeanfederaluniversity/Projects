﻿<%@ Page Title="История согласования" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ProcessHistory.aspx.cs" Inherits="EDM.edm.ProcessHistory" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <style type="text/css">
    .top_panel {
    position:fixed;
    left:0;
    top:3.5em;
    width:100%;
    height:30px;
    background-color:#70463A !important;
    z-index:10;
    color:#05ff01;  
    padding-top:5px;
    font-weight:bold;
}
   .button_right 
   {
       float:right
   }     
</style> 
<asp:Panel runat="server" ID="top_panel2" CssClass="top_panel" Height="40" Visible="true">    
    <div>          
        &nbsp;&nbsp;&nbsp;&nbsp;<asp:Button ID="goBackButton" CausesValidation="false" runat="server" Enabled="true" CssClass="btn btn-default" Text="Назад" Width="150" Height="30" OnClientClick="showSimpleLoadingScreen()" OnClick="goBackButton_Click" />
        &nbsp;&nbsp;&nbsp;&nbsp;<asp:Button ID="goForwardButton" CausesValidation="false" Enabled="false" CssClass="btn btn-default" runat="server" Text="Вперед" Width="150" Height="30" OnClientClick="history.forward ()"/>
    </div>
</asp:Panel>
<br />
<br />
      <%--<style type="text/css">
   TABLE {
    border-collapse: collapse; /* Убираем двойные линии между ячейками */
    width: 300px; /* Ширина таблицы */
   }
   TH, TD {
    border: 1px solid black; /* Параметры рамки */
    text-align: center; /* Выравнивание по центру */
    padding: 4px; /* Поля вокруг текста */
   }
   TH {
    background: #fc0; /* Цвет фона ячейки */
    height: 40px; /* Высота ячеек */
    vertical-align: bottom; /* Выравнивание по нижнему краю */
    padding: 0; /* Убираем поля вокруг текста */
   }
  </style>--%>

    <div runat="server" id="TitleDiv">
        <br/>
        <asp:Label ID="InitiatorLabel" runat="server" Text=""></asp:Label>
        <br/>
        <asp:Label ID="ProcessNameLabel" runat="server" Text=""></asp:Label>
        <br/>
        <asp:Label ID="ProcessTypeLabel" runat="server" Text=""></asp:Label>
        <br/>
        <asp:Label ID="StartDateLebel" runat="server" Text=""></asp:Label>
        <br/>
        <asp:Label ID="EndDateLabel" runat="server" Text=""></asp:Label>
        <br/>
        <asp:Label ID="ProcessStatus" runat="server" Text=""></asp:Label>
        <br/>
    </div>
    
    <div runat="server" id="historyTableDiv" class="centered-block">
</div>
</asp:Content>
