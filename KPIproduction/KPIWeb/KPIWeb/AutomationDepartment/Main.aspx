<%@ Page Language="C#"  MasterPageFile="~/Site.Master"  Title="Администрированние" AutoEventWireup="true" EnableViewStateMac="false" CodeBehind="Main.aspx.cs" Inherits="KPIWeb.AutomationDepartment.Main" %>


<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
  


       <h2>Администрирование</h2>
        <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Редактировать структуру университета" Width="400px" Height="50" Font-Size="Large"/>
        <br />
        <br />
        <asp:Button ID="Button2" runat="server" OnClick="Button2_Click" Text="Регистрация нового пользователя" Width="400px" Height="50" Font-Size="Large"/>
        <br />
        <br />
        <asp:Button ID="Button3" runat="server" OnClick="Button3_Click" Text="Распределить показатели по ролям" Width="400px" Height="50" Font-Size="Large" Enabled="False"/>
        <br />
        <br />
        <asp:Button ID="Button4" runat="server" OnClick="Button4_Click" Text="Проверить полноту перекрытия показателей" Width="400px" Height="50" Font-Size="Large" Enabled="False"/>
 
<br />
<br />
<asp:Button ID="Button5" runat="server" Height="50px" OnClick="Button5_Click" Text="Редактировать индикаторы" Width="400px" />
<br />
<br />
<asp:Button ID="Button6" runat="server" Height="50px" OnClick="Button6_Click" Text="Редактировать базовые показатели" Width="400px" />
<br />
<br />
<asp:Button ID="Button7" runat="server" Height="50px" OnClick="Button7_Click" Text="Редактировать отчёты" Width="400px" />
 
       <br />
       <br />
       <asp:Button ID="Button8" runat="server" Height="50px" OnClick="Button8_Click" Text="Добавить новый шаблон/роль" Width="400px" />
 
       <br />
       <br />
       <asp:Button ID="Button9" runat="server" Height="50px" OnClick="Button9_Click" Text="Добавление области знаний/индикатора/формулы/специальности" Width="400px" />
 
       <br />
       <br />
       <asp:Button ID="Button10" runat="server" Height="50px" OnClick="Button10_Click" Text="База пользователей" Width="400px" />
 
       <br />
       <br />
        
</asp:Content>
