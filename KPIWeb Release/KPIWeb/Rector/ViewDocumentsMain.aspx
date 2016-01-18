<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ViewDocumentsMain.aspx.cs" Inherits="KPIWeb.Rector.ViewDocumentsMain" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    
    <style type="text/css">
   .button_right 
   {
       float: right;
       margin-right: 10px;
   }     
</style>
                <asp:Panel runat="server" ID="top_panel2" CssClass="top_panel" Height="40" Visible="true">    
        <div>    
      <asp:Button ID="GoBackButton" runat="server" Text="Назад" Width="125px" OnClick="GoBackButton_Click1" />
      <asp:Button ID="Button2" runat="server" Text="На главную" Width="125px" OnClick="Button2_Click" />
        </div>

    </asp:Panel> 
    <br />
    
    <!--
    <asp:GridView ID="GridView1" AutoGenerateColumns="False" runat="server" Visible="False" Height="16px" Width="220px" >           
             <Columns>               
                            
                 <asp:TemplateField HeaderText="">
                        <ItemTemplate>  
                            <asp:LinkButton ID="ViewDocsButton" runat="server" CommandArgument='<%# Eval("DocTypeId") %>' OnClick="ViewDocumentClick" Text='<%# Eval("DocTypeName") %>' ></asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>   
                      
                </Columns>
       </asp:GridView>
        -->
    <br />

    

    <br />
    


    
    
    
    <div id="all_menu_Wrapper">
        <asp:Label ID="Label1" runat="server" Text="Просмотр нормативных документов"></asp:Label>

    <div id="all_menu">
             <div id="left_side">
        <asp:LinkButton ID="Button3" runat="server"  Text="Постановления Правительства"  CommandArgument="1" OnClick="ViewDocumentClick"/> 
                  <br />
         <asp:LinkButton ID="LinkButton1" runat="server"  Text="Приказы по системе организации Программы развития" CommandArgument="2" OnClick="ViewDocumentClick"/>
               <br />  </div>
           <div id="right_side">
        <asp:LinkButton ID="Button5" runat="server" Text="Дорожная карта, положение"  CommandArgument="3" OnClick="ViewDocumentClick"/>
                <br />
    <asp:LinkButton ID="Button7" runat="server" Text="Протоколы Координационного совета"  CommandArgument="4" OnClick="ViewDocumentClick"/>
       <br />
               </div>   

    </div>
    <div id="wrapapiu_bot_but">
        <asp:LinkButton ID="Button6" runat="server" Text="Приказы о реализации проектов Программы развития"  CommandArgument="5" OnClick="ViewDocumentClick"/>
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
