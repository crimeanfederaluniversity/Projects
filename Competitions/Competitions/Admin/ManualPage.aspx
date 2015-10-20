<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="ManualPage.aspx.cs" Inherits="Competitions.Admin.ManualPage" %>
<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
    <div>
        <asp:Button ID="Button4" runat="server" Height="25px" Text="Назад" OnClick="Button4_Click" />
          <h2>
              
              <span style="font-size: 20px"> Справочники:</span></h2>
        <p>
            <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Мероприятия Программы развития" Width="300px" />
        </p>
        <p>
            <asp:Button ID="Button2" runat="server" OnClick="Button2_Click" Text="Целевые показатели" Width="300px" />
        </p>
        <p>
            <asp:Button ID="Button3" runat="server" OnClick="Button3_Click" Text="Задачи Программы развития" Width="300px" />
        </p>
        <br />
        
    </div>
        </asp:Content>
