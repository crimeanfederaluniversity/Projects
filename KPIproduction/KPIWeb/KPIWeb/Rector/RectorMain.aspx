<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="RectorMain.aspx.cs" Inherits="KPIWeb.Rector.RectorMain" %>
<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">   
    <style type="text/css">
   .button_right 
   {
       float:right
   }     
</style>    
    <asp:Panel runat="server" ID="top_panel2" CssClass="top_panel" Height="40" Visible="true">    
        <div>    
      <asp:Button ID="GoBackButton" runat="server" OnClick="GoBackButton_Click" Text="Назад" Width="125px" />
      <asp:Button ID="GoForwardButton" runat="server" OnClick="GoForwardButton_Click" Text="Вперед" Width="125px" />
        &nbsp; &nbsp; <asp:Button ID="Button2" runat="server" OnClick="Button4_Click" Text="На главную" Width="125px" />
        &nbsp; &nbsp;       
        <asp:Button ID="Button5" runat="server" CssClass="button_right" OnClick="Button5_Click" Text="Нормативные документы" Width="225px" />
        &nbsp; &nbsp;
        <asp:Button ID="Button6" runat="server" CssClass="button_right" OnClick="Button6_Click" Text="Button" Width="150px" Visible="False" />
        &nbsp; &nbsp;
        <asp:Button ID="Help" runat="server" CssClass="button_right"  Text="Помощь" Width="150px" />
        </div>

    </asp:Panel>

    <div>
    
        <br />
        Выберите пункт<br />
    
    </div>
    

        <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Целевые показатели" Width="350px" />
&nbsp;<asp:Button ID="Button3" runat="server" OnClick="Button3_Click" Text="Расчетные показатели" Width="350px" />
    <asp:Button ID="Button4" runat="server" OnClick="Button4_Click1" Text="Нормативные документы" Width="351px" />
</asp:Content>
