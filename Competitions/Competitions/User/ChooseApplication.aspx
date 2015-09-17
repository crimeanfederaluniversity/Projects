<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ChooseApplication.aspx.cs" Inherits="Competitions.User.ChooseApplication" %>
<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
    <div>
        <h2><span style="font-size: 30px">Мои заявки </span></h2>
        <br />
        <asp:GridView ID="ApplicationGV" runat="server" AutoGenerateColumns="False">
            <Columns>
                <asp:BoundField DataField="ID"   HeaderText="" Visible="false" />
                <asp:BoundField DataField="Name"   HeaderText="Название" Visible="true" />
                <asp:BoundField DataField="CompetitionName"   HeaderText="Конкурс" Visible="true" />
            
                <asp:TemplateField HeaderText="Заполнить">
                        <ItemTemplate>
                            <asp:Button ID="FillButton" runat="server" CommandName="Select" Text="Заполнить" CommandArgument='<%# Eval("ID") %>'   Width="200px" OnClick="FillButtonClick"/>
                        </ItemTemplate>
                </asp:TemplateField>
                
                <asp:TemplateField HeaderText="Отправить на рассмотрение">
                        <ItemTemplate>
                            <asp:Button ID="SendButton" runat="server" CommandName="Select" OnClientClick="return confirm('Вы уверены, что хотите отправить заявку на рассмотрение?');" Text="Отправить" CommandArgument='<%# Eval("ID") %>'   Width="200px" OnClick="SendButtonClick"/>
                        </ItemTemplate>
                </asp:TemplateField>

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
