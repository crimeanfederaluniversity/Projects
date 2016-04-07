<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="StructureForm.aspx.cs" MasterPageFile="~/Site.Master" EnableEventValidation="false" Inherits="EDM.edmAdmin.StructureForm" %>


<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div style="margin-top:auto">
        <br />
        <br />
    <asp:Button ID="Button2" runat="server" Height="25px" OnClick="Button2_Click" Text="На главную" Width="124px" />
    <br />
    <br />
    Выберите струкутрное подразделение<br />
    &nbsp;<asp:DropDownList ID="DropDownList1" runat="server" Height="25px" Width="280px">
    </asp:DropDownList>
    &nbsp;<asp:Button ID="DeleteStructure" runat="server" Height="25px" Text="Удалить подразделение" Width="200px" OnClick="DeleteStructure_Click" />
    <br />
    <br />
    Укажите новое название<br />
    &nbsp;<asp:TextBox ID="NewNameBox" runat="server" Height="25px" Width="280px"></asp:TextBox>

    &nbsp;<asp:Button ID="AddNewName" runat="server" Height="25px" Text="Применить" Width="200px" OnClick="AddNewName_Click" />
    <br />
    <br />
    Прикрепить новое подразделение<br />
    &nbsp;<asp:TextBox ID="NewStructureBox" runat="server" Height="25px" Width="280px"></asp:TextBox>

    &nbsp;<asp:Button ID="AddNewStructure" runat="server" Height="25px" Text="Применить" Width="200px" OnClick="AddNewStructure_Click" />
        </div>
</asp:Content>
