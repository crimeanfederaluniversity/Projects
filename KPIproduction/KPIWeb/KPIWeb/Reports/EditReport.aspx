<%@ Page Language="C#" Title="Редактирование отчёта"  MasterPageFile="~/Site.Master" AutoEventWireup="true" EnableViewStateMac="false" CodeBehind="EditReport.aspx.cs" Inherits="KPIWeb.Reports.EditReport" %>


<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
    <h2>Редактирование отчёта</h2>
        <div>
            <table>
                  <tr>
                    <td>Название отчёта
                    </td>
                    <td>
                        <asp:TextBox Width="400 px" runat="server" ID="TextBoxName" />
                    </td>
                </tr>

                <tr>
                    <td>Активный отчёт
                    </td>
                    <td>
                        <asp:CheckBox runat="server" ID="CheckBoxActive" />
                    </td>
                </tr>

                <tr>
                    <td>Отчёт рассчитан
                    </td>
                    <td>
                        <asp:CheckBox runat="server" ID="CheckBoxCalculeted" />
                    </td>
                </tr>
                
                <tr>
                    <td>Планируемая дата отправки
                    </td>
                    <td>
                        <asp:Calendar ID="CalendarDateToSend" runat="server" SelectionMode="Day" ShowGridLines="True">
                            <SelectedDayStyle BackColor="Yellow" ForeColor="Red"></SelectedDayStyle>
                        </asp:Calendar>
                    </td>
                </tr>

                <tr>
                    <td>Отчёт отправлен
                    </td>
                    <td>
                        <asp:CheckBox runat="server" ID="CheckBoxSent" />
                    </td>
                </tr>
                
                <tr>
                    <td>Дата отправки
                    </td>
                    <td>
                        <asp:Calendar ID="CalendarSentDateTime" runat="server" SelectionMode="Day" ShowGridLines="True" Height="77px">
                            <SelectedDayStyle BackColor="Yellow" ForeColor="Red"></SelectedDayStyle>
                        </asp:Calendar>
                    </td>
                </tr>

                <tr>
                    <td>Отчёт принят получателем
                    </td>
                    <td>
                        <asp:CheckBox runat="server" ID="CheckBoxRecipientConfirmed" />
                    </td>
                </tr>
                
                  <tr>
                    <td>Дата принятия получателем
                    </td>
                    <td>
                        <asp:Calendar ID="CalendarReportRecived" runat="server" SelectionMode="Day" ShowGridLines="True" Height="77px">
                            <SelectedDayStyle BackColor="Yellow" ForeColor="Red"></SelectedDayStyle>
                        </asp:Calendar>
                    </td>
                </tr>

              
                
                <tr>
                    <td>
                        Стартовая дата отчёта
                        </td>
                    <td>
                        Конечная дата отчёта</td>
                </tr>

                <tr>
                    <td>
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
                    <td>
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; </td>
                    <td>
                        &nbsp;</td>
                </tr>

                </table>
            <br />
            <asp:Button ID="Button3" runat="server" OnClick="Button3_Click" Text="Выделить все академии" Width="286px" />
            <br />
            Выберите академии, участвующие в отчёте<br />
            <asp:CheckBoxList ID="CheckBoxList1" runat="server" Height="16px" Width="724px">
            </asp:CheckBoxList>
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
                                <asp:BoundField DataField="IndicatorName" HeaderText="Индикатор" />
                                <asp:TemplateField HeaderText="Активен">
                                    <ItemTemplate>
                                        <asp:Label ID="IndicatorID" runat="server" Visible="false" Text='<%# Bind("IndicatorID") %>'></asp:Label>
                                        <asp:CheckBox ID="IndicatorCheckBox" runat="server" Checked='<%# Bind("IndicatorCheckBox") %>'></asp:CheckBox>
                                    </ItemTemplate>
                                </asp:TemplateField>                       
                            </Columns>
            </asp:GridView>
            
             <br />
            
             <asp:GridView ID="CalculatedParametrsTable" runat="server" ShowFooter="true" AutoGenerateColumns="false" Width="1000px">
                            <Columns>
                                <asp:BoundField DataField="CalculatedParametrsName" HeaderText="Расчётный параметр" />
                                <asp:TemplateField HeaderText="Активен">
                                    <ItemTemplate>
                                        <asp:Label ID="CalculatedParametrsID" runat="server" Visible="false" Text='<%# Bind("CalculatedParametrsID") %>'></asp:Label>
                                        <asp:CheckBox ID="CalculatedParametrsCheckBox" runat="server" Checked='<%# Bind("CalculatedParametrsCheckBox") %>'></asp:CheckBox>
                                    </ItemTemplate>
                                </asp:TemplateField>                                
                            </Columns>
            </asp:GridView>

            <br />

            <asp:GridView ID="BasicParametrsTable" runat="server" ShowFooter="true" AutoGenerateColumns="false" Width="1000px" OnSelectedIndexChanged="BasicParametrsTable_SelectedIndexChanged">
                            <Columns>
                                <asp:BoundField DataField="BasicParametrsName" HeaderText="Базовый параметр" />
                                <asp:TemplateField HeaderText="Активен">
                                    <ItemTemplate>
                                        <asp:Label ID="BasicParametrsID" runat="server" Visible="false" Text='<%# Bind("BasicParametrsID") %>'></asp:Label>
                                        <asp:CheckBox ID="BasicParametrsCheckBox" runat="server" Checked='<%# Bind("BasicParametrsCheckBox") %>'></asp:CheckBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
            </asp:GridView>
        </div>

</asp:Content>
