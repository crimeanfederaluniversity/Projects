<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="ExpertPointPage.aspx.cs" Inherits="Competitions.Admin.ExpertPointPage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    
    
    Прикрепленные эксперты
    <asp:GridView ID="ExpertsPointGV" runat="server" AutoGenerateColumns="False">
            <Columns>
                <asp:BoundField DataField="ID"   HeaderText="" Visible="false" />
                <asp:BoundField DataField="Name"   HeaderText="Имя" Visible="true" />
                <asp:BoundField DataField="AccessLevel"   HeaderText="" Visible="false" />
                <asp:TemplateField HeaderText="Скачать экспертное заключение">
                        <ItemTemplate>
                            <asp:Button ID="ExpertDownloadButton" runat="server" CommandName="Select" Text="Скачать" CommandArgument='<%# Eval("ID") %>' Width="200px" OnClick="ExpertDownloadButtonClick"/>
                        </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </asp:Content>