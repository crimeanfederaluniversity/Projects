<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Form_User_and_BP.aspx.cs" Inherits="WebApplication3.WebForm3" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <style type="text/css">
        #form1 {
            height: 302px;
            width: 932px;
        }
    </style>
</head>
<body style="height: 109px; width: 928px;">
    <form id="form1" runat="server">
    <div>
    
    </div>
        <asp:Label ID="Label1" runat="server" Text="ID пользователя         "></asp:Label>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Label ID="Label2" runat="server" Text="     Активный?"></asp:Label>
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Label ID="Label3" runat="server" Text="Login"></asp:Label>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Label ID="Label4" runat="server" Text="Пароль"></asp:Label>
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Label ID="Label5" runat="server" Text="E-mail"></asp:Label>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Label ID="Label6" runat="server" Text="Дата регистрации"></asp:Label>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <p>
            <asp:TextBox ID="TextBox4" runat="server" Width="109px"></asp:TextBox>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:CheckBox ID="CheckBox1" runat="server" />
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;
            <asp:TextBox ID="TextBox2" runat="server" Height="16px" Width="101px"></asp:TextBox>
&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:TextBox ID="TextBox3" runat="server" Width="117px" Height="16px"></asp:TextBox>
&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:TextBox ID="TextBox6" runat="server" Width="137px" Height="16px"></asp:TextBox>
&nbsp;&nbsp;&nbsp;
            <asp:TextBox ID="TextBox7" runat="server" Width="128px" Height="16px"></asp:TextBox>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        </p>
        <p>
            &nbsp;<asp:Button ID="Button7" runat="server" Text="Поиск" Width="157px" />
        </p>
        <p>
            Права редактирования&nbsp;&nbsp;
        </p>
        <p style="height: 41px; width: 924px">
            <asp:DropDownList ID="DropDownList2" runat="server" Height="16px" Width="130px" style="margin-top: 3px">
            </asp:DropDownList>
&nbsp;&nbsp;
            <asp:DropDownList ID="DropDownList3" runat="server" Height="16px" style="margin-top: 0px; margin-left: 4px;" Width="130px">
            </asp:DropDownList>
&nbsp;&nbsp;
            <asp:DropDownList ID="DropDownList4" runat="server" Height="16px" style="margin-top: 0px" Width="130px">
            </asp:DropDownList>
&nbsp;&nbsp;
            <asp:DropDownList ID="DropDownList1" runat="server" Height="16px" Width="130px">
            </asp:DropDownList>
        &nbsp; <asp:Button ID="Button4" runat="server" Text="Добавить" Height="24px" Width="85px" />
&nbsp;&nbsp;
            <asp:Button ID="Button5" runat="server" Text="Изменить" Height="25px" Width="85px" />
&nbsp;&nbsp;
            <asp:Button ID="Button6" runat="server" Text="Удалить" Width="85px" Height="25px" />
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        </p>
        <p>
            Права просмотра&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            </p>
        <p>
            &nbsp;<asp:DropDownList ID="DropDownList7" runat="server" Height="16px" style="margin-top: 0px" Width="130px">
            </asp:DropDownList>
&nbsp;&nbsp;&nbsp;
            <asp:DropDownList ID="DropDownList8" runat="server" Height="16px" Width="130px" style="margin-left: 0px">
            </asp:DropDownList>
        &nbsp;&nbsp;
            <asp:DropDownList ID="DropDownList9" runat="server" Height="16px" Width="130px" style="margin-left: 0px">
            </asp:DropDownList>
        &nbsp;&nbsp;
            <asp:DropDownList ID="DropDownList10" runat="server" Height="16px" Width="130px" style="margin-left: 0px">
            </asp:DropDownList>
        &nbsp; <asp:Button ID="Button1" runat="server" Text="Добавить" Height="25px" Width="85px" />
&nbsp;&nbsp;
            <asp:Button ID="Button2" runat="server" Text="Изменить" Height="25px" Width="85px" />
&nbsp;&nbsp;
            <asp:Button ID="Button3" runat="server" Text="Удалить" Width="85px" Height="25px" />
&nbsp;&nbsp;&nbsp;&nbsp;
        </p>
        <p>
            &nbsp;
        </p>
        <asp:Button ID="Button8" runat="server" Text="Применить" Width="168px" />
    </form>
    <p style="width: 932px">
        &nbsp;</p>
</body>
</html>
