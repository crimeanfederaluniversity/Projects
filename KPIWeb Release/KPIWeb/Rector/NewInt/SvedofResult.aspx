<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="SvedofResult.aspx.cs" Inherits="KPIWeb.Rector.NewInt.SvedofResult" %>
<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">   
    <div id="wrapper">
    <asp:Panel runat="server" ID="top_panel2" CssClass="top_panel" Height="40" Visible="true">    
                <div>    
              <asp:Button ID="GoBackButton" runat="server" OnClientClick="showLoadPanel()" OnClick="GoBackButton_Click" Text="Назад" Width="125px" />
              <asp:Button ID="GoForwardButton" runat="server" OnClientClick="showLoadPanel()" OnClick="GoForwardButton_Click" Text="Вперед" Width="125px" />
                &nbsp; &nbsp; <asp:Button ID="Button4" runat="server" OnClientClick="showLoadPanel()" OnClick="Button4_Click" Text="На главную" Width="125px" />
                &nbsp; &nbsp;       
                <asp:Button ID="Button5" runat="server" CssClass="button_right"  OnClick="Button5_Click" Text="Нормативные документы" Width="250px" OnClientClick="showLoadPanel()" />
                    &nbsp; &nbsp; &nbsp; &nbsp;
                </div>

            </asp:Panel>
    
    
<div id="all_menu_Wrapper">
        <asp:Label ID="Label1" runat="server" Text="Сведения о результатах реализации Программы развития"></asp:Label>
    <div id="all_menu">
        <br />
        <div id="left_side">
        <asp:LinkButton ID="Button6" runat="server" Text="Модернизация образовательной 
деятельности университета на базе
современных образовательных технологий и с 
учетом перспективной потребности 
экономики причерноморского макрорегиона 
в квалифицированных кадрах" />
        <br />
        <asp:LinkButton ID="Button7" runat="server" Text="Сетевое взаимодействие и повышение 
квалификации на базе партнерских 
организаций" />
        <br />
        </div>
        <div id="right_side">
        <asp:LinkButton ID="Button8" runat="server" Text="Создание современного научно-
исследовательского и инновационного 
комплекса университета, обеспечивающего 
международный уровень исследований и 
разработок для решения актуальных проблем 
развития региона" />
        <br />
        <asp:LinkButton ID="Button9" runat="server" Text="Мотивация персонала и создание 
комфортных условий работы" />
        <br />
        </div>
     
   </div><div id="wrapapiu_bot_but">
        <asp:LinkButton ID="Button10" runat="server" Text="Брендинг и маркетинг" />
       </div>
</div>   

</div>
<style>
    #all_menu_Wrapper {
        display:block;
        text-align:center;  
        padding:0 0 20px 0; background-color:#cecda0;
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
            font-size:200%;
            font-weight:bold;
                    display: block;
padding: 10px;
            }
</style>
   </asp:Content>