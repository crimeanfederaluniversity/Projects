<%@ Page Title="Contact" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Contact.aspx.cs" Inherits="PersonalPages.Contact" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
     <h2><%: Title %></h2>
    <h3>Крымский федеральный университет имени В. И. Вернадского</h3>
    <address>
        Департамент управления качеством и проектных решений<br />
        Центр развития единого информационного пространства</address>
    <address>
        <!--<abbr title="Телефон">P:</abbr>
        временно отсутствует -->
        По вопросам заполнения форм отчетности обращайтесь по телефону: +7-978-823-14-32</address>
    <address>
        Техническая поддержка: +7-978-117-53-98</address>

    <address>
        E-mail адрес:  <a href="mailto:otdel-avtomatizatsii-kfu@yandex.ru?subject=Обращение в техподдержку">otdel-avtomatizatsii-kfu@yandex.ru</a><br />
        
     </address>
    Отправить вопрос в диспетчерскую
     <br />
     <asp:TextBox ID="TextBox1" runat="server" TextMode="MultiLine" Height="27px" Width="310px"></asp:TextBox>
     <br />
    Укажите Ваш номер телефона:<br />
    <asp:TextBox ID="TextBox3" runat="server" Width="192px"></asp:TextBox>
     <br />
     <br />
     <asp:Button ID="Button1" runat="server" Height="37px" OnClick="Button1_Click" Text="Отправить" Width="197px" />
</asp:Content>
