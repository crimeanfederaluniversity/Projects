<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Form_fill_indicators.aspx.cs" Inherits="WebApplication3.Form_fill_indicators" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <asp:Button ID="Button1" runat="server" Text="Button" />
    
    </div>

        <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
            ConnectionString="<%$ ConnectionStrings:KPIConnectionString %>" 
            SelectCommand="SELECT [name], [id_second_stage], [fk_first_stage] FROM [Second_stage]" OnUpdated="SqlDataSource1_Updated"></asp:SqlDataSource>

        
        <asp:SqlDataSource ID="SqlDataSource2" runat="server" 
            ConnectionString="<%$ ConnectionStrings:KPIConnectionString %>" 
            SelectCommand="SELECT [active], [name], [cnt], [id] FROM [test]"
            UpdateCommand="UPDATE test SET name=@name FROM test WHERE id=@id">
        </asp:SqlDataSource>

        
        <asp:SqlDataSource ID="SqlDataSource3" runat="server" 
            ConnectionString="<%$ ConnectionStrings:KPIConnectionString %>" 
            SelectCommand="SELECT [id_baseline_parametrs], [active], [name], [ab], [unit] FROM [Baseline_parametrs]">
        </asp:SqlDataSource>
        

        
        < <asp:GridView ID="GridView1" runat="server"
            DataSourceID="SqlDataSource4" DataKeyNames="name" AutoGenerateColumns="False"
            Font-Names="Trebuchet MS" Font-Size="Small" ForeColor="#333333" GridLines="None"
            RowStyle-CssClass="Row" AutoGenerateEditButton="True">

            <FooterStyle BackColor="#990000" Font-Bold="True" ForeColor="White" />
            <RowStyle BackColor="#FFFBD6" ForeColor="#333333" />
            <PagerStyle BackColor="#FFCC66" ForeColor="#333333" HorizontalAlign="Center" />
            <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="Navy" />
            <HeaderStyle BackColor="#28a4fb" Font-Bold="True" ForeColor="White" CssClass="Header" />
            <AlternatingRowStyle BackColor="White" />

            <Columns>
             
                <asp:BoundField DataField="name" HeaderText="name"  />
                
                
      
            </Columns>

        </asp:GridView>

       <asp:SqlDataSource ID="SqlDataSource4" runat="server" 
            ConnectionString="<%$ ConnectionStrings:KPIConnectionString %>" 
            SelectCommand="SELECT value name FROM Parametrs_data Baseline_parametrs">
        </asp:SqlDataSource>
    </form>
</body>
</html>
