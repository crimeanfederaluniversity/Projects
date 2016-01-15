<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="SvedofResult.aspx.cs" Inherits="KPIWeb.Rector.NewInt.SvedofResult" %>
<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">   
    
    <asp:Panel runat="server" ID="top_panel2" CssClass="top_panel" Height="40" Visible="true">    
                <div>    
              <asp:Button ID="GoBackButton" runat="server" OnClientClick="showLoadPanel()" OnClick="GoBackButton_Click" Text="Назад" Width="125px" />
              <asp:Button ID="GoForwardButton" runat="server" OnClientClick="showLoadPanel()" OnClick="GoForwardButton_Click" Text="Вперед" Width="125px" />
                &nbsp; &nbsp; <asp:Button ID="Button4" runat="server" OnClientClick="showLoadPanel()" OnClick="Button4_Click" Text="На главную" Width="125px" />
                &nbsp; &nbsp;       
                <asp:Button ID="Button5" runat="server" CssClass="button_right"  OnClick="Button5_Click" Text="Нормативные документы" Width="250px" OnClientClick="showLoadPanel()" />
                    &nbsp; &nbsp; &nbsp; &nbsp;
                </div>

            </asp:Panel>
    
    <div>
    
        <asp:Label ID="Label1" runat="server" Text="Сведения о результатах реализации Программы развития"></asp:Label>
    
        <br />
        <asp:Button ID="Button6" runat="server" Text="Button" />
        <br />
        <asp:Button ID="Button7" runat="server" Text="Button" />
        <br />
        <asp:Button ID="Button8" runat="server" Text="Button" />
        <br />
        <asp:Button ID="Button9" runat="server" Text="Button" />
        <br />
        <asp:Button ID="Button10" runat="server" Text="Button" />
    
    </div>
   </asp:Content>
