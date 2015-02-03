<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FillingTheReport.aspx.cs" Inherits="KPIWeb.Reports.FillingTheReport" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Сводная таблица показателей</title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <h2>Ввведите значения в таблицу показателей и нажмите кнопку "Сохранить" внизу формы</h2>
            <br />
            <asp:GridView ID="GridviewCollectedBasicParameters" runat="server" ShowFooter="true" AutoGenerateColumns="false">
                <Columns>

                    <asp:BoundField DataField="CurrentReportArchiveID" HeaderText="Current Report ID" Visible="false" />
                    <asp:BoundField DataField="BasicParametersTableID" HeaderText="Basic Parameter ID" Visible="false" />

                    <asp:TemplateField Visible="false" InsertVisible="False" SortExpression="sno">
                        <ItemTemplate>
                            <asp:Label ID="LabelCollectedBasicParametersTableID" runat="server" Visible="false" Text='<%# Bind("CollectedBasicParametersTableID") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:BoundField DataField="Name" HeaderText="Название показателя" />

                    <asp:TemplateField HeaderText="Значение">
                        <ItemTemplate>
                            <asp:TextBox ID="TextBoxCollectedValue" style="text-align:center" BorderWidth="0" runat="server" Text='<%# Bind("CollectedValue") %>'></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>

                </Columns>
            </asp:GridView>
            <br />
            <asp:Button ID="ButtonSave" Width="100%" runat="server" Text="Сохранить" OnClick="ButtonSave_Click" />

        </div>
    </form>
</body>
</html>
