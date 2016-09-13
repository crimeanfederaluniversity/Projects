<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="FinishPage.aspx.cs" Inherits="Registration.Account.FinishPage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
     <h3>
         <asp:Label ID="Label1" runat="server" Text="Форма успешно отправлена! Ваши данные будут проверены отделом мониторинга и рейтингов. После подтверждения данных Вы получите письмо на указанную Вами почту с дальнейшими инструкциями. " Visible="False"></asp:Label>
     </h3>
     <h3>
         <asp:Label ID="Label2" runat="server" Text="Регистрация успешно завершена! Начать работать с системой вы сможете с 01.10.2016. Логином является адрес Вашей почты, пароль - указанный Вами на странице подтверждения регистрации. Ожидайте письмо о начале сбора данных с сылкой на сайт!" Visible="False"></asp:Label>
     </h3>
</asp:Content>
