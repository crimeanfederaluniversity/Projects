<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ArchiveApplications.aspx.cs" Inherits="Competitions.User.ArchiveApplications" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
       <div>
           <span style="font-size: 30px">Отправленные заявки:<br />
           </span>
        <br />
        <asp:GridView ID="ApplicationGV" runat="server" AutoGenerateColumns="False">
            <Columns>
                <asp:BoundField DataField="ID"   HeaderText="" Visible="false" />
                <asp:BoundField DataField="Name"   HeaderText="Название" Visible="true" />
                <asp:BoundField DataField="CompetitionName"   HeaderText="Конкурс" Visible="true" />
            
                 
                <asp:TemplateField HeaderText="Скачать заявку">
                        <ItemTemplate>
                            <asp:Button ID="GetDocButton" runat="server" CommandName="Select" Text="Загрузить" CommandArgument='<%# Eval("ID") %>' Width="200px" OnClick="GetDocButtonClick"/>
                        </ItemTemplate>
                </asp:TemplateField>
             
            </Columns>
        </asp:GridView>

        <br />

        
    </div>
</asp:Content>
