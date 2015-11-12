<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ChooseApplication.aspx.cs" Inherits="Competitions.Expert.Applications" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <br />
    <asp:Button ID="GoBackButton" runat="server" OnClick="GoBackButton_Click" Text="Назад" />
    <h2><span style="font-size: 30px">Заявки, имеющие экспертную оценку: </span></h2>
    <br />
    <asp:GridView ID="ApplicationGV" runat="server" AutoGenerateColumns="False">
            <Columns>
                <asp:BoundField DataField="ID"   HeaderText="Код заявки" Visible="true" />
                <asp:BoundField DataField="Name"   HeaderText="Название" Visible="true" />
                <asp:BoundField DataField="Description"   HeaderText="Описание" Visible="false" />
                <asp:BoundField DataField="Competition"   HeaderText="Конкурс" Visible="true" />
                <asp:BoundField DataField="Autor"   HeaderText="Автор" Visible="false" />     
                 <asp:TemplateField HeaderText="Cкачать заявку">
                        <ItemTemplate>
                            <asp:Button ID="GetApplicationButton" runat="server" CommandName="Select" Text="Скачать" CommandArgument='<%# Eval("ID") %>' Width="150px" OnClick="GetApplicationButtonClick"/>
                        </ItemTemplate>
                </asp:TemplateField>       
                   <asp:TemplateField HeaderText="Cкачать экспертную оценку">
                        <ItemTemplate>
                            <asp:Button ID="GetExpertPointButton" runat="server" CommandName="Select" Text="Скачать" CommandArgument='<%# Eval("ID") %>' Width="150px" OnClick="GetExpertPointButtonClick"/>
                        </ItemTemplate>
                </asp:TemplateField>            
            </Columns>
        </asp:GridView>
</asp:Content>
