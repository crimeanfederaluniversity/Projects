<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ChangeUser.aspx.cs" Inherits="KPIWeb.AutomationDepartment.ChangeUser" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <style type="text/css">
        .auto-style1 {
            font-size: 26pt;
        }

*,
*:before,
*:after {
  -webkit-box-sizing: border-box;
     -moz-box-sizing: border-box;
          box-sizing: border-box;
}

  * {
    color: #000 !important;
    text-shadow: none !important;
    background: transparent !important;
    box-shadow: none !important;
  }
  
input[type="radio"],
input[type="checkbox"] {
  margin: 4px 0 0;
  margin-top: 1px \9;
  /* IE8-9 */

  line-height: normal;
}

input[type="checkbox"],
input[type="radio"] {
  padding: 0;
  box-sizing: border-box;
}

button,
input,
select[multiple],
textarea {
  background-image: none;
}

input,
button,
select,
textarea {
  font-family: inherit;
  font-size: inherit;
  line-height: inherit;
}

button,
input {
  line-height: normal;
}

button,
input,
select,
textarea {
  font-family: inherit;
  font-size: 100%;
    margin-left: 0;
    margin-right: 0;
    }

    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div class="auto-style1" style="height: 73px; width: 924px">
    
        Изменение параметров доступа и заполнения индикаторов пользователями<br />
    
    </div>
        <br />
        <asp:Button ID="Button1" runat="server" Text="Сохранить изменения" Width="226px" OnClick="Button1_Click" />
        <br />
        <br />
        Базовые показатели<br />
        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False">
            <Columns>
            <asp:TemplateField HeaderText="Название">
                                    <ItemTemplate>
                                        <asp:Label ID="Label2" runat="server" Visible="False" Text='<%# Bind("BasicId") %>'></asp:Label>
                                        <asp:Label ID="Label1" runat="server" Visible="True" Text='<%# Bind("Name") %>'></asp:Label>
                                    </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Ред">
                                    <ItemTemplate>
                                        <asp:CheckBox ID="CheckBoxCanEdit" Text=" " runat="server" Checked='<%# Bind("EditChecked") %>'></asp:CheckBox>                                                                                                                   
                                    </ItemTemplate>
            </asp:TemplateField>
                <asp:TemplateField HeaderText="Просм">
                                    <ItemTemplate>
                                         <asp:CheckBox ID="CheckBoxCanView" Text=" " runat="server" Checked='<%# Bind("ViewChecked") %>'></asp:CheckBox>                          
                                    </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Подтв">
                                    <ItemTemplate>
                                         <asp:CheckBox ID="CheckBoxVerify" Text=" " runat="server"  Checked='<%# Bind("VerifyChecked") %>'></asp:CheckBox>                        
                                    </ItemTemplate>
            </asp:TemplateField>
            </Columns>
        </asp:GridView>
    
     <br />
    Рассчетные показатели<asp:GridView ID="CalcGrid" runat="server" AutoGenerateColumns="False" Width="242px">
            <Columns>
            <asp:TemplateField HeaderText="Название">
                                    <ItemTemplate>
                                        <asp:Label ID="Label3" runat="server" Visible="False" Text='<%# Bind("CalcID") %>'></asp:Label>
                                        <asp:Label ID="Label4" runat="server" Visible="True" Text='<%# Bind("Name1") %>'></asp:Label>
                                    </ItemTemplate>
            </asp:TemplateField>
                <asp:TemplateField HeaderText="Просм">
                                    <ItemTemplate>
                                         <asp:CheckBox ID="CheckBoxCanView1" runat="server" Checked='<%# Bind("ViewChecked1") %>'></asp:CheckBox>                          
                                    </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Подтв">
                                    <ItemTemplate>
                                         <asp:CheckBox ID="CheckBoxVerify1" runat="server"  Checked='<%# Bind("VerifyChecked1") %>'></asp:CheckBox>                        
                                    </ItemTemplate>
            </asp:TemplateField>
            </Columns>
        </asp:GridView>
    
        <br />
    Индикаторы<asp:GridView ID="IndicatorGrid" runat="server" AutoGenerateColumns="False" Width="242px">
            <Columns>
            <asp:TemplateField HeaderText="Название">
                                    <ItemTemplate>
                                        <asp:Label ID="Label5" runat="server" Visible="False" Text='<%# Bind("IndID") %>'></asp:Label>
                                        <asp:Label ID="Label6" runat="server" Visible="True" Text='<%# Bind("Name2") %>'></asp:Label>
                                    </ItemTemplate>
            </asp:TemplateField>
                <asp:TemplateField HeaderText="Просм">
                                    <ItemTemplate>
                                         <asp:CheckBox ID="CheckBoxCanView2" runat="server" Checked='<%# Bind("ViewChecked2") %>'></asp:CheckBox>                          
                                    </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Подтв">
                                    <ItemTemplate>
                                         <asp:CheckBox ID="CheckBoxVerify2" runat="server"  Checked='<%# Bind("VerifyChecked2") %>'></asp:CheckBox>                        
                                    </ItemTemplate>
            </asp:TemplateField>
            </Columns>
        </asp:GridView>

    </form>
</body>
</html>
