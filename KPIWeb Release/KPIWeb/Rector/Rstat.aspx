<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Rstat.aspx.cs" Inherits="KPIWeb.Rector.Rstat" %>
<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
        <div id="wrapper">  
    


    <asp:Panel runat="server" ID="top_panel2" CssClass="top_panel" Height="40" Visible="true">    
        <div>    
      <asp:Button ID="GoBackButton" runat="server"   Text="Назад" Width="125px" Enabled="True" OnClick="Button222_Click" />
            <asp:Button ID="Button4" runat="server"  OnClick="Button4_Click" Text="На главную" Width="125px" />
        </div>

    </asp:Panel>
         <div id="page_wrapper">
    <div id="all_menu">
      <div id="left_side">
    <asp:LinkButton ID="Button1" runat="server" Text="Высшее профессиональное образование" OnClick="Button1_Click" />
 
      
    <asp:LinkButton ID="Button2" runat="server" OnClick="Button2_Click" Text="Среднее профессиональное образование" />
     </div>
    </div>
 
        
    </div> </div>
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
position: relative;
padding: 0px 25%;
display: flex;
    }
    #left_side {
                    background-color:#86DB7A;
            display:inline-block;
            position:relative;
              padding:0 20px 20px 20px;
              width:100%;
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
margin-top: 20px;
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
