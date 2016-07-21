﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" EnableEventValidation ="false" CodeBehind="HeadAcceptFirst.aspx.cs" Inherits="Rank.Forms.StructUserPointsForAccept" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h3>Результаты сотрудников Вашего структурного подразделения:</h3>

    <asp:GridView ID="GridView1" AutoGenerateColumns="false" runat="server" OnRowDataBound ="GridView1_RowDataBound" >
        <Columns>
            <asp:TemplateField HeaderText="" HeaderStyle-HorizontalAlign="Center"   HeaderStyle-VerticalAlign="Middle" Visible = "false" >
                <ItemTemplate>
                    <asp:Label ID="ID" runat="server" Text='<%# Bind("ID") %>'  Visible="false"></asp:Label>
                </ItemTemplate>
                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle"></HeaderStyle>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="ФИО сотрудника" HeaderStyle-HorizontalAlign="Center"   HeaderStyle-VerticalAlign="Middle" Visible = "True" >
                <ItemTemplate>
                    <asp:Label ID="User" runat="server" Text='<%# Bind("User") %>'  Visible="True"></asp:Label>
                </ItemTemplate>
                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle"></HeaderStyle>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Баллы" HeaderStyle-HorizontalAlign="Center"   HeaderStyle-VerticalAlign="Middle" Visible = "True" >
                <ItemTemplate>
                    <asp:Label ID="Point" runat="server" Text='<%# Bind("Point") %>'  Visible="True"></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
               <asp:TemplateField HeaderText="Статус" HeaderStyle-HorizontalAlign="Center"   HeaderStyle-VerticalAlign="Middle" Visible = "True" >
                <ItemTemplate>
                    <asp:Label ID="Status" runat="server" Text='<%# Bind("Status") %>'  Visible="True"></asp:Label>
                     <asp:Label ID="Color"  runat="server" Visible="false" Text='<%# Bind("Color") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Подробнее">
                <ItemTemplate>
                    <asp:Button ID="ShowButton" runat="server" CommandName="Select" Text="Просмотреть и утвердить" Width="200px" CommandArgument='<%# Eval("ID") %>' OnClick="ShowButtonClik" />
                </ItemTemplate>
            </asp:TemplateField>
              
        </Columns>
    </asp:GridView>
</asp:Content>
