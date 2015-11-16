<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="OwnersProcessInfo.aspx.cs" Inherits="KPIWeb.OwnersProcessInfo" %>

<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">

<asp:Panel runat="server" ID="top_panel2" CssClass="top_panel" Height="40" Visible="true">    
    <div>    
        <asp:Button ID="GoBackButton" runat="server" OnClientClick="window.history.back(1); return false;" Text="Назад" Width="125px" />
        <asp:Button ID="Button2" runat="server" OnClientClick="showLoadPanel()" Text="На главную" Width="125px" OnClick="Button2_Click1" />     
    </div> 
</asp:Panel> 

     <br />
    <br />
     Выберите отчет<br />
     <asp:DropDownList ID="DropDownList1" runat="server" OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged">
     </asp:DropDownList>
     <br />   
     Введите пароль
     <br />
    <asp:TextBox ID="TextBox1" runat="server" Width="292px"></asp:TextBox>
     <br />
        <asp:Button ID="Button1" runat="server" OnClientClick="showLoadPanel()" Text="Показать" Width="307px" OnClick="Button1_Click" />
     <br />
     <br />

    Описание<br />
    Название 1/2/3/4/5/6<br />
    1) Всего должно быть<br />
    2) Согласовал директор<br />
    3) Утвердили данные<br />
    4) Отправили на утверждение<br />
    5) Начали работу с системой<br />
    6) Не входили на страницу заполнения отчета<br />
     <br />
     <asp:Label ID="Label1" runat="server" Visible="false" Text="Label"></asp:Label>
     <br />
     <asp:Label ID="Label2" runat="server" Visible="false" Text="Label"></asp:Label>
     <br />
     <asp:Label ID="Label3" runat="server" Visible="false" Text="Label"></asp:Label>
     <br />
     <asp:Label ID="Label4" runat="server" Visible="false" Text="Label"></asp:Label>
     <br />
     <asp:Label ID="Label5" runat="server" Visible="false" Text="Label"></asp:Label>
     <br />   
     <asp:Label ID="Label7" runat="server" Visible="false" Text="Label"></asp:Label>
     <br />
     <br />
     <br />
     <br />
     <br />
     <asp:TreeView ID="TreeView1" runat="server" ShowLines="True">
     </asp:TreeView>
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
</asp:Content>