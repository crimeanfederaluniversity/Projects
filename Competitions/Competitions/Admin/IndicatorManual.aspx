<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="IndicatorManual.aspx.cs" Inherits="Competitions.Admin.IndicatorManual" %>

<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
    <asp:Button ID="Button2" runat="server" OnClick="Button2_Click" Text="Назад" />
    <div>
        <h2><span style="font-size: 20px"> Целевые показатели Программы развития:</span></h2>
         <asp:GridView ID="IndicatorGV" runat="server" AutoGenerateColumns="False">
            <Columns>
                <asp:BoundField DataField="ID"   HeaderText="Номер" Visible="true" />
                <asp:BoundField DataField="IndicatorName"   HeaderText="Название индикатора" Visible="true" />
                <asp:BoundField DataField="IndicatorValue"   HeaderText="Значение" Visible="true" />
                    </Columns>
        </asp:GridView>
        
        <br />
        Добавить новый целевой показатель:<br />
        <asp:TextBox ID="TextBox1" runat="server" TextMode="MultiLine" Width="401px"></asp:TextBox>
        <br />
        Значение:<br />
        <asp:TextBox ID="TextBox2" runat="server" Width="145px"></asp:TextBox>
        <br />
        <asp:Button ID="Button1" runat="server" Text="Сохранить" OnClick="Button1_Click1" />
        
    </div>
        </asp:Content>