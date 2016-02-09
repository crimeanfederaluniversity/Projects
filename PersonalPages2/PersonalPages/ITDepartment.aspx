<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ITDepartment.aspx.cs" MasterPageFile="~/Site.Master" Inherits="PersonalPages.ITDepartment" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
  
     <h3>Пожалуйста, отправьте Ваш вопрос </h3>


    Укажите госномер Вашего транспортного средства:<br />
    <asp:TextBox ID="TextBox4" runat="server" Width="193px"></asp:TextBox>
     <br />
    Укажите Ваш номер телефона:<br />
    <asp:TextBox ID="TextBox3" runat="server" Width="192px"></asp:TextBox>
    <br />
    <br />
    <asp:Button ID="Button1" runat="server" Text="Отправить заявку" Width="300px" OnClick="Button1_Click" />


</asp:Content>
