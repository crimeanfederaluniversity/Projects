<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CuratorExpertPointPage.aspx.cs" Inherits="Competitions.Curator.CuratorExpertPointPage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
      <asp:GridView ID="ExpertsPointGV" runat="server" AutoGenerateColumns="False"  >
            <Columns>
                <asp:BoundField DataField="ID"   HeaderText="" Visible="false" />
                <asp:BoundField DataField="Name"   HeaderText="Имя" Visible="true" />
                <asp:BoundField DataField="AccessLevel"   HeaderText="Вид эксперта" Visible="true" />
                
                <asp:TemplateField HeaderText="Скачать экспертное заключение">
                        <ItemTemplate>
                            <asp:Button ID="ExpertDownloadButton" runat="server" CommandName="Select" Text="Скачать" CommandArgument='<%# Eval("ID") %>' Width="200px" OnClick="ExpertDownloadButtonClick"/>
                          
                        </ItemTemplate>
                </asp:TemplateField>
             
            </Columns>
        </asp:GridView>
</asp:Content>
