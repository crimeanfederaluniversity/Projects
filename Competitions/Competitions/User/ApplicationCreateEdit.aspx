<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="ApplicationCreateEdit.aspx.cs" Inherits="Competitions.User.ApplicationCreateEdit" %>
<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
   <asp:Panel runat="server" ID="top_panel2" CssClass="top_panel" Height="40" Visible="true">    
    <div>    
     <asp:ImageButton ID="GoBackButton" runat="server" OnClientClick="showLoadPanel()" Width="30px" OnClick="GoBackButton_Click" ImageAlign="Middle" ImageUrl="~/Images/Back.png" ToolTip="К заявке"/>
 <asp:ImageButton ID="Button2" runat="server" OnClientClick="showLoadPanel()"  Width="30px" OnClick="Button2_Click" ImageUrl="~/Images/Home.png" ImageAlign="Middle" ToolTip="На главную" />
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

      <div>   
      
        <br />
                <h2><span style="font-size: 30px">Создание новой заявки  </span></h2>
        <br />
        Название Вашего проекта <br />
        <asp:TextBox ID="ApplicationNameTextBox" runat="server" Height="38px" Width="484px"></asp:TextBox>
          
          <asp:RequiredFieldValidator runat="server" ID="TextBox" ControlToValidate="ApplicationNameTextBox" Enabled="True" Text="Введите название Вашего проекта "  ForeColor="Red" > 
                            </asp:RequiredFieldValidator>
        <br />
        <br />
        <br />
        <asp:Button ID="CreateEditButton" runat="server" OnClick="CreateEditButton_Click" Text="Создать заявку" />
    
    </div>
</asp:Content>