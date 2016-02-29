<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="RegisterView.aspx.cs" Inherits="Chancelerry.kanz.RegisterView" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    
    <script>
        function runScript(e)
        {
            if (e.keyCode == 13 || e.which == 13)
            {
                document.getElementById('MainContent_Button2').focus();
                return false;
            }
        }
        </script>
    <asp:Button ID="Button1" runat="server" Text="Добавить" Width="362px" OnClick="Button1_Click" OnClientClick="showLoadingScreen()"/>

    <br />
    <asp:Button ID="Button3" runat="server" OnClick="Button3_Click" Text="Настройка страницы" OnClientClick="showLoadingScreen()"/>

    <br />
    <asp:Button ID="Button2" runat="server" OnClick="Button2_Click" Text="Поиск" OnClientClick="showLoadingScreen()"/>

    <br />

    <asp:Button ID="Button4" runat="server" OnClick="Button4_Click" Text="Очистить поиск" OnClientClick="showLoadingScreen()"/>

    <br />

    <asp:Button ID="Button7" runat="server" OnClick="Button7_Click" Text="На первую" OnClientClick="showLoadingScreen()"/>

    <asp:Button ID="Button5" runat="server" OnClick="Button5_Click" Text="Назад" Width="61px" OnClientClick="showLoadingScreen()"/>
    <asp:Label ID="PageNumberLabel" runat="server" Text="Label"></asp:Label>
    <asp:Button ID="Button6" runat="server" OnClick="Button6_Click" Text="Вперёд" OnClientClick="showLoadingScreen()"/>

    <asp:Button ID="Button8" runat="server" Text="К последней" OnClick="Button8_Click" OnClientClick="showLoadingScreen()"/>

    <br />
    <asp:Label ID="RegisterNameLabel" runat="server" Text="Label"></asp:Label>
    
    <asp:Table ID="dataTable" runat="server" Width="100%" >


         
</asp:Table>

</asp:Content>
