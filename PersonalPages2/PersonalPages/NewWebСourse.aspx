<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="NewWebСourse.aspx.cs" Inherits="PersonalPages.NewWebСourse" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h3>Заявка на создание нового электронного курса в ЭСУО MOODLE</h3>
    <br />Название курса:<br />
    <asp:TextBox ID="TextBox1" runat="server" Height="22px" TextMode="MultiLine" Width="500px"></asp:TextBox>
    <br />
    <br />Укажите руководителя данного курса:<br />
    <asp:TextBox ID="TextBox2" runat="server" Height="22px" TextMode="MultiLine" Width="500px"></asp:TextBox>
    <br />
    <br />
    Укажите преподавателя данного курса:<br />
    <asp:TextBox ID="TextBox4" runat="server" Height="22px" TextMode="MultiLine" Width="500px"></asp:TextBox>
    <br />
    <br />Укажите Ваш номер телефона:<br />
    <asp:TextBox ID="TextBox3" runat="server"></asp:TextBox>
    <br />
    <br />
    Прикрепите файл программы Вашего учебного курса:<asp:FileUpload ID="FileUpload1" runat="server" Width="363px" />
    <br />
    <br />
    <asp:Button ID="Button1" runat="server" Text="Отправить заявку" Width="300px" OnClick="Button1_Click" />
</asp:Content>
