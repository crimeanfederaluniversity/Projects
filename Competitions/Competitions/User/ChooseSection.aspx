<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ChooseSection.aspx.cs" Inherits="Competitions.User.ChooseSection" %>
<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">    <div>
    
        <asp:Button ID="GoBackButton" runat="server" OnClick="GoBackButton_Click" Text="Назад" />
        <br />
        <br />
        <asp:GridView ID="ApplicationGV" runat="server" AutoGenerateColumns="False">
            <Columns>
                <asp:BoundField DataField="ID" HeaderText="" Visible="false" />
                <asp:BoundField DataField="Name" HeaderText="Название" Visible="true" />
                <asp:BoundField DataField="Status" HeaderText="Состояние" Visible="true" />

                <asp:TemplateField HeaderText="Заполнение">
                    <ItemTemplate>
                        <asp:Button ID="FillButton" runat="server" CommandArgument='<%# Eval("ID") %>' CommandName="Select" OnClick="FillButtonClick" Text="Заполнить" Width="200px" />
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
</asp:Content>
