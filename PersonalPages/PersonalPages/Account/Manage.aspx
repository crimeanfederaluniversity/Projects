<%@ Page Title="Учетные данные" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Manage.aspx.cs" Inherits="PersonalPages.Account.Manage" %>

<%@ Register Src="~/Account/OpenAuthProviders.ascx" TagPrefix="uc" TagName="OpenAuthProviders" %>

<asp:Content ContentPlaceHolderID="MainContent" runat="server">
    <div>
     
        <br />
        <br />
        <asp:Label ID="Label1" runat="server" Font-Bold="True" Font-Italic="False" Font-Names="Courier New" Font-Size="X-Large" Text=""></asp:Label>
        <br />
        <asp:Label ID="Label2" runat="server" Font-Bold="True" Font-Italic="False" Font-Names="Courier New" Font-Size="X-Large" Text=""></asp:Label>
        <br />
        <asp:Label ID="Label3" runat="server" Font-Bold="True" Font-Italic="False" Font-Names="Courier New" Font-Size="X-Large" Text=""></asp:Label>
        <br />
        <asp:Label ID="Label4" runat="server" Font-Bold="True" Font-Italic="False" Font-Names="Courier New" Font-Size="X-Large" Text=""></asp:Label>
        <br />
        <asp:Label ID="Label5" runat="server" Font-Bold="True" Font-Italic="False" Font-Names="Courier New" Font-Size="X-Large" Text=""></asp:Label>
        <br />
        <asp:Label ID="Label6" runat="server" Font-Bold="True" Font-Italic="False" Font-Names="Courier New" Font-Size="X-Large" Text=""></asp:Label>
        <br />
        <asp:Label ID="Label7" runat="server" Font-Bold="True" Font-Italic="False" Font-Names="Courier New" Font-Size="X-Large" Text=""></asp:Label>
        <br />
        <asp:Label ID="Label8" runat="server" Font-Bold="True" Font-Italic="False" Font-Names="Courier New" Font-Size="X-Large" Text=""></asp:Label>
        <br />
        <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Сменить пароль" CssClass="form-control" />
        <br />
        <asp:Label ID="Label9" runat="server" Text="Текущий пароль" Visible="False"></asp:Label>
&nbsp;&nbsp;
        <asp:TextBox ID="TextBox1" TextMode="Password" runat="server" Visible="False" CssClass="form-control"></asp:TextBox>
        <br />
        <asp:Label ID="Label10" runat="server" Text="Новый пароль" Visible="False"></asp:Label>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:TextBox ID="TextBox2" TextMode="Password" runat="server" Visible="False" CssClass="form-control"></asp:TextBox>
        <br />
        <asp:Label ID="Label11" runat="server" Text="Подтверждение" Visible="False"></asp:Label>
&nbsp;&nbsp;&nbsp;
        <asp:TextBox ID="TextBox3" TextMode="Password" runat="server" Visible="False" CssClass="form-control"></asp:TextBox>
        <br />
        <br />
        <asp:Button ID="Button2" runat="server" Text="Сохранить изменения" Visible="False" CssClass="form-control" OnClick="Button2_Click" />
        <br />
        <asp:Label ID="Label12" runat="server" Font-Bold="True" Font-Italic="False" Font-Names="Courier New" Font-Size="X-Large" Text="Варианты написания Вашего имени (в Scopus, WebOfSince):"></asp:Label>
        <br />
        <br />
        <br />
    
    </div>
</asp:Content>
