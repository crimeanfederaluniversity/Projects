<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Form_kafedra.aspx.cs" Inherits="WebApplication3.Form_kafedra" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body style="height: 285px">
    <form id="form1" runat="server">
    <div>
    
        <asp:Label ID="Label1" runat="server" Text="Пользователь &quot;Kafedra&quot;. Кафедра Компьютерная инженерия и моделирования. 151049" align="center"></asp:Label>
        <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
            ConnectionString="<%$ ConnectionStrings:KPIConnectionString %>" 
            SelectCommand="SELECT * FROM [Baseline_parametrs] WHERE kaf = 2"
            UpdateCommand="UPDATE Baseline_parametrs SET Value=@Value FROM Baseline_parametrs WHERE id_baseline_parametrs=@id_baseline_parametrs">
        </asp:SqlDataSource>
            
        <br />
        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataKeyNames="id_baseline_parametrs" DataSourceID="SqlDataSource1" AutoGenerateEditButton="True" BorderStyle="Groove">
            <Columns>
                <asp:BoundField DataField="id_baseline_parametrs" HeaderText="id_baseline_parametrs" InsertVisible="False" ReadOnly="True" SortExpression="id_baseline_parametrs" Visible="False" />
                <asp:CheckBoxField DataField="active" HeaderText="active" SortExpression="active" Visible="False" ReadOnly="True"/>
                <asp:BoundField DataField="name" HeaderText="name" SortExpression="name" ReadOnly="True"/>
                <asp:BoundField DataField="ab" HeaderText="ab" SortExpression="ab" ReadOnly="True"/>
                <asp:BoundField DataField="unit" HeaderText="unit" SortExpression="unit" />
                <asp:BoundField DataField="kaf" HeaderText="kaf" SortExpression="kaf"  Visible="False"/>
                <asp:BoundField DataField="Value" HeaderText="Value" SortExpression="Val"/>
            </Columns>
        </asp:GridView>
        <br />
    
    </div>
    </form>
</body>
</html>
