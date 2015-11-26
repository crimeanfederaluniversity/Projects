<%@ Page Language="C#" Title="Добавление структуры" MasterPageFile="~/Site.Master" AutoEventWireup="true" EnableViewStateMac="false" CodeBehind="AddLevel.aspx.cs" Inherits="KPIWeb.AutomationDepartment.AddLevel" %>

<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
        <h2>Форма редактирования подразделений</h2>
        &nbsp;
        <asp:Label ID="Label1" runat="server" Text="Академия"></asp:Label>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="Label2" runat="server" Text="Факультет"></asp:Label>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="Label3" runat="server" Text="Кафедра"></asp:Label>
    
        <br />
    


        &nbsp;<asp:DropDownList ID="DropDownList1" runat="server" AutoPostBack="True" Height="25px" OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged" Width="200px">
        </asp:DropDownList>
        &nbsp;<asp:DropDownList ID="DropDownList2" runat="server" AutoPostBack="True" Height="25px" OnSelectedIndexChanged="DropDownList2_SelectedIndexChanged" Width="200px">
        </asp:DropDownList>
        &nbsp;<asp:DropDownList ID="DropDownList3" runat="server" AutoPostBack="True" Height="25px" OnSelectedIndexChanged="DropDownList3_SelectedIndexChanged" Width="200px">
        </asp:DropDownList>
        <br />
        <br />
        &nbsp;<asp:TextBox ID="TextBox1" runat="server" Height="150px" TextMode="MultiLine" Width="200px"></asp:TextBox>
        &nbsp;<asp:TextBox ID="TextBox2" runat="server" Height="150px" TextMode="MultiLine" Width="200px"></asp:TextBox>
        &nbsp;<asp:TextBox ID="TextBox3" runat="server" Height="150px" TextMode="MultiLine" Width="200px"></asp:TextBox>
        <br />
        <br />
        &nbsp;<asp:Button ID="Button1" runat="server" Height="25px" OnClick="Button1_Click" Text="Добавить академию" Width="200px" />
        &nbsp;<asp:Button ID="Button2" runat="server" Height="25px" OnClick="Button2_Click" Text="Добавить факультет" Width="200px" />
        &nbsp;<asp:Button ID="Button3" runat="server" Height="25px" OnClick="Button3_Click" Text="Добавить кафедру" Width="200px" />
        <br />
        <br />
        <br />
        &nbsp;<asp:Button ID="Button6" runat="server" Height="25px" OnClick="Button6_Click1" Text="Изменить/удалить" Width="200px" Enabled="False" />
        &nbsp;<asp:Button ID="Button7" runat="server" Height="25px" Text="Изменить/удалить" Width="200px" OnClick="Button7_Click" Enabled="False" />
        &nbsp;<asp:Button ID="Button8" runat="server" Height="25px" Text="Изменить/удалить" Width="200px" OnClick="Button8_Click" Enabled="False" />
        <br />
        <br />
        &nbsp;<asp:TextBox ID="TextBox4" runat="server" Width="200px" class="tb1" Height="50px" TextMode="MultiLine"></asp:TextBox>
        &nbsp;<asp:TextBox ID="TextBox5" runat="server" Width="200px" class="tb1" Height="50px" TextMode="MultiLine"></asp:TextBox>
        &nbsp;<asp:TextBox ID="TextBox6" runat="server" Width="200px" class="tb1" Height="50px" TextMode="MultiLine"></asp:TextBox>
        <br />
        &nbsp;<asp:Label ID="Label4" runat="server" Text="Активный"></asp:Label>
        <asp:CheckBox ID="CheckBox1" Text=" " runat="server" Enabled="False" />
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;<asp:Label ID="Label5" runat="server" Text="Активный"></asp:Label>
        <asp:CheckBox ID="CheckBox2" Text=" " runat="server" Enabled="False" />
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Label ID="Label6" runat="server" Text="Активный"></asp:Label>
        <asp:CheckBox ID="CheckBox3" Text=" " runat="server" Enabled="False" />
        <br />
        <br />
        &nbsp;<asp:Button ID="Button9" runat="server" Height="25px" OnClick="Button9_Click" Text="Сохранить" Width="200px" Enabled="False" />
        &nbsp;<asp:Button ID="Button10" runat="server" Height="25px" Text="Сохранить" Width="200px" OnClick="Button10_Click" Enabled="False" />
        &nbsp;<asp:Button ID="Button11" runat="server" Height="25px" Text="Сохранить" Width="200px" OnClick="Button11_Click" Enabled="False" />
        <br />
        <br />
        &nbsp;
        <asp:Button ID="Button5" runat="server" OnClick="Button5_Click" Text="Меню администрирования" Width="600px" />

        <br />
        <br />
        <br />

</asp:Content>
