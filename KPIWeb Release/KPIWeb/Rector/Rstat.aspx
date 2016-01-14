<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Rstat.aspx.cs" Inherits="KPIWeb.Rector.Rstat" %>
<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">



    <div>
    <asp:Panel runat="server" ID="top_panel2" CssClass="top_panel" Height="40" Visible="true">    
        <div>    
      <asp:Button ID="GoBackButton" runat="server" OnClientClick="showLoadPanel();"  Text="Назад" Width="125px" Enabled="True" OnClick="Button222_Click" />
            &nbsp; &nbsp; &nbsp;&nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp; &nbsp; &nbsp; &nbsp;
        </div>

    </asp:Panel>
     
           </div>
    <asp:Button ID="Button1" runat="server" Text="ВПО очное" OnClick="Button1_Click" />

    <asp:Button ID="Button2" runat="server" OnClick="Button2_Click" Text="СПО очное" />
    <br />
    <asp:Button ID="Button3" runat="server" OnClick="Button3_Click" Text="ВПО заочное" />
    <asp:Button ID="Button4" runat="server" OnClick="Button4_Click" Text="СПО заочное" />
    <br />
    <asp:Button ID="Button5" runat="server" OnClick="Button5_Click" Text="ВПО очно-заочное" />
    
    
</asp:Content>
