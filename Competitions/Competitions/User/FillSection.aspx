<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="FillSection.aspx.cs" Inherits="Competitions.User.FillSection" %>
<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">    <div>

<asp:Panel runat="server" ID="top_panel2" CssClass="top_panel" Height="40" Visible="true">    
    <div>    
        <asp:Button ID="GoBackButton" runat="server" OnClientClick="showLoadPanel()" Text="Назад" Width="125px" OnClick="GoBackButton_Click" />
        <asp:Button ID="Button2" runat="server" OnClientClick="showLoadPanel()" Text="На главную" Width="125px" OnClick="Button2_Click" />   
    </div> 
</asp:Panel> 

    <script type="text/javascript">
        function setHeightAndWidth(txtdesc) {
            txtdesc.style.height = txtdesc.scrollHeight + "px";
            if (txtdesc.style.width < txtdesc.scrollWidth) {
                txtdesc.style.width = txtdesc.scrollWidth + "px";
            }
        }
</script>

    <style>
            
            .top_panel {
    position:fixed;
    left:0;
    top:3.5em;
    width:100%;
    height:30px;
    background-color:#222222;
    z-index:10;
    color:#05ff01;  
    padding-top:5px;
    font-weight:bold;
}
         .dropdown {
             max-width: 200px;
             
             min-width: 110px;
         }

        .textBox {
            margin-top: auto;
            margin-left: auto; 
           min-width: 110px;
            width: 100%;       
        }
        .textBox1 {     
           min-width: 200px;
            min-height: 100px;     
            width: 100%;      
        }
        
    </style>
     <br />
        <br />
        <asp:Button ID="PreviousSection" runat="server" OnClick="PreviousSection_Click" CausesValidation="False" Text="Предыдущий пункт" />
&nbsp;<asp:Button ID="NextSection" runat="server" OnClick="NextSection_Click" Text="Далее" Width="192px" />
        <br />
        <br />
    <asp:Label   ID="LabelHint"  runat="server"  Visible="true"> </asp:Label>
    <asp:GridView ID="FillingGV" Width="100%"  BorderStyle="Solid" runat="server" AutoGenerateColumns="False" 
        CssClass="gridView"
                BorderColor="Black"  BorderWidth="1px" CellPadding="0" EnableTheming="True" OnRowDataBound="FillingGV_RowDataBound" >
               <Columns>         
               
                     <asp:TemplateField Visible="false"   HeaderText="Значение">
                        <ItemTemplate >
                            <asp:Label          ID="ID0"                 runat="server"  Visible="false"                                      Text='<%# Bind("ID0") %>'                              ></asp:Label>
                            <asp:Label          ID="ReadOnlyLablel0"     runat="server"  Visible='<%# Bind("ReadOnlyLablelVisible0") %>'      Text='<%# Bind("ReadOnlyLablelValue0") %>'             ></asp:Label>                       
                            <asp:TextBox        ID="EditTextBox0"        runat="server"  Visible='<%# Bind("EditTextBoxVisible0") %>'         Text='<%# Bind("EditTextBoxValue0") %>'    TextMode='<%# Bind("EditTextBoxMode0") %>'            ></asp:TextBox>                                                   
                            <asp:CheckBox       ID="EditBoolCheckBox0"   runat="server"  Visible='<%# Bind("EditBoolCheckBoxVisible0") %>'    Checked='<%# Bind("EditBoolCheckBoxValue0") %>'        ></asp:CheckBox>                            
                            <asp:DropDownList   ID="ChooseOnlyDropDown0" runat="server"  Visible='<%# Bind("ChooseOnlyDropDownVisible0") %>'  CssClass="dropdown"     ></asp:DropDownList>                                                       
                            <asp:RequiredFieldValidator runat="server" ID="TextBoxRequire0" ControlToValidate="EditTextBox0" Enabled=     '<%# Bind("TextBoxRequireValidateEnable0") %>' Text="Введите данные"  ForeColor="Red" > 
                            </asp:RequiredFieldValidator>
                            <asp:RangeValidator runat="server" ID="TextBoxValidate0" ControlToValidate="EditTextBox0" 
                               
                                Enabled=     '<%# Bind("TextBoxValidateEnable0") %>'
                                MaximumValue='<%# Bind("TextBoxValidateMaxValue0") %>' 
                                MinimumValue='<%# Bind("TextBoxValidateMinValue0") %>'
                                Text=        '<%# Bind("TextBoxValidateText0") %>'
                                Type=        '<%# Bind("TextBoxValidateType0") %>'
                                
                            ErrorMessage='<%# Bind("TextBoxValidateText0") %>' ForeColor="Red" Display="Dynamic" 
                            SetFocusOnError="True">
                        </asp:RangeValidator>
                        </ItemTemplate>
                    </asp:TemplateField>  
                   
                    <asp:TemplateField Visible="false"   HeaderText="Значение">
                        <ItemTemplate >
                            <asp:Label          ID="ID1"                 runat="server"  Visible="false"                                      Text='<%# Bind("ID1") %>'                              ></asp:Label>
                            <asp:Label          ID="ReadOnlyLablel1"     runat="server"  Visible='<%# Bind("ReadOnlyLablelVisible1") %>'      Text='<%# Bind("ReadOnlyLablelValue1") %>'             ></asp:Label>                       
                            <asp:TextBox        ID="EditTextBox1"        runat="server"  Visible='<%# Bind("EditTextBoxVisible1") %>'         Text='<%# Bind("EditTextBoxValue1") %>'          TextMode='<%# Bind("EditTextBoxMode1") %>'      ></asp:TextBox>                                                   
                            <asp:CheckBox       ID="EditBoolCheckBox1"   runat="server"  Visible='<%# Bind("EditBoolCheckBoxVisible1") %>'    Checked='<%# Bind("EditBoolCheckBoxValue1") %>'        ></asp:CheckBox>                            
                            <asp:DropDownList   ID="ChooseOnlyDropDown1" runat="server"  Visible='<%# Bind("ChooseOnlyDropDownVisible1") %>'  CssClass="dropdown"       ></asp:DropDownList>                            
                        <asp:RequiredFieldValidator runat="server" ID="TextBoxRequire1" ControlToValidate="EditTextBox1" Enabled=     '<%# Bind("TextBoxRequireValidateEnable1") %>' Text="Введите данные"  ForeColor="Red"> 
                            </asp:RequiredFieldValidator>
                            <asp:RangeValidator runat="server" ID="TextBoxValidate1" ControlToValidate="EditTextBox1" 
                                Enabled=     '<%# Bind("TextBoxValidateEnable1") %>'
                                MaximumValue='<%# Bind("TextBoxValidateMaxValue1") %>' 
                                MinimumValue='<%# Bind("TextBoxValidateMinValue1") %>'
                                Text=        '<%# Bind("TextBoxValidateText1") %>'
                                Type=        '<%# Bind("TextBoxValidateType1") %>'
                            ErrorMessage='<%# Bind("TextBoxValidateText1") %>' ForeColor="Red" Display="Dynamic" 
                            SetFocusOnError="True">
                        </asp:RangeValidator>                        
                        </ItemTemplate>
                    </asp:TemplateField> 
                   
                    <asp:TemplateField Visible="false"   HeaderText="Значение">
                        <ItemTemplate >
                            <asp:Label          ID="ID2"                 runat="server"  Visible="false"                                      Text='<%# Bind("ID2") %>'                              ></asp:Label>
                            <asp:Label          ID="ReadOnlyLablel2"     runat="server"  Visible='<%# Bind("ReadOnlyLablelVisible2") %>'      Text='<%# Bind("ReadOnlyLablelValue2") %>'             ></asp:Label>                       
                            <asp:TextBox        ID="EditTextBox2"        runat="server"  Visible='<%# Bind("EditTextBoxVisible2") %>'         Text='<%# Bind("EditTextBoxValue2") %>'           TextMode='<%# Bind("EditTextBoxMode2") %>'      ></asp:TextBox>                                                   
                            <asp:CheckBox       ID="EditBoolCheckBox2"   runat="server"  Visible='<%# Bind("EditBoolCheckBoxVisible2") %>'    Checked='<%# Bind("EditBoolCheckBoxValue2") %>'        ></asp:CheckBox>                            
                            <asp:DropDownList   ID="ChooseOnlyDropDown2" runat="server"  Visible='<%# Bind("ChooseOnlyDropDownVisible2") %>'  CssClass="dropdown"      ></asp:DropDownList>                           
                            <asp:RequiredFieldValidator runat="server" ID="TextBoxRequire2" ControlToValidate="EditTextBox2" Enabled=     '<%# Bind("TextBoxRequireValidateEnable2") %>' Text="Введите данные"  ForeColor="Red"> 
                            </asp:RequiredFieldValidator>
                            <asp:RangeValidator runat="server" ID="TextBoxValidate2" ControlToValidate="EditTextBox2" 
                                Enabled=     '<%# Bind("TextBoxValidateEnable2") %>'
                                MaximumValue='<%# Bind("TextBoxValidateMaxValue2") %>' 
                                MinimumValue='<%# Bind("TextBoxValidateMinValue2") %>'
                                Text=        '<%# Bind("TextBoxValidateText2") %>'
                                Type=        '<%# Bind("TextBoxValidateType2") %>'
                            ErrorMessage='<%# Bind("TextBoxValidateText2") %>' ForeColor="Red" Display="Dynamic" 
                            SetFocusOnError="True">
                        </asp:RangeValidator>
                        </ItemTemplate>
                    </asp:TemplateField> 
                   
                    <asp:TemplateField Visible="false"   HeaderText="Значение">
                        <ItemTemplate >
                            <asp:Label          ID="ID3"                 runat="server"  Visible="false"                                      Text='<%# Bind("ID3") %>'                              ></asp:Label>
                            <asp:Label          ID="ReadOnlyLablel3"     runat="server"  Visible='<%# Bind("ReadOnlyLablelVisible3") %>'      Text='<%# Bind("ReadOnlyLablelValue3") %>'             ></asp:Label>                       
                            <asp:TextBox        ID="EditTextBox3"        runat="server"  Visible='<%# Bind("EditTextBoxVisible3") %>'         Text='<%# Bind("EditTextBoxValue3") %>'          TextMode='<%# Bind("EditTextBoxMode3") %>'       ></asp:TextBox>                                                   
                            <asp:CheckBox       ID="EditBoolCheckBox3"   runat="server"  Visible='<%# Bind("EditBoolCheckBoxVisible3") %>'    Checked='<%# Bind("EditBoolCheckBoxValue3") %>'        ></asp:CheckBox>                            
                            <asp:DropDownList   ID="ChooseOnlyDropDown3" runat="server"  Visible='<%# Bind("ChooseOnlyDropDownVisible3") %>'  CssClass="dropdown"       ></asp:DropDownList>
                            <asp:RequiredFieldValidator runat="server" ID="TextBoxRequire3" ControlToValidate="EditTextBox3" Enabled=     '<%# Bind("TextBoxRequireValidateEnable3") %>' Text="Введите данные"  ForeColor="Red"> 
                            </asp:RequiredFieldValidator>
                            <asp:RangeValidator runat="server" ID="TextBoxValidate3" ControlToValidate="EditTextBox3" 
                                Enabled=     '<%# Bind("TextBoxValidateEnable3") %>'
                                MaximumValue='<%# Bind("TextBoxValidateMaxValue3") %>' 
                                MinimumValue='<%# Bind("TextBoxValidateMinValue3") %>'
                                Text=        '<%# Bind("TextBoxValidateText3") %>'
                                Type=        '<%# Bind("TextBoxValidateType3") %>'
                            ErrorMessage='<%# Bind("TextBoxValidateText3") %>' ForeColor="Red" Display="Dynamic" 
                            SetFocusOnError="True">
                        </asp:RangeValidator>
                        </ItemTemplate>
                    </asp:TemplateField> 
                   
                    <asp:TemplateField Visible="false"   HeaderText="Значение">
                        <ItemTemplate >
                            <asp:Label          ID="ID4"                 runat="server"  Visible="false"                                      Text='<%# Bind("ID4") %>'                              ></asp:Label>
                            <asp:Label          ID="ReadOnlyLablel4"     runat="server"  Visible='<%# Bind("ReadOnlyLablelVisible4") %>'      Text='<%# Bind("ReadOnlyLablelValue4") %>'             ></asp:Label>                       
                            <asp:TextBox        ID="EditTextBox4"        runat="server"  Visible='<%# Bind("EditTextBoxVisible4") %>'         Text='<%# Bind("EditTextBoxValue4") %>'          TextMode='<%# Bind("EditTextBoxMode4") %>'       ></asp:TextBox>                                                   
                            <asp:CheckBox       ID="EditBoolCheckBox4"   runat="server"  Visible='<%# Bind("EditBoolCheckBoxVisible4") %>'    Checked='<%# Bind("EditBoolCheckBoxValue4") %>'        ></asp:CheckBox>                            
                            <asp:DropDownList   ID="ChooseOnlyDropDown4" runat="server"  Visible='<%# Bind("ChooseOnlyDropDownVisible4") %>'  CssClass="dropdown"   ></asp:DropDownList>
                            <asp:RequiredFieldValidator runat="server" ID="TextBoxRequire4" ControlToValidate="EditTextBox4" Enabled=     '<%# Bind("TextBoxRequireValidateEnable4") %>' Text="Введите данные"  ForeColor="Red"> 
                            </asp:RequiredFieldValidator>
                            <asp:RangeValidator runat="server" ID="TextBoxValidate4" ControlToValidate="EditTextBox4" 
                                Enabled=     '<%# Bind("TextBoxValidateEnable4") %>'
                                MaximumValue='<%# Bind("TextBoxValidateMaxValue4") %>' 
                                MinimumValue='<%# Bind("TextBoxValidateMinValue4") %>'
                                Text=        '<%# Bind("TextBoxValidateText4") %>'
                                Type=        '<%# Bind("TextBoxValidateType4") %>'
                            ErrorMessage='<%# Bind("TextBoxValidateText4") %>' ForeColor="Red" Display="Dynamic" 
                            SetFocusOnError="True">
                        </asp:RangeValidator>
                        </ItemTemplate>
                    </asp:TemplateField> 
                   
                    <asp:TemplateField Visible="false"   HeaderText="Значение">
                        <ItemTemplate >
                            <asp:Label          ID="ID5"                 runat="server"  Visible="false"                                      Text='<%# Bind("ID5") %>'                              ></asp:Label>
                            <asp:Label          ID="ReadOnlyLablel5"     runat="server"  Visible='<%# Bind("ReadOnlyLablelVisible5") %>'      Text='<%# Bind("ReadOnlyLablelValue5") %>'             ></asp:Label>                       
                            <asp:TextBox        ID="EditTextBox5"        runat="server"  Visible='<%# Bind("EditTextBoxVisible5") %>'         Text='<%# Bind("EditTextBoxValue5") %>'        TextMode='<%# Bind("EditTextBoxMode5") %>'         ></asp:TextBox>                                                   
                            <asp:CheckBox       ID="EditBoolCheckBox5"   runat="server"  Visible='<%# Bind("EditBoolCheckBoxVisible5") %>'    Checked='<%# Bind("EditBoolCheckBoxValue5") %>'        ></asp:CheckBox>                            
                            <asp:DropDownList   ID="ChooseOnlyDropDown5" runat="server"  Visible='<%# Bind("ChooseOnlyDropDownVisible5") %>'  CssClass="dropdown"       ></asp:DropDownList>
                            <asp:RequiredFieldValidator runat="server" ID="TextBoxRequire5" ControlToValidate="EditTextBox5" Enabled=     '<%# Bind("TextBoxRequireValidateEnable5") %>' Text="Введите данные"  ForeColor="Red"> 
                            </asp:RequiredFieldValidator>
                            <asp:RangeValidator runat="server" ID="TextBoxValidate5" ControlToValidate="EditTextBox5" 
                                Enabled=     '<%# Bind("TextBoxValidateEnable5") %>'
                                MaximumValue='<%# Bind("TextBoxValidateMaxValue5") %>' 
                                MinimumValue='<%# Bind("TextBoxValidateMinValue5") %>'
                                Text=        '<%# Bind("TextBoxValidateText5") %>'
                                Type=        '<%# Bind("TextBoxValidateType5") %>'
                            ErrorMessage='<%# Bind("TextBoxValidateText5") %>' ForeColor="Red" Display="Dynamic" 
                            SetFocusOnError="True">
                        </asp:RangeValidator>
                        </ItemTemplate>
                    </asp:TemplateField> 
                   
                    <asp:TemplateField Visible="false"   HeaderText="Значение">
                        <ItemTemplate >
                            <asp:Label          ID="ID6"                 runat="server"  Visible="false"                                      Text='<%# Bind("ID6") %>'                              ></asp:Label>
                            <asp:Label          ID="ReadOnlyLablel6"     runat="server"  Visible='<%# Bind("ReadOnlyLablelVisible6") %>'      Text='<%# Bind("ReadOnlyLablelValue6") %>'             ></asp:Label>                       
                            <asp:TextBox        ID="EditTextBox6"        runat="server"  Visible='<%# Bind("EditTextBoxVisible6") %>'         Text='<%# Bind("EditTextBoxValue6") %>'         TextMode='<%# Bind("EditTextBoxMode6") %>'      ></asp:TextBox>                                                   
                            <asp:CheckBox       ID="EditBoolCheckBox6"   runat="server"  Visible='<%# Bind("EditBoolCheckBoxVisible6") %>'    Checked='<%# Bind("EditBoolCheckBoxValue6") %>'        ></asp:CheckBox>                            
                            <asp:DropDownList   ID="ChooseOnlyDropDown6" runat="server"  Visible='<%# Bind("ChooseOnlyDropDownVisible6") %>'  CssClass="dropdown"      ></asp:DropDownList>
                            <asp:RequiredFieldValidator runat="server" ID="TextBoxRequire6" ControlToValidate="EditTextBox6" Enabled=     '<%# Bind("TextBoxRequireValidateEnable6") %>' Text="Введите данные"  ForeColor="Red"> 
                            </asp:RequiredFieldValidator>
                            <asp:RangeValidator runat="server" ID="TextBoxValidate6" ControlToValidate="EditTextBox6" 
                                Enabled=     '<%# Bind("TextBoxValidateEnable6") %>'
                                MaximumValue='<%# Bind("TextBoxValidateMaxValue6") %>' 
                                MinimumValue='<%# Bind("TextBoxValidateMinValue6") %>'
                                Text=        '<%# Bind("TextBoxValidateText6") %>'
                                Type=        '<%# Bind("TextBoxValidateType6") %>'
                            ErrorMessage='<%# Bind("TextBoxValidateText6") %>' ForeColor="Red" Display="Dynamic" 
                            SetFocusOnError="True">
                        </asp:RangeValidator>
                        </ItemTemplate>
                    </asp:TemplateField> 
                   
                    <asp:TemplateField Visible="false"   HeaderText="Значение">
                        <ItemTemplate >
                            <asp:Label          ID="ID7"                 runat="server"  Visible="false"                                      Text='<%# Bind("ID7") %>'                              ></asp:Label>
                            <asp:Label          ID="ReadOnlyLablel7"     runat="server"  Visible='<%# Bind("ReadOnlyLablelVisible7") %>'      Text='<%# Bind("ReadOnlyLablelValue7") %>'             ></asp:Label>                       
                            <asp:TextBox        ID="EditTextBox7"        runat="server"  Visible='<%# Bind("EditTextBoxVisible7") %>'         Text='<%# Bind("EditTextBoxValue7") %>'         TextMode='<%# Bind("EditTextBoxMode7") %>'       ></asp:TextBox>                                                   
                            <asp:CheckBox       ID="EditBoolCheckBox7"   runat="server"  Visible='<%# Bind("EditBoolCheckBoxVisible7") %>'    Checked='<%# Bind("EditBoolCheckBoxValue7") %>'        ></asp:CheckBox>                            
                            <asp:DropDownList   ID="ChooseOnlyDropDown7" runat="server"  Visible='<%# Bind("ChooseOnlyDropDownVisible7") %>'  CssClass="dropdown"     ></asp:DropDownList>
                            <asp:RequiredFieldValidator runat="server" ID="TextBoxRequire7" ControlToValidate="EditTextBox7" Enabled=     '<%# Bind("TextBoxRequireValidateEnable7") %>' Text="Введите данные"  ForeColor="Red" > 
                            </asp:RequiredFieldValidator>
                            <asp:RangeValidator runat="server" ID="TextBoxValidate7" ControlToValidate="EditTextBox7" 
                                Enabled=     '<%# Bind("TextBoxValidateEnable7") %>'
                                MaximumValue='<%# Bind("TextBoxValidateMaxValue7") %>' 
                                MinimumValue='<%# Bind("TextBoxValidateMinValue7") %>'
                                Text=        '<%# Bind("TextBoxValidateText7") %>'
                                Type=        '<%# Bind("TextBoxValidateType7") %>'
                            ErrorMessage='<%# Bind("TextBoxValidateText7") %>' ForeColor="Red" Display="Dynamic" 
                            SetFocusOnError="True">
                        </asp:RangeValidator>
                        </ItemTemplate>
                    </asp:TemplateField> 
                   
                    <asp:TemplateField Visible="false"   HeaderText="Значение">
                        <ItemTemplate >
                            <asp:Label          ID="ID8"                 runat="server"  Visible="false"                                      Text='<%# Bind("ID8") %>'                              ></asp:Label>
                            <asp:Label          ID="ReadOnlyLablel8"     runat="server"  Visible='<%# Bind("ReadOnlyLablelVisible8") %>'      Text='<%# Bind("ReadOnlyLablelValue8") %>'             ></asp:Label>                       
                            <asp:TextBox        ID="EditTextBox8"        runat="server"  Visible='<%# Bind("EditTextBoxVisible8") %>'         Text='<%# Bind("EditTextBoxValue8") %>'          TextMode='<%# Bind("EditTextBoxMode8") %>'       ></asp:TextBox>                                                   
                            <asp:CheckBox       ID="EditBoolCheckBox8"   runat="server"  Visible='<%# Bind("EditBoolCheckBoxVisible8") %>'    Checked='<%# Bind("EditBoolCheckBoxValue8") %>'        ></asp:CheckBox>                            
                            <asp:DropDownList   ID="ChooseOnlyDropDown8" runat="server"  Visible='<%# Bind("ChooseOnlyDropDownVisible8") %>'  CssClass="dropdown"        ></asp:DropDownList>
                            <asp:RequiredFieldValidator runat="server" ID="TextBoxRequire8" ControlToValidate="EditTextBox8" Enabled=     '<%# Bind("TextBoxRequireValidateEnable8") %>' Text="Введите данные"  ForeColor="Red"> 
                            </asp:RequiredFieldValidator>
                            <asp:RangeValidator runat="server" ID="TextBoxValidate8" ControlToValidate="EditTextBox8" 
                                Enabled=     '<%# Bind("TextBoxValidateEnable8") %>'
                                MaximumValue='<%# Bind("TextBoxValidateMaxValue8") %>' 
                                MinimumValue='<%# Bind("TextBoxValidateMinValue8") %>'
                                Text=        '<%# Bind("TextBoxValidateText8") %>'
                                Type=        '<%# Bind("TextBoxValidateType8") %>'
                                
                            ErrorMessage='<%# Bind("TextBoxValidateText8") %>' ForeColor="Red" Display="Dynamic" 
                            SetFocusOnError="True">
                        </asp:RangeValidator>
                        </ItemTemplate>
                    </asp:TemplateField> 
                   
                    <asp:TemplateField Visible="false"   HeaderText="Значение">
                        <ItemTemplate >
                            <asp:Label          ID="ID9"                 runat="server"  Visible="false"                                      Text='<%# Bind("ID9") %>'                              ></asp:Label>
                            <asp:Label          ID="ReadOnlyLablel9"     runat="server"  Visible='<%# Bind("ReadOnlyLablelVisible9") %>'      Text='<%# Bind("ReadOnlyLablelValue9") %>'             ></asp:Label>                       
                            <asp:TextBox        ID="EditTextBox9"        runat="server"  Visible='<%# Bind("EditTextBoxVisible9") %>'         Text='<%# Bind("EditTextBoxValue9") %>'        TextMode='<%# Bind("EditTextBoxMode9") %>'         ></asp:TextBox>                                                   
                            <asp:CheckBox       ID="EditBoolCheckBox9"   runat="server"  Visible='<%# Bind("EditBoolCheckBoxVisible9") %>'    Checked='<%# Bind("EditBoolCheckBoxValue9") %>'        ></asp:CheckBox>                            
                            <asp:DropDownList   ID="ChooseOnlyDropDown9" runat="server"  Visible='<%# Bind("ChooseOnlyDropDownVisible9") %>'  CssClass="dropdown"        ></asp:DropDownList>
                            <asp:RequiredFieldValidator runat="server" ID="TextBoxRequire9" ControlToValidate="EditTextBox9" Enabled=     '<%# Bind("TextBoxRequireValidateEnable9") %>' Text="Введите данные"  ForeColor="Red"> 
                            </asp:RequiredFieldValidator>
                            <asp:RangeValidator runat="server" ID="TextBoxValidate9" ControlToValidate="EditTextBox9" 
                                Enabled=     '<%# Bind("TextBoxValidateEnable9") %>'
                                MaximumValue='<%# Bind("TextBoxValidateMaxValue9") %>' 
                                MinimumValue='<%# Bind("TextBoxValidateMinValue9") %>'
                                Text=        '<%# Bind("TextBoxValidateText9") %>'
                                Type=        '<%# Bind("TextBoxValidateType9") %>'
                            ErrorMessage='<%# Bind("TextBoxValidateText9") %>' ForeColor="Red" Display="Dynamic" 
                            SetFocusOnError="True">
                        </asp:RangeValidator>
                        </ItemTemplate>
                    </asp:TemplateField> 

                    <asp:TemplateField HeaderText="Удалить">
                        <ItemTemplate>
                            <asp:Button ID="DeleteRowButton" runat="server" CommandName="Select" Text="Удалить" CommandArgument='<%# Eval("ID0") %>' CausesValidation="False" Width="200px" OnClick="DeleteRowButtonClick"/>
                        </ItemTemplate>
                    </asp:TemplateField>
                    
                                                  
               </Columns>
        </asp:GridView>       
    </div>       
        <asp:Button ID="SaveButton" runat="server" Text="Сохранить" CausesValidation="False" OnClick="SaveButton_Click" />
        <asp:Button ID="AddRowButton" runat="server" Text="Добавить строку" CausesValidation="False" OnClick="AddRowButton_Click" />
</asp:Content>
