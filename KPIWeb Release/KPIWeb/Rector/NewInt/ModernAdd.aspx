<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="ModernAdd.aspx.cs" Inherits="KPIWeb.Rector.NewInt.ModernAdd" %>
<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">   
    
    <asp:Panel runat="server" ID="top_panel32" CssClass="top_panel" Height="40" Visible="true">    
                <div>    
              <asp:Button ID="GoBackButton3" runat="server" OnClientClick="showLoadPanel()" OnClick="GoBackButton_Click" Text="Назад" Width="125px" />
              <asp:Button ID="GoForwardButton3" runat="server" OnClientClick="showLoadPanel()" OnClick="GoForwardButton_Click" Text="Вперед" Width="125px" />
                &nbsp; &nbsp; <asp:Button ID="Butto3n4" runat="server" OnClientClick="showLoadPanel()" OnClick="Button4_Click" Text="На главную" Width="125px" />
                    &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                </div>

            </asp:Panel>

    <div>
    
    
        <asp:Label ID="Label1" runat="server" Text="Модернизация образовательной деятельности университета на базе современных образовательных технологий и с учетом перспективной потребности экономики причерноморского макрорегиона в квалифицированных кадрах"></asp:Label>
        <br />
        <br />
        <asp:Button ID="Button1" runat="server" Text="Button" />
        <br />
        <asp:Button ID="Button2" runat="server" Text="Button" />
        <br />
        <asp:Button ID="Button3" runat="server" Text="Button" />
        <br />
    
    
    </div>
  </asp:Content>
