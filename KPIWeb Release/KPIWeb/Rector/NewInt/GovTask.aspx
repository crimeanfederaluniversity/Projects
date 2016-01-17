<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="GovTask.aspx.cs" Inherits="KPIWeb.Rector.NewInt.GovTask" %>
<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
  <div id="wrapper">
        <asp:Panel runat="server" ID="top_panel252" CssClass="top_panel" Height="40" Visible="true">    
                <div>    
              <asp:Button ID="GoBackButton52" runat="server" OnClientClick="showLoadPanel()" OnClick="GoBackButton_Click" Text="Назад" Width="125px" />
                &nbsp; &nbsp; <asp:Button ID="Button254" runat="server" OnClientClick="showLoadPanel()" OnClick="Button4_Click" Text="На главную" Width="125px" />
                    &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                </div>

            </asp:Panel>  
      <div id="all_menu_Wrapper">
         <asp:Label ID="Label1" runat="server" Text="Государственное задание КФУ"></asp:Label>
  
          <div id="all_menu">    <div id="left_side">
       <p class="year">2015</p>
        <asp:HyperLink ID="Button257" runat="server">Обучение</asp:HyperLink>
    
        <asp:HyperLink ID="Button258" runat="server">Наука</asp:HyperLink>
 
    
        <asp:HyperLink ID="Button255" runat="server" OnClick="Button255_Click">Специальная часть</asp:HyperLink>   </div>
            <div id="right_side">
       <p class="year">2016</p>
        <asp:HyperLink ID="Button256" runat="server" OnClick="Button256_Click">Плановые цифры</asp:HyperLink>
         </div> 
    </div>
    
<style>
    .year {
    text-align:center;
   
    font-weight:bold;
    font-size:24px;

    }
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
                 margin-top:20px;
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