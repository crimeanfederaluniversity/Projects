<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Indicators.aspx.cs" Inherits="KPIWeb.StatisticsDepartment.Indicators" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
    </div>
        <asp:DropDownList ID="DropDownList1" runat="server" AutoPostBack="True" Height="26px" OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged" Width="500px">
        </asp:DropDownList>
        <br />
        <asp:Label ID="Label5" runat="server" Text="0"></asp:Label>
        <br />
        <asp:Label ID="Label1" runat="server" Text="Название индикатора"></asp:Label>
        <br />
        <asp:TextBox ID="IndicatorName" runat="server" Height="70px" TextMode="MultiLine" Width="500px"></asp:TextBox>
        <br />
        <br />
        <asp:Label ID="Label3" runat="server" Text="Единица измерения"></asp:Label>
&nbsp;&nbsp;&nbsp;
        <asp:TextBox ID="IndicatorMeasure" runat="server"></asp:TextBox>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Label ID="Label4" runat="server" Text="Активен"></asp:Label>
        <asp:CheckBox ID="CheckBox1" runat="server" />
        <br />
        <br />
        <asp:Label ID="Label2" runat="server" Text="Формула рассчета"></asp:Label>
        <br />
        <asp:TextBox ID="IndicatorFormula" runat="server" Height="70px" TextMode="MultiLine" Width="500px"></asp:TextBox>
        <br />
        <br />
        <asp:Button ID="Button3" runat="server" OnClick="Button3_Click" Text="Сохранить все изменения" Width="500px" />
        <br />
        ----------------------------------------------------------------------------------------------------<br />
        <asp:Button ID="Button2" runat="server" OnClick="Button2_Click" Text="Посчитать поле формулы с тестовыми данными" Width="500px" />
        <br />
        <asp:Label ID="Label8" runat="server" Text="Результат или список ошибок"></asp:Label>
        <br />
        <asp:TextBox ID="TextBox1" runat="server" Height="100px" ReadOnly="True" TextMode="MultiLine" Width="500px"></asp:TextBox>
        <br />
        <br />
        <asp:Label ID="Label7" runat="server" Text="Поиск базовых показателей"></asp:Label>
        <br />
        <asp:TextBox ID="SearchBox" runat="server" Width="500px"></asp:TextBox>
        <br />
        <br />
        <asp:Button ID="Button4" runat="server" OnClick="Button4_Click" Text="Искать" Width="500px" />
        <br />
        <asp:GridView ID="GridView1" AutoGenerateColumns="False"  runat="server">
            <Columns>
                <asp:BoundField DataField="Name"  />
                <asp:BoundField DataField="AbbreviationEN"  />
            </Columns>                          
        </asp:GridView>
        <br />
        <br />
    </form>
</body>
</html>
