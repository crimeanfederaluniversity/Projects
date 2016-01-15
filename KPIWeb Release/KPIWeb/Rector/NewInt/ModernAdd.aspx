<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="ModernAdd.aspx.cs" Inherits="KPIWeb.Rector.NewInt.ModernAdd" %>
<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">   
      <div id="wrapper">  
    <asp:Panel runat="server" ID="top_panel32" CssClass="top_panel" Height="40" Visible="true">    
                <div>    
              <asp:Button ID="GoBackButton3" runat="server" OnClientClick="showLoadPanel()" OnClick="GoBackButton_Click" Text="Назад" Width="125px" />
              <asp:Button ID="GoForwardButton3" runat="server" OnClientClick="showLoadPanel()" OnClick="GoForwardButton_Click" Text="Вперед" Width="125px" />
                &nbsp; &nbsp; <asp:Button ID="Butto3n4" runat="server" OnClientClick="showLoadPanel()" OnClick="Button4_Click" Text="На главную" Width="125px" />
                    &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                </div>

            </asp:Panel>


        
<div id="all_menu_Wrapper">
        <asp:Label ID="Label1" runat="server" Text="Модернизация образовательной деятельности университета на базе современных образовательных технологий и с учетом перспективной потребности экономики причерноморского макрорегиона в квалифицированных кадрах"></asp:Label>

    <div id="all_menu">
             <div id="left_side">
        <asp:LinkButton ID="Button1" runat="server" Text="Создание новых конкурентоспособных

образовательных программ" />
        <br />
                 </div>
           <div id="right_side">
        <asp:LinkButton ID="Button2" runat="server" Text="Внедрение в образовательный процесс 
современного учебного оборудования" />
        <br />
               </div>   

    </div>
    <div id="wrapapiu_bot_but">
        <asp:LinkButton ID="Button3" runat="server" Text="Развитие современных форм профориентационной работы" />
        <br /></div>
    
    </div>
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
