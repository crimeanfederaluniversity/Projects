<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="RegisterView.aspx.cs" Inherits="Chancelerry.kanz.RegisterView" %>



 <asp:Content ID="TableContent1" ContentPlaceHolderID="TableContent" runat="server">
     
     <style> 
   
    </style>

    <div style="margin-left: 5px">
       <asp:Table ID="dataTable" runat="server" Width="100%" >
    </asp:Table>
        </div>
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

            $(".container").click(function (e) {
                var $target = $(e.target);

                if (!$target.closest(".search-field").length) {
                    var search_field = document.getElementById("ctl00_MainContent_SearchPanel");
                    search_field.classList.add("hidden");
                }
            });

            $(".search-field").focus(function () {
                var $clicker = $(this);
                var position = $clicker.position();

                var search_field = document.getElementById("ctl00_MainContent_SearchPanel");
                search_field.classList.remove("hidden");
                search_field.style.top = 35 + position.top + "px";
                search_field.style.left = position.left - search_field.clientWidth / 2 + "px";
            });
        })
    </script>
    
    <asp:Label ID="timeStampsLabel" runat="server" Text="" Height="5" Font-Size="3"></asp:Label>
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <script src="toggleLoadingScreen.js" type="text/javascript"></script>
    <link href="../Content/Site.css" rel="stylesheet" />
   
    <script type="text/javascript">
   function toggle_visibility(id) {
       var e = document.getElementById(id);
       if(e.style.display == 'block')
          e.style.display = 'none';
       else
          e.style.display = 'block';
   }
</script>
    
    <style>
        .c1 { width: 500px; height: 30px; margin: auto; background-color: #c0c0c0; }
        .c2 { margin: 0px; text-align: center; background-color: #a0a0a0; }
        .fullwidth { width: 100%; }
         
    </style>

     <script>
        function runScript(e)
        {
            if (e.keyCode == 13 || e.which == 13)
            {
                document.getElementById('ctl00_MainContent_Button2').focus();
                return false;
            }
        }
        </script>

    <br />

    <asp:Panel runat="server" CssClass="edit-panel" Height="30px">
        <%--<asp:Button ID="Button4" runat="server" OnClick="Button4_Click" Text="Очистить поиск" OnClientClick="showLoadingScreen()"/>
        <asp:Button ID="Button2" runat="server" OnClick="Button2_Click" Text="Поиск" OnClientClick="showLoadingScreen()"/>--%>
        <asp:Button ID="Button1" runat="server" CssClass="float-left" Text="Добавить новую карточку" Width="362px" OnClick="Button1_Click" OnClientClick="showLoadingScreen()"/>
        <asp:Button ID="Button3" runat="server" CssClass="float-right" OnClick="Button3_Click" Text="Настройка страницы" OnClientClick="showLoadingScreen()"/>
    </asp:Panel>
    
    
    <div style="width: 100%; height: 20px; border-bottom: 1px solid black; text-align: center">
      <span style="font-size: 15px; padding: 0 0px;" onclick="toggle_visibility('searchDiv')">
        Поиск
      </span>
    </div>
    

    <div id="searchDiv">
        <table>
            <tr>
                <td>
                    Поиск по всему реестру
                </td> 
                <td>
                     <asp:TextBox ID="SearchAllTextBox" runat="server" Height="20px" Width="200px"></asp:TextBox> 
                </td> 
                <td>
                    <asp:Button ID="SearchAllButton" runat="server" Text="Поиск" Width="200px" OnClick="SearchAllButton_Click" OnClientClick="showLoadingScreen()"/>
                </td> 
            </tr>
            <tr>
                <td>
                    Поиск по номеру 
                 </td> 
                <td>
                    <asp:TextBox ID="SearchByIdTextbox" runat="server" Height="20px" Width="200px"></asp:TextBox> 
                 </td> 
                <td>
                    <asp:Button ID="SearchById" runat="server" Text="Поиск" Width="200px" OnClientClick="showLoadingScreen()" OnClick="SearchById_Click"/>
                </td>
            </tr>
            <tr>
                <td>
                    Откыть карточку по номеру 
                </td> 
                <td>
                    <asp:TextBox ID="OpenByIdTextBox" runat="server" Height="20px" Width="200px"></asp:TextBox>
                </td> 
                <td>
                    <asp:Button ID="OpenByIdButton" runat="server" Text="Открыть" Width="200px" OnClientClick="showLoadingScreen()" OnClick="OpenByIdButton_Click"/>  
                </td> 
                </tr>
            <tr>   
                <td > 
                    Поиск по фильтрам 
                </td> 
                <td colspan="2"> 
                    <asp:Button ID="Button2" runat="server" OnClick="Button2_Click" Width="402px" Text="Поиск" OnClientClick="showLoadingScreen()"/>
                </td> 
            </tr>
            <tr>
                <td > 
                    Очистить
                    поиск</td>  
                <td colspan="2"> 
                    <asp:Button ID="Button44" runat="server" OnClick="Button4_Click" Width="402px" Text="Очистить" OnClientClick="showLoadingScreen()"/>
                <td>  
             </tr>      
        </table>
    </div>

    
    
    

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
    
 

</asp:Content>
