<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EditReport.aspx.cs" Inherits="KPIWeb.Reports.EditReport" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
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
                    <td>Стартовая дата отчета
                    </td>
                    <td>
                        <asp:Calendar ID="CalendarStartDateTime" runat="server" SelectionMode="Day" ShowGridLines="True">
                            <SelectedDayStyle BackColor="Yellow" ForeColor="Red"></SelectedDayStyle>
                        </asp:Calendar>
                    </td>
                </tr>

                <tr>
                    <td>Конечная дата отчета
                    </td>
                    <td>
                        <asp:Calendar ID="CalendarEndDateTime" runat="server" SelectionMode="Day" ShowGridLines="True">
                            <SelectedDayStyle BackColor="Yellow" ForeColor="Red"></SelectedDayStyle>
                        </asp:Calendar>
                    </td>
                </tr>

                <tr>
                    <td>Запланированная дата отправки отчета
                    </td>
                    <td>
                        <asp:Calendar ID="CalendarDateToSend" runat="server" SelectionMode="Day" ShowGridLines="True">
                            <SelectedDayStyle BackColor="Yellow" ForeColor="Red"></SelectedDayStyle>
                        </asp:Calendar>
                    </td>
                </tr>

                <tr>
                    <td>Дата отправки отчета
                    </td>
                    <td>
                        <asp:Calendar ID="CalendarSentDateTime" runat="server" SelectionMode="Day" ShowGridLines="True">
                            <SelectedDayStyle BackColor="Yellow" ForeColor="Red"></SelectedDayStyle>
                        </asp:Calendar>
                    </td>
                </tr>

                <tr>
                    <td>Выберите роли задействованные в кампании (отчете)
                    </td>
                    <td>
                        <asp:GridView ID="GridviewRoles" runat="server" ShowFooter="true" AutoGenerateColumns="false">
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
                    </td>
                </tr>

            </table>
            <asp:Button ID="ButtonSave" runat="server" Width="100%" Text="Сохранить" OnClick="ButtonSave_Click" />
        </div>
    </form>
</body>
</html>
