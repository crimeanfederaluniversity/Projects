<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SmetaForm.aspx.cs" Inherits="Competition.SmetaForm" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        Форма заполнения сметы<br />
        <asp:GridView ID="GridView1" AutoGenerateColumns="false" runat="server">
             <Columns>
                   <asp:TemplateField HeaderText="Номер" HeaderStyle-HorizontalAlign="Center"  HeaderStyle-VerticalAlign="Middle" Visible = "true" >
                        <ItemTemplate> 
                            <asp:Label ID="ID_Value" runat="server" Text='<%# Bind("ID_Value") %>'  Visible="True"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Наименование" HeaderStyle-HorizontalAlign="Center"  HeaderStyle-VerticalAlign="Middle" Visible = "true" >
                        <ItemTemplate> 
                            <asp:TextBox ID="Name_state" runat="server" Text='<%# Bind("Name_state") %>'  Visible="True"></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>
                 <asp:TemplateField HeaderText="Значение" HeaderStyle-HorizontalAlign="Center"  HeaderStyle-VerticalAlign="Middle" Visible = "true" >
                        <ItemTemplate> 
                            <asp:TextBox ID="Value" runat="server" Text='<%# Bind("Value") %>'  Visible="True"></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>
                 </Columns>
        </asp:GridView>
        <br />
        <asp:Button ID="Button1" runat="server" Text="Сохранить" Width="101px" OnClick="Button1_Click" />
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Button ID="Button2" runat="server" Text="Добавить строку" Width="128px" OnClick="Button2_Click" />
    </form>
</body>
</html>
