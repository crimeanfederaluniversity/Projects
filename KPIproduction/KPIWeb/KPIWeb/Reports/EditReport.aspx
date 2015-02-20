<%@ Page Language="C#" Title="Редактирование отчета"  MasterPageFile="~/Site.Master" AutoEventWireup="true" EnableViewStateMac="false" CodeBehind="EditReport.aspx.cs" Inherits="KPIWeb.Reports.EditReport" %>


<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
    <h2>Редактирование отчета</h2>
        <div>
            <table>
                <tr>
                    <td>Активный отчет
                    </td>
                    <td>
                        <asp:CheckBox runat="server" ID="CheckBoxActive" />
                    </td>
                </tr>

                <tr>
                    <td>Отчет рассчитан
                    </td>
                    <td>
                        <asp:CheckBox runat="server" ID="CheckBoxCalculeted" />
                    </td>
                </tr>

                <tr>
                    <td>Отчет отправлен
                    </td>
                    <td>
                        <asp:CheckBox runat="server" ID="CheckBoxSent" />
                    </td>
                </tr>

                <tr>
                    <td>Отчет принят получателем
                    </td>
                    <td>
                        <asp:CheckBox runat="server" ID="CheckBoxRecipientConfirmed" />
                    </td>
                </tr>

                <tr>
                    <td>Наименование отчета
                    </td>
                    <td>
                        <asp:TextBox Width="400 px" runat="server" ID="TextBoxName" />
                    </td>
                </tr>
                
                <tr>
                    <td>
                        <br />
                        Стартовая дата отчета
                        </td>
                    <td>
                        &nbsp;
                        <br />
                        Конечная дата отчета</td>
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
                        <br />
                        Запланированная дата отправки отчета&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </td>
                    <td>
                        &nbsp;&nbsp;
                        <br />
                        Дата отправки отчета </td>
                </tr>

                <tr>
                    <td>
                        <asp:Calendar ID="CalendarDateToSend" runat="server" SelectionMode="Day" ShowGridLines="True">
                            <SelectedDayStyle BackColor="Yellow" ForeColor="Red"></SelectedDayStyle>
                        </asp:Calendar>
                    </td>
                    <td>
                        <asp:Calendar ID="CalendarSentDateTime" runat="server" SelectionMode="Day" ShowGridLines="True">
                            <SelectedDayStyle BackColor="Yellow" ForeColor="Red"></SelectedDayStyle>
                        </asp:Calendar>
                    </td>
                </tr>

                </table>
            <br />
            Выберите академии участвующие в отчете<br />
            <asp:CheckBoxList ID="CheckBoxList1" runat="server" Height="16px" Width="325px">
            </asp:CheckBoxList>
            <br />
            <asp:Button ID="ButtonSave" runat="server" Width="500px" Text="Сохранить" OnClick="ButtonSave_Click" />
            <br />
            <br />
            <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Очистить все связи" Width="500px" />
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
                                <asp:BoundField DataField="CalculatedParametrsName" HeaderText="Расчетный параметр" />
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
