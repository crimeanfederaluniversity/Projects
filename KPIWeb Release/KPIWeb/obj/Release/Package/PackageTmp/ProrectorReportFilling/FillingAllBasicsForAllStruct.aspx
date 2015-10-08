<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="FillingAllBasicsForAllStruct.aspx.cs" Inherits="KPIWeb.ProrectorReportFilling.FillingAllBasicsForAllStruct" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    
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
     <style type="text/css">
   .button_right 
   {
       float: right;
       margin-right: 10px;
   }      
</style>
<asp:Panel runat="server" ID="top_panel2" CssClass="top_panel" Height="40" Visible="true">    
    <div>    
        <asp:Button ID="GoBackButton" runat="server" OnClientClick="showLoadPanel()" Text="Назад" Width="125px" OnClick="GoBackButton_Click" />
        <asp:Button ID="Button2" runat="server" OnClientClick="showLoadPanel()" Text="На главную" Width="125px" OnClick="Button2_Click" />

        <asp:Label ID="DataStatusLabel" runat="server" Text=""></asp:Label>

        <asp:Button ID="Button5" runat="server" CssClass="button_right" OnClientClick="showLoadPanel()"  Text="Нормативные документы" Width="250px" OnClick="Button5_Click" />
    </div> 
</asp:Panel> 
    <br />
        
    
     <h4>
<asp:Label ID="ReportNameLabel" runat="server" Text="reportName"></asp:Label>
</h4>
    <br>
    <asp:GridView ID="GridviewCollectedBasicParameters"  BorderStyle="Solid" runat="server" AutoGenerateColumns="False" 
                BorderColor="Black"  BorderWidth="1px" CellPadding="0">
               <Columns>                
                    <asp:BoundField DataField="BasicParametersTableID" HeaderText="Код показателя" Visible="true" />
                    <asp:TemplateField Visible="false"  InsertVisible="False">
                        <ItemTemplate >
                            <asp:Label ID="LabelCollectedBasicParametersTableID"  runat="server" Visible="false"  Text='<%# Bind("CollectedBasicParametersTableID") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField  HeaderText="Название показателя" >
                        <ItemTemplate>
                            <asp:Label ID="Name" CssClass="NameMin"  runat="server" Visible="True" Text='<%# Bind("Name") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                     <asp:BoundField DataField="Comment"  HeaderText="Комментарий" Visible="true" />  
                                            
                     <asp:TemplateField  HeaderText="Значение">
                        <ItemTemplate  >
                            <asp:Label ID="NotNull0"  runat="server" Visible="false" Text='<%# Bind("NotNull0") %>'></asp:Label>
                            <asp:TextBox ID="Value0"  Width="95%" style="text-align: center; background-color:transparent;" BorderWidth="0" ReadOnly='<%# Bind("TextBoxReadOnly0") %>' runat="server" Text='<%# Bind("Value0") %>'></asp:TextBox>
                            <asp:RangeValidator runat="server" ID="Validate0" ControlToValidate="Value0" 
                            ErrorMessage='<%# Bind("RangeValidatorMessage0") %>'
                            Text=        '<%# Bind("RangeValidatorMessage0") %>'
                            Type=        '<%# Bind("RangeValidatorType0") %>'
                            MaximumValue='<%# Bind("RangeValidatorMaxValue0") %>' 
                            MinimumValue='<%# Bind("RangeValidatorMinValue0") %>'
                            Enabled=     '<%# Bind("RangeValidatorEnabled0") %>'
                            ForeColor="Red" Display="Dynamic"                               
                            SetFocusOnError="True" >
                            </asp:RangeValidator>
                            <asp:Label ID="CollectId0" runat="server"  Visible="false" Text='<%# Bind("CollectId0") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>  
                    </Columns>   
    </asp:GridView>
    
    <br />
    
    <asp:Button ID="SaveButton" runat="server" Text="Сохранить" OnClick="SaveButton_Click" Width="399px" />
    <br />
    <asp:Button ID="SendButton" runat="server" Text="Отправить" OnClick="SendButton_Click" Width="399px" />
    

</asp:Content>
