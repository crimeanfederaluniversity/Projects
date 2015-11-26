<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="OwnersProcessInfo.aspx.cs" Inherits="KPIWeb.OwnersProcessInfo" %>

<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">

    
     Введите пароль</span>
     <br />
    <asp:TextBox ID="TextBox1" runat="server" Width="292px"></asp:TextBox>
     <br />
        <asp:Button ID="Button1" runat="server" OnClientClick="showLoadPanel()" Text="Показать" Width="307px" OnClick="Button1_Click" />

     <br />
     <br />
     <br />
     Список структурных подразделений не начавших внесение данных, с email адресами ответственных за ввод.<br />
     (На данный момент есть несколько &quot;лишних&quot; структурных подразделений).<asp:GridView ID="GridView1" runat="server">
     </asp:GridView>

    <br />
      <br />
    Описание<br />
    Название 1/2/3/4/5<br />
    1) Всего должно быть<br />
    2) Утвердили данные<br />
    3) Отправили на утверждение<br />
    4) Начали работу с системой<br />
    5) Не входили на страницу заполнения отчета<br />
     <br />
     <asp:Label ID="Label1" runat="server" Visible="false" Text="Label"></asp:Label>
     <br />
     <asp:Label ID="Label2" runat="server" Visible="false" Text="Label"></asp:Label>
     <br />
     <asp:Label ID="Label3" runat="server" Visible="false" Text="Label"></asp:Label>
     <br />
     <asp:Label ID="Label4" runat="server" Visible="false" Text="Label"></asp:Label>
     <br />
     <asp:Label ID="Label5" runat="server" Visible="false" Text="Label"></asp:Label>
     <br />
     <br />
     <asp:TreeView ID="TreeView1" runat="server" ShowLines="True">
     </asp:TreeView>

</asp:Content>