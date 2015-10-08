<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="FillingPage.aspx.cs" Inherits="KPIWeb.ProrectorReportFilling.FillingPage" %>
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
    
    <br>
    
     <h4>
<asp:Label ID="ReportNameLabel" runat="server" Text="reportName"></asp:Label>
</h4>
<h4>
<asp:Label ID="FirstLevelNameLabel" runat="server" Text="firetLevelName"></asp:Label>
</h4>
<h4>
<asp:Label ID="SecondLevelNameLabel" runat="server" Text="secondLevelName"></asp:Label>
</h4>
<h4>
<asp:Label ID="ThirdLevelNameLabel" runat="server" Text="thirdLevelName"></asp:Label>
</h4> 
    <p>
        &nbsp;</p>
        <asp:GridView ID="GridviewCollectedBasicParameters"  BorderStyle="Solid" runat="server" AutoGenerateColumns="False" 
                BorderColor="Black"  BorderWidth="1px" CellPadding="0" OnRowDataBound="GridviewCollectedBasicParameters_RowDataBound">
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
                                            
                     <asp:TemplateField Visible="false"   HeaderText="Значение">
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
                    <asp:TemplateField Visible="false"  HeaderText="Значение">
                        <ItemTemplate>
                            <asp:Label ID="NotNull1"  runat="server" Visible="false" Text='<%# Bind("NotNull1") %>'></asp:Label>
                            <asp:TextBox ID="Value1" Width="95%" style="text-align: center; background-color:transparent;" BorderWidth="0" ReadOnly='<%# Bind("TextBoxReadOnly1") %>' runat="server" Text='<%# Bind("Value1") %>'></asp:TextBox>
                            <asp:RangeValidator runat="server" ID="Validate1" ControlToValidate="Value1" 
                            ErrorMessage='<%# Bind("RangeValidatorMessage1") %>'
                            Text=        '<%# Bind("RangeValidatorMessage1") %>'
                            Type=        '<%# Bind("RangeValidatorType1") %>'
                            MaximumValue='<%# Bind("RangeValidatorMaxValue1") %>' 
                            MinimumValue='<%# Bind("RangeValidatorMinValue1") %>'
                            Enabled=     '<%# Bind("RangeValidatorEnabled1") %>'
                            ForeColor="Red" Display="Dynamic"                               
                            SetFocusOnError="True" >
                            </asp:RangeValidator>
                            <asp:Label ID="CollectId1" runat="server" Visible="false" Text='<%# Bind("CollectId1") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>               
                     <asp:TemplateField Visible="false" HeaderText="Значение">
                        <ItemTemplate>
                            <asp:Label ID="NotNull2"  runat="server" Visible="false" Text='<%# Bind("NotNull2") %>'></asp:Label>
                            <asp:TextBox ID="Value2" Width="95%" style="text-align: center; background-color:transparent;" BorderWidth="0" ReadOnly='<%# Bind("TextBoxReadOnly2") %>' runat="server" Text='<%# Bind("Value2") %>'></asp:TextBox>
                            <asp:RangeValidator runat="server" ID="Validate2" ControlToValidate="Value2" 
                            ErrorMessage='<%# Bind("RangeValidatorMessage2") %>'
                            Text=        '<%# Bind("RangeValidatorMessage2") %>'
                            Type=        '<%# Bind("RangeValidatorType2") %>'
                            MaximumValue='<%# Bind("RangeValidatorMaxValue2") %>' 
                            MinimumValue='<%# Bind("RangeValidatorMinValue2") %>'
                            Enabled=     '<%# Bind("RangeValidatorEnabled2") %>'
                            ForeColor="Red" Display="Dynamic"                               
                            SetFocusOnError="True" >
                            </asp:RangeValidator>
                            <asp:Label ID="CollectId2" runat="server" Visible="false" Text='<%# Bind("CollectId2") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField Visible="false" HeaderText="Значение">
                        <ItemTemplate>
                            <asp:Label ID="NotNull3"  runat="server" Visible="false" Text='<%# Bind("NotNull3") %>'></asp:Label>
                            <asp:TextBox ID="Value3" Width="95%" style="text-align: center; background-color:transparent;" BorderWidth="0" ReadOnly='<%# Bind("TextBoxReadOnly3") %>' runat="server" Text='<%# Bind("Value3") %>'></asp:TextBox>
                            <asp:RangeValidator runat="server" ID="Validate3" ControlToValidate="Value3" 
                            ErrorMessage='<%# Bind("RangeValidatorMessage3") %>'
                            Text=        '<%# Bind("RangeValidatorMessage3") %>'
                            Type=        '<%# Bind("RangeValidatorType3") %>'
                            MaximumValue='<%# Bind("RangeValidatorMaxValue3") %>' 
                            MinimumValue='<%# Bind("RangeValidatorMinValue3") %>'
                            Enabled=     '<%# Bind("RangeValidatorEnabled3") %>'
                            ForeColor="Red" Display="Dynamic"                               
                            SetFocusOnError="True" >
                            </asp:RangeValidator>
                            <asp:Label ID="CollectId3" runat="server" Visible="false" Text='<%# Bind("CollectId3") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>     
                     <asp:TemplateField Visible="false" HeaderText="Значение">
                        <ItemTemplate>
                            <asp:Label ID="NotNull4"  runat="server" Visible="false" Text='<%# Bind("NotNull4") %>'></asp:Label>
                            <asp:TextBox ID="Value4" Width="95%" style="text-align: center; background-color:transparent;" BorderWidth="0" ReadOnly='<%# Bind("TextBoxReadOnly4") %>' runat="server" Text='<%# Bind("Value4") %>'></asp:TextBox>
                            <asp:RangeValidator runat="server" ID="Validate4" ControlToValidate="Value4" 
                            ErrorMessage='<%# Bind("RangeValidatorMessage4") %>'
                            Text=        '<%# Bind("RangeValidatorMessage4") %>'
                            Type=        '<%# Bind("RangeValidatorType4") %>'
                            MaximumValue='<%# Bind("RangeValidatorMaxValue4") %>' 
                            MinimumValue='<%# Bind("RangeValidatorMinValue4") %>'
                            Enabled=     '<%# Bind("RangeValidatorEnabled4") %>'
                            ForeColor="Red" Display="Dynamic"                               
                            SetFocusOnError="True" >
                            </asp:RangeValidator>
                            <asp:Label ID="CollectId4" runat="server" Visible="false" Text='<%# Bind("CollectId4") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                     <asp:TemplateField Visible="false" HeaderText="Значение">
                        <ItemTemplate>
                            <asp:Label ID="NotNull5"  runat="server" Visible="false" Text='<%# Bind("NotNull5") %>'></asp:Label>
                            <asp:TextBox ID="Value5" Width="95%" style="text-align: center; background-color:transparent;" BorderWidth="0" ReadOnly='<%# Bind("TextBoxReadOnly5") %>' runat="server" Text='<%# Bind("Value5") %>'></asp:TextBox>
                            <asp:RangeValidator runat="server" ID="Validate5" ControlToValidate="Value5" 
                            ErrorMessage='<%# Bind("RangeValidatorMessage5") %>'
                            Text=        '<%# Bind("RangeValidatorMessage5") %>'
                            Type=        '<%# Bind("RangeValidatorType5") %>'
                            MaximumValue='<%# Bind("RangeValidatorMaxValue5") %>' 
                            MinimumValue='<%# Bind("RangeValidatorMinValue5") %>'
                            Enabled=     '<%# Bind("RangeValidatorEnabled5") %>'
                            ForeColor="Red" Display="Dynamic"                               
                            SetFocusOnError="True" >
                            </asp:RangeValidator>
                            <asp:Label ID="CollectId5" runat="server" Visible="false" Text='<%# Bind("CollectId5") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>               
                     <asp:TemplateField Visible="false" HeaderText="Значение">
                        <ItemTemplate>
                            <asp:Label ID="NotNull6"  runat="server" Visible="false" Text='<%# Bind("NotNull6") %>'></asp:Label>
                            <asp:TextBox ID="Value6" Width="95%" style="text-align: center; background-color:transparent;" BorderWidth="0" ReadOnly='<%# Bind("TextBoxReadOnly6") %>' runat="server" Text='<%# Bind("Value6") %>'></asp:TextBox>
                            <asp:RangeValidator runat="server" ID="Validate6" ControlToValidate="Value6" 
                            ErrorMessage='<%# Bind("RangeValidatorMessage6") %>'
                            Text=        '<%# Bind("RangeValidatorMessage6") %>'
                            Type=        '<%# Bind("RangeValidatorType6") %>'
                            MaximumValue='<%# Bind("RangeValidatorMaxValue6") %>' 
                            MinimumValue='<%# Bind("RangeValidatorMinValue6") %>'
                            Enabled=     '<%# Bind("RangeValidatorEnabled6") %>'
                            ForeColor="Red" Display="Dynamic"                               
                            SetFocusOnError="True" >
                            </asp:RangeValidator>
                            <asp:Label ID="CollectId6" runat="server" Visible="false" Text='<%# Bind("CollectId6") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField  Visible="false" HeaderText="Значение">
                        <ItemTemplate>
                            <asp:Label ID="NotNull7"  runat="server" Visible="false" Text='<%# Bind("NotNull7") %>'></asp:Label>
                            <asp:TextBox ID="Value7" Width="95%" style="text-align: center; background-color:transparent;" BorderWidth="0" ReadOnly='<%# Bind("TextBoxReadOnly7") %>' runat="server" Text='<%# Bind("Value7") %>'></asp:TextBox>
                            <asp:RangeValidator runat="server" ID="Validate7" ControlToValidate="Value7" 
                            ErrorMessage='<%# Bind("RangeValidatorMessage7") %>'
                            Text=        '<%# Bind("RangeValidatorMessage7") %>'
                            Type=        '<%# Bind("RangeValidatorType7") %>'
                            MaximumValue='<%# Bind("RangeValidatorMaxValue7") %>' 
                            MinimumValue='<%# Bind("RangeValidatorMinValue7") %>'
                            Enabled=     '<%# Bind("RangeValidatorEnabled7") %>'
                            ForeColor="Red" Display="Dynamic"                               
                            SetFocusOnError="True" >
                            </asp:RangeValidator>
                            <asp:Label ID="CollectId7" runat="server" Visible="false" Text='<%# Bind("CollectId7") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>     
                     <asp:TemplateField Visible="false" HeaderText="Значение">
                        <ItemTemplate>
                            <asp:Label ID="NotNull8"  runat="server" Visible="false" Text='<%# Bind("NotNull8") %>'></asp:Label>
                            <asp:TextBox ID="Value8" Width="95%" style="text-align: center; background-color:transparent;" BorderWidth="0" ReadOnly='<%# Bind("TextBoxReadOnly8") %>' runat="server" Text='<%# Bind("Value8") %>'></asp:TextBox>
                            <asp:RangeValidator runat="server" ID="Validate8" ControlToValidate="Value8" 
                            ErrorMessage='<%# Bind("RangeValidatorMessage8") %>'
                            Text=        '<%# Bind("RangeValidatorMessage8") %>'
                            Type=        '<%# Bind("RangeValidatorType8") %>'
                            MaximumValue='<%# Bind("RangeValidatorMaxValue8") %>' 
                            MinimumValue='<%# Bind("RangeValidatorMinValue8") %>'
                            Enabled=     '<%# Bind("RangeValidatorEnabled8") %>'
                            ForeColor="Red" Display="Dynamic"                               
                            SetFocusOnError="True" >
                            </asp:RangeValidator>
                            <asp:Label ID="CollectId8" runat="server" Visible="false" Text='<%# Bind("CollectId8") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField Visible="false" HeaderText="Значение">
                        <ItemTemplate>
                            <asp:Label ID="NotNull9"  runat="server" Visible="false" Text='<%# Bind("NotNull9") %>'></asp:Label>
                            <asp:TextBox ID="Value9" Width="95%" style="text-align: center; background-color:transparent;" BorderWidth="0" ReadOnly='<%# Bind("TextBoxReadOnly9") %>' runat="server" Text='<%# Bind("Value9") %>'></asp:TextBox>
                            <asp:RangeValidator runat="server" ID="Validate9" ControlToValidate="Value9" 
                            ErrorMessage='<%# Bind("RangeValidatorMessage9") %>'
                            Text=        '<%# Bind("RangeValidatorMessage9") %>'
                            Type=        '<%# Bind("RangeValidatorType9") %>'
                            MaximumValue='<%# Bind("RangeValidatorMaxValue9") %>' 
                            MinimumValue='<%# Bind("RangeValidatorMinValue9") %>'
                            Enabled=     '<%# Bind("RangeValidatorEnabled9") %>'
                            ForeColor="Red" Display="Dynamic"                               
                            SetFocusOnError="True" >
                            </asp:RangeValidator>
                            <asp:Label ID="CollectId9" runat="server" Visible="false" Text='<%# Bind("CollectId9") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>  
                    <asp:TemplateField Visible="false" HeaderText="Значение">
                        <ItemTemplate>
                            <asp:Label ID="NotNull10"   runat="server" Visible="false" Text='<%# Bind("NotNull10") %>'></asp:Label>
                            <asp:TextBox ID="Value10" Width="95%" style="text-align: center; background-color:transparent;" BorderWidth="0" ReadOnly='<%# Bind("TextBoxReadOnly10") %>' runat="server" Text='<%# Bind("Value10") %>'></asp:TextBox>
                            <asp:RangeValidator runat="server" ID="Validate10" ControlToValidate="Value10" 
                            ErrorMessage='<%# Bind("RangeValidatorMessage10") %>'
                            Text=        '<%# Bind("RangeValidatorMessage10") %>'
                            Type=        '<%# Bind("RangeValidatorType10") %>'
                            MaximumValue='<%# Bind("RangeValidatorMaxValue10") %>' 
                            MinimumValue='<%# Bind("RangeValidatorMinValue10") %>'
                            Enabled=     '<%# Bind("RangeValidatorEnabled10") %>'
                            ForeColor="Red" Display="Dynamic"                               
                            SetFocusOnError="True" >
                            </asp:RangeValidator>
                            <asp:Label ID="CollectId10" runat="server" Visible="false" Text='<%# Bind("CollectId10") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>     
                     <asp:TemplateField Visible="false"   HeaderText="Значение">
                        <ItemTemplate >
                            <asp:Label ID="NotNull11"  runat="server" Visible="false" Text='<%# Bind("NotNull11") %>'></asp:Label>
                            <asp:TextBox ID="Value11" Width="95%" style="text-align: center; background-color:transparent;" BorderWidth="0" ReadOnly='<%# Bind("TextBoxReadOnly11") %>' runat="server" Text='<%# Bind("Value11") %>'></asp:TextBox>
                            <asp:RangeValidator runat="server" ID="Validate11" ControlToValidate="Value11" 
                            ErrorMessage='<%# Bind("RangeValidatorMessage11") %>'
                            Text=        '<%# Bind("RangeValidatorMessage11") %>'
                            Type=        '<%# Bind("RangeValidatorType11") %>'
                            MaximumValue='<%# Bind("RangeValidatorMaxValue11") %>' 
                            MinimumValue='<%# Bind("RangeValidatorMinValue11") %>'
                            Enabled=     '<%# Bind("RangeValidatorEnabled11") %>'
                            ForeColor="Red" Display="Dynamic"                               
                            SetFocusOnError="True" >
                            </asp:RangeValidator>
                            <asp:Label ID="CollectId11" runat="server" Visible="false" Text='<%# Bind("CollectId11") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>  
                    <asp:TemplateField Visible="false"   HeaderText="Значение">
                        <ItemTemplate >
                            <asp:Label ID="NotNull12"  runat="server" Visible="false" Text='<%# Bind("NotNull12") %>'></asp:Label>
                            <asp:TextBox ID="Value12" Width="95%" style="text-align: center; background-color:transparent;" BorderWidth="0" ReadOnly='<%# Bind("TextBoxReadOnly12") %>' runat="server" Text='<%# Bind("Value12") %>'></asp:TextBox>
                            <asp:RangeValidator runat="server" ID="Validate12" ControlToValidate="Value12" 
                            ErrorMessage='<%# Bind("RangeValidatorMessage12") %>'
                            Text=        '<%# Bind("RangeValidatorMessage12") %>'
                            Type=        '<%# Bind("RangeValidatorType12") %>'
                            MaximumValue='<%# Bind("RangeValidatorMaxValue12") %>' 
                            MinimumValue='<%# Bind("RangeValidatorMinValue12") %>'
                            Enabled=     '<%# Bind("RangeValidatorEnabled12") %>'
                            ForeColor="Red" Display="Dynamic"                               
                            SetFocusOnError="True" >
                            </asp:RangeValidator>
                            <asp:Label ID="CollectId12" runat="server" Visible="false" Text='<%# Bind("CollectId12") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>  
                    <asp:TemplateField Visible="false"   HeaderText="Значение">
                        <ItemTemplate >
                            <asp:Label ID="NotNull13"  runat="server" Visible="false" Text='<%# Bind("NotNull13") %>'></asp:Label>
                            <asp:TextBox ID="Value13" Width="95%" style="text-align: center; background-color:transparent;" BorderWidth="0" ReadOnly='<%# Bind("TextBoxReadOnly13") %>' runat="server" Text='<%# Bind("Value13") %>'></asp:TextBox>
                            <asp:RangeValidator runat="server" ID="Validate13" ControlToValidate="Value13" 
                            ErrorMessage='<%# Bind("RangeValidatorMessage13") %>'
                            Text=        '<%# Bind("RangeValidatorMessage13") %>'
                            Type=        '<%# Bind("RangeValidatorType13") %>'
                            MaximumValue='<%# Bind("RangeValidatorMaxValue13") %>' 
                            MinimumValue='<%# Bind("RangeValidatorMinValue13") %>'
                            Enabled=     '<%# Bind("RangeValidatorEnabled13") %>'
                            ForeColor="Red" Display="Dynamic"                               
                            SetFocusOnError="True" >
                            </asp:RangeValidator>
                            <asp:Label ID="CollectId13" runat="server" Visible="false" Text='<%# Bind("CollectId13") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>  
                    <asp:TemplateField Visible="false"   HeaderText="Значение">
                        <ItemTemplate >
                            <asp:Label ID="NotNull14"  runat="server" Visible="false" Text='<%# Bind("NotNull14") %>'></asp:Label>
                            <asp:TextBox ID="Value14" Width="95%" style="text-align: center; background-color:transparent;" BorderWidth="0" ReadOnly='<%# Bind("TextBoxReadOnly14") %>' runat="server" Text='<%# Bind("Value14") %>'></asp:TextBox>
                            <asp:RangeValidator runat="server" ID="Validate14" ControlToValidate="Value14" 
                            ErrorMessage='<%# Bind("RangeValidatorMessage14") %>'
                            Text=        '<%# Bind("RangeValidatorMessage14") %>'
                            Type=        '<%# Bind("RangeValidatorType14") %>'
                            MaximumValue='<%# Bind("RangeValidatorMaxValue14") %>' 
                            MinimumValue='<%# Bind("RangeValidatorMinValue14") %>'
                            Enabled=     '<%# Bind("RangeValidatorEnabled14") %>'
                            ForeColor="Red" Display="Dynamic"                               
                            SetFocusOnError="True" >
                            </asp:RangeValidator>
                            <asp:Label ID="CollectId14" runat="server" Visible="false" Text='<%# Bind("CollectId14") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField> 
                    <asp:TemplateField Visible="false"   HeaderText="Значение">
                        <ItemTemplate >
                            <asp:Label ID="NotNull15"  runat="server" Visible="false" Text='<%# Bind("NotNull15") %>'></asp:Label>
                            <asp:TextBox ID="Value15" Width="95%" style="text-align: center; background-color:transparent;" BorderWidth="0" ReadOnly='<%# Bind("TextBoxReadOnly15") %>' runat="server" Text='<%# Bind("Value15") %>'></asp:TextBox>
                            <asp:RangeValidator runat="server" ID="Validate15" ControlToValidate="Value15" 
                            ErrorMessage='<%# Bind("RangeValidatorMessage15") %>'
                            Text=        '<%# Bind("RangeValidatorMessage15") %>'
                            Type=        '<%# Bind("RangeValidatorType15") %>'
                            MaximumValue='<%# Bind("RangeValidatorMaxValue15") %>' 
                            MinimumValue='<%# Bind("RangeValidatorMinValue15") %>'
                            Enabled=     '<%# Bind("RangeValidatorEnabled15") %>'
                            ForeColor="Red" Display="Dynamic"                               
                            SetFocusOnError="True" >
                            </asp:RangeValidator>
                            <asp:Label ID="CollectId15" runat="server" Visible="false" Text='<%# Bind("CollectId15") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField> 
                    <asp:TemplateField Visible="false"   HeaderText="Значение">
                        <ItemTemplate >
                            <asp:Label ID="NotNull16"  runat="server" Visible="false" Text='<%# Bind("NotNull16") %>'></asp:Label>
                            <asp:TextBox ID="Value16" Width="95%" style="text-align: center; background-color:transparent;" BorderWidth="0" ReadOnly='<%# Bind("TextBoxReadOnly16") %>' runat="server" Text='<%# Bind("Value16") %>'></asp:TextBox>
                            <asp:RangeValidator runat="server" ID="Validate16" ControlToValidate="Value16" 
                            ErrorMessage='<%# Bind("RangeValidatorMessage16") %>'
                            Text=        '<%# Bind("RangeValidatorMessage16") %>'
                            Type=        '<%# Bind("RangeValidatorType16") %>'
                            MaximumValue='<%# Bind("RangeValidatorMaxValue16") %>' 
                            MinimumValue='<%# Bind("RangeValidatorMinValue16") %>'
                            Enabled=     '<%# Bind("RangeValidatorEnabled16") %>'
                            ForeColor="Red" Display="Dynamic"                               
                            SetFocusOnError="True" >
                            </asp:RangeValidator>
                            <asp:Label ID="CollectId16" runat="server" Visible="false" Text='<%# Bind("CollectId16") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField> 
                    <asp:TemplateField Visible="false"   HeaderText="Значение">
                        <ItemTemplate >
                            <asp:Label ID="NotNull17"  runat="server" Visible="false" Text='<%# Bind("NotNull17") %>'></asp:Label>
                            <asp:TextBox ID="Value17" Width="95%" style="text-align: center; background-color:transparent;" BorderWidth="0" ReadOnly='<%# Bind("TextBoxReadOnly17") %>' runat="server" Text='<%# Bind("Value17") %>'></asp:TextBox>
                            <asp:RangeValidator runat="server" ID="Validate17" ControlToValidate="Value17" 
                            ErrorMessage='<%# Bind("RangeValidatorMessage17") %>'
                            Text=        '<%# Bind("RangeValidatorMessage17") %>'
                            Type=        '<%# Bind("RangeValidatorType17") %>'
                            MaximumValue='<%# Bind("RangeValidatorMaxValue17") %>' 
                            MinimumValue='<%# Bind("RangeValidatorMinValue17") %>'
                            Enabled=     '<%# Bind("RangeValidatorEnabled17") %>'
                            ForeColor="Red" Display="Dynamic"                               
                            SetFocusOnError="True" >
                            </asp:RangeValidator>
                            <asp:Label ID="CollectId17" runat="server" Visible="false" Text='<%# Bind("CollectId17") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField> 
                    <asp:TemplateField Visible="false"   HeaderText="Значение">
                        <ItemTemplate >
                            <asp:Label ID="NotNull18"  runat="server" Visible="false" Text='<%# Bind("NotNull18") %>'></asp:Label>
                            <asp:TextBox ID="Value18" Width="95%" style="text-align: center; background-color:transparent;" BorderWidth="0" ReadOnly='<%# Bind("TextBoxReadOnly18") %>' runat="server" Text='<%# Bind("Value18") %>'></asp:TextBox>
                            <asp:RangeValidator runat="server" ID="Validate18" ControlToValidate="Value18" 
                            ErrorMessage='<%# Bind("RangeValidatorMessage18") %>'
                            Text=        '<%# Bind("RangeValidatorMessage18") %>'
                            Type=        '<%# Bind("RangeValidatorType18") %>'
                            MaximumValue='<%# Bind("RangeValidatorMaxValue18") %>' 
                            MinimumValue='<%# Bind("RangeValidatorMinValue18") %>'
                            Enabled=     '<%# Bind("RangeValidatorEnabled18") %>'
                            ForeColor="Red" Display="Dynamic"                               
                            SetFocusOnError="True" >
                            </asp:RangeValidator>
                            <asp:Label ID="CollectId18" runat="server" Visible="false" Text='<%# Bind("CollectId18") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField> 
                    <asp:TemplateField Visible="false"   HeaderText="Значение">
                        <ItemTemplate >
                            <asp:Label ID="NotNull19"   runat="server" Visible="false" Text='<%# Bind("NotNull19") %>'></asp:Label>
                            <asp:TextBox ID="Value19" Width="95%" style="text-align: center; background-color:transparent;" BorderWidth="0" ReadOnly='<%# Bind("TextBoxReadOnly19") %>' runat="server" Text='<%# Bind("Value19") %>'></asp:TextBox>
                            <asp:RangeValidator runat="server" ID="Validate19" ControlToValidate="Value19" 
                            ErrorMessage='<%# Bind("RangeValidatorMessage19") %>'
                            Text=        '<%# Bind("RangeValidatorMessage19") %>'
                            Type=        '<%# Bind("RangeValidatorType19") %>'
                            MaximumValue='<%# Bind("RangeValidatorMaxValue19") %>' 
                            MinimumValue='<%# Bind("RangeValidatorMinValue19") %>'
                            Enabled=     '<%# Bind("RangeValidatorEnabled19") %>'
                            ForeColor="Red" Display="Dynamic"                               
                            SetFocusOnError="True" >
                            </asp:RangeValidator>
                            <asp:Label ID="CollectId19" runat="server" Visible="false" Text='<%# Bind("CollectId19") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField> 
                    <asp:TemplateField Visible="false"   HeaderText="Значение">
                        <ItemTemplate >
                            <asp:Label ID="NotNull20"  runat="server" Visible="false" Text='<%# Bind("NotNull20") %>'></asp:Label>
                            <asp:TextBox ID="Value20" Width="95%" style="text-align: center; background-color:transparent;" BorderWidth="0" ReadOnly='<%# Bind("TextBoxReadOnly20") %>' runat="server" Text='<%# Bind("Value20") %>'></asp:TextBox>
                            <asp:RangeValidator runat="server" ID="Validate20" ControlToValidate="Value20" 
                            ErrorMessage='<%# Bind("RangeValidatorMessage20") %>'
                            Text=        '<%# Bind("RangeValidatorMessage20") %>'
                            Type=        '<%# Bind("RangeValidatorType20") %>'
                            MaximumValue='<%# Bind("RangeValidatorMaxValue20") %>' 
                            MinimumValue='<%# Bind("RangeValidatorMinValue20") %>'
                            Enabled=     '<%# Bind("RangeValidatorEnabled20") %>'
                            ForeColor="Red" Display="Dynamic"                               
                            SetFocusOnError="True" >
                            </asp:RangeValidator>
                            <asp:Label ID="CollectId20" runat="server" Visible="false" Text='<%# Bind("CollectId20") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField> 
                    <asp:TemplateField Visible="false"   HeaderText="Значение">
                        <ItemTemplate >
                            <asp:Label ID="NotNull21"  runat="server" Visible="false" Text='<%# Bind("NotNull21") %>'></asp:Label>
                            <asp:TextBox ID="Value21" Width="95%" style="text-align: center; background-color:transparent;" BorderWidth="0" ReadOnly='<%# Bind("TextBoxReadOnly21") %>' runat="server" Text='<%# Bind("Value21") %>'></asp:TextBox>
                            <asp:RangeValidator runat="server" ID="Validate21" ControlToValidate="Value21" 
                            ErrorMessage='<%# Bind("RangeValidatorMessage21") %>'
                            Text=        '<%# Bind("RangeValidatorMessage21") %>'
                            Type=        '<%# Bind("RangeValidatorType21") %>'
                            MaximumValue='<%# Bind("RangeValidatorMaxValue21") %>' 
                            MinimumValue='<%# Bind("RangeValidatorMinValue21") %>'
                            Enabled=     '<%# Bind("RangeValidatorEnabled21") %>'
                            ForeColor="Red" Display="Dynamic"                               
                            SetFocusOnError="True" >
                            </asp:RangeValidator>
                            <asp:Label ID="CollectId21" runat="server" Visible="false" Text='<%# Bind("CollectId21") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField> 
                    <asp:TemplateField Visible="false"   HeaderText="Значение">
                        <ItemTemplate >
                            <asp:Label ID="NotNull22"  runat="server" Visible="false" Text='<%# Bind("NotNull22") %>'></asp:Label>
                            <asp:TextBox ID="Value22" Width="95%" style="text-align: center; background-color:transparent;" BorderWidth="0" ReadOnly='<%# Bind("TextBoxReadOnly22") %>' runat="server" Text='<%# Bind("Value22") %>'></asp:TextBox>
                            <asp:RangeValidator runat="server" ID="Validate22" ControlToValidate="Value22" 
                            ErrorMessage='<%# Bind("RangeValidatorMessage22") %>'
                            Text=        '<%# Bind("RangeValidatorMessage22") %>'
                            Type=        '<%# Bind("RangeValidatorType22") %>'
                            MaximumValue='<%# Bind("RangeValidatorMaxValue22") %>' 
                            MinimumValue='<%# Bind("RangeValidatorMinValue22") %>'
                            Enabled=     '<%# Bind("RangeValidatorEnabled22") %>'
                            ForeColor="Red" Display="Dynamic"                               
                            SetFocusOnError="True" >
                            </asp:RangeValidator>
                            <asp:Label ID="CollectId22" runat="server" Visible="false" Text='<%# Bind("CollectId22") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField> 
                    <asp:TemplateField Visible="false"   HeaderText="Значение">
                        <ItemTemplate >
                            <asp:Label ID="NotNull23"  runat="server" Visible="false" Text='<%# Bind("NotNull23") %>'></asp:Label>
                            <asp:TextBox ID="Value23" Width="95%" style="text-align: center; background-color:transparent;" BorderWidth="0" ReadOnly='<%# Bind("TextBoxReadOnly23") %>' runat="server" Text='<%# Bind("Value23") %>'></asp:TextBox>
                            <asp:RangeValidator runat="server" ID="Validate23" ControlToValidate="Value23" 
                            ErrorMessage='<%# Bind("RangeValidatorMessage23") %>'
                            Text=        '<%# Bind("RangeValidatorMessage23") %>'
                            Type=        '<%# Bind("RangeValidatorType23") %>'
                            MaximumValue='<%# Bind("RangeValidatorMaxValue23") %>' 
                            MinimumValue='<%# Bind("RangeValidatorMinValue23") %>'
                            Enabled=     '<%# Bind("RangeValidatorEnabled23") %>'
                            ForeColor="Red" Display="Dynamic"                               
                            SetFocusOnError="True" >
                            </asp:RangeValidator>
                            <asp:Label ID="CollectId23" runat="server" Visible="false" Text='<%# Bind("CollectId23") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField> 
                    <asp:TemplateField Visible="false"   HeaderText="Значение">
                        <ItemTemplate >
                            <asp:Label ID="NotNull24"  runat="server" Visible="false" Text='<%# Bind("NotNull24") %>'></asp:Label>
                            <asp:TextBox ID="Value24" Width="95%" style="text-align: center; background-color:transparent;" BorderWidth="0" ReadOnly='<%# Bind("TextBoxReadOnly24") %>' runat="server" Text='<%# Bind("Value24") %>'></asp:TextBox>
                            <asp:RangeValidator runat="server" ID="Validate24" ControlToValidate="Value24" 
                            ErrorMessage='<%# Bind("RangeValidatorMessage24") %>'
                            Text=        '<%# Bind("RangeValidatorMessage24") %>'
                            Type=        '<%# Bind("RangeValidatorType24") %>'
                            MaximumValue='<%# Bind("RangeValidatorMaxValue24") %>' 
                            MinimumValue='<%# Bind("RangeValidatorMinValue24") %>'
                            Enabled=     '<%# Bind("RangeValidatorEnabled24") %>'
                            ForeColor="Red" Display="Dynamic"                               
                            SetFocusOnError="True" >
                            </asp:RangeValidator>
                            <asp:Label ID="CollectId24" runat="server" Visible="false" Text='<%# Bind("CollectId24") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField> 
                    <asp:TemplateField Visible="false"   HeaderText="Значение">
                        <ItemTemplate >
                            <asp:Label ID="NotNull25"  runat="server" Visible="false" Text='<%# Bind("NotNull25") %>'></asp:Label>
                            <asp:TextBox ID="Value25" Width="95%" style="text-align: center; background-color:transparent;" BorderWidth="0" ReadOnly='<%# Bind("TextBoxReadOnly25") %>' runat="server" Text='<%# Bind("Value25") %>'></asp:TextBox>                            
                            <asp:RangeValidator runat="server" ID="Validate25" ControlToValidate="Value25" 
                            ErrorMessage='<%# Bind("RangeValidatorMessage25") %>'
                            Text=        '<%# Bind("RangeValidatorMessage25") %>'
                            Type=        '<%# Bind("RangeValidatorType25") %>'
                            MaximumValue='<%# Bind("RangeValidatorMaxValue25") %>' 
                            MinimumValue='<%# Bind("RangeValidatorMinValue25") %>'
                            Enabled=     '<%# Bind("RangeValidatorEnabled25") %>'
                            ForeColor="Red" Display="Dynamic"                               
                            SetFocusOnError="True" >
                            </asp:RangeValidator>
                            <asp:Label ID="CollectId25" runat="server" Visible="false" Text='<%# Bind("CollectId25") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>          
                    <asp:TemplateField Visible="false"   HeaderText="Значение">
                        <ItemTemplate >
                            <asp:Label ID="NotNull26"  runat="server" Visible="false" Text='<%# Bind("NotNull26") %>'></asp:Label>
                            <asp:TextBox ID="Value26" Width="95%" style="text-align: center; background-color:transparent;" BorderWidth="0" ReadOnly='<%# Bind("TextBoxReadOnly26") %>' runat="server" Text='<%# Bind("Value26") %>'></asp:TextBox>                            
                            <asp:RangeValidator runat="server" ID="Validate26" ControlToValidate="Value26" 
                            ErrorMessage='<%# Bind("RangeValidatorMessage26") %>'
                            Text=        '<%# Bind("RangeValidatorMessage26") %>'
                            Type=        '<%# Bind("RangeValidatorType26") %>'
                            MaximumValue='<%# Bind("RangeValidatorMaxValue26") %>' 
                            MinimumValue='<%# Bind("RangeValidatorMinValue26") %>'
                            Enabled=     '<%# Bind("RangeValidatorEnabled26") %>'
                            ForeColor="Red" Display="Dynamic"                               
                            SetFocusOnError="True" >
                            </asp:RangeValidator>
                            <asp:Label ID="CollectId26" runat="server" Visible="false" Text='<%# Bind("CollectId26") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>            
                    <asp:TemplateField Visible="false"   HeaderText="Значение">
                        <ItemTemplate >
                            <asp:Label ID="NotNull27"  runat="server" Visible="false" Text='<%# Bind("NotNull27") %>'></asp:Label>
                            <asp:TextBox ID="Value27" Width="95%" style="text-align: center; background-color:transparent;" BorderWidth="0" ReadOnly='<%# Bind("TextBoxReadOnly27") %>' runat="server" Text='<%# Bind("Value27") %>'></asp:TextBox>                            
                            <asp:RangeValidator runat="server" ID="Validate27" ControlToValidate="Value27" 
                            ErrorMessage='<%# Bind("RangeValidatorMessage27") %>'
                            Text=        '<%# Bind("RangeValidatorMessage27") %>'
                            Type=        '<%# Bind("RangeValidatorType27") %>'
                            MaximumValue='<%# Bind("RangeValidatorMaxValue27") %>' 
                            MinimumValue='<%# Bind("RangeValidatorMinValue27") %>'
                            Enabled=     '<%# Bind("RangeValidatorEnabled27") %>'
                            ForeColor="Red" Display="Dynamic"                               
                            SetFocusOnError="True" >
                            </asp:RangeValidator>
                            <asp:Label ID="CollectId27" runat="server" Visible="false" Text='<%# Bind("CollectId27") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField> 
                    <asp:TemplateField Visible="false"   HeaderText="Значение">
                        <ItemTemplate >
                            <asp:Label ID="NotNull28"  runat="server" Visible="false" Text='<%# Bind("NotNull28") %>'></asp:Label>
                            <asp:TextBox ID="Value28" Width="95%" style="text-align: center; background-color:transparent;" BorderWidth="0" ReadOnly='<%# Bind("TextBoxReadOnly28") %>' runat="server" Text='<%# Bind("Value28") %>'></asp:TextBox>                            
                            <asp:RangeValidator runat="server" ID="Validate28" ControlToValidate="Value28" 
                            ErrorMessage='<%# Bind("RangeValidatorMessage28") %>'
                            Text=        '<%# Bind("RangeValidatorMessage28") %>'
                            Type=        '<%# Bind("RangeValidatorType28") %>'
                            MaximumValue='<%# Bind("RangeValidatorMaxValue28") %>' 
                            MinimumValue='<%# Bind("RangeValidatorMinValue28") %>'
                            Enabled=     '<%# Bind("RangeValidatorEnabled28") %>'
                            ForeColor="Red" Display="Dynamic"                               
                            SetFocusOnError="True" >
                            </asp:RangeValidator>
                            <asp:Label ID="CollectId28" runat="server" Visible="false" Text='<%# Bind("CollectId28") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField> 
                    <asp:TemplateField Visible="false"   HeaderText="Значение">
                        <ItemTemplate >
                            <asp:Label ID="NotNull29"  runat="server" Visible="false" Text='<%# Bind("NotNull29") %>'></asp:Label>
                            <asp:TextBox ID="Value29" Width="95%" style="text-align: center; background-color:transparent;" BorderWidth="0" ReadOnly='<%# Bind("TextBoxReadOnly29") %>' runat="server" Text='<%# Bind("Value29") %>'></asp:TextBox>                            
                            <asp:RangeValidator runat="server" ID="Validate29" ControlToValidate="Value29" 
                            ErrorMessage='<%# Bind("RangeValidatorMessage29") %>'
                            Text=        '<%# Bind("RangeValidatorMessage29") %>'
                            Type=        '<%# Bind("RangeValidatorType29") %>'
                            MaximumValue='<%# Bind("RangeValidatorMaxValue29") %>' 
                            MinimumValue='<%# Bind("RangeValidatorMinValue29") %>'
                            Enabled=     '<%# Bind("RangeValidatorEnabled29") %>'
                            ForeColor="Red" Display="Dynamic"                               
                            SetFocusOnError="True" >
                            </asp:RangeValidator>
                            <asp:Label ID="CollectId29" runat="server" Visible="false" Text='<%# Bind("CollectId29") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField> 
                    <asp:TemplateField Visible="false"   HeaderText="Значение">
                        <ItemTemplate >
                            <asp:Label ID="NotNull30"  runat="server" Visible="false" Text='<%# Bind("NotNull30") %>'></asp:Label>
                            <asp:TextBox ID="Value30" Width="95%" style="text-align: center; background-color:transparent;" BorderWidth="0" ReadOnly='<%# Bind("TextBoxReadOnly30") %>' runat="server" Text='<%# Bind("Value30") %>'></asp:TextBox>                            
                            <asp:RangeValidator runat="server" ID="Validate30" ControlToValidate="Value30" 
                            ErrorMessage='<%# Bind("RangeValidatorMessage30") %>'
                            Text=        '<%# Bind("RangeValidatorMessage30") %>'
                            Type=        '<%# Bind("RangeValidatorType30") %>'
                            MaximumValue='<%# Bind("RangeValidatorMaxValue30") %>' 
                            MinimumValue='<%# Bind("RangeValidatorMinValue30") %>'
                            Enabled=     '<%# Bind("RangeValidatorEnabled30") %>'
                            ForeColor="Red" Display="Dynamic"                               
                            SetFocusOnError="True" >
                            </asp:RangeValidator>
                            <asp:Label ID="CollectId30" runat="server" Visible="false" Text='<%# Bind("CollectId30") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField> 
                    <asp:TemplateField Visible="false"   HeaderText="Значение">
                        <ItemTemplate >
                            <asp:Label ID="NotNull31"  runat="server" Visible="false" Text='<%# Bind("NotNull31") %>'></asp:Label>
                            <asp:TextBox ID="Value31" Width="95%" style="text-align: center; background-color:transparent;" BorderWidth="0" ReadOnly='<%# Bind("TextBoxReadOnly31") %>' runat="server" Text='<%# Bind("Value31") %>'></asp:TextBox>                            
                            <asp:RangeValidator runat="server" ID="Validate31" ControlToValidate="Value31" 
                            ErrorMessage='<%# Bind("RangeValidatorMessage31") %>'
                            Text=        '<%# Bind("RangeValidatorMessage31") %>'
                            Type=        '<%# Bind("RangeValidatorType31") %>'
                            MaximumValue='<%# Bind("RangeValidatorMaxValue31") %>' 
                            MinimumValue='<%# Bind("RangeValidatorMinValue31") %>'
                            Enabled=     '<%# Bind("RangeValidatorEnabled31") %>'
                            ForeColor="Red" Display="Dynamic"                               
                            SetFocusOnError="True" >
                            </asp:RangeValidator>
                            <asp:Label ID="CollectId31" runat="server" Visible="false" Text='<%# Bind("CollectId31") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField> 
                                        <asp:TemplateField Visible="false"   HeaderText="Значение">
                        <ItemTemplate >
                            <asp:Label ID="NotNull32"  runat="server" Visible="false" Text='<%# Bind("NotNull32") %>'></asp:Label>
                            <asp:TextBox ID="Value32" Width="95%" style="text-align: center; background-color:transparent;" BorderWidth="0" ReadOnly='<%# Bind("TextBoxReadOnly32") %>' runat="server" Text='<%# Bind("Value32") %>'></asp:TextBox>                            
                            <asp:RangeValidator runat="server" ID="Validate32" ControlToValidate="Value32" 
                            ErrorMessage='<%# Bind("RangeValidatorMessage32") %>'
                            Text=        '<%# Bind("RangeValidatorMessage32") %>'
                            Type=        '<%# Bind("RangeValidatorType32") %>'
                            MaximumValue='<%# Bind("RangeValidatorMaxValue32") %>' 
                            MinimumValue='<%# Bind("RangeValidatorMinValue32") %>'
                            Enabled=     '<%# Bind("RangeValidatorEnabled32") %>'
                            ForeColor="Red" Display="Dynamic"                               
                            SetFocusOnError="True" >
                            </asp:RangeValidator>
                            <asp:Label ID="CollectId32" runat="server" Visible="false" Text='<%# Bind("CollectId32") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField> 
                                        <asp:TemplateField Visible="false"   HeaderText="Значение">
                        <ItemTemplate >
                            <asp:Label ID="NotNull33"  runat="server" Visible="false" Text='<%# Bind("NotNull33") %>'></asp:Label>
                            <asp:TextBox ID="Value33" Width="95%" style="text-align: center; background-color:transparent;" BorderWidth="0" ReadOnly='<%# Bind("TextBoxReadOnly33") %>' runat="server" Text='<%# Bind("Value33") %>'></asp:TextBox>                            
                            <asp:RangeValidator runat="server" ID="Validate33" ControlToValidate="Value33" 
                            ErrorMessage='<%# Bind("RangeValidatorMessage33") %>'
                            Text=        '<%# Bind("RangeValidatorMessage33") %>'
                            Type=        '<%# Bind("RangeValidatorType33") %>'
                            MaximumValue='<%# Bind("RangeValidatorMaxValue33") %>' 
                            MinimumValue='<%# Bind("RangeValidatorMinValue33") %>'
                            Enabled=     '<%# Bind("RangeValidatorEnabled33") %>'
                            ForeColor="Red" Display="Dynamic"                               
                            SetFocusOnError="True" >
                            </asp:RangeValidator>
                            <asp:Label ID="CollectId33" runat="server" Visible="false" Text='<%# Bind("CollectId33") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField> 
                                        <asp:TemplateField Visible="false"   HeaderText="Значение">
                        <ItemTemplate >
                            <asp:Label ID="NotNull34"  runat="server" Visible="false" Text='<%# Bind("NotNull34") %>'></asp:Label>
                            <asp:TextBox ID="Value34" Width="95%" style="text-align: center; background-color:transparent;" BorderWidth="0" ReadOnly='<%# Bind("TextBoxReadOnly34") %>' runat="server" Text='<%# Bind("Value34") %>'></asp:TextBox>                            
                            <asp:RangeValidator runat="server" ID="Validate34" ControlToValidate="Value34" 
                            ErrorMessage='<%# Bind("RangeValidatorMessage34") %>'
                            Text=        '<%# Bind("RangeValidatorMessage34") %>'
                            Type=        '<%# Bind("RangeValidatorType34") %>'
                            MaximumValue='<%# Bind("RangeValidatorMaxValue34") %>' 
                            MinimumValue='<%# Bind("RangeValidatorMinValue34") %>'
                            Enabled=     '<%# Bind("RangeValidatorEnabled34") %>'
                            ForeColor="Red" Display="Dynamic"                               
                            SetFocusOnError="True" >
                            </asp:RangeValidator>
                            <asp:Label ID="CollectId34" runat="server" Visible="false" Text='<%# Bind("CollectId34") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField> 
                                        <asp:TemplateField Visible="false"   HeaderText="Значение">
                        <ItemTemplate >
                            <asp:Label ID="NotNull35"  runat="server" Visible="false" Text='<%# Bind("NotNull35") %>'></asp:Label>
                            <asp:TextBox ID="Value35" Width="95%" style="text-align: center; background-color:transparent;" BorderWidth="0" ReadOnly='<%# Bind("TextBoxReadOnly35") %>' runat="server" Text='<%# Bind("Value35") %>'></asp:TextBox>                            
                            <asp:RangeValidator runat="server" ID="Validate35" ControlToValidate="Value35" 
                            ErrorMessage='<%# Bind("RangeValidatorMessage35") %>'
                            Text=        '<%# Bind("RangeValidatorMessage35") %>'
                            Type=        '<%# Bind("RangeValidatorType35") %>'
                            MaximumValue='<%# Bind("RangeValidatorMaxValue35") %>' 
                            MinimumValue='<%# Bind("RangeValidatorMinValue35") %>'
                            Enabled=     '<%# Bind("RangeValidatorEnabled35") %>'
                            ForeColor="Red" Display="Dynamic"                               
                            SetFocusOnError="True" >
                            </asp:RangeValidator>
                            <asp:Label ID="CollectId35" runat="server" Visible="false" Text='<%# Bind("CollectId35") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField> 
                                        <asp:TemplateField Visible="false"   HeaderText="Значение">
                        <ItemTemplate >
                            <asp:Label ID="NotNull36"  runat="server" Visible="false" Text='<%# Bind("NotNull36") %>'></asp:Label>
                            <asp:TextBox ID="Value36" Width="95%" style="text-align: center; background-color:transparent;" BorderWidth="0" ReadOnly='<%# Bind("TextBoxReadOnly36") %>' runat="server" Text='<%# Bind("Value36") %>'></asp:TextBox>                            
                            <asp:RangeValidator runat="server" ID="Validate36" ControlToValidate="Value36" 
                            ErrorMessage='<%# Bind("RangeValidatorMessage36") %>'
                            Text=        '<%# Bind("RangeValidatorMessage36") %>'
                            Type=        '<%# Bind("RangeValidatorType36") %>'
                            MaximumValue='<%# Bind("RangeValidatorMaxValue36") %>' 
                            MinimumValue='<%# Bind("RangeValidatorMinValue36") %>'
                            Enabled=     '<%# Bind("RangeValidatorEnabled36") %>'
                            ForeColor="Red" Display="Dynamic"                               
                            SetFocusOnError="True" >
                            </asp:RangeValidator>
                            <asp:Label ID="CollectId36" runat="server" Visible="false" Text='<%# Bind("CollectId36") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField> 
                                        <asp:TemplateField Visible="false"   HeaderText="Значение">
                        <ItemTemplate >
                            <asp:Label ID="NotNull37"  runat="server" Visible="false" Text='<%# Bind("NotNull37") %>'></asp:Label>
                            <asp:TextBox ID="Value37" Width="95%" style="text-align: center; background-color:transparent;" BorderWidth="0" ReadOnly='<%# Bind("TextBoxReadOnly37") %>' runat="server" Text='<%# Bind("Value37") %>'></asp:TextBox>                            
                            <asp:RangeValidator runat="server" ID="Validate37" ControlToValidate="Value37" 
                            ErrorMessage='<%# Bind("RangeValidatorMessage37") %>'
                            Text=        '<%# Bind("RangeValidatorMessage37") %>'
                            Type=        '<%# Bind("RangeValidatorType37") %>'
                            MaximumValue='<%# Bind("RangeValidatorMaxValue37") %>' 
                            MinimumValue='<%# Bind("RangeValidatorMinValue37") %>'
                            Enabled=     '<%# Bind("RangeValidatorEnabled37") %>'
                            ForeColor="Red" Display="Dynamic"                               
                            SetFocusOnError="True" >
                            </asp:RangeValidator>
                            <asp:Label ID="CollectId37" runat="server" Visible="false" Text='<%# Bind("CollectId37") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField> 
                                        <asp:TemplateField Visible="false"   HeaderText="Значение">
                        <ItemTemplate >
                            <asp:Label ID="NotNull38"  runat="server" Visible="false" Text='<%# Bind("NotNull38") %>'></asp:Label>
                            <asp:TextBox ID="Value38" Width="95%" style="text-align: center; background-color:transparent;" BorderWidth="0" ReadOnly='<%# Bind("TextBoxReadOnly38") %>' runat="server" Text='<%# Bind("Value38") %>'></asp:TextBox>                            
                            <asp:RangeValidator runat="server" ID="Validate38" ControlToValidate="Value38" 
                            ErrorMessage='<%# Bind("RangeValidatorMessage38") %>'
                            Text=        '<%# Bind("RangeValidatorMessage38") %>'
                            Type=        '<%# Bind("RangeValidatorType38") %>'
                            MaximumValue='<%# Bind("RangeValidatorMaxValue38") %>' 
                            MinimumValue='<%# Bind("RangeValidatorMinValue38") %>'
                            Enabled=     '<%# Bind("RangeValidatorEnabled38") %>'
                            ForeColor="Red" Display="Dynamic"                               
                            SetFocusOnError="True" >
                            </asp:RangeValidator>
                            <asp:Label ID="CollectId38" runat="server" Visible="false" Text='<%# Bind("CollectId38") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField> 
                                        <asp:TemplateField Visible="false"   HeaderText="Значение">
                        <ItemTemplate >
                            <asp:Label ID="NotNull39"  runat="server" Visible="false" Text='<%# Bind("NotNull39") %>'></asp:Label>
                            <asp:TextBox ID="Value39" Width="95%" style="text-align: center; background-color:transparent;" BorderWidth="0" ReadOnly='<%# Bind("TextBoxReadOnly39") %>' runat="server" Text='<%# Bind("Value39") %>'></asp:TextBox>                            
                            <asp:RangeValidator runat="server" ID="Validate39" ControlToValidate="Value39" 
                            ErrorMessage='<%# Bind("RangeValidatorMessage39") %>'
                            Text=        '<%# Bind("RangeValidatorMessage39") %>'
                            Type=        '<%# Bind("RangeValidatorType39") %>'
                            MaximumValue='<%# Bind("RangeValidatorMaxValue39") %>' 
                            MinimumValue='<%# Bind("RangeValidatorMinValue39") %>'
                            Enabled=     '<%# Bind("RangeValidatorEnabled39") %>'
                            ForeColor="Red" Display="Dynamic"                               
                            SetFocusOnError="True" >
                            </asp:RangeValidator>
                            <asp:Label ID="CollectId39" runat="server" Visible="false" Text='<%# Bind("CollectId39") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField> 
                                        <asp:TemplateField Visible="false"   HeaderText="Значение">
                        <ItemTemplate >
                            <asp:Label ID="NotNull40"  runat="server" Visible="false" Text='<%# Bind("NotNull40") %>'></asp:Label>
                            <asp:TextBox ID="Value40" Width="95%" style="text-align: center; background-color:transparent;" BorderWidth="0" ReadOnly='<%# Bind("TextBoxReadOnly40") %>' runat="server" Text='<%# Bind("Value40") %>'></asp:TextBox>                            
                            <asp:RangeValidator runat="server" ID="Validate40" ControlToValidate="Value40" 
                            ErrorMessage='<%# Bind("RangeValidatorMessage40") %>'
                            Text=        '<%# Bind("RangeValidatorMessage40") %>'
                            Type=        '<%# Bind("RangeValidatorType40") %>'
                            MaximumValue='<%# Bind("RangeValidatorMaxValue40") %>' 
                            MinimumValue='<%# Bind("RangeValidatorMinValue40") %>'
                            Enabled=     '<%# Bind("RangeValidatorEnabled40") %>'
                            ForeColor="Red" Display="Dynamic"                               
                            SetFocusOnError="True" >
                            </asp:RangeValidator>
                            <asp:Label ID="CollectId40" runat="server"  Visible="false" Text='<%# Bind("CollectId40") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField> 
                </Columns>
            </asp:GridView>
    
    <br />
    
    <asp:Button ID="FillButton" runat="server" Text="Заполнить пустые поля нулями" OnClientClick="showLoadPanel()" OnClick="FillWithZeroButtonClick" Width="400px" />
    <br />  
    <asp:Button ID="SaveButton" runat="server" Text="Сохранить внесенные данные" OnClientClick="showLoadPanel()" OnClick="SaveButton_Click" Width="400px" />
    <br />
    <asp:Button ID="Button6" runat="server" OnClick="Button6_Click" OnClientClick="showLoadPanel()" Text="Отправить данные" Width="400px" />
    <br />
    <script type="text/javascript" >
        var counter = 0;
        var masoftables = [];
        function parseGW() {
            // document.body.style.backgroundColor = gw_name;
            windowheighter = window.screen.availWidth;
            $(window).load(function () {

                var texts = "def";
                var i = 0;
                var ownerdiv = [];
                var cthead = "ControlHead";

                var gridHeader;
                var s;
                for (i = 0; i < document.all.length; i++) {
                    if (((document.all[i].tagName.toString()).toLowerCase()) == 'table') {
                        masoftables[counter] = document.all[i].getAttribute("id").toString();
                        counter++;
                    }
                }
                $('#cdf').text(masoftables[1]);
                for (i = 0; i < counter; i++) {

                    ownerdiv[i] = "diva"; cthead = "ControlHead";
                    ownerdiv[i] += '_' + i.toString();
                    cthead += '_' + i.toString();
                    $('#' + masoftables[i]).wrap(("<div id='" + ownerdiv[i] + "' style='position:absolute;'></div>"));
                    $(('#' + ownerdiv[i])).prepend(("<div id='" + cthead + "'></div>"));

                    $('#' + ownerdiv[i]).wrap(("<div id='" + "pre_" + ownerdiv[i] + "' style='position:absolute;'></div>"));
                    $("#pre_" + ownerdiv[i]).css('position', 'relative');
                    $("#pre_" + ownerdiv[i]).css('height', (document.getElementById("diva_" + (i).toString()).getBoundingClientRect().bottom - document.getElementById("diva_" + (i).toString()).getBoundingClientRect().top).toString() + "px");

                    // if (i == 0) { $('#' + ownerdiv).before("<div id ='SHEET' style='position:relative;'>"); }
                    // if ((i + 1) == counter) { $('#' + ownerdiv).after("</div>"); }
                    //  if (i > 0) { $(("#" + ownerdiv[i])).css('top', (document.getElementById(ownerdiv[i - 1]).getBoundingClientRect().bottom + window.pageYOffset + (window.pageYOffset - document.getElementById(ownerdiv[i - 1]).getBoundingClientRect().top)).toString() + "px"); }


                    gridHeader = $('#' + masoftables[i]).clone(true);
                    // document.getElementById(masoftables[i]).id = "FF_" + i.toString();
                    $(gridHeader).find("tr:gt(0)").remove();
                    // $(gridHeader).id = "FFF";

                    $('#' + masoftables[i] + ' tr th').each(function (s) {

                        $("th:nth-child(" + (s + 1) + ")", gridHeader).css('width', ($(this).width() + 21).toString() + "px");
                    });


                    $(("#" + cthead)).append(gridHeader);
                    document.getElementById(masoftables[i]).id = "ffg" + i.toString();
                    $(("#" + cthead)).css('position', 'absolute');

                    $(("#" + cthead)).css('z-index', '9');
                    //  $(("#" + cthead + " tr")).css('left', ($('#' + 'ffg' + i.toString()).offset().left + 1).toString() + "px");



                    // $('#cdf').text(i.toString());
                }
                //$('#cdf').text((((document.getElementById("diva_" + (0).toString()).getBoundingClientRect().bottom - document.getElementById("diva_" + (0).toString()).getBoundingClientRect().top).toString() + "px")));
                //$('#cdf').text(gridHeader.id.toString());
                $("#aroundgwsdivALL").css('position', 'relative');


            });

        }

        parseGW();
        mtb();
        function rth() {
            var cthead = "ControlHead";


            for (i = 0; i < counter; i++) {
                cthead += '_' + i.toString();
                $('#' + masoftables[i] + ' tr th').each(function (f) {
                    cthead = "ControlHead";
                    cthead += '_' + i.toString();
                    //$("th:nth-child(" + (s + 1) + ")", $("#" + cthead)).css('width', ($(this).width() + 3).toString() + "px");
                    // $("th:nth-child(" + (s + 1) + ")", $("#" + cthead)).css('color', "#ff0000");
                    if (($('#' + masoftables[i] + ' tr th:nth-child(' + ((f + 1).toString()) + ')').width()) != ($('#' + cthead + ' tr th:nth-child(' + ((f + 1).toString()) + ')').width()))
                    { $("#" + cthead + " tr th:nth-child(" + ((f + 1).toString()) + ")").css('width', (($('#' + masoftables[i] + ' tr th:nth-child(' + ((f + 1).toString()) + ')').width() + 21).toString()) + "px"); }
                    //$('#cdf').text("ws= "+window.screen.availWidth.toString() + "\nwh= " + windowheighter.toString());

                });
                // if (i == 0) break;
            }
            //document.body.style.backgroundColor = "#80ff00";
            //  windowheighter = window.screen.availWidth;
            $('#cdf').text(($('#' + masoftables[0] + ' tr th:nth-child(' + ((2).toString()) + ')').width().toString()));



        }

        var windowheighter;
        function mtb() {
            window.setTimeout("mtb()", 50);

            // if (window.screen.availWidth != windowheighter) rth();
            rth();

            //$('#cdf').text("WW " + window.screen.availWidth + "\n\n"+"SW " + screen.width);
            //$('#cdf').text(((document.getElementById("diva_0").getBoundingClientRect().right - document.getElementById("diva_0").getBoundingClientRect().left).toString()));
            for (i = 0; i < counter; i++) {
                ownerdiv = "diva"; cthead = "ControlHead";
                ownerdiv += '_' + i.toString();
                cthead += '_' + i.toString();

                if ((document.getElementById(ownerdiv).getBoundingClientRect().top) < 90) {


                    if ((document.getElementById(ownerdiv).getBoundingClientRect().bottom) < 140) {
                        $('#' + cthead).css('position', 'absolute');
                    }
                    else {
                        $('#' + cthead).css('position', 'fixed');
                        $('#' + cthead).css('width', (((document.getElementById(ownerdiv).getBoundingClientRect().right - document.getElementById(ownerdiv).getBoundingClientRect().left).toString()) + "px"));
                        $('#' + cthead).css('top', '88px');
                        $('#' + cthead).css('left', ((document.getElementById(ownerdiv).getBoundingClientRect().left + 1).toString()) + "px");
                    }
                }
                else {
                    $('#' + cthead).css('position', 'absolute');
                    $('#' + cthead).css('width', (((document.getElementById(ownerdiv).getBoundingClientRect().right - document.getElementById(ownerdiv).getBoundingClientRect().left).toString()) + "px"));
                    $('#' + cthead).css('left', ((0).toString()) + "px");
                    $('#' + cthead).css('top', '0');
                }


            }
        }

       </script>
</asp:Content>