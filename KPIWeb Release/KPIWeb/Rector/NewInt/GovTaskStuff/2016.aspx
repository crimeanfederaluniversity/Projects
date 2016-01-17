<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="_2016.aspx.cs" Inherits="KPIWeb.Rector.NewInt.GovTaskStuff._2016" %>
<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    
    <asp:Panel runat="server" ID="top_panel1212" CssClass="top_panel" Height="40" Visible="true">    
                <div>    
              <asp:Button ID="GoBackButton142" runat="server" OnClientClick="showLoadPanel()" OnClick="GoBackButton_Click" Text="Назад" Width="125px" />
                    &nbsp;&nbsp;
              <asp:Button ID="Button1264" runat="server" OnClientClick="showLoadPanel()" OnClick="Button4_Click" Text="На главную" Width="125px" />
                    &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                </div>

            </asp:Panel>

    <div>
    
        <asp:HyperLink ID="HyperLink1" runat="server">Бакалавры</asp:HyperLink>
        <br />
        <asp:HyperLink ID="HyperLink2" runat="server">Специалисты</asp:HyperLink>
        <br />
        <asp:HyperLink ID="HyperLink3" runat="server">Магистры</asp:HyperLink>
        <br />
        <asp:HyperLink ID="HyperLink4" runat="server">СПО</asp:HyperLink>
        <br />
        <asp:HyperLink ID="HyperLink5" runat="server">Аспирантура</asp:HyperLink>
        <br />
        <asp:HyperLink ID="HyperLink6" runat="server">Интернатура</asp:HyperLink>
        <br />
        <asp:HyperLink ID="HyperLink7" runat="server">Ординатура</asp:HyperLink>
        <br />
        <asp:HyperLink ID="HyperLink8" runat="server">Докторантура</asp:HyperLink>
        <br />
        <asp:HyperLink ID="HyperLink9" runat="server">Специальности не вошедшие в ГЗ</asp:HyperLink>
    
    </div>
    

</asp:Content>
