<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="GovTask.aspx.cs" Inherits="KPIWeb.Rector.NewInt.GovTask" %>
<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
  
        <asp:Panel runat="server" ID="top_panel252" CssClass="top_panel" Height="40" Visible="true">    
                <div>    
              <asp:Button ID="GoBackButton52" runat="server" OnClientClick="showLoadPanel()" OnClick="GoBackButton_Click" Text="Назад" Width="125px" />
              <asp:Button ID="GoForwardButton52" runat="server" OnClientClick="showLoadPanel()" OnClick="GoForwardButton_Click" Text="Вперед" Width="125px" />
                &nbsp; &nbsp; <asp:Button ID="Button254" runat="server" OnClientClick="showLoadPanel()" OnClick="Button4_Click" Text="На главную" Width="125px" />
                    &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                </div>

            </asp:Panel>  
       
    <div>
    
        <asp:Button ID="Button257" runat="server" Text="Button" />
        <br />
        <asp:Button ID="Button258" runat="server" Text="Button" />
        <br />
    
        <asp:Button ID="Button255" runat="server" Text="Госзадание №4 (НАУКА)" OnClick="Button255_Click" />
&nbsp;<asp:Button ID="Button256" runat="server" Text="Госзадание №4 (СПЕЦЧАСТЬ)" OnClick="Button256_Click" />
    
    </div>
    

</asp:Content>