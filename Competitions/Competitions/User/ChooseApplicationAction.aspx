﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ChooseApplicationAction.aspx.cs" Inherits="Competitions.User.ChooseApplicationAction" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    
    <asp:Panel runat="server" ID="top_panel2" CssClass="top_panel" Height="40" Visible="true">    
    <div>    
        <asp:Button ID="GoBackButton" runat="server" OnClientClick="showLoadPanel()" Text="Назад" Width="125px" OnClick="GoBackButton_Click" />   
         <asp:Button ID="Button1" runat="server" OnClientClick="showLoadPanel()" Text="На главную" Width="125px" OnClick="Button2_Click" /> 
    </div> 
</asp:Panel> 
        
        <style type="text/css">
            
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
        <br />
    <br />
    <asp:Label ID="Label1" style="font-size: 20px" runat="server" Text="Label"></asp:Label>
    <br />
    <br />
    <p>
        <asp:GridView ID="BlockGV" runat="server" AutoGenerateColumns="False">
            <Columns>
                <asp:BoundField DataField="ID"   HeaderText="" Visible="false" />
                <asp:BoundField DataField="BlockName"   HeaderText="Название" Visible="true" />   
                   
                <asp:TemplateField HeaderText="Перейти к заполнению">
                        <ItemTemplate>
                            <asp:Button ID="FillButton" runat="server" CommandName="Select" Text="Заполнить" CommandArgument='<%# Eval("ID") %>' Width="200px" OnClick="FillButtonClick"/>
                        </ItemTemplate>
                </asp:TemplateField>
                           
            </Columns>
        </asp:GridView>
    </p>
<p>
        <asp:GridView ID="DocumentsGV" runat="server" AutoGenerateColumns="False">
            <Columns>
                <asp:BoundField DataField="ID"   HeaderText="" Visible="false" />
                <asp:BoundField DataField="Name"   HeaderText="Название" Visible="true" />   
                <asp:BoundField DataField="CreateDate"   HeaderText="Дата загрузки" Visible="true" />     
                <asp:TemplateField HeaderText="Скачать документ">
                        <ItemTemplate>
                            <asp:Button ID="OpenButton" runat="server" CommandName="Select" Text="Скачать" CommandArgument='<%# Eval("ID") %>' Width="200px" OnClick="OpenButtonClick"/>
                        </ItemTemplate>
                </asp:TemplateField>
                
                <asp:TemplateField HeaderText="Удалить документ">
                        <ItemTemplate>
                            <asp:Button ID="DeleteButton" runat="server" CommandName="Select" OnClientClick="return confirm('Вы уверены что хотите удалить документ?');" Text="Удалить" CommandArgument='<%# Eval("ID") %>' Width="200px" OnClick="DeleteButtonClick"/>
                        </ItemTemplate>
                </asp:TemplateField>
                          
            </Columns>
        </asp:GridView>
    <p>
        <asp:Button ID="Button2" runat="server" OnClick="Button2_Click1" Text="Скачать в архив" />
    <p>
        <asp:FileUpload ID="FileUpload1" runat="server" AllowMultiple="True" />
    </p>
<p>
        <asp:Button ID="AddDocumentsButton" runat="server" OnClick="AddDocumentsButton_Click" Text="Загрузить" />
    </p>
    <p>
        &nbsp;</p>
</asp:Content>
