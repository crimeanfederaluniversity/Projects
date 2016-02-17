<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="1CForm.aspx.cs"  MasterPageFile="~/Site.Master" Inherits="PersonalPages._1CForm" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    
     <span style="font-size: large">Заявка на доступ в систему 1С
    </span>
     <br />
     Пожалуйста, укажите Ваш номер телефона:<br />
     <asp:TextBox ID="TextBox1" runat="server" Height="23px" Width="268px"></asp:TextBox>

     <br />
     Опишите, чем вызвана необходимость открытия доступа<br />
     <br />
     <asp:TextBox ID="TextBox2" TextMode="MultiLine" runat="server" Height="29px" Width="270px"></asp:TextBox>
     <br />
     <br />
     <asp:Button ID="Button1" runat="server" Height="40px" OnClick="Button1_Click" Text="Отправить" Width="276px" />
     <br />

</asp:Content>