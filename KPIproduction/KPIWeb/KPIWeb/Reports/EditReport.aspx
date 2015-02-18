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
            <br />
            Выберите роли задействованные в кампании (отчете)<asp:GridView ID="GridviewRoles" runat="server" ShowFooter="true" AutoGenerateColumns="false" Width="500px">
                            <Columns>

                                <asp:TemplateField HeaderText="Активен">
                                    <ItemTemplate>
                                        <asp:Label ID="LabelRolesTableID" runat="server" Visible="false" Text='<%# Bind("RolesTableID") %>'></asp:Label>
                                        <asp:CheckBox ID="CheckBoxRoleChecked" runat="server" Checked='<%# Bind("RoleChecked") %>'></asp:CheckBox>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:BoundField DataField="Name" HeaderText="Роль" />

                            </Columns>
                        </asp:GridView>
                    <br />
            <asp:Button ID="ButtonSave" runat="server" Width="500px" Text="Сохранить" OnClick="ButtonSave_Click" />
        </div>

</asp:Content>
