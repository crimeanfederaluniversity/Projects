<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="CreationModern.aspx.cs" Inherits="KPIWeb.Rector.NewInt.CreationModern" %>
<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">   
      <div id="wrapper">  
    
    <asp:Panel runat="server" ID="top_panel22" CssClass="top_panel" Height="40" Visible="true">    
                <div>    
              <asp:Button ID="GoBackButton2" runat="server" OnClientClick="showLoadPanel()" OnClick="GoBackButton_Click" Text="Назад" Width="125px" />
              <asp:Button ID="GoForwardButton2" runat="server" OnClientClick="showLoadPanel()" OnClick="GoForwardButton_Click" Text="Вперед" Width="125px" />
                &nbsp; &nbsp; <asp:Button ID="Button24" runat="server" OnClientClick="showLoadPanel()" OnClick="Button4_Click" Text="На главную" Width="125px" />
                    &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                </div>

            </asp:Panel>
    

    <div id="all_menu_Wrapper">
        <asp:Label ID="Label1" runat="server" Text="Создание современного научно-исследовательского и инновационного
комплекса университета, обеспечивающего международный уровень 
исследований и разработок для решения актуальных проблем развития 
региона"></asp:Label>
         <div id="all_menu">
       <div id="left_side">
           <asp:HyperLink ID="Button25" runat="server" NavigateUrl="CreationModernStuff/NauchObr.png"> Научно-образовательные центры </asp:HyperLink>
        <br />
        <asp:HyperLink ID="Button26" runat="server" NavigateUrl="CreationModernStuff/CentComp_1.png"> Центры компетенций и экспертизы по направлениям, актуальным для устойчивого развития региона </asp:HyperLink>
        <br />
           </div>
                <div id="right_side">
                    <asp:HyperLink ID="Button27" runat="server" NavigateUrl="CreationModernStuff/Researchlab_1.png"> Научные лаборатории и центры, оснащенные
современным оборудованием, по прорывным 
для университета тематикам в рамках 
приоритетных направлений исследований </asp:HyperLink>
        <br />
        <asp:HyperLink ID="Button28" runat="server" NavigateUrl="CreationModernStuff/simulatCentr_1.png"> Симуляционный центр </asp:HyperLink>
    </div>
           
    </div>
          </div>
          </div>
     <style>
    #all_menu_Wrapper {
        display:block;
        text-align:center;  
        padding:0 0 0 0; background-color:#cecda0;
    }
    #wrapper {
        margin-top:50px;
    }
    #all_menu {
         
            position:relative;
         padding:0 0 0 0;
             display:flex;
    }
    #left_side {
                    background-color:#86DB7A;
            display:inline-block;
            position:relative;
              padding:20px;
              width:50%;
    }
    #right_side {
                    background-color:#A3CCAC;
            display:inline-block;
            position:relative;
              padding:20px;
              width:50%;
    }

     #all_menu span {
            display:block;
        text-align:center;

        }
             #left_side a,
            #right_side a,
              #all_menu_Wrapper a {
display: block;
line-height: 12px;
color: #000;

padding: 20px 10px;
text-align: center;

background: linear-gradient(to bottom, #CEDCE7 0%, #596A72 100%) repeat scroll 0% 0%;
        }
    #wrapapiu_bot_but {
      padding:0 20px;
    }


            #left_side a:hover,
            #right_side a:hover,
             #all_menu_Wrapper a:hover {
               background: transparent linear-gradient(to bottom, #D7F2FF 0%, #8298AA  100%) repeat scroll 0% 0%;
            }
                      #all_menu_Wrapper span {
            line-height: 28px;
            font-size:200%;
            font-weight:bold;
            display: block;
            padding: 10px;
            }
</style>
</asp:Content>