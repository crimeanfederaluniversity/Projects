<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="RNmain.aspx.cs" Inherits="KPIWeb.Rector.NewInt.RNmain" %>
<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">   
    <div>
        <div id="all_menu">
    <div id="left_menu">
       <asp:Label ID="Label2" runat="server" Text="Основная деятельность"></asp:Label>
             <br />
        <br />
        
        <asp:LinkButton ID="LinkButton1" runat="server" PostBackUrl="~/Rector/Rstat.aspx">Статистические сведения о 
Крымском федеральном
университете</asp:LinkButton>
        <br />
        <asp:LinkButton ID="LinkButton2" runat="server">Штатное расписание КФУ</asp:LinkButton>
        <br />
        <asp:LinkButton ID="LinkButton3" runat="server" PostBackUrl="~/Rector/NewInt/GovTask.aspx">Государственное задание КФУ</asp:LinkButton>
        <br />
        <br />

    </div>
            <div id="right_menu">
        <asp:Label ID="Label1" runat="server" Text="Программа развития"></asp:Label>
    
        <br />
        <br />
        <asp:LinkButton ID="LinkButton4" runat="server" PostBackUrl="~/Rector/NewInt/SvedofResult.aspx">Сведения о результатах реализации
Программы развития</asp:LinkButton>
        <br />
        <asp:LinkButton ID="LinkButton5" runat="server">Сведения о достижении целевых
показателей Программы развития</asp:LinkButton>
        <br />
       <asp:HyperLink ID="LinkButton6" runat="server" NavigateUrl="picforr1.png">Сведения о расходовании средств
Программы развития КФУ<</asp:HyperLink>
                
        <br />
        <asp:LinkButton ID="LinkButton7" runat="server">Сведения об участии структурных
подразделений в достижении 
целевых показателей Программы 
развития КФУ</asp:LinkButton>
        <br />
        <asp:LinkButton ID="LinkButton8" runat="server">Нормативные документы</asp:LinkButton>
    </div>
            </div>
    </div>
    <style>
        #left_menu {
            background-color:#86DB7A;
            display:inline-block;
            position:relative;
              padding:20px;
              width:50%;
        }
        #right_menu {
            background-color:#A3CCAC;
            display:inline-block;
            position:relative;
              padding:20px;
              width:50%;
        }
        #left_menu span,
        #right_menu span {
            display:block;
        text-align:center;
        }
        #left_menu a,
        #right_menu a {
display: block;
line-height: 10px;
color: #000;

padding: 20px 10px;
text-align: center;
background: linear-gradient(to bottom, #CEDCE7 0%, #596A72 100%) repeat scroll 0% 0%;
        }

            #left_menu a:hover,
            #right_menu a:hover {
               background: transparent linear-gradient(to bottom, #D7F2FF 0%, #8298AA  100%) repeat scroll 0% 0%;
            }
        #all_menu {  
            padding:20px;
            position:relative;
        
             display:inline-flex;
        }

            #all_menu span {
            font-size:2em;
            font-weight:bold;
            }

    </style>
    </asp:Content>
