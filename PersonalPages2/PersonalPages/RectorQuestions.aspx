<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="RectorQuestions.aspx.cs" Inherits="PersonalPages.RectorQustions" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h3>Для того, чтобы обратиться с вопросом к ректору,пожалуйста, заполните данную форму:</h3>


    <br />
    Опишите Ваше обращение к ректору:<br />
    &nbsp;<asp:TextBox ID="TextBox1" runat="server" Height="40px" TextMode="MultiLine" Width="800px"></asp:TextBox>
    <br />
    <br />
    Укажите руководителей, к которым Вы обращались по данному вопросу:<br />
    &nbsp;<asp:TextBox ID="TextBox2" runat="server" Height="39px" TextMode="MultiLine" Width="800px"></asp:TextBox>
    <br />
    <br />
    Укажите Ваш номер телефона:<br />
    <asp:TextBox ID="TextBox3" runat="server" Width="256px" Height="25px"></asp:TextBox>
    <br />
    <br />
    &nbsp;<asp:Button ID="Button1" runat="server" Text="Отправить обращение" Width="300px" OnClick="Button1_Click" />

</asp:Content>
