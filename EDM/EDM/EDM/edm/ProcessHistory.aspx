<%@ Page Title="История согласования" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ProcessHistory.aspx.cs" Inherits="EDM.edm.ProcessHistory" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    
      <style type="text/css">
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
  </style>

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
    
    <div runat="server" id="historyTableDiv">
</div>
</asp:Content>
