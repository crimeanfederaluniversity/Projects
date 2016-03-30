<%@ Page Title="История согласования" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ProcessHistory.aspx.cs" Inherits="EDM.edm.ProcessHistory" %>
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
<br />
<br />
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
