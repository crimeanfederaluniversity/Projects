﻿<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="ReadyApplications.aspx.cs" Inherits="Competitions.Admin.ReadyApplications" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <br />
    <h2><span style="font-size: 30px">Список обработанных заявок:</span></h2>
    <br />
 <asp:GridView ID="ApplicationGV" runat="server" AutoGenerateColumns="False">
            <Columns>
                <asp:BoundField DataField="ID"   HeaderText="Код заявки" Visible="true" />
                <asp:BoundField DataField="Name"   HeaderText="Название" Visible="true" />
                <asp:BoundField DataField="Description"   HeaderText="Описание" Visible="false" />
                <asp:BoundField DataField="Competition"   HeaderText="Конкурс" Visible="true" />
                <asp:BoundField DataField="SendedDataTime"   HeaderText="Дата отправки на рассмотрение" Visible="true" />
                <asp:BoundField DataField="Autor"   HeaderText="Автор" Visible="false" />            
                 
               
                 <asp:TemplateField HeaderText="Экспертное заключение">
                        <ItemTemplate>
                            <asp:Button ID="ExpertPointButton" runat="server" CommandName="Select" Text="Просмотреть" CommandArgument='<%# Eval("ID") %>' Width="200px" OnClick="ExpertPointButtonClick"/>
                        </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
</asp:Content>