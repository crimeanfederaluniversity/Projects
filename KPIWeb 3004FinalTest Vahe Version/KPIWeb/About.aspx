<%@ Page Title="Справка" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="About.aspx.cs" Inherits="KPIWeb.About" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <style>
        .divaroundhlp {
            background-color: #ffffff;
            opacity: 0.9;
        }
    </style>
    <h2><%: Title %></h2>
    <h3>Справочники:</h3>
    <p>
        <span class="divaroundhlp"><asp:Label ID="LinksLable" runat="server" Text="Label"></asp:Label></span>
    </p>
    <p>&nbsp;</p>
    <div>          
        <br />
      
     
    
 </div>
</asp:Content>
