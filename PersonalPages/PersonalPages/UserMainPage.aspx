﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="UserMainPage.aspx.cs" Inherits="PersonalPages.UserMainPage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
  
        <h2>Добро пожаловать в личный кабинет пользователя!&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            </h2>
    <div>
         <br />
         <asp:Button ID="Button1" runat="server" Text="Рейтинги" Width="200px" OnClick="Button1_Click" />
         &nbsp;&nbsp;&nbsp;&nbsp;
         <asp:Button ID="Button8" runat="server" Text="Мои дисциплины и расписание" Width="235px" OnClick="Button8_Click" />
         <br />
         <br />
         <asp:Button ID="Button2" runat="server" Text="Конкурсы" Width="200px" OnClick="Button2_Click" />
         &nbsp;&nbsp;&nbsp;&nbsp;
         <asp:Button ID="Button9" runat="server" OnClick="Button9_Click" Text="Учебная деятельность" Width="235px" />
         <br />
         <br />
         <asp:Button ID="Button3" runat="server" Text="Индикаторы" Width="200px" OnClick="Button3_Click" />
         <br />
         <br />
         <asp:Button ID="Button4" runat="server" Text="Мониторинг" Width="200px" OnClick="Button4_Click" />
         <br />
         <br />
         <asp:Button ID="Button5" runat="server" Text="Документооборот" Width="200px" OnClick="Button5_Click" />
         <br />
        <br />
         <asp:Button ID="Button6" runat="server" Text="Научная библиотека" Width="200px" />
         <br />
        <br />
         <asp:Button ID="Button7" runat="server" Text="Репозиторий" Width="200px" />
         <br />
         <br />
    </div>
        
    
   
</asp:Content>