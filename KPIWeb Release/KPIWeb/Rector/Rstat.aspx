<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Rstat.aspx.cs" Inherits="KPIWeb.Rector.Rstat" %>
<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <style>
        #page_wrapper {
            margin-top: 50px;
        }
         #wrap_both_buttons {
             text-align: center;
         }
        #but_1 {
            margin-top: 25px;
        }
        #but_2 {
              margin-top: 25px;
        }
    </style>

    <div id="page_wrapper">
    <asp:Panel runat="server" ID="top_panel2" CssClass="top_panel" Height="40" Visible="true">    
        <div>    
      <asp:Button ID="GoBackButton" runat="server"   Text="Назад" Width="125px" Enabled="True" OnClick="Button222_Click" />
            <asp:Button ID="Button4" runat="server"  OnClick="Button4_Click" Text="На главную" Width="125px" />
        </div>

    </asp:Panel>
     
    <div id="wrap_both_buttons">
        <div id="but_1">
    <asp:Button ID="Button1" runat="server" Text="Высшее профессиональное образование" OnClick="Button1_Click" />
        </div>
         <div id="but_2">
    <asp:Button ID="Button2" runat="server" OnClick="Button2_Click" Text="Среднее профессиональное образование" />
        </div>
    </div>
    <br />
    <br />
        
    </div>
</asp:Content>
