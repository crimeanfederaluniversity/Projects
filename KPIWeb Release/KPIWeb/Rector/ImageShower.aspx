<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ImageShower.aspx.cs" Inherits="KPIWeb.Rector.NewInt.ImageShower" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <br />
    <br />
    <asp:Image ID="mainImage" CssClass="imageClass" runat="server" />

    <style>
        .imageClass
        {
            max-width:100%;
            height:auto;
            
        }
    </style>

</asp:Content>
