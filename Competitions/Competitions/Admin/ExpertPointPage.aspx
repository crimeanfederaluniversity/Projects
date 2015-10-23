<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="ExpertPointPage.aspx.cs" Inherits="Competitions.Admin.ExpertPointPage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    
     
    <asp:Button ID="GoBackButton" runat="server" OnClick="Button2_Click" Text="Назад" Width="113px" />
    <br />
        <asp:Label   ID="Label1" style="font-size: 20px"  runat="server"  Visible="true"> </asp:Label>
    <br />
    <br />
     
    <asp:GridView ID="ExpertsPointGV" runat="server" AutoGenerateColumns="False" OnRowDataBound="GridView1_RowDataBound">
            <Columns>
                <asp:BoundField DataField="ID"   HeaderText="" Visible="false" />
                <asp:BoundField DataField="Name"   HeaderText="Имя" Visible="true" />
                <asp:BoundField DataField="AccessLevel"   HeaderText="Вид эксперта" Visible="true" />
                <asp:BoundField DataField="SendedDataTime"   HeaderText="Дата экспертного заключения" Visible="true" />
                

                <asp:TemplateField HeaderText="Скачать экспертное заключение">
                        <ItemTemplate>
                            <asp:Button ID="ExpertDownloadButton" runat="server" CommandName="Select" Text="Скачать" CommandArgument='<%# Eval("ID") %>' Width="200px" OnClick="ExpertDownloadButtonClick"/>
                            <asp:Label ID="Color"  runat="server" Visible="false" Text='<%# Bind("Color") %>'></asp:Label>
                        </ItemTemplate>
                </asp:TemplateField>
             
            </Columns>
        </asp:GridView>
    <br />
    </asp:Content>