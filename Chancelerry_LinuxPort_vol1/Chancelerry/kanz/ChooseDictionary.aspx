<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ChooseDictionary.aspx.cs" Inherits="Chancelerry.kanz.ChooseDictionary" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <link href="../Content/Site.css" rel="stylesheet" />
    <br />
    Выберите справочник
    <asp:DropDownList ID="DictionarysList" runat="server" AutoPostBack="True" Height="20px" OnSelectedIndexChanged="DictionarysList_SelectedIndexChanged" Width="263px"></asp:DropDownList>

    <br />
    <div runat="server" id="divForTable">
        
        
        
        <br />
        
        </div>
        <asp:ImageButton ID="AddValue" runat="server" ImageUrl="~/kanz/icons/addButtonIcon.jpg" Height="20px" OnClick="AddValue_Click" Width="20px" />
        
    <br />

</asp:Content>
