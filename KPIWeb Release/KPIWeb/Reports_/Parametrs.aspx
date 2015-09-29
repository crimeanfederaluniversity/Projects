<%@ Page Title="Параметры" Language="C#" MasterPageFile="~/Site.Master" EnableEventValidation="false"  AutoEventWireup="true" CodeBehind="Parametrs.aspx.cs" Inherits="KPIWeb.Reports.Parametrs" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div>
        <span style="font-size: 30px">Настройка параметров направления подготовки<br />
        </span><br />
    <asp:GridView ID="GridView1" AutoGenerateColumns="False" runat="server" OnRowDataBound="GridView1_RowDataBound">           
             <Columns>               
                 <asp:BoundField DataField="SpecializationID"   HeaderText="Current Report ID" Visible="false" />    
                 <asp:BoundField DataField="SpecializationName" HeaderText="Название направления подготовки" Visible="True" />         
               
                   
                 <asp:TemplateField HeaderText="Id направления подготовки" HeaderStyle-HorizontalAlign="Center"   HeaderStyle-VerticalAlign="Middle" Visible = "False" >
                        <ItemTemplate> 
                            <asp:Label ID="FourthlvlId" runat="server" Text='<%# Bind("FourthlvlId") %>'  Visible="True"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>   
                 
                  <asp:TemplateField HeaderText="Номер направления подготовки" HeaderStyle-HorizontalAlign="Center"   HeaderStyle-VerticalAlign="Middle" Visible = "True" >
                        <ItemTemplate> 
                            <asp:Label ID="SpecNumber" runat="server" Text='<%# Bind("SpecNumber") %>'  Visible="True"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>  

                   <asp:TemplateField HeaderText="Используются совеременные образовательные технологии" HeaderStyle-HorizontalAlign="Center"   HeaderStyle-VerticalAlign="Middle" Visible = "True" >
                        <ItemTemplate> 
                            <asp:Label ID="ParamLabel1" runat="server" Text='<%# Bind("Param1Label") %>' Visible="false"></asp:Label>
                            <asp:Checkbox  Text=" " ID="IsModern" runat="server" CommandName="Select" CommandArgument='<%# Eval("Param1CheckBox") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>     

                     <asp:TemplateField HeaderText="Осуществляется сетевое взаимодействие"  HeaderStyle-HorizontalAlign="Center"   HeaderStyle-VerticalAlign="Middle" Visible = "True">
                        <ItemTemplate>
                            <asp:Label ID="ParamLabel2" runat="server" Text='<%# Bind("Param2Label") %>' Visible="false"></asp:Label>
                            <asp:Checkbox  Text=" " ID="IsNetwork" runat="server" CommandName="Select" CommandArgument='<%# Eval("Param2CheckBox") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>   

                    <asp:TemplateField HeaderText="Осуществляется обучение студентов с особыми потребностяим" HeaderStyle-HorizontalAlign="Center"   HeaderStyle-VerticalAlign="Middle" Visible = "True">
                        <ItemTemplate>
                            <asp:Label ID="ParamLabel3" runat="server" Text='<%# Bind("Param3Label") %>' Visible="false"></asp:Label>
                            <asp:Checkbox  Text=" " ID="IsInvalid" runat="server" CommandName="Select" CommandArgument='<%# Eval("Param3CheckBox") %>' />
                        </ItemTemplate>
                    </asp:TemplateField> 
                     
                  <asp:TemplateField HeaderText="Предусмотрено обучение иностранных студентов" HeaderStyle-HorizontalAlign="Center"   HeaderStyle-VerticalAlign="Middle" Visible = "True">
                        <ItemTemplate>
                            <asp:Label ID="ParamLabel4" runat="server" Text='<%# Bind("Param4Label") %>' Visible="false"></asp:Label>
                            <asp:Checkbox  Text=" " ID="IsForeign" runat="server" CommandName="Select" CommandArgument='<%# Eval("Param4Label") %>' />
                        </ItemTemplate>
                    </asp:TemplateField> 
                 
                  <asp:TemplateField HeaderText="Param5" Visible = "False">
                        <ItemTemplate>
                            <asp:Label ID="ParamLabel5" runat="server" Text='<%# Bind("Param5Label") %>' Visible="false"></asp:Label>
                            <asp:Checkbox  Text=" " ID="ParamCheckBox5" runat="server" CommandName="Select" CommandArgument='<%# Eval("Param5CheckBox") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>      
                    
                                           
       
                </Columns>
       </asp:GridView>
        
                <br />
        
                <asp:Button ID="Button1" runat="server"  OnClientClick="showLoadPanel()" Text="Сохранить и перейти к заполнению" Width="702px" OnClick="Button1_Click" />
        
    </div>


    
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
