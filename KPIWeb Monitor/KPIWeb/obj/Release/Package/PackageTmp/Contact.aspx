<%@ Page Title="Обратная связь" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Contact.aspx.cs" Inherits="KPIWeb.Contact" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <h2><%: Title %></h2>
    <h3>Крымский федеральный университет имени В. И. Вернадского</h3>
    <address>
        Департамент управления качеством и проектных решений<br />
        Отдел автоматизации программы развития</address>
    <address>
        <!--<abbr title="Телефон">P:</abbr>
        временно отсутствует -->
        По вопросам заполнения форм отчетности обращайтесь по телефону: +7-978-823-14-32</address>
    <address>
        Техническая поддержка: +7-978-117-53-98</address>

    <address>
        E-mail адрес:  <a href="mailto:otdel-avtomatizatsii-kfu@yandex.ru?subject=Обращение в техподдержку">otdel-avtomatizatsii-kfu@yandex.ru</a><br />
        
    </address>
</asp:Content>
