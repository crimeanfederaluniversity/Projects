<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="NetworkCommunication.aspx.cs" Inherits="KPIWeb.Rector.NewInt.NetworkCommunication" %>
<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">   
       <div id="wrapper">  
        <asp:Panel runat="server" ID="top_panel122" CssClass="top_panel" Height="40" Visible="true">    
                <div>    
              <asp:Button ID="GoBackButton12" runat="server" OnClientClick="showLoadPanel()" OnClick="GoBackButton_Click" Text="Назад" Width="125px" />
              <asp:Button ID="GoForwardButton12" runat="server" OnClientClick="showLoadPanel()" OnClick="GoForwardButton_Click" Text="Вперед" Width="125px" />
                &nbsp; &nbsp; <asp:Button ID="Button124" runat="server" OnClientClick="showLoadPanel()" OnClick="Button4_Click" Text="На главную" Width="125px" />
                    &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                </div>

            </asp:Panel>

    
    <div id="all_menu_Wrapper">
        <asp:Label ID="Label1" runat="server" Text="Сетевое взаимодействие и повышение квалификации на базе партнерских организаций"></asp:Label>
          <div id="all_menu">
                <div id="left_side">
                    <asp:HyperLink ID="Button125" runat="server" NavigateUrl="NetworkCommunicationStuff/ObrProg_1.png">  Создание образовательных программ с использованием сетевой формы, в том числе в рамках сети федеральных университетов </asp:HyperLink>
      </div>
                    <div id="right_side">
                         <asp:HyperLink ID="Button126" runat="server" NavigateUrl="NetworkCommunicationStuff/stimul_1.png"> Стимулирование академической мобильности работников университета и обучающихся в университете </asp:HyperLink>
        <br />
       </div>
              </div>
               <div id="wrapapiu_bot_but">
                  
        <asp:HyperLink ID="Button127" runat="server" NavigateUrl="NetworkCommunicationStuff/Clenstvo_1.png"> Членство в ассоциациях и объединениях научно-образовательных организаций </asp:HyperLink>

               </div>
                  <br /></div>  
    
    </div>
    <style>
    #all_menu_Wrapper {
        display:block;
        text-align:center;  
        padding:0 0 10px 0; background-color:#cecda0;
    }
    #wrapper {
        margin-top:50px;
    }
    #all_menu {
         
        position:relative;
         padding:0 0 20px 0;
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
