<%@ Page Language="C#" Title="Данные о пользователе" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="PersonalInfo.aspx.cs" Inherits="KPIWeb.PersonalInfo" %>

<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
    <div>
     
        <br />
    
        <asp:Image ID="Image1" runat="server" ImageUrl="https://pp.vk.me/c625524/v625524720/2064f/l9xw4aIkqac.jpg" />
        <br />
        <br />
        <asp:Label ID="Label1" runat="server" Font-Bold="True" Font-Italic="False" Font-Names="Courier New" Font-Size="X-Large" Text="Логин: "></asp:Label>
        <br />
        <asp:Label ID="Label2" runat="server" Font-Bold="True" Font-Italic="False" Font-Names="Courier New" Font-Size="X-Large" Text="Email: "></asp:Label>
        <br />
        <asp:Label ID="Label3" runat="server" Font-Bold="True" Font-Italic="False" Font-Names="Courier New" Font-Size="X-Large" Text="Логин: "></asp:Label>
        <br />
        <asp:Label ID="Label4" runat="server" Font-Bold="True" Font-Italic="False" Font-Names="Courier New" Font-Size="X-Large" Text="Логин: "></asp:Label>
        <br />
        <asp:Label ID="Label5" runat="server" Font-Bold="True" Font-Italic="False" Font-Names="Courier New" Font-Size="X-Large" Text="Логин: "></asp:Label>
        <br />
        <asp:Label ID="Label6" runat="server" Font-Bold="True" Font-Italic="False" Font-Names="Courier New" Font-Size="X-Large" Text="Логин: "></asp:Label>
        <br />
        <asp:Label ID="Label7" runat="server" Font-Bold="True" Font-Italic="False" Font-Names="Courier New" Font-Size="X-Large" Text="Логин: "></asp:Label>
        <br />
        <asp:Label ID="Label8" runat="server" Font-Bold="True" Font-Italic="False" Font-Names="Courier New" Font-Size="X-Large" Text="Логин: "></asp:Label>
        <br />
        <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Сменить пароль" CssClass="form-control" />
        <br />
        <br />
        <asp:Label ID="Label9" runat="server" Text="Текущий пароль" Visible="False"></asp:Label>
&nbsp;&nbsp;
        <asp:TextBox ID="TextBox1" TextMode="Password" runat="server" Visible="False" CssClass="form-control"></asp:TextBox>
        <br />
        <br />
        <asp:Label ID="Label10" runat="server" Text="Новый пароль" Visible="False"></asp:Label>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:TextBox ID="TextBox2" TextMode="Password" runat="server" Visible="False" CssClass="form-control"></asp:TextBox>
        <br />
        <br />
        <asp:Label ID="Label11" runat="server" Text="Подтверждение" Visible="False"></asp:Label>
&nbsp;&nbsp;&nbsp;
        <asp:TextBox ID="TextBox3" TextMode="Password" runat="server" Visible="False" CssClass="form-control"></asp:TextBox>
        <br />
        <br />
        <asp:Button ID="Button2" runat="server" Text="Сохранить изменения" Visible="False" CssClass="form-control" OnClick="Button2_Click" />
        <br />
        <br />
        <br />
        <br />
    
    </div>

       </asp:Content>