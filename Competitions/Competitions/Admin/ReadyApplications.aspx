<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="ReadyApplications.aspx.cs" Inherits="Competitions.Admin.ReadyApplications" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <asp:Button ID="GoBackButton" runat="server" OnClick="Button1_Click" Text="Назад" Width="147px" />
    <br />
    <h2><span style="font-size: 20px">Список обработанных заявок:</span></h2>
    <br />
 <asp:GridView ID="ApplicationGV" runat="server" AutoGenerateColumns="False">
            <Columns>
                <asp:BoundField DataField="ID"   HeaderText="Код заявки" Visible="true" />
                <asp:BoundField DataField="Name"   HeaderText="Название" Visible="true" />
                <asp:BoundField DataField="Description"   HeaderText="Описание" Visible="false" />
                <asp:BoundField DataField="Competition"   HeaderText="Конкурс" Visible="true" />
                <asp:BoundField DataField="SendedDataTime"   HeaderText="Дата отправки на рассмотрение" Visible="true" />
                <asp:BoundField DataField="Autor"   HeaderText="Автор" Visible="false" />            
                 
                 <asp:TemplateField HeaderText="Скачать заявку">
                        <ItemTemplate>
                            <asp:Button ID="ApplicationButton" runat="server" CommandName="Select" Text="Скачать" CommandArgument='<%# Eval("ID") %>' Width="150px" OnClick="ApplicationButtonClick"/>
                        </ItemTemplate>
                </asp:TemplateField>
                 <asp:TemplateField HeaderText="Экспертное заключение">
                        <ItemTemplate>
                            <asp:Button ID="ExpertPointButton" runat="server" CommandName="Select" Text="Просмотреть" CommandArgument='<%# Eval("ID") %>' Width="150px" OnClick="ExpertPointButtonClick"/>
                        </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Вернуть на обработку">
                        <ItemTemplate>
                            <asp:Button ID="BackButton" runat="server" CommandName="Select" OnClientClick="return confirm('Вы уверены, что хотите вернуть заявку в раздел необработанные?');" Text="Вернуть" CommandArgument='<%# Eval("ID") %>' Width="150px" OnClick="BackButtonClick"/>
                        </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
</asp:Content>
