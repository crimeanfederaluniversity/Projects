﻿<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="Result.aspx.cs" Inherits="KPIWeb.Rector.Result" %>
 <asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">  
     <link rel="stylesheet" type="text/css" href="../Spinner.css">   

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
        visibility: hidden;
    }
   .commentSectionStyle
    {
        position:fixed;    
        width:400px;
        height:400px;    
        background-color:beige;
        z-index:15;
        top: 50%;
        left: 50%;
        margin-top: -200px;
        margin-left: -200px;
        visibility: hidden;
        border-style: solid;
        border-width: 3px;
        border-color: black;
     }
   .emailSectionStyle
    {
        position:fixed;    
        width:400px;
        height:450px;    
        background-color:beige;
        z-index:15;
        top: 50%;
        left: 50%;
        margin-top: -200px;
        margin-left: -200px;
        visibility: hidden;
        border-style: solid;
        border-width: 3px;
        border-color: black;
     }
   .commentLeftButton 
   {
       width: 185px;
        height: 40px;
       margin-left: 5px;     
       margin-top: 5px;
   }
   .commentRightButton 
   {
       width: 185px;
       height: 40px;
       margin-left: 5px;      
       margin-top: 5px;
   }
   .commentTextBox 
   {
       min-height: 250px;
       min-width: 385px;
       width: 385px;
       height: 250px;
       max-width: 385px;
       max-height: 250px;
       margin-top: 5px;
       margin-left: 5px;       
   }
   .ColorPanelLegend 
   {       
       height: 17px;
        
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
.triangle-bottomright {
            width: 25px;
            height: 25px;
            border-bottom: 25px solid red;
            border-left: 25px solid transparent;
            right:0;
            bottom:0;
            position:absolute;
            
        }
.triangle-bottomright div {
        visibility:hidden; 
        position:absolute;
        left:0;
        top:0;
        width:300px;
        height:auto;
        z-index :123;    
        max-width:300px;
        max-height:300px;       
        background-color:#b6ff00;
        overflow:auto;       
        word-wrap: break-word;
        border-style:solid;
        border-width:1px;
        border-color:black;

        }
.triangle-bottomright:hover div
       {
        visibility:visible;
       }
.td {
    position: relative;
}

</style>         
<script type="text/javascript" >
    function DoPostBack() {
        __doPostBack('cmd', 'thesearemyarguments');
    }
     </script>
<script type="text/javascript">
    var y = 0;
    var z = "";
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
    function ShowLegend() {
        document.getElementById('sidePanel').style.visibility = 'visible';
    }
    function closeLoadPanel() {
        document.getElementById('LoadPanel_').style.visibility = 'hidden';
    }
    function closeCommentSection() {
        document.getElementById('comment_Section').style.visibility = 'hidden';
        closeLoadPanel();
    }
    function closeEmailSection() {
        document.getElementById('email_Section').style.visibility = 'hidden';
        closeLoadPanel();
    }
    function showCommentSection(i) {

        document.getElementById('comment_Section').style.visibility = 'visible';
        showLoadPanel();
        y = i;
        return false;
    }

    function showEmailSection(i , j) {

        document.getElementById('email_Section').style.visibility = 'visible';
        document.getElementById('UserToSendPosition').textContent = ' Получатель: '+j;
        showLoadPanel();
        z = i;
        return false;
    }
    

    function showAlert() {
        alert(y);
    }
    
    function emailSendButtonClick() {
        document.getElementById('email_Section').style.visibility = 'hidden';
        showLoadPanel();
        __doPostBack('EmailSendParam', z);
    }

    function commentSendButtonClick() {
        document.getElementById('comment_Section').style.visibility = 'hidden';
        showLoadPanel();
        __doPostBack('CommentSendParam', y);
    }
</script>
<div id="sidePanel" class='side_legend' onclick="legendChange()"> 
    <div id="img_of_sp" class="side_img_legend"></div>   
            <!--<button type="button" onclick="legendChange()">
            O</button>-->
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:Label ID="Label7" runat="server" Text="Легенда" ValidateRequestMode="Enabled"></asp:Label>
            <br />
            <br />
            <asp:Panel ID="Panel5" runat="server" CssClass="ColorPanelLegend">
                <asp:Label ID="Label11" runat="server" Text=".....Утверждено"></asp:Label>
                <br />
            </asp:Panel>
            <br />
            <asp:Panel ID="Panel6" runat="server" Height="17" BackColor="#000099" BorderColor="#000099">
                <asp:Label ID="Label12" runat="server" Text=".....Данные полностью внесены"></asp:Label>
            </asp:Panel>
            <br />
            <asp:Panel ID="Panel7" runat="server" Height="17" BackColor="#CC0000" BorderColor="#CC0000">
                <asp:Label ID="Label13" runat="server" style="text-align: center" Text=".....Рассчитано по неполным данным"></asp:Label>
            </asp:Panel>
        </div>  
<div id="comment_Section" class='commentSectionStyle'> 
            <h4 style="text-align:center"> После утверждения данных их изменение будет невозможно! </h4>  
            <h4 style="margin-left:5px"> Комментарий к утверждаемому значению:</h4>
    
            <asp:TextBox ID="TextBox1" runat="server"  CssClass="commentTextBox" TextMode="MultiLine" ></asp:TextBox>
            <button type="button"  class="commentLeftButton" onclick="closeCommentSection()">Отмена</button>
            <button type="button" class="commentRightButton"  onclick="commentSendButtonClick()">Утвердить</button>
</div>
     
<div id="email_Section" class='emailSectionStyle'> 
            
            <h4 style="text-align:center"> Форма отправки Email сообщений.</h4>  
            <p style="margin-left:5px;margin-bottom:0px" id="UserToSendPosition"></p>
            <p style="margin-left:5px;margin-bottom:0px">Тема сообщения:</p>
            <asp:TextBox ID="EmailTitle"  runat="server" Width="500px"  TextMode="SingleLine" ></asp:TextBox>
            <p style="margin-left:5px;margin-bottom:0px">Текст сообщения:</p>
            <asp:TextBox ID="TextBox2" runat="server"  CssClass="commentTextBox" TextMode="MultiLine" ></asp:TextBox>
            <button type="button"  class="commentLeftButton" onclick="closeEmailSection()">Отмена</button>
            <button type="button" class="commentRightButton" onclick="emailSendButtonClick()">Отправить</button>
</div>

<asp:Panel runat="server" ID="top_panel2" CssClass="top_panel" Height="40" Visible="true">    
                <div>    
              <asp:Button ID="GoBackButton" runat="server" OnClientClick="showLoadPanel()" OnClick="GoBackButton_Click" Text="Назад" Width="125px" />
              <asp:Button ID="GoForwardButton" runat="server" OnClientClick="showLoadPanel()" OnClick="GoForwardButton_Click" Text="Вперед" Width="125px" />
                &nbsp; &nbsp; <asp:Button ID="Button4" runat="server" OnClientClick="showLoadPanel()" OnClick="Button4_Click" Text="На главную" Width="125px" />
                    <asp:Button ID="Button8" runat="server" OnClick="Button8_Click1" Text="К списку отчетов" />
                &nbsp; &nbsp;       
                <asp:Button ID="Button5" runat="server" CssClass="button_right"  OnClick="Button5_Click" Text="Нормативные документы" Width="250px" OnClientClick="showLoadPanel()" />
                    &nbsp; &nbsp; &nbsp; &nbsp;
                </div>

            </asp:Panel>   
 
     
    <script type="text/javascript">
        function showLoadPanel() {
            document.getElementById('LoadPanel_').style.visibility = 'visible';
        }
    </script>
    <style>  

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
     

     <br /><br /><br />
     <asp:Label ID="ReportTitle" runat="server" Text="ReportTitle" Font-Size="20pt"></asp:Label>
     <br />
     <asp:Label ID="PageFullName" runat="server" Text="PageFullName" Font-Size="15pt"></asp:Label>
     <br />
     <asp:TreeView ID="TreeView1" runat="server" NodeWrap="True" Visible="False" ShowExpandCollapse="False" ShowLines="True">
     </asp:TreeView>
     &nbsp;<asp:Button ID="Button7" runat="server" OnClientClick="showLoadPanel()" OnClick="Button7_Click" 
         Text="Отобразить значения показателей по неполным данным" Width="762px" />
     <br />
     <br />
        <asp:GridView ID="Grid" runat="server" CssClass="result_gw sova"
            AutoGenerateColumns="False"
            style="margin-top: 0px;" OnSelectedIndexChanged="Grid_SelectedIndexChanged" OnRowDataBound="Grid_RowDataBound">
             <Columns>                
   
                 <asp:TemplateField HeaderText="ID" Visible="False">
                        <ItemTemplate>
                            <asp:Label ID="IDs"  runat="server" Visible="false" Text='<%# Bind("ID") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>


                 <asp:BoundField DataField="Number"   HeaderText="Номер" Visible="true" />
                 <asp:BoundField DataField="Abb"   HeaderText="Шифр" Visible="True" />
                 <asp:BoundField DataField="Name" HeaderText="Get" Visible="True" />          
                 <asp:BoundField DataField="StartDate" HeaderText="Начальная дата отчёта" Visible="True" />
                 <asp:BoundField DataField="EndDate" HeaderText="Конечная дата отчёта" Visible="True" />

                 <asp:TemplateField  HeaderText="Значение">
                      <ItemTemplate>

                          <asp:Label ID="Value"  runat="server"  Text='<%# Eval("Value") %>' ></asp:Label>
                          <div class="triangle-bottomright" style="visibility:<%# Eval("CommentEnabled") %>" >
	                       <!---> <p style="position:relative; right:10px;top:5px; font-size:large; color:red;">!</p><!-->
	                        <div id="CommentID" runat="server">
		                        <%# Eval("Comment") %>
	                        </div>
                            </div> 
                       </ItemTemplate>
                 </asp:TemplateField>



                 <asp:BoundField DataField="PlannedValue" HeaderText="Плановое значение ЦП" Visible="True" />
                 
                 
                 
                 <asp:TemplateField HeaderText="Прогресс заполнения">
                      <ItemTemplate>
                          <asp:Label ID="ProgressLable"  runat="server"  Text='<%# Eval("Progress") %>' ></asp:Label>
                          <asp:Button ID="ProgressButton" runat="server" CommandName="Select"  Visible='<%# Bind("CanWatchWhoOws") %>' Text="Должники" CommandArgument='<%# Eval("ID") %>' Width="200px"  
                          OnClientClick="showLoadPanel()" OnClick="ButtonOweClick" />                                                    

                       </ItemTemplate>
                 </asp:TemplateField>
                 
                 <asp:TemplateField HeaderText="Утвердить данные">
                        <ItemTemplate>
                            <asp:Label ID="lb00"  runat="server"  Text="  " ></asp:Label>
                            <asp:Button ID="ConfirmButton" runat="server" CommandName="Select" Visible='<%# Bind("CanConfirm") %>' Text="Утвердить" CommandArgument='<%# Eval("ID") %>' Width="200px"  
                              OnClientClick="showLoadPanel()" OnClick="ButtonConfirmClick"/>                                                    
                            <asp:Label ID="StatusLable"  runat="server" Visible='<%# Bind("ShowLable") %>' Text='<%# Eval("LableText") %>' ></asp:Label>
                             </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Детализация базового показателя">
                        <ItemTemplate>
                            <asp:Label ID="lb01"  runat="server"  Text="  " ></asp:Label>
                            <asp:Button ID="Button1" runat="server" CommandName="Select" Text="Детализировать" CommandArgument='<%# Eval("ID") %>' Width="200px" 
                                OnClick="Button1Click " OnClientClick="showLoadPanel()"/>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="get">
                        <ItemTemplate>
                            <asp:Label ID="lb02"  runat="server"  Text="  " ></asp:Label>
                            <asp:Button ID="Button2" runat="server" CommandName="Select" Text="Просмотреть" CommandArgument='<%# Eval("ID") %>' Width="200px" OnClick="Button2Click" OnClientClick="showLoadPanel()"/>
                        </ItemTemplate>
                    </asp:TemplateField>

                  <asp:TemplateField HeaderText="Детализация базового показателя по направлениям подготовки">
                        <ItemTemplate>
                            <asp:Label ID="lb03"  runat="server"  Text="  " ></asp:Label>
                            <asp:Button ID="Button3" runat="server" CommandName="Select" Text="Детализировать"  CommandArgument='<%# Eval("ID") %>' Width="200px" OnClick="Button3Click" OnClientClick="showLoadPanel()"/>
                        </ItemTemplate>
                    </asp:TemplateField>
                 
                 <asp:TemplateField HeaderText="Цвет" Visible="False">
                        <ItemTemplate>
                            <asp:Label ID="Color"  runat="server" Visible="false" Text='<%# Bind("Color") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                 
                 
                  <asp:TemplateField HeaderText="Отправить сообщение">
                        <ItemTemplate>
                            <asp:Button ID="Button8" runat="server" CommandName="Select" Text="Отправить" CommandArgument='<%# Bind("UserID") %>' Width="200px" 
                                OnClick="Button8Click " />
                            <asp:Label ID="LblPosition" Visible="false"  runat="server"  Text='<%# Bind("UserPosition") %>' ></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>



                </Columns>
        </asp:GridView>
     <br />
     <asp:Label ID="FormulaLable" runat="server" Text="FormulaLable" Visible="False"></asp:Label>
     <br />
     </asp:Content>
