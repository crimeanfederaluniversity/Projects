﻿<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ChooseSection.aspx.cs" Inherits="Competitions.User.ChooseSection" %>
<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">   
    <asp:Panel runat="server" ID="top_panel2" CssClass="top_panel" Height="40" Visible="true">    
    <div>    
                &nbsp;&nbsp;&nbsp;    
                <asp:ImageButton ID="GoBackButton" ImageAlign="Middle" ImageUrl="~/Images/Back.png" OnClick="GoBackButton_Click" OnClientClick="showLoadPanel()" runat="server" Width="30px" />
                &nbsp;
         <asp:ImageButton ID="Button2" runat="server" OnClick="Button2_Click" OnClientClick="showLoadPanel()" ImageAlign="Middle"  Width="30px"  ImageUrl="~/Images/Home.png"  />
    </div> 
</asp:Panel> 

    <style>
            
            .top_panel {
    position:fixed;
    left:0;
    top:3.5em;
    width:100%;
    height:30px;
    background-color:#222222;
    z-index:10;
    color:#05ff01;  
    padding-top:5px;
    font-weight:bold;
}
        </style>
     <div>  
         <br />
        <br />
        <asp:GridView ID="ApplicationGV" runat="server" AutoGenerateColumns="False">
            <Columns>
                <asp:BoundField DataField="ID" HeaderText="" Visible="false" />
                <asp:BoundField DataField="Name" HeaderText="Название" Visible="true" />
                <asp:BoundField DataField="Status" HeaderText="Состояние" Visible="false" />
                <asp:TemplateField HeaderText="Заполнение">
                    <ItemTemplate>
                        <asp:Button ID="FillButton" runat="server" CommandArgument='<%# Eval("ID") %>' CommandName="Select" OnClick="FillButtonClick" Text="Заполнить" Width="200px" />
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
           </div> 
</asp:Content>
