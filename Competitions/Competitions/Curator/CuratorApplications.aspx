<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="CuratorApplications.aspx.cs" Inherits="Competitions.Curator.CuratorApplications" %>

<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">   
    <br />
            <asp:Button ID="GoBackButton" runat="server"  Text="Назад" Width="131px" style="height: 26px" OnClick="GoBackButton_Click" />
        <br />   
    <h2><span style="font-size: 20px">Cписок всех заявок, поданных на ваши конкурсы :</span></h2>
    <br />
 <asp:GridView ID="ApplicationGV" runat="server" AutoGenerateColumns="False">
            <Columns>
                <asp:BoundField DataField="ID"   HeaderText="Код заявки" Visible="true" />
                <asp:BoundField DataField="Name"   HeaderText="Название" Visible="true" />
                <asp:BoundField DataField="Email"   HeaderText="Отправитель" Visible="true" />
                <asp:BoundField DataField="Competition"   HeaderText="Конкурс" Visible="true" />
                <asp:BoundField DataField="SendedDataTime"   HeaderText="Дата отправки на рассмотрение" Visible="true" />
                <asp:BoundField DataField="Accept"  HeaderText="Статус" Visible="true" />
                <asp:TemplateField HeaderText="Cкачать заявку">
                        <ItemTemplate>
                            <asp:Button ID="GetDocButton" runat="server" CommandName="Select" Text="Скачать" CommandArgument='<%# Eval("ID") %>' Width="200px" OnClick="GetDocButtonClick"/>
                        </ItemTemplate>
                </asp:TemplateField>    
                 <asp:TemplateField HeaderText="Экспертное заключение">
                        <ItemTemplate>
                            <asp:Button ID="ExpertPointButton" runat="server" CommandName="Select" Text="Просмотреть" CommandArgument='<%# Eval("ID") %>' Width="200px" OnClick="ExpertPointButtonClick"/>
                        </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </asp:Content>