<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="NewCuratorTZ.aspx.cs" Inherits="Competitions.Curator.NewCuratorTZ" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
         <div>   
        <br />
        <asp:Button ID="GoBackButton" runat="server"  Text="Назад" Width="131px" style="height: 26px" OnClick="GoBackButton_Click" />
        <h2><span style="font-size: 30px">Cоздание нового конкурса в системе: </span></h2>
        <br /> 
        Название
        <br />
        <asp:TextBox ID="NameTextBox" runat="server" Height="28px" Width="603px"></asp:TextBox>
        <br />
        Шифр конкурса<br />
        <asp:TextBox ID="DescriptionTextBox" runat="server" style="margin-bottom: 0px" Width="205px"></asp:TextBox>
        <br />
        <br />
        Бюджет конкурса<br />
        <asp:TextBox ID="BudjetTextBox" runat="server" Width="205px"></asp:TextBox>
        <br />
        Дата начала конкурса<br />
        <asp:Calendar ID="Calendar1" runat="server"></asp:Calendar>
        Дата окончания конкурса<asp:Calendar ID="Calendar2" runat="server"></asp:Calendar>
         <br />
         <asp:CheckBoxList ID="CheckBoxList1" runat="server">
         </asp:CheckBoxList>
         <br />
         <asp:Button ID="CreateSaveButton" runat="server" OnClick="CreateSaveButtonClick" Text="Сохранить" Width="131px" />
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
         <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Далее" />
        <br />
             </div>
</asp:Content>
