<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="1CForm.aspx.cs"  MasterPageFile="~/Site.Master" Inherits="PersonalPages._1CForm" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    
    <h3>Заявка на заведение нового пользователя в системе 1С</h3>
   
     <br />
     Укажите Ваш номер телефона:<br />
     <asp:TextBox ID="TextBox1" runat="server" Height="25px" Width="300px"></asp:TextBox>

     <br />
     Опишите, чем вызвана необходимость открытия доступа<br />
     <asp:TextBox ID="TextBox2" TextMode="MultiLine" runat="server" Height="40px" Width="800px"></asp:TextBox>
     <br />
     <br />
     <asp:Button ID="Button1" runat="server" Height="40px" OnClick="Button1_Click" Text="Отправить" Width="276px" />
     <br />

</asp:Content>