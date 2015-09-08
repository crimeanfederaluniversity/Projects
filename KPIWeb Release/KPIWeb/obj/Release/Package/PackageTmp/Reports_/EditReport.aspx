﻿<%@ Page Language="C#" Title="Редактирование отчёта"  MasterPageFile="~/Site.Master" AutoEventWireup="true" EnableViewStateMac="false" CodeBehind="EditReport.aspx.cs" Inherits="KPIWeb.Reports.EditReport" %>


<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
    <h2>Редактирование отчёта</h2>
        <div>
            <table>
                  <tr>
                    <td style="width: 272px">Название отчёта
                    </td>
                    <td>
                        <asp:TextBox Width="400 px" runat="server" ID="TextBoxName" />
                    </td>
                </tr>

                <tr>
                    <td style="width: 272px">Активный отчёт
                    </td>
                    <td>
                        <asp:Checkbox    Text=" " runat="server" ID="CheckBoxActive" />
                    </td>
                </tr>

                  <tr>
                    <td style="width: 272px">
                        Стартовая дата отчёта
                        </td>
                    <td>
                        Конечная дата отчёта</td>
                </tr>

                <tr>
                    <td style="width: 272px">
                        <asp:Calendar ID="CalendarStartDateTime" runat="server" SelectionMode="Day" ShowGridLines="True" OnSelectionChanged="CalendarStartDateTime_SelectionChanged">
                            <SelectedDayStyle BackColor="Yellow" ForeColor="Red"></SelectedDayStyle>
                        </asp:Calendar>
                    </td>
                    <td>
                        <asp:Calendar ID="CalendarEndDateTime" runat="server" SelectionMode="Day" ShowGridLines="True">
                            <SelectedDayStyle BackColor="Yellow" ForeColor="Red"></SelectedDayStyle>
                        </asp:Calendar>
                    </td>
                </tr>

                     <tr>
                    <td style="width: 272px">
                        <asp:Label ID="Label1" runat="server" Text="Дата отправки на окончательное  утверждение кафедрами"></asp:Label>
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; </td>
                    <td>
                        <br />
                        <asp:Calendar ID="CalendarConfirmEndDay" runat="server" ShowGridLines="True">
                            <SelectedDayStyle BackColor="Yellow" ForeColor="Red" />
                        </asp:Calendar>
                    </td>
                </tr>

                <tr>
                    <td style="width: 272px">Отчёт рассчитан(да/нет)</td>
                    <td>
                        <asp:Checkbox  Text=" " runat="server" ID="CheckBoxCalculeted" />
                    </td>
                </tr>
                
                <tr>
                    <td style="width: 272px">Планируемая дата отправки
                    </td>
                    <td>
                        <asp:Calendar ID="CalendarDateToSend" runat="server" SelectionMode="Day" ShowGridLines="True">
                            <SelectedDayStyle BackColor="Yellow" ForeColor="Red"></SelectedDayStyle>
                        </asp:Calendar>
                    </td>
                </tr>

                <tr>
                    <td style="width: 272px">Отчёт отправлен(да/нет)</td>
                    <td>
                        <asp:Checkbox  Text=" " runat="server" ID="CheckBoxSent" />
                    </td>
                </tr>
                
                <tr>
                    <td style="width: 272px">Дата отправки
                        отчета</td>
                    <td>
                        <asp:Calendar ID="CalendarSentDateTime" runat="server" SelectionMode="Day" ShowGridLines="True" Height="77px">
                            <SelectedDayStyle BackColor="Yellow" ForeColor="Red"></SelectedDayStyle>
                        </asp:Calendar>
                    </td>
                </tr>

                <tr>
                    <td style="width: 272px">Отчёт принят получателем(да/нет)
                    </td>
                    <td>
                        <asp:Checkbox  Text=" " runat="server" ID="CheckBoxRecipientConfirmed" />
                    </td>
                </tr>
                
                  <tr>
                    <td style="width: 272px">Дата принятия получателем
                    </td>
                    <td>
                        <asp:Calendar ID="CalendarReportRecived" runat="server" SelectionMode="Day" ShowGridLines="True" Height="77px">
                            <SelectedDayStyle BackColor="Yellow" ForeColor="Red"></SelectedDayStyle>
                        </asp:Calendar>
                    </td>
                </tr> 

                 <tr>
                    <td style="width: 272px">
                        <asp:Label ID="Label2" runat="server" Text="Дней до предварительного  расчета для проректоров"></asp:Label>
                        (конечная дата минус число дней)&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; </td>
                    <td>
                        <asp:TextBox ID="DaysBeforeToCalcForRector" runat="server"></asp:TextBox>
                        <br />
                        
                    </td>
                </tr>
                </table>
            <br />

         <script type="text/javascript">

             function postBackByObject() {
                 var o = window.event.srcElement;
                 if (o.tagName == "INPUT" && o.type == "checkbox") {
                     __doPostBack("", "");
                 }
             }
   </script>
    <br />
    <asp:TreeView ID="TreeView1" runat="server"  ShowCheckBoxes="All" OnSelectedNodeChanged="TreeView1_SelectedNodeChanged" OnTreeNodeCheckChanged="TreeView1_TreeNodeCheckChanged"></asp:TreeView>



            <br />
            <br />
            <asp:Button ID="ButtonSave" runat="server" Width="500px" Text="Сохранить" OnClick="ButtonSave_Click" />
            <br />
            <br />
            <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Очистить все связи" Width="500px" />
            <br />
            <br />
            <asp:Button ID="Button2" runat="server" OnClick="Button2_Click" Text="Отметить все" Width="500px" />
            <br />
            <br />

            <asp:GridView ID="IndicatorsTable" runat="server" ShowFooter="true" AutoGenerateColumns="false" Width="1000px">
                            <Columns>
                                <asp:BoundField DataField="IndicatorName" HeaderText="Целевые показатели" />
                                <asp:TemplateField HeaderText="Активен">
                                    <ItemTemplate>
                                        <asp:Label ID="IndicatorID" runat="server" Visible="false" Text='<%# Bind("IndicatorID") %>'></asp:Label>
                                        <asp:Checkbox  Text=" " ID="IndicatorCheckBox" runat="server" Checked='<%# Bind("IndicatorCheckBox") %>'></asp:CheckBox>
                                    </ItemTemplate>
                                </asp:TemplateField>                       
                            </Columns>
            </asp:GridView>
            
             <br />
            
             <asp:GridView ID="CalculatedParametrsTable" runat="server" ShowFooter="true" AutoGenerateColumns="false" Width="1000px">
                            <Columns>
                                <asp:BoundField DataField="CalculatedParametrsName" HeaderText="Первичные данные" />
                                <asp:TemplateField HeaderText="Активен">
                                    <ItemTemplate>
                                        <asp:Label ID="CalculatedParametrsID" runat="server" Visible="false" Text='<%# Bind("CalculatedParametrsID") %>'></asp:Label>
                                        <asp:Checkbox  Text=" " ID="CalculatedParametrsCheckBox" runat="server" Checked='<%# Bind("CalculatedParametrsCheckBox") %>'></asp:CheckBox>
                                    </ItemTemplate>
                                </asp:TemplateField>                                
                            </Columns>
            </asp:GridView>

            <br />

            <asp:GridView ID="BasicParametrsTable" runat="server" ShowFooter="true" AutoGenerateColumns="false" Width="1000px" OnSelectedIndexChanged="BasicParametrsTable_SelectedIndexChanged">
                            <Columns>
                                <asp:BoundField DataField="BasicParametrsName" HeaderText="Базовые показатели" />
                                <asp:TemplateField HeaderText="Активен">
                                    <ItemTemplate>
                                        <asp:Label ID="BasicParametrsID" runat="server" Visible="false" Text='<%# Bind("BasicParametrsID") %>'></asp:Label>
                                        <asp:Checkbox  Text=" " ID="BasicParametrsCheckBox" runat="server" Checked='<%# Bind("BasicParametrsCheckBox") %>'></asp:CheckBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
            </asp:GridView>
        </div>

</asp:Content>