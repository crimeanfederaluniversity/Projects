﻿<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="DReportView.aspx.cs" Inherits="KPIWeb.Director.DReportView" %>

<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">

    <style type="text/css">
   .button_right 
   {
       float:right
   }     
</style> 
    <asp:Panel runat="server" ID="top_panel2" CssClass="top_panel" Height="40" Visible="true">    
	<div>    
		  <asp:Button ID="GoBackButton" runat="server" OnClientClick="JavaScript:window.location.href='../Director/DMain'; return false; showLoadPanel();"  Text="Назад" Width="125px" Enabled="True" />
		  <asp:Button ID="GoForwardButton" runat="server" OnClientClick="JavaScript:window.history.forward(1); return false; showLoadPanel();"  Text="Вперед" Width="125px" />
		  <asp:Button ID="Button22" OnClientClick="showLoadPanel()" runat="server"  Text="На главную" Width="125px" Enabled="True" OnClick="Button22_Click" />
		  <asp:Button ID="Button5" runat="server" OnClientClick="showLoadPanel()" CssClass="button_right" OnClick="Button5_Click" Text="Нормативные документы" Width="300px" />
	</div>
</asp:Panel>
    <script type="text/javascript">
       function askandShow()
       {
           if (confirm('Вы действительно желаете согласовать показатели?'))
           {
               showLoadPanel();
               return true;
           }
           else
           {
               return false;
           }
       }
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
        
#floatingCirclesG{
position:relative;
width:128px;
height:128px;
-moz-transform:scale(0.6);
-webkit-transform:scale(0.6);
-ms-transform:scale(0.6);
-o-transform:scale(0.6);
transform:scale(0.6);

z-index:11;
top: 50%;
left: 50%;
        margin-top: -64px;
        margin-left: -64px;
}

.f_circleG{
position:absolute;
background-color:#FFFFFF;
height:23px;
width:23px;
-moz-border-radius:12px;
-moz-animation-name:f_fadeG;
-moz-animation-duration:1.04s;
-moz-animation-iteration-count:infinite;
-moz-animation-direction:normal;
-webkit-border-radius:12px;
-webkit-animation-name:f_fadeG;
-webkit-animation-duration:1.04s;
-webkit-animation-iteration-count:infinite;
-webkit-animation-direction:normal;
-ms-border-radius:12px;
-ms-animation-name:f_fadeG;
-ms-animation-duration:1.04s;
-ms-animation-iteration-count:infinite;
-ms-animation-direction:normal;
-o-border-radius:12px;
-o-animation-name:f_fadeG;
-o-animation-duration:1.04s;
-o-animation-iteration-count:infinite;
-o-animation-direction:normal;
border-radius:12px;
animation-name:f_fadeG;
animation-duration:1.04s;
animation-iteration-count:infinite;
animation-direction:normal;
}

#frotateG_01{
left:0;
top:52px;
-moz-animation-delay:0.39s;
-webkit-animation-delay:0.39s;
-ms-animation-delay:0.39s;
-o-animation-delay:0.39s;
animation-delay:0.39s;
}

#frotateG_02{
left:15px;
top:15px;
-moz-animation-delay:0.52s;
-webkit-animation-delay:0.52s;
-ms-animation-delay:0.52s;
-o-animation-delay:0.52s;
animation-delay:0.52s;
}

#frotateG_03{
left:52px;
top:0;
-moz-animation-delay:0.65s;
-webkit-animation-delay:0.65s;
-ms-animation-delay:0.65s;
-o-animation-delay:0.65s;
animation-delay:0.65s;
}

#frotateG_04{
right:15px;
top:15px;
-moz-animation-delay:0.78s;
-webkit-animation-delay:0.78s;
-ms-animation-delay:0.78s;
-o-animation-delay:0.78s;
animation-delay:0.78s;
}

#frotateG_05{
right:0;
top:52px;
-moz-animation-delay:0.91s;
-webkit-animation-delay:0.91s;
-ms-animation-delay:0.91s;
-o-animation-delay:0.91s;
animation-delay:0.91s;
}

#frotateG_06{
right:15px;
bottom:15px;
-moz-animation-delay:1.04s;
-webkit-animation-delay:1.04s;
-ms-animation-delay:1.04s;
-o-animation-delay:1.04s;
animation-delay:1.04s;
}

#frotateG_07{
left:52px;
bottom:0;
-moz-animation-delay:1.17s;
-webkit-animation-delay:1.17s;
-ms-animation-delay:1.17s;
-o-animation-delay:1.17s;
animation-delay:1.17s;
}

#frotateG_08{
left:15px;
bottom:15px;
-moz-animation-delay:1.3s;
-webkit-animation-delay:1.3s;
-ms-animation-delay:1.3s;
-o-animation-delay:1.3s;
animation-delay:1.3s;
}

@-moz-keyframes f_fadeG{
0%{
background-color:#000000}

100%{
background-color:#FFFFFF}

}

@-webkit-keyframes f_fadeG{
0%{
background-color:#000000}

100%{
background-color:#FFFFFF}

}

@-ms-keyframes f_fadeG{
0%{
background-color:#000000}

100%{
background-color:#FFFFFF}

}

@-o-keyframes f_fadeG{
0%{
background-color:#000000}

100%{
background-color:#FFFFFF}

}

@keyframes f_fadeG{
0%{
background-color:#000000}

100%{
background-color:#FFFFFF}

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
           <h2>
           <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>
           <br />
                Список подразделений           
           </h2>
           <br />
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

    .colorRed
    {
       // opacity:0.4;
        background-color:#F79999;
    }
        .colorGreen
    {
        background-color:#99F99F;
    }
                .colorYellow
    {
        background-color:#F4FF99;
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
    <div id="sidePanel" class='side_legend' onclick="legendChange()"> 
    <div id="img_of_sp" class="side_img_legend"></div>   
            <!--<button type="button" onclick="legendChange()">
            O</button>-->
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:Label ID="Label7" runat="server" Text="Легенда" ValidateRequestMode="Enabled"></asp:Label>
            <br />
            <br />
            <asp:Panel ID="Panel5" runat="server" CssClass="colorGreen">
                <asp:Label ID="Label11" runat="server" Text=".....Согласовано"></asp:Label>
                <br />
            </asp:Panel>
            <br />
            <asp:Panel ID="Panel6" runat="server" Height="17" CssClass="colorYellow">
                <asp:Label ID="Label12" runat="server" Text=".....Требует Вашего согласования"></asp:Label>
            </asp:Panel>
            <br />
            <asp:Panel ID="Panel7" runat="server" Height="17" CssClass="colorRed">
                <asp:Label ID="Label13" runat="server"  style="text-align:center;color:black;" Text=".....В процессе заполнения"></asp:Label>
            </asp:Panel>
        </div>  
    <asp:GridView ID="GridView1" runat="server" 
            AutoGenerateColumns="False"
            style="margin-top: 0px" OnRowDataBound="GridView1_RowDataBound">
             <Columns>        
                         
                 <asp:BoundField DataField="StructName" HeaderText="Структурное подразделение" Visible="True" />          

                  <asp:TemplateField HeaderText="Подробнее">
                        <ItemTemplate>
                            <asp:Button ID="ButtonViewReport" runat="server" CommandName="Select" OnClientClick="showLoadPanel()" Text="Просмотреть" Width="200px" CommandArgument='<%# Eval("StructID") %>' OnClick="ButtonDetailClick"/>
                            <asp:Label ID="Color"  runat="server" Visible="false" Text='<%# Bind("Color") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                   <asp:BoundField DataField="Status" HeaderText="Статус данных" Visible="True" />

                </Columns>
        </asp:GridView>
           <br />
<asp:Button ID="Button23" runat="server" OnClick="Button23_Click" OnClientClick="showLoadPanel()" Text="Сводные значения по факультетам" Width="518px" />
<br />
           <br />
           <asp:Button ID="Button1" runat="server" Enabled="false" OnClientClick="return askandShow();" Text="Согласовать" Width="517px" OnClick="Button1_Click" />
           <br />
    <br />
           <asp:Button ID="Button24" runat="server" Enabled="true"  Text="Должники" Width="517px" OnClick="Button24_Click" />
           <br />
           <br />
       </asp:Content>
