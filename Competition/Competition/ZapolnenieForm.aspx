<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ZapolnenieForm.aspx.cs" Inherits="Competition.ZapolnenieForm" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
     <h2><span style="font-size: 30px">Заполнение заявки на конкурс</span></h2>
    <form id="form1" runat="server">
    <div>
    
        <br />
        <br />
        <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Загрузить формы" />
    
        <br />
    
    </div>
        <br />
        <asp:GridView ID="GridView1" AutoGenerateColumns="False" OnRowDataBound="Questions_RowDataBound" runat="server">
             <Columns>

                    <asp:BoundField DataField="ID_Question" HeaderText="Код вопроса" Visible="false" />
                    <asp:BoundField DataField="Question" HeaderText="Вопрос" Visible="true" />

                   
                  <asp:TemplateField Visible="true"   HeaderText="Ответ">
                        <ItemTemplate >
                         <asp:TextBox ID="Answer" style="text-align:center" BorderWidth="0" Height="95%" runat="server" Text='<%# Bind("Answer") %>'></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>  
             
                
<asp:TemplateField Visible="false" HeaderText="Id" >
<ItemTemplate>
<asp:label ID="Id" runat="server" Visible="false" Text='<%# Bind("Id") %>'></asp:label>
</ItemTemplate>
</asp:TemplateField>

                 </Columns>
        </asp:GridView>
        <br />
        <asp:Button ID="Button2" runat="server" OnClick="Button2_Click" Text="Сохранить" />
        <br />
    </form>
</body>
</html>
