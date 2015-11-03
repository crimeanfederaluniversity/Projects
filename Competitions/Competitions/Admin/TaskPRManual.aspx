<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="TaskPRManual.aspx.cs" Inherits="Competitions.Admin.TaskPRManual" %>

<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
    <asp:Button ID="Button2" runat="server" OnClick="Button2_Click" Text="Назад" />
    <div>
        <h2><span style="font-size: 20px">Мероприятия Программы развития:</span></h2>
            <asp:GridView ID="TaskGV" runat="server" AutoGenerateColumns="False">
            <Columns>
                <asp:BoundField DataField="ID"   HeaderText="Номер" Visible="False" />
                <asp:BoundField DataField="TaskPR"   HeaderText="Наименование мероприятия" Visible="true" />             
                    </Columns>
        </asp:GridView>        
        <br />
        Добавить новое мероприятие:<br />
        <asp:TextBox ID="TextBox1" runat="server" TextMode="MultiLine" Width="401px"></asp:TextBox>
        <br />
        <asp:Button ID="Button1" runat="server" Text="Сохранить" />
        <br />
        
    </div>
        </asp:Content>