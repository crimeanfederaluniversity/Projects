<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="FinishPage.aspx.cs" Inherits="Registration.Account.FinishPage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
     <h3>
         <asp:Label ID="Label1" runat="server" Text="Форма успешно отправлена! Ваши данные будут проверены отделом мониторинга и рейтингов. После подтверждения данных Вы получите письмо на указанную Вами почту с дальнейшими инструкциями. " Visible="False"></asp:Label>
     </h3>
     <h3>
         <asp:Label ID="Label2" runat="server" Text="Регистрация успешно завершена! Теперь вы можете войти в свою учетную запись. Для этого перейдите по ссылке ..., логином является адрес Вашей почты, пароль - указанный Вами на странице подтверждения регистрации." Visible="False"></asp:Label>
     </h3>
</asp:Content>
