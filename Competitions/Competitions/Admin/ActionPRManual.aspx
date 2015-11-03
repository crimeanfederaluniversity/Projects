<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="ActionPRManual.aspx.cs" Inherits="Competitions.Admin.ActionPRManual" %>
<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
    <asp:Button ID="Button2" runat="server" OnClick="Button2_Click" Text="Назад" />
    <div>
        <h2><span style="font-size: 20px">Задачи Программы развития:</span></h2>
        
           <asp:GridView ID="ActionGV" runat="server" AutoGenerateColumns="False">
            <Columns>
                <asp:BoundField DataField="ID"   HeaderText="Номер" Visible="false" />
                <asp:BoundField DataField="ActionPR"   HeaderText="Название задачи" Visible="true" />
                    </Columns>
        </asp:GridView>
        <br />
        Добавить новую задачу:<br />
        <asp:TextBox ID="TextBox1" runat="server" TextMode="MultiLine" Width="432px"></asp:TextBox>
        <br />
        <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Сохранить" />
    </div>
        </asp:Content>
