﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ChooseApplication.aspx.cs" Inherits="Competitions.Admin.Applications" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <br />
     <h2><span style="font-size: 30px">Список заявок, ожидающих обработки:</span></h2>
    <br />
     <asp:GridView ID="ApplicationGV" runat="server" AutoGenerateColumns="False">
            <Columns>
                <asp:BoundField DataField="ID"   HeaderText="Код заявки" Visible="true" />
                <asp:BoundField DataField="Name"   HeaderText="Название" Visible="true" />
                <asp:BoundField DataField="Description"   HeaderText="Описание" Visible="false" />
                <asp:BoundField DataField="Competition"   HeaderText="Конкурс" Visible="true" />
                <asp:BoundField DataField="SendedDataTime"   HeaderText="Дата отправки на рассмотрение" Visible="true" />
                <asp:BoundField DataField="Autor"   HeaderText="Автор" Visible="false" />            
                <asp:BoundField DataField="Experts"   HeaderText="Привлеченные эксперты" Visible="true" />     
                <asp:TemplateField HeaderText="Изменить состав экспертов">
                        <ItemTemplate>
                            <asp:Button ID="ExpertChangeButton" runat="server" CommandName="Select" Text="Изменить" CommandArgument='<%# Eval("ID") %>' Width="200px" OnClick="ExpertChangeButtonClick"/>
                        </ItemTemplate>
                </asp:TemplateField>
                   <asp:TemplateField HeaderText="Принять заявку">
                        <ItemTemplate>
                            <asp:Button ID="AcceptButton" runat="server" CommandName="Select" OnClientClick="return confirm('Вы уверены, что хотите принять заявку?');" Text="Принять" CommandArgument='<%# Eval("ID") %>' Width="200px" OnClick="AcceptButtonClick"/>
                        </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
</asp:Content>
