<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ApplicationSovetexpertEdit.aspx.cs" Inherits="Competitions.Admin.ApplicationSovetexpertEdit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server"><br />
    <asp:Button ID="GoBackButton" runat="server" Text="Назад" OnClick="GoBackButton_Click" Width="157px" />
    <br />
       Состав экспертного совета (определяется в настройках конкурса):
            <br />
            <asp:GridView ID="sovetExpertsGV" runat="server" AutoGenerateColumns="False" >
                <Columns>
                    <asp:BoundField DataField="ID" HeaderText="" Visible="false" />
                    <asp:BoundField DataField="Name" HeaderText="Имя" Visible="true" />
                </Columns>
            </asp:GridView>
            <br />
            <asp:Button ID="LinkExpertsButton" runat="server" OnClick="LinkExpertsButtonClick" Text="Прикрепить cовет к заявке" />
</asp:Content>
