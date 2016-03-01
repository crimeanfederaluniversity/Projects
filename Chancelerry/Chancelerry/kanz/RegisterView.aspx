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

    <br />

    <asp:Panel runat="server" CssClass="edit-panel" Height="30px">
        <asp:Button ID="Button1" runat="server" CssClass="float-left" Text="Добавить" Width="362px" OnClick="Button1_Click" OnClientClick="showLoadingScreen()"/>
        <asp:Button ID="Button3" runat="server" CssClass="float-right" OnClick="Button3_Click" Text="Настройка страницы" OnClientClick="showLoadingScreen()"/>
    </asp:Panel>

    <asp:Panel ID="SearchPanel" runat="server" CssClass="hidden">
        <asp:Button ID="Button4" runat="server" OnClick="Button4_Click" Text="Очистить поиск" OnClientClick="showLoadingScreen()"/>
        <asp:Button ID="Button2" runat="server" OnClick="Button2_Click" Text="Поиск" OnClientClick="showLoadingScreen()"/>
    </asp:Panel>

    <br />

    <asp:Label ID="RegisterNameLabel" runat="server" Text="Label"></asp:Label>
    
    <br />

    <asp:Panel runat="server" CssClass="center pagination">
        <asp:Button ID="Button7" runat="server" OnClick="Button7_Click" Text="На первую" OnClientClick="showLoadingScreen()"/>

        <asp:Button ID="Button5" runat="server" OnClick="Button5_Click" Text="Назад" Width="61px" OnClientClick="showLoadingScreen()"/>
        <asp:Label ID="PageNumberLabel" runat="server" Text="Label"></asp:Label>
        <asp:Button ID="Button6" runat="server" OnClick="Button6_Click" Text="Вперёд" OnClientClick="showLoadingScreen()"/>

        <asp:Button ID="Button8" runat="server" Text="К последней" OnClick="Button8_Click" OnClientClick="showLoadingScreen()"/>
    </asp:Panel>

    <br />
    
    <asp:Table ID="dataTable" runat="server" Width="100%" >
    </asp:Table>

    <asp:Panel runat="server" CssClass="center pagination">
        <asp:Button ID="BottomButton9" runat="server" OnClick="Button7_Click" Text="На первую" OnClientClick="showLoadingScreen()"/>

        <asp:Button ID="BottomButton10" runat="server" OnClick="Button5_Click" Text="Назад" Width="61px" OnClientClick="showLoadingScreen()"/>
        <asp:Label ID="BottomPageNumberLabel" runat="server" Text="Label"></asp:Label>
        <asp:Button ID="BottomButton11" runat="server" OnClick="Button6_Click" Text="Вперёд" OnClientClick="showLoadingScreen()"/>

        <asp:Button ID="BottomButton12" runat="server" Text="К последней" OnClick="Button8_Click" OnClientClick="showLoadingScreen()"/>
    </asp:Panel>

    <br />

    <script>
        $(function () {
            $(".search-field").focusin(function () {
                var $clicker = $(this);
                var position = $clicker.position();

                var search_field = document.getElementById("MainContent_SearchPanel");
                search_field.classList.remove("hidden");
                search_field.style.top = 35 + position.top + "px";
                search_field.style.left = position.left - search_field.clientWidth / 2 + "px" ;
            });

            $(".search-field").focusout(function () {
                var search_field = document.getElementById("MainContent_SearchPanel");
                search_field.classList.add("hidden");
            });
        })
    </script>

</asp:Content>
