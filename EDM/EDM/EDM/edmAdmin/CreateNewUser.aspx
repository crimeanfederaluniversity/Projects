<%@ Page Title="Создание нового пользователя" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CreateNewUser.aspx.cs" Inherits="EDM.edmAdmin.CreateNewUser" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">


    <Table ID="RegisterTable">
        <tr>
            <td>
                Email
                <asp:RequiredFieldValidator runat="server" ControlToValidate="EmailTextBox" ErrorMessage="!" ForeColor="red"/>
            </td>
            <td style="width: 130px">
                <asp:TextBox ID="EmailTextBox" runat="server"></asp:TextBox>
            </td>
        </tr>       
                <tr>
            <td>
                Логин
                <asp:RequiredFieldValidator runat="server" ControlToValidate="LoginTextBox" ErrorMessage="!" ForeColor="red"/>
            </td>
            <td style="width: 130px">
                    <asp:TextBox ID="LoginTextBox" runat="server"></asp:TextBox>
            </td>
        </tr>
        
                <tr>
            <td>
                Имя
                <asp:RequiredFieldValidator runat="server" ControlToValidate="NameTextBox" ErrorMessage="!" ForeColor="red"/>
            </td>
            <td style="width: 130px">
                <asp:TextBox ID="NameTextBox" runat="server"></asp:TextBox>
            </td>
        </tr>
        
                <tr>
            <td>
                Структура
                <asp:RequiredFieldValidator runat="server" ControlToValidate="StructTextBox" ErrorMessage="!" ForeColor="red"/>
            </td>
            <td style="width: 130px">
                <asp:TextBox ID="StructTextBox" runat="server"></asp:TextBox>
                <br />
                <asp:DropDownList ID="DropDownList1" runat="server" Height="25px" Width="274px">
                </asp:DropDownList>
            </td>
        </tr>
                  
                <tr>
            <td>
                Пароль
                <asp:RequiredFieldValidator runat="server" ControlToValidate="PasswordTextBox" ErrorMessage="!" ForeColor="red"/>
            </td>
            <td style="width: 130px">
                <asp:TextBox ID="PasswordTextBox" runat="server"></asp:TextBox>
            </td>
        </tr>
           <tr>
            <td>
               Повторите пароль
                <asp:RequiredFieldValidator runat="server" ControlToValidate="PasswordConfirmTextBox" ErrorMessage="!" ForeColor="red"/>
            </td>
            <td style="width: 130px">
                <asp:TextBox ID="PasswordConfirmTextBox" runat="server"></asp:TextBox>
            </td>
        </tr>
    </Table>
   

    
    
    

    <br />
    <asp:Button ID="AddUserButton" runat="server" Height="50px" Text="Создать" Width="300px" OnClick="AddUserButton_Click" />
    <br />
   

    
    
    

</asp:Content>
