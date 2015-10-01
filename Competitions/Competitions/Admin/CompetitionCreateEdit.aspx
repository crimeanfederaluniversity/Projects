<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="CompetitionCreateEdit.aspx.cs" Inherits="Competitions.Admin.NewCompetition" %>
<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
            
    <div>   
        <br />
        <asp:Button ID="GoBackButton" runat="server"  Text="Назад" Width="131px" style="height: 26px" OnClick="GoBackButton_Click" />
        <br />
        <h2><span style="font-size: 30px">Cоздание нового конкурса в системе: </span></h2>
        <br /> 
        Название
        <br />
        <asp:TextBox ID="NameTextBox" runat="server" Height="28px" Width="603px"></asp:TextBox>
        <br />
        <br />
        Шифр конкурса<br />
        <asp:TextBox ID="DescriptionTextBox" runat="server" style="margin-bottom: 0px" Width="205px"></asp:TextBox>
        <br />
        <br />
        Бюджет конкурса<br />
        <asp:TextBox ID="BudjetTextBox" runat="server" Width="205px"></asp:TextBox>
        <br />
        <br />
        Куратор<br />
        <asp:DropDownList ID="DropDownList1" runat="server" Height="16px" Width="216px">
        </asp:DropDownList>
        <br />
        <br />
        Дата начала конкурса<br />
        <asp:Calendar ID="Calendar1" runat="server"></asp:Calendar>
        <br />
        Дата окончания конкурса<asp:Calendar ID="Calendar2" runat="server"></asp:Calendar>
        <br />
        Файл шаблона<asp:FileUpload ID="FileUpload1" runat="server" />
        <asp:LinkButton ID="LinkButton1" runat="server" OnClick="LinkButton1_Click">LinkButton</asp:LinkButton>
        <br />
        <br />
        <asp:Button ID="CreateSaveButton" runat="server" OnClick="CreateSaveButtonClick" Text="Сохранить" Width="131px" />
        <br />
    
    </div>
</asp:Content>