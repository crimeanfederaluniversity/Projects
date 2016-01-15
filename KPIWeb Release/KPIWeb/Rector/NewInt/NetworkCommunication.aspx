<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="NetworkCommunication.aspx.cs" Inherits="KPIWeb.Rector.NewInt.NetworkCommunication" %>
<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">   
    
        <asp:Panel runat="server" ID="top_panel122" CssClass="top_panel" Height="40" Visible="true">    
                <div>    
              <asp:Button ID="GoBackButton12" runat="server" OnClientClick="showLoadPanel()" OnClick="GoBackButton_Click" Text="Назад" Width="125px" />
              <asp:Button ID="GoForwardButton12" runat="server" OnClientClick="showLoadPanel()" OnClick="GoForwardButton_Click" Text="Вперед" Width="125px" />
                &nbsp; &nbsp; <asp:Button ID="Button124" runat="server" OnClientClick="showLoadPanel()" OnClick="Button4_Click" Text="На главную" Width="125px" />
                    &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                </div>

            </asp:Panel>

    <div>
    
        <asp:Label ID="Label1" runat="server" Text="Сетевое взаимодействие и повышение квалификации на базе партнерских организаций"></asp:Label>
        <br />
        <br />
        <asp:Button ID="Button125" runat="server" Text="Button" />
        <br />
        <asp:Button ID="Button126" runat="server" Text="Button" />
        <br />
        <asp:Button ID="Button127" runat="server" Text="Button" />
    
    </div>
</asp:Content>
