<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="RegisterView.aspx.cs" Inherits="Chancelerry.kanz.RegisterView" %>
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

        function ChangeAllInputToMyInput(mainDiv) {
            var newDiv = mainDiv.cloneNode(true);
            var main = newDiv.children[0];
            var tableBody = main.children[0];
            tableBody.removeChild(tableBody.children[0]);

            for (var i = 0; i < tableBody.children.length; i++) {
                var tmpRow = tableBody.children[i];
                if (i > 0)
                {
                    tmpRow.removeChild(tmpRow.children[tmpRow.children.length-1]);
                }
            }
            return newDiv;
        }
        function openCntlInNewPrintPage(cntrlName) {
            var myTable = document.getElementById(cntrlName);
            myTable = ChangeAllInputToMyInput(myTable);
            var headstr =
                "<html>\
                 <input  type='button' onClick='window.print();' value=' Печать '>        \
                    <head>\
                    <title>\
                    </title>\
                    \
                    <style>        \
                    table {border-collapse: collapse;}\
                    table, tr, td { border: 1px solid black; } \
                    table, tr { border: 1px solid black; }\
                    </style>         \
                   <style type='text/css' media='print'>         \
                           \
                    </style>         \
                    </head>\
                 <body>";
            var footstr = "</body></html>";
            var newstr = myTable.innerHTML;
            window.open().document.write(headstr + newstr + footstr);
        }
    </script>
    <style>
        .c1 {
            width: 500px;
            height: 30px;
            margin: auto;
            background-color: #c0c0c0;
        }

        .c2 {
            margin: 0px;
            text-align: center;
            background-color: #a0a0a0;
        }

        .fullwidth {
            width: 100%;
        }
    </style>   
    <style> 
        .modalDialog {
            position: fixed;
            font-family: Arial, Helvetica, sans-serif;
            top: 0;
            right: 0;
            bottom: 0;
            left: 0;
            background: rgba(0,0,0,0.8);
            z-index: 99999;
            -webkit-transition: opacity 400ms ease-in;
            -moz-transition: opacity 400ms ease-in;
            transition: opacity 400ms ease-in;
            display: none;
            pointer-events: none;
        }
        .modalDialog:target {
            display: block;
            pointer-events: auto;
        }
        .modalDialog > div {
            width: 800px;
            position: relative;
            margin: 10% auto;
            padding: 5px 20px 13px 20px;
            border-radius: 10px;
            background: #fff;
            background: -moz-linear-gradient(#fff, #999);
            background: -webkit-linear-gradient(#fff, #999);
            background: -o-linear-gradient(#fff, #999);
        }
        .close {
            background: #606061;
            color: #FFFFFF;
            line-height: 25px;
            position: absolute;
            right: -12px;
            text-align: center;
            top: -10px;
            width: 24px;
            text-decoration: none;
            font-weight: bold;
            -webkit-border-radius: 12px;
            -moz-border-radius: 12px;
            border-radius: 12px;
            -moz-box-shadow: 1px 1px 3px #000;
            -webkit-box-shadow: 1px 1px 3px #000;
            box-shadow: 1px 1px 3px #000;
        }
        .close:hover { background: #00d9ff; }

    </style>  
    <div id="searchWithParamInfo" class="modalDialog">
        <div>
        <a href="#close" title="Закрыть" class="close">X</a>
        <h2>Поиск по всему реестру с параметрами</h2>
        <p>Поиск позволяет использовать логические операторы (И ИЛИ НЕ)</p>
        <p>Поиск производится по всему реестру</p>
        <p>Пример: поиск строки "параметр1 ИЛИ параметр2" выведет все карточки в которых содержится одно из значений 'параметр1' либо 'параметр2' </p>
        <p>Пример: поиск строки "(параметр1 И параметр2) ИЛИ НЕ параметр3" выведет все карточки в которых содержатся значения и 'параметр1' и 'параметр2', а также все карточки в которых отсутсвует значение 'параметр3' </p>
    </div>
         </div>
    <div id="searchAllInfo" class="modalDialog">
        <div>
        <a href="#close" title="Закрыть" class="close">X</a>
        <h2>Поиск по всему реестру</h2>
        <p>Поиск производится по всему реестру</p>
    </div>
        

    </div>

    <br />
    <asp:Panel runat="server" CssClass="edit-panel" Height="30px">
        <asp:Button ID="Button1" runat="server" CssClass="float-left" Text="Добавить новую карточку" Width="1192px" OnClick="Button1_Click" OnClientClick="showLoadingScreen()" Height="25px" />
        &nbsp;</asp:Panel>
    <div style="width: 100%; height: 20px; border-bottom: 1px solid black; text-align: center">
        <span style="font-size: 15px; padding: 0 0px;" onclick="toggle_visibility('searchDiv')">Поиск
        </span>
    </div>
    <div id="searchDiv">
        <table>
            <tr>
                <td class="auto-style1">Поиск по всему реестру <a href="#searchAllInfo" ><img border="0" alt="W3Schools" src="icons/infoButtonIcon.png" width="15" height="15"></a>
                </td>
                <td>
                    <asp:TextBox ID="SearchAllTextBox" runat="server" Height="20px" Width="200px"></asp:TextBox>
                </td>
                <td>
                    <asp:Button ID="SearchAllButton" runat="server" Text="Поиск" Width="200px" OnClick="SearchAllButton_Click" OnClientClick="showLoadingScreen()" />
                </td>
                <td>&nbsp;</td>
                <td>Расширенный поиск <a href="#searchWithParamInfo"><img border="0" alt="W3Schools" src="icons/infoButtonIcon.png" width="15" height="15"></a> </td>
                <td>
                    <asp:TextBox ID="SearchAllTextBoxExtended" runat="server" Height="20px" Width="200px"></asp:TextBox>
                </td>
                <td>
                    <asp:Button ID="SearchAllExtendedButton" runat="server" Text="Поиск" Width="200px" OnClientClick="showLoadingScreen()" OnClick="SearchAllExtendedButton_Click" />
                </td>
            </tr>
            <tr>
                <td class="auto-style1">Поиск по номеру 
                </td>
                <td>
                    <asp:TextBox ID="SearchByIdTextbox" runat="server" Height="20px" Width="200px"></asp:TextBox>
                </td>
                <td>
                    <asp:Button ID="SearchById" runat="server" Text="Поиск" Width="200px" OnClientClick="showLoadingScreen()" OnClick="SearchById_Click" />
                </td>
                <td>&nbsp;</td>
                <td>
                    Карточек на странице</td>
                <td>
                    <asp:DropDownList ID="CardsOnPageDropDownList" runat="server" AutoPostBack="True" CssClass="float-right" Height="25px" OnSelectedIndexChanged="CardsOnPageDropDownList_SelectedIndexChanged" Width="100px">
                        <asp:ListItem>5</asp:ListItem>
                        <asp:ListItem>10</asp:ListItem>
                        <asp:ListItem>20</asp:ListItem>
                        <asp:ListItem>50</asp:ListItem>
                        <asp:ListItem>100</asp:ListItem>
                        <asp:ListItem>200</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td class="auto-style1">Откыть карточку по номеру 
                </td>
                <td>
                    <asp:TextBox ID="OpenByIdTextBox" runat="server" Height="20px" Width="200px"></asp:TextBox>
                </td>
                <td >
                    <asp:Button ID="OpenByIdButton" runat="server" Text="Открыть" Width="200px" OnClientClick="showLoadingScreen()" OnClick="OpenByIdButton_Click" />
                </td>
                <td>&nbsp;</td>
                <td>
                    Сортировать по</td>
                <td>
        <asp:DropDownList ID="ChooseSortFieldIdDropDownList" runat="server" CssClass="float-right" Height="17px" OnSelectedIndexChanged="ChooseSortFieldIdDropDownList_SelectedIndexChanged" AutoPostBack="True" Width="200px"></asp:DropDownList>
        
                </td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td class="auto-style1" >Поиск по фильтрам 
                </td>
                <td colspan="3" >
                    <asp:Button ID="Button2" runat="server" OnClick="Button2_Click" Width="400px" Text="Поиск" OnClientClick="showLoadingScreen()" />
                </td>
                <td colspan="3" >
        <asp:Button ID="Button3" runat="server"  OnClick="Button3_Click" Text="Настройка страницы" OnClientClick="showLoadingScreen()" Width="400px"/>
        
                </td>
            </tr>
            <tr>
                <td class="auto-style1" >Очистить
                    поиск</td>
                <td colspan="2" >
                    <asp:Button ID="Button44" runat="server" OnClick="Button4_Click" Width="400px" Text="Очистить" OnClientClick="showLoadingScreen()" />
                <td colspan="4" >
        <input name="b_print" onclick="openCntlInNewPrintPage('tableDiv');" type="button" value=" Печать таблицы" style="width: 400px" /><td >
            </tr>
        </table>
    </div>
    <br />
    <asp:Label ID="RegisterNameLabel" runat="server" Text="Label"></asp:Label>
    <br />
    <asp:Panel runat="server" CssClass="center pagination">
        <asp:Label ID="PageInfoTop" runat="server" Text=""></asp:Label>
        <br>
        <asp:Button ID="GoToFirstTop" runat="server" Text=" <<< " />
        <asp:Button ID="GoToPreviousTop" runat="server" Text="  <  " />
        <asp:Label ID="PagesListTop" runat="server" Text=""></asp:Label>
        <asp:Button ID="GoToNextTop" runat="server" Text="  >  " />
        <asp:Button ID="GoToLastTop" runat="server" Text=" >>> " />
    </asp:Panel>
    <br />
</asp:Content>
<asp:Content ID="TableContent1" ContentPlaceHolderID="TableContent" runat="server">
    <!-- sticky header styles -->
    <style>
        
        /*main table style*/


        /* sticky header styles */
         .search-field {
             width: 100%;
             max-width: none !important;
         }

        #TableHeaderDiv {
            margin-left: 5px;
            margin-top: 50px;
            display: none;
            position: absolute;
        }

        #tableForHeader {
            width: 100%;
            border: 0px solid transparent;
        }



        #tableForHeader tr:nth-child(n+2)  {
            z-index: -1000;
            visibility: hidden;
            border: transparent;
        }
      
        .auto-style1 {
            width: 241px;
        }
      
    </style>
    <div id="TableHeaderDiv">
        <table id="tableForHeader" style="">
        </table>
    </div>
    <script>
        /*
        var myBody;
        window.onload = function ()
        {
           
            var myTable = document.getElementById("TableContent_dataTable");
            myBody = myTable.children[0].cloneNode(true);
            myBody.removeChild(myBody.children[0]);
            for (var i = 1; i < myBody.children.length; i++)
            {
                //myBody.children[i].style.visibility = 'hidden';
                //myBody.children[i].style.zIndex = '-1';
            }
            var tableForHeader = document.getElementById("tableForHeader");
            tableForHeader.appendChild(myBody);
            
        }
        window.onscroll = function ()
        {
            
            var tablHeaderDiv = document.getElementById("TableHeaderDiv");
            tablHeaderDiv.style.top = window.pageYOffset  + 'px';
            var scrolled = window.pageYOffset || document.documentElement.scrollTop;
            if (scrolled > 400)
            {
                tablHeaderDiv.style.display = 'block';
            }
            else
            {
                tablHeaderDiv.style.display = 'none';
            }
            
        }*/
    </script>
    <div id="tableDiv" style="margin-left: 5px">
        <asp:Table ID="dataTable" runat="server" Width="100%">
        </asp:Table>
    </div>
    <asp:Panel runat="server" CssClass="center pagination">
        <asp:Label ID="PageInfoBottom" runat="server" Text=""></asp:Label>
        <br>
        <asp:Button ID="GoToFirstBottom" runat="server" Text=" <<< " />
        <asp:Button ID="GoToPreviousBottom" runat="server" Text="  <  " />
        <asp:Label ID="PagesListBottom" runat="server" Text=""></asp:Label>
        <asp:Button ID="GoToNextBottom" runat="server" Text="  >  " />
        <asp:Button ID="GoToLastBottom" runat="server" Text=" >>> " />
    </asp:Panel>
    <br />
    <asp:Label ID="timeStampsLabel" runat="server" Text="" Height="5" Font-Size="3"></asp:Label>
</asp:Content>