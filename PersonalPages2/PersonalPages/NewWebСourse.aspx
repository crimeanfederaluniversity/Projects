<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="NewWebСourse.aspx.cs" Inherits="PersonalPages.NewWebСourse" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h3>&nbsp;&nbsp; Заявка на создание нового электронного курса в ЭСУО MOODLE</h3>
    <br />&nbsp;&nbsp; Название курса:<br />
    &nbsp;&nbsp;
    <asp:TextBox ID="TextBox1" runat="server" Height="22px" TextMode="MultiLine" Width="500px"></asp:TextBox>
    <br />
    <br />&nbsp;&nbsp; Укажите руководителя данного курса:<br />
    &nbsp;&nbsp;
    <asp:TextBox ID="TextBox2" runat="server" Height="22px" TextMode="MultiLine" Width="500px"></asp:TextBox>
    <br />
    <br />
    &nbsp;&nbsp;
    Укажите преподавателя данного курса:<br />
    &nbsp;&nbsp;
    <asp:TextBox ID="TextBox4" runat="server" Height="22px" TextMode="MultiLine" Width="500px"></asp:TextBox>
    <br />
    <br />&nbsp;&nbsp; Укажите Ваш номер телефона:<br />
    &nbsp;&nbsp;
    <asp:TextBox ID="TextBox3" runat="server" Width="191px"></asp:TextBox>
    <br />
    <br />
    &nbsp;&nbsp;
    Прикрепите файл программы Вашего учебного курса:<asp:FileUpload ID="FileUpload1" runat="server" Width="363px" />
    <br />
    <br />
    &nbsp;&nbsp;
    <asp:Button ID="Button1" runat="server" Text="Отправить заявку" Width="300px" OnClick="Button1_Click" />
</asp:Content>
