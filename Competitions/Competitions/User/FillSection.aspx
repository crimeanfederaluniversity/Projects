<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="FillSection.aspx.cs" Inherits="Competitions.User.FillSection" %>
<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">    <div>
        <asp:Button ID="GoBackButton" runat="server" Text="Назад" OnClick="GoBackButton_Click" />
    <style>
        .dropdown {
            max-width: 200px;
        }

        .textBox {

            width: 100%;          
            min-width: 100px;
            min-height: 100px;
            margin-top: 0px;
            top: 0px;
        }
        
    </style>
        <br />
    
    <asp:GridView ID="FillingGV" Width="100%"  BorderStyle="Solid" runat="server" AutoGenerateColumns="False" 
        CssClass="gridView"
                BorderColor="Black"  BorderWidth="1px" CellPadding="0" EnableTheming="True" OnRowDataBound="FillingGV_RowDataBound" >
               <Columns>         
               
                     <asp:TemplateField Visible="false"   HeaderText="Значение">
                        <ItemTemplate >
                            <asp:Label          ID="ID0"                 runat="server"  Visible="false"                                      Text='<%# Bind("ID0") %>'                              ></asp:Label>
                            <asp:Label          ID="ReadOnlyLablel0"     runat="server"  Visible='<%# Bind("ReadOnlyLablelVisible0") %>'      Text='<%# Bind("ReadOnlyLablelValue0") %>'             ></asp:Label>                       
                            <asp:TextBox        ID="EditTextBox0"        runat="server"  CssClass="textBox" Visible='<%# Bind("EditTextBoxVisible0") %>'         Text='<%# Bind("EditTextBoxValue0") %>'    TextMode='<%# Bind("EditTextBoxMode0") %>'            ></asp:TextBox>                                                   
                            <asp:CheckBox       ID="EditBoolCheckBox0"   runat="server"  Visible='<%# Bind("EditBoolCheckBoxVisible0") %>'    Checked='<%# Bind("EditBoolCheckBoxValue0") %>'        ></asp:CheckBox>                            
                            <asp:DropDownList   ID="ChooseOnlyDropDown0" runat="server"  Visible='<%# Bind("ChooseOnlyDropDownVisible0") %>'  CssClass="dropdown"     ></asp:DropDownList>
                            <asp:Calendar       ID="ChooseDateCalendar0" runat="server"  Visible='<%# Bind("ChooseDateCalendarVisible0") %>'  SelectedDate='<%# Bind("ChooseDateCalendarValue0") %>' ></asp:Calendar>                            
                            <asp:RequiredFieldValidator runat="server" ID="TextBoxRequire0" ControlToValidate="EditTextBox0" Enabled=     '<%# Bind("TextBoxRequireValidateEnable0") %>' Text="Введите данные"  ForeColor="Red" > 
                            </asp:RequiredFieldValidator>
                            <asp:RangeValidator runat="server" ID="TextBoxValidate0" ControlToValidate="EditTextBox0" 
                               
                                Enabled=     '<%# Bind("TextBoxValidateEnable0") %>'
                                MaximumValue='<%# Bind("TextBoxValidateMaxValue0") %>' 
                                MinimumValue='<%# Bind("TextBoxValidateMinValue0") %>'
                                Text=        '<%# Bind("TextBoxValidateText0") %>'
                                Type=        '<%# Bind("TextBoxValidateType0") %>'
                                
                            ErrorMessage="Ошибка" ForeColor="Red" Display="Dynamic" 
                            SetFocusOnError="True">
                        </asp:RangeValidator>
                        </ItemTemplate>
                    </asp:TemplateField>  
                   
                    <asp:TemplateField Visible="false"   HeaderText="Значение">
                        <ItemTemplate >
                            <asp:Label          ID="ID1"                 runat="server"  Visible="false"                                      Text='<%# Bind("ID1") %>'                              ></asp:Label>
                            <asp:Label          ID="ReadOnlyLablel1"     runat="server"  Visible='<%# Bind("ReadOnlyLablelVisible1") %>'      Text='<%# Bind("ReadOnlyLablelValue1") %>'             ></asp:Label>                       
                            <asp:TextBox        ID="EditTextBox1"        runat="server"  CssClass="textBox" Visible='<%# Bind("EditTextBoxVisible1") %>'         Text='<%# Bind("EditTextBoxValue1") %>'          TextMode='<%# Bind("EditTextBoxMode1") %>'      ></asp:TextBox>                                                   
                            <asp:CheckBox       ID="EditBoolCheckBox1"   runat="server"  Visible='<%# Bind("EditBoolCheckBoxVisible1") %>'    Checked='<%# Bind("EditBoolCheckBoxValue1") %>'        ></asp:CheckBox>                            
                            <asp:DropDownList   ID="ChooseOnlyDropDown1" runat="server"  Visible='<%# Bind("ChooseOnlyDropDownVisible1") %>'  CssClass="dropdown"       ></asp:DropDownList>
                            <asp:Calendar       ID="ChooseDateCalendar1" runat="server"  Visible='<%# Bind("ChooseDateCalendarVisible1") %>'  SelectedDate='<%# Bind("ChooseDateCalendarValue1") %>' ></asp:Calendar>
                        <asp:RequiredFieldValidator runat="server" ID="TextBoxRequire1" ControlToValidate="EditTextBox1" Enabled=     '<%# Bind("TextBoxRequireValidateEnable1") %>' Text="Введите данные"  ForeColor="Red"> 
                            </asp:RequiredFieldValidator>
                            <asp:RangeValidator runat="server" ID="TextBoxValidate1" ControlToValidate="EditTextBox1" 
                                Enabled=     '<%# Bind("TextBoxValidateEnable1") %>'
                                MaximumValue='<%# Bind("TextBoxValidateMaxValue1") %>' 
                                MinimumValue='<%# Bind("TextBoxValidateMinValue1") %>'
                                Text=        '<%# Bind("TextBoxValidateText1") %>'
                                Type=        '<%# Bind("TextBoxValidateType1") %>'
                            ErrorMessage="Ошибка" ForeColor="Red" Display="Dynamic" 
                            SetFocusOnError="True">
                        </asp:RangeValidator>                        
                        </ItemTemplate>
                    </asp:TemplateField> 
                   
                    <asp:TemplateField Visible="false"   HeaderText="Значение">
                        <ItemTemplate >
                            <asp:Label          ID="ID2"                 runat="server"  Visible="false"                                      Text='<%# Bind("ID2") %>'                              ></asp:Label>
                            <asp:Label          ID="ReadOnlyLablel2"     runat="server"  Visible='<%# Bind("ReadOnlyLablelVisible2") %>'      Text='<%# Bind("ReadOnlyLablelValue2") %>'             ></asp:Label>                       
                            <asp:TextBox        ID="EditTextBox2"        runat="server"  CssClass="textBox" Visible='<%# Bind("EditTextBoxVisible2") %>'         Text='<%# Bind("EditTextBoxValue2") %>'           TextMode='<%# Bind("EditTextBoxMode2") %>'      ></asp:TextBox>                                                   
                            <asp:CheckBox       ID="EditBoolCheckBox2"   runat="server"  Visible='<%# Bind("EditBoolCheckBoxVisible2") %>'    Checked='<%# Bind("EditBoolCheckBoxValue2") %>'        ></asp:CheckBox>                            
                            <asp:DropDownList   ID="ChooseOnlyDropDown2" runat="server"  Visible='<%# Bind("ChooseOnlyDropDownVisible2") %>'  CssClass="dropdown"      ></asp:DropDownList>
                            <asp:Calendar       ID="ChooseDateCalendar2" runat="server"  Visible='<%# Bind("ChooseDateCalendarVisible2") %>'  SelectedDate='<%# Bind("ChooseDateCalendarValue2") %>' ></asp:Calendar>
                            <asp:RequiredFieldValidator runat="server" ID="TextBoxRequire2" ControlToValidate="EditTextBox2" Enabled=     '<%# Bind("TextBoxRequireValidateEnable2") %>' Text="Введите данные"  ForeColor="Red"> 
                            </asp:RequiredFieldValidator>
                            <asp:RangeValidator runat="server" ID="TextBoxValidate2" ControlToValidate="EditTextBox2" 
                                Enabled=     '<%# Bind("TextBoxValidateEnable2") %>'
                                MaximumValue='<%# Bind("TextBoxValidateMaxValue2") %>' 
                                MinimumValue='<%# Bind("TextBoxValidateMinValue2") %>'
                                Text=        '<%# Bind("TextBoxValidateText2") %>'
                                Type=        '<%# Bind("TextBoxValidateType2") %>'
                            ErrorMessage="Ошибка" ForeColor="Red" Display="Dynamic" 
                            SetFocusOnError="True">
                        </asp:RangeValidator>
                        </ItemTemplate>
                    </asp:TemplateField> 
                   
                    <asp:TemplateField Visible="false"   HeaderText="Значение">
                        <ItemTemplate >
                            <asp:Label          ID="ID3"                 runat="server"  Visible="false"                                      Text='<%# Bind("ID3") %>'                              ></asp:Label>
                            <asp:Label          ID="ReadOnlyLablel3"     runat="server"  Visible='<%# Bind("ReadOnlyLablelVisible3") %>'      Text='<%# Bind("ReadOnlyLablelValue3") %>'             ></asp:Label>                       
                            <asp:TextBox        ID="EditTextBox3"        runat="server"  CssClass="textBox" Visible='<%# Bind("EditTextBoxVisible3") %>'         Text='<%# Bind("EditTextBoxValue3") %>'          TextMode='<%# Bind("EditTextBoxMode3") %>'       ></asp:TextBox>                                                   
                            <asp:CheckBox       ID="EditBoolCheckBox3"   runat="server"  Visible='<%# Bind("EditBoolCheckBoxVisible3") %>'    Checked='<%# Bind("EditBoolCheckBoxValue3") %>'        ></asp:CheckBox>                            
                            <asp:DropDownList   ID="ChooseOnlyDropDown3" runat="server"  Visible='<%# Bind("ChooseOnlyDropDownVisible3") %>'  CssClass="dropdown"       ></asp:DropDownList>
                            <asp:Calendar       ID="ChooseDateCalendar3" runat="server"  Visible='<%# Bind("ChooseDateCalendarVisible3") %>'  SelectedDate='<%# Bind("ChooseDateCalendarValue3") %>' ></asp:Calendar>
                            <asp:RequiredFieldValidator runat="server" ID="TextBoxRequire3" ControlToValidate="EditTextBox3" Enabled=     '<%# Bind("TextBoxRequireValidateEnable3") %>' Text="Введите данные"  ForeColor="Red"> 
                            </asp:RequiredFieldValidator>
                            <asp:RangeValidator runat="server" ID="TextBoxValidate3" ControlToValidate="EditTextBox3" 
                                Enabled=     '<%# Bind("TextBoxValidateEnable3") %>'
                                MaximumValue='<%# Bind("TextBoxValidateMaxValue3") %>' 
                                MinimumValue='<%# Bind("TextBoxValidateMinValue3") %>'
                                Text=        '<%# Bind("TextBoxValidateText3") %>'
                                Type=        '<%# Bind("TextBoxValidateType3") %>'
                            ErrorMessage="Ошибка" ForeColor="Red" Display="Dynamic" 
                            SetFocusOnError="True">
                        </asp:RangeValidator>
                        </ItemTemplate>
                    </asp:TemplateField> 
                   
                    <asp:TemplateField Visible="false"   HeaderText="Значение">
                        <ItemTemplate >
                            <asp:Label          ID="ID4"                 runat="server"  Visible="false"                                      Text='<%# Bind("ID4") %>'                              ></asp:Label>
                            <asp:Label          ID="ReadOnlyLablel4"     runat="server"  Visible='<%# Bind("ReadOnlyLablelVisible4") %>'      Text='<%# Bind("ReadOnlyLablelValue4") %>'             ></asp:Label>                       
                            <asp:TextBox        ID="EditTextBox4"        runat="server"  CssClass="textBox" Visible='<%# Bind("EditTextBoxVisible4") %>'         Text='<%# Bind("EditTextBoxValue4") %>'          TextMode='<%# Bind("EditTextBoxMode4") %>'       ></asp:TextBox>                                                   
                            <asp:CheckBox       ID="EditBoolCheckBox4"   runat="server"  Visible='<%# Bind("EditBoolCheckBoxVisible4") %>'    Checked='<%# Bind("EditBoolCheckBoxValue4") %>'        ></asp:CheckBox>                            
                            <asp:DropDownList   ID="ChooseOnlyDropDown4" runat="server"  Visible='<%# Bind("ChooseOnlyDropDownVisible4") %>'  CssClass="dropdown"   ></asp:DropDownList>
                            <asp:Calendar       ID="ChooseDateCalendar4" runat="server"  Visible='<%# Bind("ChooseDateCalendarVisible4") %>'  SelectedDate='<%# Bind("ChooseDateCalendarValue4") %>' ></asp:Calendar>
                            <asp:RequiredFieldValidator runat="server" ID="TextBoxRequire4" ControlToValidate="EditTextBox4" Enabled=     '<%# Bind("TextBoxRequireValidateEnable4") %>' Text="Введите данные"  ForeColor="Red"> 
                            </asp:RequiredFieldValidator>
                            <asp:RangeValidator runat="server" ID="TextBoxValidate4" ControlToValidate="EditTextBox4" 
                                Enabled=     '<%# Bind("TextBoxValidateEnable4") %>'
                                MaximumValue='<%# Bind("TextBoxValidateMaxValue4") %>' 
                                MinimumValue='<%# Bind("TextBoxValidateMinValue4") %>'
                                Text=        '<%# Bind("TextBoxValidateText4") %>'
                                Type=        '<%# Bind("TextBoxValidateType4") %>'
                            ErrorMessage="Ошибка" ForeColor="Red" Display="Dynamic" 
                            SetFocusOnError="True">
                        </asp:RangeValidator>
                        </ItemTemplate>
                    </asp:TemplateField> 
                   
                    <asp:TemplateField Visible="false"   HeaderText="Значение">
                        <ItemTemplate >
                            <asp:Label          ID="ID5"                 runat="server"  Visible="false"                                      Text='<%# Bind("ID5") %>'                              ></asp:Label>
                            <asp:Label          ID="ReadOnlyLablel5"     runat="server"  Visible='<%# Bind("ReadOnlyLablelVisible5") %>'      Text='<%# Bind("ReadOnlyLablelValue5") %>'             ></asp:Label>                       
                            <asp:TextBox        ID="EditTextBox5"        runat="server"  CssClass="textBox" Visible='<%# Bind("EditTextBoxVisible5") %>'         Text='<%# Bind("EditTextBoxValue5") %>'        TextMode='<%# Bind("EditTextBoxMode5") %>'         ></asp:TextBox>                                                   
                            <asp:CheckBox       ID="EditBoolCheckBox5"   runat="server"  Visible='<%# Bind("EditBoolCheckBoxVisible5") %>'    Checked='<%# Bind("EditBoolCheckBoxValue5") %>'        ></asp:CheckBox>                            
                            <asp:DropDownList   ID="ChooseOnlyDropDown5" runat="server"  Visible='<%# Bind("ChooseOnlyDropDownVisible5") %>'  CssClass="dropdown"       ></asp:DropDownList>
                            <asp:Calendar       ID="ChooseDateCalendar5" runat="server"  Visible='<%# Bind("ChooseDateCalendarVisible5") %>'  SelectedDate='<%# Bind("ChooseDateCalendarValue5") %>' ></asp:Calendar>
                            <asp:RequiredFieldValidator runat="server" ID="TextBoxRequire5" ControlToValidate="EditTextBox5" Enabled=     '<%# Bind("TextBoxRequireValidateEnable5") %>' Text="Введите данные"  ForeColor="Red"> 
                            </asp:RequiredFieldValidator>
                            <asp:RangeValidator runat="server" ID="TextBoxValidate5" ControlToValidate="EditTextBox5" 
                                Enabled=     '<%# Bind("TextBoxValidateEnable5") %>'
                                MaximumValue='<%# Bind("TextBoxValidateMaxValue5") %>' 
                                MinimumValue='<%# Bind("TextBoxValidateMinValue5") %>'
                                Text=        '<%# Bind("TextBoxValidateText5") %>'
                                Type=        '<%# Bind("TextBoxValidateType5") %>'
                            ErrorMessage="Ошибка" ForeColor="Red" Display="Dynamic" 
                            SetFocusOnError="True">
                        </asp:RangeValidator>
                        </ItemTemplate>
                    </asp:TemplateField> 
                   
                    <asp:TemplateField Visible="false"   HeaderText="Значение">
                        <ItemTemplate >
                            <asp:Label          ID="ID6"                 runat="server"  Visible="false"                                      Text='<%# Bind("ID6") %>'                              ></asp:Label>
                            <asp:Label          ID="ReadOnlyLablel6"     runat="server"  Visible='<%# Bind("ReadOnlyLablelVisible6") %>'      Text='<%# Bind("ReadOnlyLablelValue6") %>'             ></asp:Label>                       
                            <asp:TextBox        ID="EditTextBox6"        runat="server"  CssClass="textBox" Visible='<%# Bind("EditTextBoxVisible6") %>'         Text='<%# Bind("EditTextBoxValue6") %>'         TextMode='<%# Bind("EditTextBoxMode6") %>'      ></asp:TextBox>                                                   
                            <asp:CheckBox       ID="EditBoolCheckBox6"   runat="server"  Visible='<%# Bind("EditBoolCheckBoxVisible6") %>'    Checked='<%# Bind("EditBoolCheckBoxValue6") %>'        ></asp:CheckBox>                            
                            <asp:DropDownList   ID="ChooseOnlyDropDown6" runat="server"  Visible='<%# Bind("ChooseOnlyDropDownVisible6") %>'  CssClass="dropdown"      ></asp:DropDownList>
                            <asp:Calendar       ID="ChooseDateCalendar6" runat="server"  Visible='<%# Bind("ChooseDateCalendarVisible6") %>'  SelectedDate='<%# Bind("ChooseDateCalendarValue6") %>' ></asp:Calendar>
                            <asp:RequiredFieldValidator runat="server" ID="TextBoxRequire6" ControlToValidate="EditTextBox6" Enabled=     '<%# Bind("TextBoxRequireValidateEnable6") %>' Text="Введите данные"  ForeColor="Red"> 
                            </asp:RequiredFieldValidator>
                            <asp:RangeValidator runat="server" ID="TextBoxValidate6" ControlToValidate="EditTextBox6" 
                                Enabled=     '<%# Bind("TextBoxValidateEnable6") %>'
                                MaximumValue='<%# Bind("TextBoxValidateMaxValue6") %>' 
                                MinimumValue='<%# Bind("TextBoxValidateMinValue6") %>'
                                Text=        '<%# Bind("TextBoxValidateText6") %>'
                                Type=        '<%# Bind("TextBoxValidateType6") %>'
                            ErrorMessage="Ошибка" ForeColor="Red" Display="Dynamic" 
                            SetFocusOnError="True">
                        </asp:RangeValidator>
                        </ItemTemplate>
                    </asp:TemplateField> 
                   
                    <asp:TemplateField Visible="false"   HeaderText="Значение">
                        <ItemTemplate >
                            <asp:Label          ID="ID7"                 runat="server"  Visible="false"                                      Text='<%# Bind("ID7") %>'                              ></asp:Label>
                            <asp:Label          ID="ReadOnlyLablel7"     runat="server"  Visible='<%# Bind("ReadOnlyLablelVisible7") %>'      Text='<%# Bind("ReadOnlyLablelValue7") %>'             ></asp:Label>                       
                            <asp:TextBox        ID="EditTextBox7"        runat="server"  CssClass="textBox" Visible='<%# Bind("EditTextBoxVisible7") %>'         Text='<%# Bind("EditTextBoxValue7") %>'         TextMode='<%# Bind("EditTextBoxMode7") %>'       ></asp:TextBox>                                                   
                            <asp:CheckBox       ID="EditBoolCheckBox7"   runat="server"  Visible='<%# Bind("EditBoolCheckBoxVisible7") %>'    Checked='<%# Bind("EditBoolCheckBoxValue7") %>'        ></asp:CheckBox>                            
                            <asp:DropDownList   ID="ChooseOnlyDropDown7" runat="server"  Visible='<%# Bind("ChooseOnlyDropDownVisible7") %>'  CssClass="dropdown"     ></asp:DropDownList>
                            <asp:Calendar       ID="ChooseDateCalendar7" runat="server"  Visible='<%# Bind("ChooseDateCalendarVisible7") %>'  SelectedDate='<%# Bind("ChooseDateCalendarValue7") %>' ></asp:Calendar>
                            <asp:RequiredFieldValidator runat="server" ID="TextBoxRequire7" ControlToValidate="EditTextBox7" Enabled=     '<%# Bind("TextBoxRequireValidateEnable7") %>' Text="Введите данные"  ForeColor="Red" > 
                            </asp:RequiredFieldValidator>
                            <asp:RangeValidator runat="server" ID="TextBoxValidate7" ControlToValidate="EditTextBox7" 
                                Enabled=     '<%# Bind("TextBoxValidateEnable7") %>'
                                MaximumValue='<%# Bind("TextBoxValidateMaxValue7") %>' 
                                MinimumValue='<%# Bind("TextBoxValidateMinValue7") %>'
                                Text=        '<%# Bind("TextBoxValidateText7") %>'
                                Type=        '<%# Bind("TextBoxValidateType7") %>'
                            ErrorMessage="Ошибка" ForeColor="Red" Display="Dynamic" 
                            SetFocusOnError="True">
                        </asp:RangeValidator>
                        </ItemTemplate>
                    </asp:TemplateField> 
                   
                    <asp:TemplateField Visible="false"   HeaderText="Значение">
                        <ItemTemplate >
                            <asp:Label          ID="ID8"                 runat="server"  Visible="false"                                      Text='<%# Bind("ID8") %>'                              ></asp:Label>
                            <asp:Label          ID="ReadOnlyLablel8"     runat="server"  Visible='<%# Bind("ReadOnlyLablelVisible8") %>'      Text='<%# Bind("ReadOnlyLablelValue8") %>'             ></asp:Label>                       
                            <asp:TextBox        ID="EditTextBox8"        runat="server"  CssClass="textBox" Visible='<%# Bind("EditTextBoxVisible8") %>'         Text='<%# Bind("EditTextBoxValue8") %>'          TextMode='<%# Bind("EditTextBoxMode8") %>'       ></asp:TextBox>                                                   
                            <asp:CheckBox       ID="EditBoolCheckBox8"   runat="server"  Visible='<%# Bind("EditBoolCheckBoxVisible8") %>'    Checked='<%# Bind("EditBoolCheckBoxValue8") %>'        ></asp:CheckBox>                            
                            <asp:DropDownList   ID="ChooseOnlyDropDown8" runat="server"  Visible='<%# Bind("ChooseOnlyDropDownVisible8") %>'  CssClass="dropdown"        ></asp:DropDownList>
                            <asp:Calendar       ID="ChooseDateCalendar8" runat="server"  Visible='<%# Bind("ChooseDateCalendarVisible8") %>'  SelectedDate='<%# Bind("ChooseDateCalendarValue8") %>' ></asp:Calendar>
                            <asp:RequiredFieldValidator runat="server" ID="TextBoxRequire8" ControlToValidate="EditTextBox8" Enabled=     '<%# Bind("TextBoxRequireValidateEnable8") %>' Text="Введите данные"  ForeColor="Red"> 
                            </asp:RequiredFieldValidator>
                            <asp:RangeValidator runat="server" ID="TextBoxValidate8" ControlToValidate="EditTextBox8" 
                                Enabled=     '<%# Bind("TextBoxValidateEnable8") %>'
                                MaximumValue='<%# Bind("TextBoxValidateMaxValue8") %>' 
                                MinimumValue='<%# Bind("TextBoxValidateMinValue8") %>'
                                Text=        '<%# Bind("TextBoxValidateText8") %>'
                                Type=        '<%# Bind("TextBoxValidateType8") %>'
                                
                            ErrorMessage="Ошибка" ForeColor="Red" Display="Dynamic" 
                            SetFocusOnError="True">
                        </asp:RangeValidator>
                        </ItemTemplate>
                    </asp:TemplateField> 
                   
                    <asp:TemplateField Visible="false"   HeaderText="Значение">
                        <ItemTemplate >
                            <asp:Label          ID="ID9"                 runat="server"  Visible="false"                                      Text='<%# Bind("ID9") %>'                              ></asp:Label>
                            <asp:Label          ID="ReadOnlyLablel9"     runat="server"  Visible='<%# Bind("ReadOnlyLablelVisible9") %>'      Text='<%# Bind("ReadOnlyLablelValue9") %>'             ></asp:Label>                       
                            <asp:TextBox        ID="EditTextBox9"        runat="server"  CssClass="textBox" Visible='<%# Bind("EditTextBoxVisible9") %>'         Text='<%# Bind("EditTextBoxValue9") %>'        TextMode='<%# Bind("EditTextBoxMode9") %>'         ></asp:TextBox>                                                   
                            <asp:CheckBox       ID="EditBoolCheckBox9"   runat="server"  Visible='<%# Bind("EditBoolCheckBoxVisible9") %>'    Checked='<%# Bind("EditBoolCheckBoxValue9") %>'        ></asp:CheckBox>                            
                            <asp:DropDownList   ID="ChooseOnlyDropDown9" runat="server"  Visible='<%# Bind("ChooseOnlyDropDownVisible9") %>'  CssClass="dropdown"        ></asp:DropDownList>
                            <asp:Calendar       ID="ChooseDateCalendar9" runat="server"  Visible='<%# Bind("ChooseDateCalendarVisible9") %>'  SelectedDate='<%# Bind("ChooseDateCalendarValue9") %>' ></asp:Calendar>
                            <asp:RequiredFieldValidator runat="server" ID="TextBoxRequire9" ControlToValidate="EditTextBox9" Enabled=     '<%# Bind("TextBoxRequireValidateEnable9") %>' Text="Введите данные"  ForeColor="Red"> 
                            </asp:RequiredFieldValidator>
                            <asp:RangeValidator runat="server" ID="TextBoxValidate9" ControlToValidate="EditTextBox9" 
                                Enabled=     '<%# Bind("TextBoxValidateEnable9") %>'
                                MaximumValue='<%# Bind("TextBoxValidateMaxValue9") %>' 
                                MinimumValue='<%# Bind("TextBoxValidateMinValue9") %>'
                                Text=        '<%# Bind("TextBoxValidateText9") %>'
                                Type=        '<%# Bind("TextBoxValidateType9") %>'
                            ErrorMessage="Ошибка" ForeColor="Red" Display="Dynamic" 
                            SetFocusOnError="True">
                        </asp:RangeValidator>
                        </ItemTemplate>
                    </asp:TemplateField> 

                    <asp:TemplateField HeaderText="Удалить">
                        <ItemTemplate>
                            <asp:Button ID="DeleteRowButton" runat="server" CommandName="Select" Text="Удалить" CommandArgument='<%# Eval("ID0") %>' Width="200px" OnClick="DeleteRowButtonClick"/>
                        </ItemTemplate>
                    </asp:TemplateField>
                    
                                                  
               </Columns>
        </asp:GridView>       
    </div>       
        <asp:Button ID="SaveButton" runat="server" Text="Сохранить" OnClick="SaveButton_Click" />
        <asp:Button ID="AddRowButton" runat="server" Text="Добавить строку" OnClick="AddRowButton_Click" />
</asp:Content>
