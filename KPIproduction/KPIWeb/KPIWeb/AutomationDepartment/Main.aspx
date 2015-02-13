<%@ Page Language="C#" MasterPageFile="~/Site.Master"  Title="Администрированние" AutoEventWireup="true" EnableViewStateMac="false" CodeBehind="Main.aspx.cs" Inherits="KPIWeb.AutomationDepartment.Main" %>


<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
  


       <h2>Администрирование</h2>
        <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Редактировать структуру университета" Width="400px" Height="50" Font-Size="Large"/>
        <br />
        <br />
        <asp:Button ID="Button2" runat="server" OnClick="Button2_Click" Text="Регистрация нового пользователя" Width="400px" Height="50" Font-Size="Large"/>
        <br />
        <br />
        <asp:Button ID="Button3" runat="server" OnClick="Button3_Click" Text="Распределить показатели по ролям" Width="400px" Height="50" Font-Size="Large"/>
        <br />
        <br />
        <asp:Button ID="Button4" runat="server" OnClick="Button4_Click" Text="Проверить полноту перекрытия показателей" Width="400px" Height="50" Font-Size="Large"/>
 
</asp:Content>
