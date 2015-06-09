<%@ Page Language="C#" Title="Заполение" MasterPageFile="~/Site.Master" AutoEventWireup="true" EnableViewStateMac="false" CodeBehind="FillingTheReport.aspx.cs" Inherits="KPIWeb.Reports.FillingTheReport" %>

<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent" >

     <link rel="stylesheet" type="text/css" href="../Spinner.css">     

    <script type="text/javascript">
        function showLoadPanel() {
            document.getElementById('LoadPanel_').style.visibility = 'visible';
        }
    </script>
    <style>  
        .Grid_view_V_header_style
        {
           -webkit-transform: rotate(-90deg);
-moz-transform: rotate(-90deg);
-ms-transform: rotate(-90deg);
-o-transform: rotate(-90deg);
transform: rotate(-90deg)
        }
   
    .Grid_view_V_style tr td + td + td+ td {
      
        font-size: small;
        font-weight: normal;
        padding-left:1px;
        padding-right:1px;
        padding-top:1px;
        padding-bottom:1px;
    }


    .GridHeader th
    {
        height:75px;
    }

    .NameMin
    {
        min-width:3000px;
    }

        .LoadPanel 
   {
          position: fixed;
          z-index: 10;
          background-color: #101010;
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
          <div style="  
   
    position: fixed; left: 38%; top: 60%;text-align:center;
    "><font style=" 
    color:#7fff00;
    font-size:20px;
    font-style:normal;
    font-weight:900;
    text-shadow: 1px 1px 1px black, 0 0 1em #00ffff;
    ">Происходит обработка данных</font><br/>
      <font style=" 
    color:#ff0000;
    font-size:20px;
    font-style:normal;
    font-weight:900;
     text-shadow: 1px 1px 1px black, 0 0 1em #ffffff;"
          >Дождитесь завершения процесса</font></div>
        </div>
     
           <link href="/App_Themes/theme_1/css/login.css" rel="stylesheet" type="text/css" />
    <asp:Panel runat="server" ID="top_panel2" CssClass="top_panel" Visible="true">
        
     <span id="span1"><asp:Label ID="Label2" runat="server"  CssClass="Panel_label1"></asp:Label></span>
     <span id="span2"><asp:Label ID="Label3" runat="server" CssClass="Panel_label2"></asp:Label></span>
     
         </asp:Panel>
        <div class="relative_position">
            <h2>
                <asp:Label ID="Label1" runat="server" Text="Ввведите значения в таблицу показателей и нажмите кнопку &quot;Сохранить&quot; внизу формы"></asp:Label>
            </h2>

            <br />

            <asp:GridView ID="GridviewCollectedBasicParameters"  BorderStyle="Solid" runat="server" CssClass="Grid_view_V_style Grid_view_style GridHeader" ShowFooter="true" AutoGenerateColumns="False" 
                BorderColor="Black"  BorderWidth="1px" CellPadding="0" 

                OnRowDataBound="GridviewCollectedBasicParameters_RowDataBound" OnSelectedIndexChanged="GridviewCollectedBasicParameters_SelectedIndexChanged" OnSelectedIndexChanging="GridviewCollectedBasicParameters_SelectedIndexChanging" OnPageIndexChanging="GridviewCollectedBasicParameters_PageIndexChanging">
            
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
                        <ItemTemplate >
                            <asp:Label ID="NotNull0"  runat="server" Visible="false" Text='<%# Bind("NotNull0") %>'></asp:Label>
                            <asp:TextBox ID="Value0"  Width="95%" style="text-align:center;" BorderWidth="0" runat="server" Text='<%# Bind("Value0") %>'></asp:TextBox>
                            
                            <asp:RangeValidator runat="server" ID="Validate0" ControlToValidate="Value0" 
                            ErrorMessage="Ошибка" ForeColor="Red" Display="Dynamic" MaximumValue="100000" MinimumValue="0"                              
                            SetFocusOnError="True" Text="error">
                        </asp:RangeValidator>
                            <asp:Checkbox  Text=" " ID="Checked0" style="text-align:center" runat="server" Visible="false"></asp:CheckBox>
                            <asp:Label ID="CollectId0" runat="server"  Visible="false" Text='<%# Bind("CollectId0") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>  

                    <asp:TemplateField Visible="false"  HeaderText="Значение">

                        <ItemTemplate>
                            <asp:Label ID="NotNull1"  runat="server" Visible="false" Text='<%# Bind("NotNull1") %>'></asp:Label>
                            <asp:TextBox ID="Value1" Width="95%" style="text-align:center" BorderWidth="0" runat="server" Text='<%# Bind("Value1") %>'></asp:TextBox>
                            
                            <asp:RangeValidator runat="server" ID="Validate1" ControlToValidate="Value1" 
                            ErrorMessage="Ошибка" ForeColor="Red" Display="Dynamic" MaximumValue="100000" MinimumValue="0"
                            SetFocusOnError="True">
                        </asp:RangeValidator>
                            <asp:Checkbox  Text=" " ID="Checked1" style="text-align:center" runat="server" Visible="false"></asp:CheckBox>
                            <asp:Label ID="CollectId1" runat="server" Visible="false" Text='<%# Bind("CollectId1") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>               
                     <asp:TemplateField Visible="false" HeaderText="Значение">
                        <ItemTemplate>
                            <asp:Label ID="NotNull2"  runat="server" Visible="false" Text='<%# Bind("NotNull2") %>'></asp:Label>
                            <asp:TextBox ID="Value2" Width="95%" style="text-align:center" BorderWidth="0" runat="server" Text='<%# Bind("Value2") %>'></asp:TextBox>
                            
                             <asp:RangeValidator runat="server" ID="Validate2" ControlToValidate="Value2" 
                            ErrorMessage="Ошибка" ForeColor="Red" Display="Dynamic" MaximumValue="100000" MinimumValue="0"
                            SetFocusOnError="True">
                        </asp:RangeValidator>
                            <asp:Checkbox  Text=" " ID="Checked2" style="text-align:center" runat="server" Visible="false"></asp:CheckBox>
                            <asp:Label ID="CollectId2" runat="server" Visible="false" Text='<%# Bind("CollectId2") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField Visible="false" HeaderText="Значение">
                        <ItemTemplate>
                            <asp:Label ID="NotNull3"  runat="server" Visible="false" Text='<%# Bind("NotNull3") %>'></asp:Label>
                            <asp:TextBox ID="Value3" Width="95%" style="text-align:center" BorderWidth="0" runat="server" Text='<%# Bind("Value3") %>'></asp:TextBox>
                           
                             <asp:RangeValidator runat="server" ID="Validate3" ControlToValidate="Value3" 
                            ErrorMessage="Ошибка" ForeColor="Red" Display="Dynamic" MaximumValue="100000" MinimumValue="0"
                            SetFocusOnError="True">
                        </asp:RangeValidator>
                             <asp:Checkbox  Text=" " ID="Checked3" style="text-align:center" runat="server" Visible="false"></asp:CheckBox>
                            <asp:Label ID="CollectId3" runat="server" Visible="false" Text='<%# Bind("CollectId3") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>     
                     <asp:TemplateField Visible="false" HeaderText="Значение">
                        <ItemTemplate>
                            <asp:Label ID="NotNull4"  runat="server" Visible="false" Text='<%# Bind("NotNull4") %>'></asp:Label>
                            <asp:TextBox ID="Value4" Width="95%" style="text-align:center" BorderWidth="0" runat="server" Text='<%# Bind("Value4") %>'></asp:TextBox>
                            
                             <asp:RangeValidator runat="server" ID="Validate4" ControlToValidate="Value4" 
                            ErrorMessage="Ошибка" ForeColor="Red" Display="Dynamic" MaximumValue="100000" MinimumValue="0"
                            SetFocusOnError="True">
                        </asp:RangeValidator>
                            <asp:Checkbox  Text=" " ID="Checked4" style="text-align:center" runat="server" Visible="false"></asp:CheckBox>
                            <asp:Label ID="CollectId4" runat="server" Visible="false" Text='<%# Bind("CollectId4") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                     <asp:TemplateField Visible="false" HeaderText="Значение">
                        <ItemTemplate>
                            <asp:Label ID="NotNull5"  runat="server" Visible="false" Text='<%# Bind("NotNull5") %>'></asp:Label>
                            <asp:TextBox ID="Value5" Width="95%" style="text-align:center" BorderWidth="0" runat="server" Text='<%# Bind("Value5") %>'></asp:TextBox>
                            
                             <asp:RangeValidator runat="server" ID="Validate5" ControlToValidate="Value5" 
                            ErrorMessage="Ошибка" ForeColor="Red" Display="Dynamic" MaximumValue="100000" MinimumValue="0"
                            SetFocusOnError="True">
                        </asp:RangeValidator>
                            <asp:Checkbox  Text=" " ID="Checked5" style="text-align:center" runat="server" Visible="false"></asp:CheckBox>
                            <asp:Label ID="CollectId5" runat="server" Visible="false" Text='<%# Bind("CollectId5") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>               
                     <asp:TemplateField Visible="false" HeaderText="Значение">
                        <ItemTemplate>
                            <asp:Label ID="NotNull6"  runat="server" Visible="false" Text='<%# Bind("NotNull6") %>'></asp:Label>
                            <asp:TextBox ID="Value6" Width="95%" style="text-align:center" BorderWidth="0" runat="server" Text='<%# Bind("Value6") %>'></asp:TextBox>
                            
                             <asp:RangeValidator runat="server" ID="Validate6" ControlToValidate="Value6" 
                            ErrorMessage="Ошибка" ForeColor="Red" Display="Dynamic" MaximumValue="100000" MinimumValue="0"
                            SetFocusOnError="True">
                        </asp:RangeValidator>
                            <asp:Checkbox  Text=" " ID="Checked6" style="text-align:center" runat="server" Visible="false"></asp:CheckBox>
                            <asp:Label ID="CollectId6" runat="server" Visible="false" Text='<%# Bind("CollectId6") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField  Visible="false" HeaderText="Значение">
                        <ItemTemplate>
                            <asp:Label ID="NotNull7"  runat="server" Visible="false" Text='<%# Bind("NotNull7") %>'></asp:Label>
                            <asp:TextBox ID="Value7" Width="95%" style="text-align:center" BorderWidth="0" runat="server" Text='<%# Bind("Value7") %>'></asp:TextBox>
                            
                             <asp:RangeValidator runat="server" ID="Validate7" ControlToValidate="Value7" 
                            ErrorMessage="Ошибка" ForeColor="Red" Display="Dynamic" MaximumValue="100000" MinimumValue="0"
                            SetFocusOnError="True">
                        </asp:RangeValidator>
                            <asp:Checkbox  Text=" " ID="Checked7" style="text-align:center" runat="server" Visible="false"></asp:CheckBox>
                            <asp:Label ID="CollectId7" runat="server" Visible="false" Text='<%# Bind("CollectId7") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>     
                     <asp:TemplateField Visible="false" HeaderText="Значение">
                        <ItemTemplate>
                            <asp:Label ID="NotNull8"  runat="server" Visible="false" Text='<%# Bind("NotNull8") %>'></asp:Label>
                            <asp:TextBox ID="Value8" Width="95%" style="text-align:center" BorderWidth="0" runat="server" Text='<%# Bind("Value8") %>'></asp:TextBox>
                            
                             <asp:RangeValidator runat="server" ID="Validate8" ControlToValidate="Value8" 
                            ErrorMessage="Ошибка" ForeColor="Red" Display="Dynamic" MaximumValue="100000" MinimumValue="0"
                            SetFocusOnError="True">
                        </asp:RangeValidator>
                            <asp:Checkbox  Text=" " ID="Checked8" style="text-align:center" runat="server" Visible="false"></asp:CheckBox>
                            <asp:Label ID="CollectId8" runat="server" Visible="false" Text='<%# Bind("CollectId8") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    
                    <asp:TemplateField Visible="false" HeaderText="Значение">
                        <ItemTemplate>
                            <asp:Label ID="NotNull9"  runat="server" Visible="false" Text='<%# Bind("NotNull9") %>'></asp:Label>
                            <asp:TextBox ID="Value9" Width="95%" style="text-align:center" BorderWidth="0" runat="server" Text='<%# Bind("Value9") %>'></asp:TextBox>
                           
                            <asp:RangeValidator runat="server" ID="Validate9" ControlToValidate="Value9" 
                            ErrorMessage="Ошибка" ForeColor="Red" Display="Dynamic" MaximumValue="100000" MinimumValue="0"
                            SetFocusOnError="True">
                        </asp:RangeValidator>
                             <asp:Checkbox  Text=" " ID="Checked9" style="text-align:center" runat="server" Visible="false"></asp:CheckBox>
                            <asp:Label ID="CollectId9" runat="server" Visible="false" Text='<%# Bind("CollectId9") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>  
                    <asp:TemplateField Visible="false" HeaderText="Значение">
                        <ItemTemplate>
                            <asp:Label ID="NotNull10"   runat="server" Visible="false" Text='<%# Bind("NotNull10") %>'></asp:Label>
                            <asp:TextBox ID="Value10" Width="95%" style="text-align:center" BorderWidth="0" runat="server" Text='<%# Bind("Value10") %>'></asp:TextBox>
                          
                            <asp:RangeValidator runat="server" ID="Validate10" ControlToValidate="Value10" 
                            ErrorMessage="Ошибка" ForeColor="Red" Display="Dynamic" MaximumValue="100000" MinimumValue="0"

                            SetFocusOnError="True">
                        </asp:RangeValidator>
                              <asp:Checkbox  Text=" " ID="Checked10" style="text-align:center" runat="server" Visible="false"></asp:CheckBox>
                            <asp:Label ID="CollectId10" runat="server" Visible="false" Text='<%# Bind("CollectId10") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>     

                     <asp:TemplateField Visible="false"   HeaderText="Значение">
                        <ItemTemplate >
                            <asp:Label ID="NotNull11"  runat="server" Visible="false" Text='<%# Bind("NotNull11") %>'></asp:Label>
                            <asp:TextBox ID="Value11" Width="95%" style="text-align:center" BorderWidth="0" runat="server" Text='<%# Bind("Value11") %>'></asp:TextBox>
                           
                             <asp:RangeValidator runat="server" ID="Validate11" ControlToValidate="Value11" 
                            ErrorMessage="Ошибка" ForeColor="Red" Display="Dynamic" MaximumValue="100000" MinimumValue="0"                              
                            SetFocusOnError="True" Text="error">
                        </asp:RangeValidator>
                             <asp:Checkbox  Text=" " ID="Checked11" style="text-align:center" runat="server" Visible="false"></asp:CheckBox>
                            <asp:Label ID="CollectId11" runat="server" Visible="false" Text='<%# Bind("CollectId11") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>  

                    <asp:TemplateField Visible="false"   HeaderText="Значение">
                        <ItemTemplate >
                            <asp:Label ID="NotNull12"  runat="server" Visible="false" Text='<%# Bind("NotNull12") %>'></asp:Label>
                            <asp:TextBox ID="Value12" Width="95%" style="text-align:center" BorderWidth="0" runat="server" Text='<%# Bind("Value12") %>'></asp:TextBox>
                            
                             <asp:RangeValidator runat="server" ID="Validate12" ControlToValidate="Value12" 
                            ErrorMessage="Ошибка" ForeColor="Red" Display="Dynamic" MaximumValue="100000" MinimumValue="0"                              
                            SetFocusOnError="True" Text="error">
                        </asp:RangeValidator>
                            <asp:Checkbox  Text=" " ID="Checked12" style="text-align:center" runat="server" Visible="false"></asp:CheckBox>
                            <asp:Label ID="CollectId12" runat="server" Visible="false" Text='<%# Bind("CollectId12") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>  
                    <asp:TemplateField Visible="false"   HeaderText="Значение">
                        <ItemTemplate >
                            <asp:Label ID="NotNull13"  runat="server" Visible="false" Text='<%# Bind("NotNull13") %>'></asp:Label>
                            <asp:TextBox ID="Value13" Width="95%" style="text-align:center" BorderWidth="0" runat="server" Text='<%# Bind("Value13") %>'></asp:TextBox>
                            
                             <asp:RangeValidator runat="server" ID="Validate13" ControlToValidate="Value13" 
                            ErrorMessage="Ошибка" ForeColor="Red" Display="Dynamic" MaximumValue="100000" MinimumValue="0"                              
                            SetFocusOnError="True" Text="error">
                        </asp:RangeValidator>
                            <asp:Checkbox  Text=" " ID="Checked13" style="text-align:center" runat="server" Visible="false"></asp:CheckBox>
                            <asp:Label ID="CollectId13" runat="server" Visible="false" Text='<%# Bind("CollectId13") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>  
                    <asp:TemplateField Visible="false"   HeaderText="Значение">
                        <ItemTemplate >
                            <asp:Label ID="NotNull14"  runat="server" Visible="false" Text='<%# Bind("NotNull14") %>'></asp:Label>
                            <asp:TextBox ID="Value14" Width="95%" style="text-align:center" BorderWidth="0" runat="server" Text='<%# Bind("Value14") %>'></asp:TextBox>
                            
                             <asp:RangeValidator runat="server" ID="Validate14" ControlToValidate="Value14" 
                            ErrorMessage="Ошибка" ForeColor="Red" Display="Dynamic" MaximumValue="100000" MinimumValue="0"                              
                            SetFocusOnError="True" Text="error">
                        </asp:RangeValidator>
                            <asp:Checkbox  Text=" " ID="Checked14" style="text-align:center" runat="server" Visible="false"></asp:CheckBox>
                            <asp:Label ID="CollectId14" runat="server" Visible="false" Text='<%# Bind("CollectId14") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField> 
                    <asp:TemplateField Visible="false"   HeaderText="Значение">
                        <ItemTemplate >
                            <asp:Label ID="NotNull15"  runat="server" Visible="false" Text='<%# Bind("NotNull15") %>'></asp:Label>
                            <asp:TextBox ID="Value15" Width="95%" style="text-align:center" BorderWidth="0" runat="server" Text='<%# Bind("Value15") %>'></asp:TextBox>
                           
                             <asp:RangeValidator runat="server" ID="Validate15" ControlToValidate="Value15" 
                            ErrorMessage="Ошибка" ForeColor="Red" Display="Dynamic" MaximumValue="100000" MinimumValue="0"                              
                            SetFocusOnError="True" Text="error">
                        </asp:RangeValidator>
                             <asp:Checkbox  Text=" " ID="Checked15" style="text-align:center" runat="server" Visible="false"></asp:CheckBox>
                            <asp:Label ID="CollectId15" runat="server" Visible="false" Text='<%# Bind("CollectId15") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField> 
                    <asp:TemplateField Visible="false"   HeaderText="Значение">
                        <ItemTemplate >
                            <asp:Label ID="NotNull16"  runat="server" Visible="false" Text='<%# Bind("NotNull16") %>'></asp:Label>
                            <asp:TextBox ID="Value16" Width="95%" style="text-align:center" BorderWidth="0" runat="server" Text='<%# Bind("Value16") %>'></asp:TextBox>
                            
                             <asp:RangeValidator runat="server" ID="Validate16" ControlToValidate="Value16" 
                            ErrorMessage="Ошибка" ForeColor="Red" Display="Dynamic" MaximumValue="100000" MinimumValue="0"                              
                            SetFocusOnError="True" Text="error">
                        </asp:RangeValidator>
                            <asp:Checkbox  Text=" " ID="Checked16" style="text-align:center" runat="server" Visible="false"></asp:CheckBox>
                            <asp:Label ID="CollectId16" runat="server" Visible="false" Text='<%# Bind("CollectId16") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField> 
                    <asp:TemplateField Visible="false"   HeaderText="Значение">
                        <ItemTemplate >
                            <asp:Label ID="NotNull17"  runat="server" Visible="false" Text='<%# Bind("NotNull17") %>'></asp:Label>
                            <asp:TextBox ID="Value17" Width="95%" style="text-align:center" BorderWidth="0" runat="server" Text='<%# Bind("Value17") %>'></asp:TextBox>
                          
                             <asp:RangeValidator runat="server" ID="Validate17" ControlToValidate="Value17" 
                            ErrorMessage="Ошибка" ForeColor="Red" Display="Dynamic" MaximumValue="100000" MinimumValue="0"                              
                            SetFocusOnError="True" Text="error">
                        </asp:RangeValidator>
                              <asp:Checkbox  Text=" " ID="Checked17" style="text-align:center" runat="server" Visible="false"></asp:CheckBox>
                            <asp:Label ID="CollectId17" runat="server" Visible="false" Text='<%# Bind("CollectId17") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField> 
                    <asp:TemplateField Visible="false"   HeaderText="Значение">
                        <ItemTemplate >
                            <asp:Label ID="NotNull18"  runat="server" Visible="false" Text='<%# Bind("NotNull18") %>'></asp:Label>
                            <asp:TextBox ID="Value18" Width="95%" style="text-align:center" BorderWidth="0" runat="server" Text='<%# Bind("Value18") %>'></asp:TextBox>
                            
                             <asp:RangeValidator runat="server" ID="Validate18" ControlToValidate="Value18" 
                            ErrorMessage="Ошибка" ForeColor="Red" Display="Dynamic" MaximumValue="100000" MinimumValue="0"                              
                            SetFocusOnError="True" Text="error">
                        </asp:RangeValidator>
                            <asp:Checkbox  Text=" " ID="Checked18" style="text-align:center" runat="server" Visible="false"></asp:CheckBox>
                            <asp:Label ID="CollectId18" runat="server" Visible="false" Text='<%# Bind("CollectId18") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField> 
                    <asp:TemplateField Visible="false"   HeaderText="Значение">
                        <ItemTemplate >
                            <asp:Label ID="NotNull19"   runat="server" Visible="false" Text='<%# Bind("NotNull19") %>'></asp:Label>
                            <asp:TextBox ID="Value19" Width="95%" style="text-align:center" BorderWidth="0" runat="server" Text='<%# Bind("Value19") %>'></asp:TextBox>
                            
                             <asp:RangeValidator runat="server" ID="Validate19" ControlToValidate="Value19" 
                            ErrorMessage="Ошибка" ForeColor="Red" Display="Dynamic" MaximumValue="100000" MinimumValue="0"                              
                            SetFocusOnError="True" Text="error">
                        </asp:RangeValidator>
                            <asp:Checkbox  Text=" " ID="Checked19" style="text-align:center" runat="server" Visible="false"></asp:CheckBox>
                            <asp:Label ID="CollectId19" runat="server" Visible="false" Text='<%# Bind("CollectId19") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField> 
                    <asp:TemplateField Visible="false"   HeaderText="Значение">
                        <ItemTemplate >
                            <asp:Label ID="NotNull20"  runat="server" Visible="false" Text='<%# Bind("NotNull20") %>'></asp:Label>
                            <asp:TextBox ID="Value20" Width="95%" style="text-align:center" BorderWidth="0" runat="server" Text='<%# Bind("Value20") %>'></asp:TextBox>
                            
                             <asp:RangeValidator runat="server" ID="Validate20" ControlToValidate="Value20" 
                            ErrorMessage="Ошибка" ForeColor="Red" Display="Dynamic" MaximumValue="100000" MinimumValue="0"                              
                            SetFocusOnError="True" Text="error">
                        </asp:RangeValidator>
                            <asp:Checkbox  Text=" " ID="Checked20" style="text-align:center" runat="server" Visible="false"></asp:CheckBox>
                            <asp:Label ID="CollectId20" runat="server" Visible="false" Text='<%# Bind("CollectId20") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField> 
                    <asp:TemplateField Visible="false"   HeaderText="Значение">
                        <ItemTemplate >
                            <asp:Label ID="NotNull21"  runat="server" Visible="false" Text='<%# Bind("NotNull21") %>'></asp:Label>
                            <asp:TextBox ID="Value21" Width="95%" style="text-align:center" BorderWidth="0" runat="server" Text='<%# Bind("Value21") %>'></asp:TextBox>
                           
                             <asp:RangeValidator runat="server" ID="Validate21" ControlToValidate="Value21" 
                            ErrorMessage="Ошибка" ForeColor="Red" Display="Dynamic" MaximumValue="100000" MinimumValue="0"                              
                            SetFocusOnError="True" Text="error">
                        </asp:RangeValidator>
                             <asp:Checkbox  Text=" " ID="Checked21" style="text-align:center" runat="server" Visible="false"></asp:CheckBox>
                            <asp:Label ID="CollectId21" runat="server" Visible="false" Text='<%# Bind("CollectId21") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField> 
                    <asp:TemplateField Visible="false"   HeaderText="Значение">
                        <ItemTemplate >
                            <asp:Label ID="NotNull22"  runat="server" Visible="false" Text='<%# Bind("NotNull22") %>'></asp:Label>
                            <asp:TextBox ID="Value22" Width="95%" style="text-align:center" BorderWidth="0" runat="server" Text='<%# Bind("Value22") %>'></asp:TextBox>
                           
                             <asp:RangeValidator runat="server" ID="Validate22" ControlToValidate="Value22" 
                            ErrorMessage="Ошибка" ForeColor="Red" Display="Dynamic" MaximumValue="100000" MinimumValue="0"                              
                            SetFocusOnError="True" Text="error">
                        </asp:RangeValidator>
                             <asp:Checkbox  Text=" " ID="Checked22" style="text-align:center" runat="server" Visible="false"></asp:CheckBox>
                            <asp:Label ID="CollectId22" runat="server" Visible="false" Text='<%# Bind("CollectId22") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField> 
                    <asp:TemplateField Visible="false"   HeaderText="Значение">
                        <ItemTemplate >
                            <asp:Label ID="NotNull23"  runat="server" Visible="false" Text='<%# Bind("NotNull23") %>'></asp:Label>
                            <asp:TextBox ID="Value23" Width="95%" style="text-align:center" BorderWidth="0" runat="server" Text='<%# Bind("Value23") %>'></asp:TextBox>
                            
                             <asp:RangeValidator runat="server" ID="Validate23" ControlToValidate="Value23" 
                            ErrorMessage="Ошибка" ForeColor="Red" Display="Dynamic" MaximumValue="100000" MinimumValue="0"                              
                            SetFocusOnError="True" Text="error">
                        </asp:RangeValidator>
                            <asp:Checkbox  Text=" " ID="Checked23" style="text-align:center" runat="server" Visible="false"></asp:CheckBox>
                            <asp:Label ID="CollectId23" runat="server" Visible="false" Text='<%# Bind("CollectId23") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField> 
                    <asp:TemplateField Visible="false"   HeaderText="Значение">
                        <ItemTemplate >
                            <asp:Label ID="NotNull24"  runat="server" Visible="false" Text='<%# Bind("NotNull24") %>'></asp:Label>
                            <asp:TextBox ID="Value24" Width="95%" style="text-align:center" BorderWidth="0" runat="server" Text='<%# Bind("Value24") %>'></asp:TextBox>
                           
                             <asp:RangeValidator runat="server" ID="Validate24" ControlToValidate="Value24" 
                            ErrorMessage="Ошибка" ForeColor="Red" Display="Dynamic" MaximumValue="100000" MinimumValue="0"                              
                            SetFocusOnError="True" Text="error">
                        </asp:RangeValidator>
                             <asp:Checkbox  Text=" " ID="Checked24" style="text-align:center" runat="server" Visible="false"></asp:CheckBox>
                            <asp:Label ID="CollectId24" runat="server" Visible="false" Text='<%# Bind("CollectId24") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField> 

                    <asp:TemplateField Visible="false"   HeaderText="Значение">
                        <ItemTemplate >
                            <asp:Label ID="NotNull25"  runat="server" Visible="false" Text='<%# Bind("NotNull25") %>'></asp:Label>
                            <asp:TextBox ID="Value25" Width="95%" style="text-align:center" BorderWidth="0" runat="server" Text='<%# Bind("Value25") %>'></asp:TextBox>                            
                             <asp:RangeValidator runat="server" ID="Validate25" ControlToValidate="Value25" 
                            ErrorMessage="Ошибка" ForeColor="Red" Display="Dynamic" MaximumValue="100000" MinimumValue="0"                              
                            SetFocusOnError="True" Text="error">
                        </asp:RangeValidator>
                            <asp:Checkbox  Text=" " ID="Checked25" style="text-align:center" runat="server" Visible="false"></asp:CheckBox>
                            <asp:Label ID="CollectId25" runat="server" Visible="false" Text='<%# Bind("CollectId25") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>          
                    
                    <asp:TemplateField Visible="false"   HeaderText="Значение">
                        <ItemTemplate >
                            <asp:Label ID="NotNull26"  runat="server" Visible="false" Text='<%# Bind("NotNull26") %>'></asp:Label>
                            <asp:TextBox ID="Value26" Width="95%" style="text-align:center" BorderWidth="0" runat="server" Text='<%# Bind("Value26") %>'></asp:TextBox>                            
                             <asp:RangeValidator runat="server" ID="Validate26" ControlToValidate="Value26" 
                            ErrorMessage="Ошибка" ForeColor="Red" Display="Dynamic" MaximumValue="100000" MinimumValue="0"                              
                            SetFocusOnError="True" Text="error">
                        </asp:RangeValidator>
                            <asp:Checkbox  Text=" " ID="Checked26" style="text-align:center" runat="server" Visible="false"></asp:CheckBox>
                            <asp:Label ID="CollectId26" runat="server" Visible="false" Text='<%# Bind("CollectId26") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>            
                    <asp:TemplateField Visible="false"   HeaderText="Значение">
                        <ItemTemplate >
                            <asp:Label ID="NotNull27"  runat="server" Visible="false" Text='<%# Bind("NotNull27") %>'></asp:Label>
                            <asp:TextBox ID="Value27" Width="95%" style="text-align:center" BorderWidth="0" runat="server" Text='<%# Bind("Value27") %>'></asp:TextBox>                            
                             <asp:RangeValidator runat="server" ID="Validate27" ControlToValidate="Value27" 
                            ErrorMessage="Ошибка" ForeColor="Red" Display="Dynamic" MaximumValue="100000" MinimumValue="0"                              
                            SetFocusOnError="True" Text="error">
                        </asp:RangeValidator>
                            <asp:Checkbox  Text=" " ID="Checked27" style="text-align:center" runat="server" Visible="false"></asp:CheckBox>
                            <asp:Label ID="CollectId27" runat="server" Visible="false" Text='<%# Bind("CollectId27") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField> 
                    <asp:TemplateField Visible="false"   HeaderText="Значение">
                        <ItemTemplate >
                            <asp:Label ID="NotNull28"  runat="server" Visible="false" Text='<%# Bind("NotNull28") %>'></asp:Label>
                            <asp:TextBox ID="Value28" Width="95%" style="text-align:center" BorderWidth="0" runat="server" Text='<%# Bind("Value28") %>'></asp:TextBox>                            
                             <asp:RangeValidator runat="server" ID="Validate28" ControlToValidate="Value28" 
                            ErrorMessage="Ошибка" ForeColor="Red" Display="Dynamic" MaximumValue="100000" MinimumValue="0"                              
                            SetFocusOnError="True" Text="error">
                        </asp:RangeValidator>
                            <asp:Checkbox  Text=" " ID="Checked28" style="text-align:center" runat="server" Visible="false"></asp:CheckBox>
                            <asp:Label ID="CollectId28" runat="server" Visible="false" Text='<%# Bind("CollectId28") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField> 
                    <asp:TemplateField Visible="false"   HeaderText="Значение">
                        <ItemTemplate >
                            <asp:Label ID="NotNull29"  runat="server" Visible="false" Text='<%# Bind("NotNull29") %>'></asp:Label>
                            <asp:TextBox ID="Value29" Width="95%" style="text-align:center" BorderWidth="0" runat="server" Text='<%# Bind("Value29") %>'></asp:TextBox>                            
                             <asp:RangeValidator runat="server" ID="Validate29" ControlToValidate="Value29" 
                            ErrorMessage="Ошибка" ForeColor="Red" Display="Dynamic" MaximumValue="100000" MinimumValue="0"                              
                            SetFocusOnError="True" Text="error">
                        </asp:RangeValidator>
                            <asp:Checkbox  Text=" " ID="Checked29" style="text-align:center" runat="server" Visible="false"></asp:CheckBox>
                            <asp:Label ID="CollectId29" runat="server" Visible="false" Text='<%# Bind("CollectId29") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField> 
                    <asp:TemplateField Visible="false"   HeaderText="Значение">
                        <ItemTemplate >
                            <asp:Label ID="NotNull30"  runat="server" Visible="false" Text='<%# Bind("NotNull30") %>'></asp:Label>
                            <asp:TextBox ID="Value30" Width="95%" style="text-align:center" BorderWidth="0" runat="server" Text='<%# Bind("Value30") %>'></asp:TextBox>                            
                             <asp:RangeValidator runat="server" ID="Validate30" ControlToValidate="Value30" 
                            ErrorMessage="Ошибка" ForeColor="Red" Display="Dynamic" MaximumValue="100000" MinimumValue="0"                              
                            SetFocusOnError="True" Text="error">
                        </asp:RangeValidator>
                            <asp:Checkbox  Text=" " ID="Checked30" style="text-align:center" runat="server" Visible="false"></asp:CheckBox>
                            <asp:Label ID="CollectId30" runat="server" Visible="false" Text='<%# Bind("CollectId30") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField> 
                    <asp:TemplateField Visible="false"   HeaderText="Значение">
                        <ItemTemplate >
                            <asp:Label ID="NotNull31"  runat="server" Visible="false" Text='<%# Bind("NotNull31") %>'></asp:Label>
                            <asp:TextBox ID="Value31" Width="95%" style="text-align:center" BorderWidth="0" runat="server" Text='<%# Bind("Value31") %>'></asp:TextBox>                            
                             <asp:RangeValidator runat="server" ID="Validate31" ControlToValidate="Value31" 
                            ErrorMessage="Ошибка" ForeColor="Red" Display="Dynamic" MaximumValue="100000" MinimumValue="0"                              
                            SetFocusOnError="True" Text="error">
                        </asp:RangeValidator>
                            <asp:Checkbox  Text=" " ID="Checked31" style="text-align:center" runat="server" Visible="false"></asp:CheckBox>
                            <asp:Label ID="CollectId31" runat="server" Visible="false" Text='<%# Bind("CollectId31") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField> 
                                        <asp:TemplateField Visible="false"   HeaderText="Значение">
                        <ItemTemplate >
                            <asp:Label ID="NotNull32"  runat="server" Visible="false" Text='<%# Bind("NotNull32") %>'></asp:Label>
                            <asp:TextBox ID="Value32" Width="95%" style="text-align:center" BorderWidth="0" runat="server" Text='<%# Bind("Value32") %>'></asp:TextBox>                            
                             <asp:RangeValidator runat="server" ID="Validate32" ControlToValidate="Value32" 
                            ErrorMessage="Ошибка" ForeColor="Red" Display="Dynamic" MaximumValue="100000" MinimumValue="0"                              
                            SetFocusOnError="True" Text="error">
                        </asp:RangeValidator>
                            <asp:Checkbox  Text=" " ID="Checked32" style="text-align:center" runat="server" Visible="false"></asp:CheckBox>
                            <asp:Label ID="CollectId32" runat="server" Visible="false" Text='<%# Bind("CollectId32") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField> 
                                        <asp:TemplateField Visible="false"   HeaderText="Значение">
                        <ItemTemplate >
                            <asp:Label ID="NotNull33"  runat="server" Visible="false" Text='<%# Bind("NotNull33") %>'></asp:Label>
                            <asp:TextBox ID="Value33" Width="95%" style="text-align:center" BorderWidth="0" runat="server" Text='<%# Bind("Value33") %>'></asp:TextBox>                            
                             <asp:RangeValidator runat="server" ID="Validate33" ControlToValidate="Value33" 
                            ErrorMessage="Ошибка" ForeColor="Red" Display="Dynamic" MaximumValue="100000" MinimumValue="0"                              
                            SetFocusOnError="True" Text="error">
                        </asp:RangeValidator>
                            <asp:Checkbox  Text=" " ID="Checked33" style="text-align:center" runat="server" Visible="false"></asp:CheckBox>
                            <asp:Label ID="CollectId33" runat="server" Visible="false" Text='<%# Bind("CollectId33") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField> 
                                        <asp:TemplateField Visible="false"   HeaderText="Значение">
                        <ItemTemplate >
                            <asp:Label ID="NotNull34"  runat="server" Visible="false" Text='<%# Bind("NotNull34") %>'></asp:Label>
                            <asp:TextBox ID="Value34" Width="95%" style="text-align:center" BorderWidth="0" runat="server" Text='<%# Bind("Value34") %>'></asp:TextBox>                            
                             <asp:RangeValidator runat="server" ID="Validate34" ControlToValidate="Value34" 
                            ErrorMessage="Ошибка" ForeColor="Red" Display="Dynamic" MaximumValue="100000" MinimumValue="0"                              
                            SetFocusOnError="True" Text="error">
                        </asp:RangeValidator>
                            <asp:Checkbox  Text=" " ID="Checked34" style="text-align:center" runat="server" Visible="false"></asp:CheckBox>
                            <asp:Label ID="CollectId34" runat="server" Visible="false" Text='<%# Bind("CollectId34") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField> 
                                        <asp:TemplateField Visible="false"   HeaderText="Значение">
                        <ItemTemplate >
                            <asp:Label ID="NotNull35"  runat="server" Visible="false" Text='<%# Bind("NotNull35") %>'></asp:Label>
                            <asp:TextBox ID="Value35" Width="95%" style="text-align:center" BorderWidth="0" runat="server" Text='<%# Bind("Value35") %>'></asp:TextBox>                            
                             <asp:RangeValidator runat="server" ID="Validate35" ControlToValidate="Value35" 
                            ErrorMessage="Ошибка" ForeColor="Red" Display="Dynamic" MaximumValue="100000" MinimumValue="0"                              
                            SetFocusOnError="True" Text="error">
                        </asp:RangeValidator>
                            <asp:Checkbox  Text=" " ID="Checked35" style="text-align:center" runat="server" Visible="false"></asp:CheckBox>
                            <asp:Label ID="CollectId35" runat="server" Visible="false" Text='<%# Bind("CollectId35") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField> 
                                        <asp:TemplateField Visible="false"   HeaderText="Значение">
                        <ItemTemplate >
                            <asp:Label ID="NotNull36"  runat="server" Visible="false" Text='<%# Bind("NotNull36") %>'></asp:Label>
                            <asp:TextBox ID="Value36" Width="95%" style="text-align:center" BorderWidth="0" runat="server" Text='<%# Bind("Value36") %>'></asp:TextBox>                            
                             <asp:RangeValidator runat="server" ID="Validate36" ControlToValidate="Value36" 
                            ErrorMessage="Ошибка" ForeColor="Red" Display="Dynamic" MaximumValue="100000" MinimumValue="0"                              
                            SetFocusOnError="True" Text="error">
                        </asp:RangeValidator>
                            <asp:Checkbox  Text=" " ID="Checked36" style="text-align:center" runat="server" Visible="false"></asp:CheckBox>
                            <asp:Label ID="CollectId36" runat="server" Visible="false" Text='<%# Bind("CollectId36") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField> 
                                        <asp:TemplateField Visible="false"   HeaderText="Значение">
                        <ItemTemplate >
                            <asp:Label ID="NotNull37"  runat="server" Visible="false" Text='<%# Bind("NotNull37") %>'></asp:Label>
                            <asp:TextBox ID="Value37" Width="95%" style="text-align:center" BorderWidth="0" runat="server" Text='<%# Bind("Value37") %>'></asp:TextBox>                            
                             <asp:RangeValidator runat="server" ID="Validate37" ControlToValidate="Value37" 
                            ErrorMessage="Ошибка" ForeColor="Red" Display="Dynamic" MaximumValue="100000" MinimumValue="0"                              
                            SetFocusOnError="True" Text="error">
                        </asp:RangeValidator>
                            <asp:Checkbox  Text=" " ID="Checked37" style="text-align:center" runat="server" Visible="false"></asp:CheckBox>
                            <asp:Label ID="CollectId37" runat="server" Visible="false" Text='<%# Bind("CollectId37") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField> 
                                        <asp:TemplateField Visible="false"   HeaderText="Значение">
                        <ItemTemplate >
                            <asp:Label ID="NotNull38"  runat="server" Visible="false" Text='<%# Bind("NotNull38") %>'></asp:Label>
                            <asp:TextBox ID="Value38" Width="95%" style="text-align:center" BorderWidth="0" runat="server" Text='<%# Bind("Value38") %>'></asp:TextBox>                            
                             <asp:RangeValidator runat="server" ID="Validate38" ControlToValidate="Value38" 
                            ErrorMessage="Ошибка" ForeColor="Red" Display="Dynamic" MaximumValue="100000" MinimumValue="0"                              
                            SetFocusOnError="True" Text="error">
                        </asp:RangeValidator>
                            <asp:Checkbox  Text=" " ID="Checked38" style="text-align:center" runat="server" Visible="false"></asp:CheckBox>
                            <asp:Label ID="CollectId38" runat="server" Visible="false" Text='<%# Bind("CollectId38") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField> 
                                        <asp:TemplateField Visible="false"   HeaderText="Значение">
                        <ItemTemplate >
                            <asp:Label ID="NotNull39"  runat="server" Visible="false" Text='<%# Bind("NotNull39") %>'></asp:Label>
                            <asp:TextBox ID="Value39" Width="95%" style="text-align:center" BorderWidth="0" runat="server" Text='<%# Bind("Value39") %>'></asp:TextBox>                            
                             <asp:RangeValidator runat="server" ID="Validate39" ControlToValidate="Value39" 
                            ErrorMessage="Ошибка" ForeColor="Red" Display="Dynamic" MaximumValue="100000" MinimumValue="0"                              
                            SetFocusOnError="True" Text="error">
                        </asp:RangeValidator>
                            <asp:Checkbox  Text=" " ID="Checked39" style="text-align:center" runat="server" Visible="false"></asp:CheckBox>
                            <asp:Label ID="CollectId39" runat="server" Visible="false" Text='<%# Bind("CollectId39") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField> 
                                        <asp:TemplateField Visible="false"   HeaderText="Значение">
                        <ItemTemplate >
                            <asp:Label ID="NotNull40"  runat="server" Visible="false" Text='<%# Bind("NotNull40") %>'></asp:Label>
                            <asp:TextBox ID="Value40" Width="95%" style="text-align:center" BorderWidth="0" runat="server" Text='<%# Bind("Value40") %>'></asp:TextBox>                            
                             <asp:RangeValidator runat="server" ID="Validate40" ControlToValidate="Value40" 
                            ErrorMessage="Ошибка" ForeColor="Red" Display="Dynamic" MaximumValue="100000" MinimumValue="0"                              
                            SetFocusOnError="True" Text="error">
                        </asp:RangeValidator>
                            <asp:Checkbox  Text=" " ID="Checked40" style="text-align:center" runat="server" Visible="false"></asp:CheckBox>
                            <asp:Label ID="CollectId40" runat="server" Visible="false" Text='<%# Bind("CollectId40") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField> 
                </Columns>
            </asp:GridView>
            <br />
            <asp:ValidationSummary runat="server" ID="Summary" DisplayMode="BulletList"
            HeaderText="Ошибка ввода данных. Операция невозможна. Для продолжения обновите страницу кнопкой 'F5' " ShowSummary="false" ShowMessageBox="true"/>

            <asp:Button ID="ButtonSave" Width="400px" runat="server" Text="Сохранить и выйти" OnClientClick="showLoadPanel()" OnClick="ButtonSave_Click" />

            <br />
            <br />
            <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" onText="Экспорт в PDF" Width="400px" OnClientClick="showLoadPanel()" Text="Экспорт в PDF" />

            <br />
            <br />
            <asp:Button ID="GoBackButton" runat="server" Height="30px" OnClick="Button2_Click" OnClientClick="showLoadPanel()" Text="Вернутся в меню без сохранения" Width="400px" />

            <br />
            <br />
            <asp:Button ID="UpnDownButton" runat="server" Height="30px" OnClientClick="showLoadPanel()" OnClick="Button3_Click" Text="Вернуть на доработку (+коментарий )" Width="400px" />
            <br />

            <br />

            <asp:TextBox ID="TextBox1" runat="server" Height="200px" TextMode="MultiLine" Width="400px"></asp:TextBox>

            <br />
               <br />
               <br />
               <br />   
            <br />
        </div>
        </asp:Content>
