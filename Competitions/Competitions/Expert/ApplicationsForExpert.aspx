<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ApplicationsForExpert.aspx.cs" Inherits="Competitions.Expert.ApplicationsForExpert" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
        <br />
        <asp:Button ID="GoBackButton" runat="server" OnClick="GoBackButton_Click" Text="Назад" />
        <br />
    <h2><span style="font-size: 30px">Заявки, ожидающие Вашей экспертной оценки: </span></h2>
        <br />
        <asp:GridView ID="ApplicationGV" runat="server" AutoGenerateColumns="False">
            <Columns>
                <asp:BoundField DataField="ID"   HeaderText="" Visible="false" />
                <asp:BoundField DataField="Name"   HeaderText="Название" Visible="true" />
                <asp:BoundField DataField="CompetitionName"   HeaderText="Конкурс" Visible="true" />    
                 
                <asp:TemplateField HeaderText="Получить сгенерированный документ">
                        <ItemTemplate>
                            <asp:Button ID="GetDocButton" runat="server" CommandName="Select" Text="Загрузить" CommandArgument='<%# Eval("ID") %>' Width="200px" OnClick="GetDocButtonClick"/>
                        </ItemTemplate>
                </asp:TemplateField>    
                <asp:TemplateField HeaderText="Оценить">
                        <ItemTemplate>
                            <asp:Button ID="EvaluateButton" runat="server" CommandName="Select" Text="Оценить" CommandArgument='<%# Eval("ID") %>' Width="200px" OnClick="EvaluateButtonClick"/>
                        </ItemTemplate>
                </asp:TemplateField>         
                    
            </Columns>
        </asp:GridView>
</asp:Content>
