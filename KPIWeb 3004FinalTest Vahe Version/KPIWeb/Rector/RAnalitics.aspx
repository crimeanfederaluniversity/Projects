<%@ Page Language="C#" AutoEventWireup="true" EnableEventValidation ="false" MasterPageFile="~/Site.Master" CodeBehind="RAnalitics.aspx.cs" Inherits="KPIWeb.Rector.RAnalitics" %>
<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">  
  
        <link rel="stylesheet" type="text/css" href="../Spinner.css">  
    <script type="text/javascript">
        function showLoadPanel() {
            document.getElementById('LoadPanel_').style.visibility = 'visible';
        }
    </script>
    <style>  
        body {
        top: 50px;
    }
        .LoadPanel 
   {
          position: fixed;
          z-index: 10;
          background-color: whitesmoke;
          top: 0;
          left: 0;
          width: 100%;
          height: 100%;
          opacity: 0.9;
          visibility: hidden;
   }
</style>     
    <div id="LoadPanel_" class='LoadPanel'>               
            <div id="floatingCirclesG">
            <div class="f_circleG" id="frotateG_01">
            </div>
            <div class="f_circleG" id="frotateG_02">
            </div>
            <div class="f_circleG" id="frotateG_03">
            </div>
            <div class="f_circleG" id="frotateG_04">
            </div>
            <div class="f_circleG" id="frotateG_05">
            </div>
            <div class="f_circleG" id="frotateG_06">
            </div>
            <div class="f_circleG" id="frotateG_07">
            </div>
            <div class="f_circleG" id="frotateG_08">
            </div>
            </div>
        </div>
    
        <asp:Panel runat="server" ID="top_panel2" CssClass="top_panel" Height="40" Visible="true">    
        <div>    
      <asp:Button ID="GoBackButton" runat="server" OnClientClick="JavaScript:window.history.back(1); return false; showLoadPanel();"  Text="Назад" Width="125px" Enabled="True" />
      <asp:Button ID="GoForwardButton" runat="server" OnClientClick="JavaScript:window.history.forward(1); return false; showLoadPanel();"  Enabled="False" Text="Вперед" Width="125px" />
        &nbsp; &nbsp; <asp:Button ID="Button2" OnClientClick="showLoadPanel()" runat="server"  Text="На главную" Width="125px" Enabled="True" OnClick="Button2_Click" />
        &nbsp; &nbsp;       
        <asp:Button ID="Button1" runat="server" OnClientClick="showLoadPanel()" CssClass="button_right"  Text="Выбор отчета" Enabled="False" Width="225px" />
        &nbsp; &nbsp;
        <asp:Button ID="Button6" runat="server" OnClientClick="showLoadPanel()" CssClass="button_right"  Text="Button" Width="150px" Visible="False" />
        &nbsp; &nbsp;
        </div>

    </asp:Panel>

     
    <div>    <h2>Выберите целевые показатели по нужным критериям для анализа</h2>
        <br />
        <div id="but4cent">
        <asp:Button ID="Button4" runat="server" Text="По всем показателям" OnClientClick="showLoadPanel()" Width="100%" Height="70px" OnClick="Button4_Click" />
        </div>
 
&nbsp;&nbsp;&nbsp;   
        <br />
        <style>
            .NoSkin
            { 
                display:inline;
                top:0px;
                border-style:none;
                border-width:0px;
             
                
            }
            .NoSkin tr th
            {
                visibility:hidden;
            }
                        .NoSkin th
            {
               visibility:hidden;
                            border-width: 0px;
            }

         
           .table {

              
               border-width:15px;
               background-color: #ffffff;
               opacity: 0.9;
               }
              .table th {
                text-align: center;
            }
            </style>
         
        <table width="100%" border="1" class="table">
          <tr>
            <th>    
                По группе показателей:   
            </th>
            <th>  
                По проректорам:
            </th>
            <th>       
                По произвольному набору показателей:
            </th>
          </tr>

          <tr >
            <td >    
                 
                <asp:GridView ID="GridView1" AutoGenerateColumns="false" runat="server" CssClass="NoSkin">
                    <Columns> 
                    <asp:TemplateField HeaderText="1">
                      <ItemTemplate>
                          <asp:Button ID="IndicatorClass" runat="server"  OnClientClick="showLoadPanel()" Text='<%# Eval("IndicatorClassName") %>' CommandArgument='<%# Eval("IndicatorClassID") %>' Width="200px"  
                          OnClick="ButtonClassClick" />           
                          <br />                                         
                       </ItemTemplate>                 
                 </asp:TemplateField>
                    </Columns> 
                </asp:GridView>
                       
            </td>
            <td >        
                <asp:GridView ID="GridView2" AutoGenerateColumns="false" runat="server" CssClass="NoSkin">
                    <Columns> 
                    <asp:TemplateField HeaderText="2">
                      <ItemTemplate>
                          <asp:Button ID="Prorector" runat="server" OnClientClick="showLoadPanel()" Text='<%# Eval("ProrectorName") %>' CommandArgument='<%# Eval("ProrectorID") %>' Width="200px"  
                          OnClick="ButtonProrectorClick" />   
                          <br />                                                  
                       </ItemTemplate>                     
                 </asp:TemplateField>
                    </Columns> 
                </asp:GridView>
                  
            </td>
            <td>       
                <asp:CheckBoxList ID="CheckBoxList1" runat="server" OnSelectedIndexChanged="CheckBoxList1_SelectedIndexChanged">
                </asp:CheckBoxList>
                <br />
                <asp:Button ID="Button5" runat="server" OnClientClick="showLoadPanel()" Text="Показать отмеченное" OnClick="Button5_Click" />
            </td>
          </tr>
        </table>
        <br />
        <br />    
    </div>

</asp:Content>