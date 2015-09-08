<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ChooseApplication.aspx.cs" Inherits="Competitions.User.ChooseApplication" %>
<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
    <div>
        <br />
        <br />
        Мои заявки<asp:GridView ID="ApplicationGV" runat="server" AutoGenerateColumns="False">
            <Columns>
                <asp:BoundField DataField="ID"   HeaderText="" Visible="false" />
                <asp:BoundField DataField="Name"   HeaderText="Название" Visible="true" />
                <asp:BoundField DataField="CompetitionName"   HeaderText="Конкурс" Visible="true" />
                
                <asp:TemplateField HeaderText="Изменить">
                        <ItemTemplate>
                            <asp:Button ID="ChangeButton" runat="server" CommandName="Select" Text="Изменить" CommandArgument='<%# Eval("ID") %>' Enabled='<%# Eval("FillNSendEnabled") %>' Width="200px" OnClick="ChangeButtonClick"/>
                        </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Заполнить">
                        <ItemTemplate>
                            <asp:Button ID="FillButton" runat="server" CommandName="Select" Text="Заполнить" CommandArgument='<%# Eval("ID") %>' Enabled='<%# Eval("FillNSendEnabled") %>' Width="200px" OnClick="FillButtonClick"/>
                        </ItemTemplate>
                </asp:TemplateField>
                
                <asp:TemplateField HeaderText="Отправить на рассмотрение">
                        <ItemTemplate>
                            <asp:Button ID="SendButton" runat="server" CommandName="Select" Text="Отправить" CommandArgument='<%# Eval("ID") %>' Enabled='<%# Eval("FillNSendEnabled") %>' Width="200px" OnClick="SendButtonClick"/>
                        </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Получить сгенерированный документ">
                        <ItemTemplate>
                            <asp:Button ID="GetDocButton" runat="server" CommandName="Select" Text="Загрузить" CommandArgument='<%# Eval("ID") %>' Width="200px" OnClick="GetDocButtonClick"/>
                        </ItemTemplate>
                </asp:TemplateField>
                
                <asp:TemplateField HeaderText="Удалить заявку">
                        <ItemTemplate>
                            <asp:Button ID="DeleteApplicationButton" runat="server" CommandName="Select" Text="Удалить" CommandArgument='<%# Eval("ID") %>' Enabled='<%# Eval("FillNSendEnabled") %>' Width="200px" OnClick="DeleteApplicationButtonClick"/>
                        </ItemTemplate>
                </asp:TemplateField>

            </Columns>
        </asp:GridView>

        <br />

        <asp:Button ID="NewApplication" runat="server" Text="Создать новую заявку" OnClick="NewApplication_Click" />
    </div>
</asp:Content>
