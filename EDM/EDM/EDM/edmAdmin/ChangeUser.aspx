<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ChangeUser.aspx.cs" EnableEventValidation="false" MasterPageFile="~/Site.Master" Inherits="EDM.edmAdmin.ChangeUser1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <Table ID="RegisterTable">
        <tr>
            <td>Email
                <asp:RequiredFieldValidator runat="server" ControlToValidate="EmailTextBox" ErrorMessage="!" ForeColor="red" ID="RequiredFieldValidator1"/>
            </td>
            <td style="width: 130px">
                <asp:TextBox ID="EmailTextBox" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>Логин
                <asp:RequiredFieldValidator runat="server" ControlToValidate="LoginTextBox" ErrorMessage="!" ForeColor="red" ID="RequiredFieldValidator2"/>
            </td>
            <td style="width: 130px">
                <asp:TextBox ID="LoginTextBox" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>Имя
                <asp:RequiredFieldValidator runat="server" ControlToValidate="NameTextBox" ErrorMessage="!" ForeColor="red" ID="RequiredFieldValidator3"/>
            </td>
            <td style="width: 130px">
                <asp:TextBox ID="NameTextBox" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>Структура
                <asp:RequiredFieldValidator runat="server" ControlToValidate="StructTextBox" ErrorMessage="!" ForeColor="red" ID="RequiredFieldValidator4"/>
            </td>
            <td style="width: 130px">
                <asp:TextBox ID="StructTextBox" runat="server"></asp:TextBox>
                <br />
                <asp:DropDownList ID="DropDownList1" runat="server" Height="25px" Width="274px">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td>Пароль
                <asp:RequiredFieldValidator runat="server" ControlToValidate="TextBox" ErrorMessage="!" ForeColor="red" ID="RequiredFieldValidator5"/>
            </td>
            <td style="width: 130px">
                <asp:TextBox ID="TextBox" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>Повторите пароль
                <asp:RequiredFieldValidator runat="server" ControlToValidate="PasswordTextBox" ErrorMessage="!" ForeColor="red" ID="RequiredFieldValidator6"/>
            </td>
            <td style="width: 130px">
                <asp:TextBox ID="PasswordTextBox" runat="server"></asp:TextBox>
            </td>
        </tr>
    </Table>
    <br />
    <asp:Button ID="ChangeUserButton" runat="server" Height="50px" Text="Изменить" Width="300px" OnClick="ChangeUserButton_Click" />

</asp:Content>