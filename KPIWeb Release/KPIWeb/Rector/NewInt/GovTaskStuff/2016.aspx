<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="_2016.aspx.cs" Inherits="KPIWeb.Rector.NewInt.GovTaskStuff._2016" %>
<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
          <div id="wrapper">  
    <asp:Panel runat="server" ID="top_panel1212" CssClass="top_panel" Height="40" Visible="true">    
                <div>    
              <asp:Button ID="GoBackButton142" runat="server" OnClientClick="showLoadPanel()" OnClick="GoBackButton_Click" Text="Назад" Width="125px" />
                    &nbsp;&nbsp;
              <asp:Button ID="Button1264" runat="server" OnClientClick="showLoadPanel()" OnClick="Button4_Click" Text="На главную" Width="125px" />
                    &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                </div>

            </asp:Panel>

         
<div id="all_menu_Wrapper">
       
    <asp:Label ID="Label1" runat="server" Text="Государственное задание 2016"></asp:Label>

    <div id="all_menu">   <div id="left_side">
        <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="otchet_gz_2015_rep_bakalavr.pdf">Бакалавры</asp:HyperLink>
        <br />
        <asp:HyperLink ID="HyperLink2" runat="server" NavigateUrl="otchet_gz_2015_rep_specialist.pdf">Специалисты</asp:HyperLink>
        <br />
        <asp:HyperLink ID="HyperLink3" runat="server" NavigateUrl="otchet_gz_2015_rep_magistr.pdf">Магистры</asp:HyperLink>
        <br />
        <asp:HyperLink ID="HyperLink4" runat="server" NavigateUrl="otchet_gz_2015_rep_spo.pdf">СПО</asp:HyperLink>
        <br />
        <asp:HyperLink ID="HyperLink5" runat="server" NavigateUrl="otchet_gz_2015_rep_aspirant.pdf">Аспирантура</asp:HyperLink>
        <br />      </div>  <div id="right_side">
        <asp:HyperLink ID="HyperLink6" runat="server" NavigateUrl="otchet_gz_2015_rep_intern.pdf">Интернатура</asp:HyperLink>
        <br />
        <asp:HyperLink ID="HyperLink7" runat="server" NavigateUrl="otchet_gz_2015_rep_ordinator.pdf">Ординатура</asp:HyperLink>
        <br />
        <asp:HyperLink ID="HyperLink8" runat="server" NavigateUrl="otchet_gz_2015_rep_doctorant.pdf">Докторантура</asp:HyperLink>
        <br />
        <asp:HyperLink ID="HyperLink9" runat="server" NavigateUrl="specnevosh.pdf" >Специальности не вошедшие в ГЗ</asp:HyperLink>
        </div>      </div>  
    </div>    </div>  
    
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
