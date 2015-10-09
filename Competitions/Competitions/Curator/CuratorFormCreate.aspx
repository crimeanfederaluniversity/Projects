<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CuratorFormCreate.aspx.cs" Inherits="Competitions.Curator.CuratorFormCreate" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <br />
        <asp:Button ID="GoBackButton" runat="server"  Text="Назад" Width="131px" style="height: 26px" OnClick="GoBackButton_Click" />
        <br />
        <h2><span style="font-size: 30px">Выберите формы, которые необходимы в шаблоне: </span></h2>
         <asp:CheckBoxList ID="CheckBoxList1" runat="server">
         </asp:CheckBoxList>
        <br />
    <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Сохранить" />
        <br /> 
</asp:Content>
