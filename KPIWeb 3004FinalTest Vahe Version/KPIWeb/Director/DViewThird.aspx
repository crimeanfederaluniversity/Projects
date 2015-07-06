<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="DViewThird.aspx.cs" Inherits="KPIWeb.Director.DViewThird" %>

<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
    
    
    <style type="text/css">
   .button_right 
   {
       float: right;
       margin-right: 10px;
   }     
   .side_legend 
   {
        position:fixed;    
        top:10em;
        width:280px;
        height:150px;    
        border-color: black;
        border-width: medium;
        background-color:azure;
        z-index:5;
        right: -260px;
        border-style: solid;
        border-width: 1px;
        border-color: black;
        visibility: visible;
    }
   
  

    .side_img_legend {
        top:0;
    position:absolute;
    width:20px;
    height:148px;
    background-color:azure;
    background-image:url('http://212.110.152.173/Styles4KPIKFU/App_Themes/theme_1/css/images/arout.png');
    background-repeat:no-repeat;
      }
    .side_img_legend:hover{
    background-image:url('http://212.110.152.173/Styles4KPIKFU/App_Themes/theme_1/css/images/arout2.png');
    background-repeat:no-repeat;
    }


</style>  
    <script>
        function legendChange() {

            if (document.getElementById('sidePanel').style.right == '0px') {
                document.getElementById('sidePanel').style.right = '-260px';
                document.getElementById('img_of_sp').style.backgroundPosition = '0 0';
            }
            else {
                document.getElementById('sidePanel').style.right = '0px';
                document.getElementById('img_of_sp').style.backgroundPosition = '-20px 0';

            }
        }
    </script>
           <h2>Список подразделений</h2>
           <div id="sidePanel" class='side_legend' onclick="legendChange()"> 
    <div id="img_of_sp" class="side_img_legend"></div>   
            <!--<button type="button" onclick="legendChange()">
            O</button>-->
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:Label ID="Label7" runat="server" Text="Легенда" ValidateRequestMode="Enabled"></asp:Label>
            <br />
            <br />
            <asp:Panel ID="Panel5" runat="server" BackColor="#00cc00">
                <asp:Label ID="Label11" runat="server" Text=".....Утверждено"></asp:Label>
                <br />
            </asp:Panel>
            <br />
            <asp:Panel ID="Panel6" runat="server" Height="17" BackColor="#000099" BorderColor="#000099">
                <asp:Label ID="Label12" runat="server" Text=".....Требует Вашего утверждения"></asp:Label>
            </asp:Panel>
            <br />
            <asp:Panel ID="Panel7" runat="server" Height="17" BackColor="#CC0000" BorderColor="#CC0000">
                <asp:Label ID="Label13" runat="server" style="text-align: center" Text=".....Рассчитано по неполным данным"></asp:Label>
            </asp:Panel>
        </div>  
        <asp:GridView ID="GridView1" runat="server" 
            AutoGenerateColumns="False"
            style="margin-top: 0px">
             <Columns>        
                         
                 <asp:BoundField DataField="StructName" HeaderText="Структурное подразделение" Visible="True" />          

                  <asp:TemplateField HeaderText="Подробнее">
                        <ItemTemplate>
                            <asp:Button ID="ButtonViewReport" runat="server" CommandName="Select" Text="Просмотреть" Width="200px" CommandArgument='<%# Eval("StructID") %>' OnClick="ButtonDetailClick"/>
                        </ItemTemplate>
                    </asp:TemplateField>

                   <asp:BoundField DataField="Status" HeaderText="Статус данных" Visible="True" />

                </Columns>
        </asp:GridView>
           <br />
       </asp:Content>