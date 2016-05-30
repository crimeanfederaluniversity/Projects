<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ShowRegisteAndPrint.aspx.cs" Inherits="Chancelerry.kanz.ShowRegisteAndPrint" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <asp:DropDownList ID="RegistersDropoDownList" runat="server" Height="17px"  Width="284px" AutoPostBack="True" OnSelectedIndexChanged="RegistersDropoDownList_SelectedIndexChanged">
    </asp:DropDownList>
    <br />
    <div id ="chooseFieldsDiv">
        
    </div>
    <br />
    <div id ="resultTableDiv">
        
    </div>
    <br />
    <asp:Button ID="GenerateResultTableButton" runat="server" Text="Сформировать таблицу" Width="283px" OnClick="GenerateResultTableButton_Click" />
    </asp:Content>

    <asp:Content ID="Content2" ContentPlaceHolderID="TableContent" runat="server">
    </asp:Content>
