<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="ConstantListCreate.aspx.cs" Inherits="Competitions.Admin.ConstantListCreate" %>
<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
    <div>
    
        <br />
    
        <asp:Button ID="GoBackButton" runat="server" Height="26px" OnClick="GoBackButton_Click" Text="Назад" />
        <br />
        <br />
        Название листа<br />
        <asp:TextBox ID="ConstantListNameTextBox" runat="server"></asp:TextBox>
        <br />
        <br />
        <asp:GridView ID="ConstantListValuesGV" runat="server" AutoGenerateColumns="False">
            <Columns>
                                
                <asp:TemplateField HeaderText=" " Visible="False">
                        <ItemTemplate>
                            <asp:Label ID="ID" runat="server" Visible="false" Text='<%# Bind("ID") %>' ></asp:Label>
                        </ItemTemplate>
                </asp:TemplateField>
                

                <asp:TemplateField HeaderText="Изменить">
                        <ItemTemplate>
                            <asp:TextBox ID="ConstValueTextBox" runat="server" Visible="True" Text='<%# Bind("ConstValue") %>' ></asp:TextBox>
                        </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Удалить">
                        <ItemTemplate>
                            <asp:Button ID="DeleteButton" runat="server" CommandName="Select" Text="Удалить" CommandArgument='<%# Eval("ID") %>' Width="200px"  OnClick="DeleteButtonClick"/>
                        </ItemTemplate>
                </asp:TemplateField>

            </Columns>
        </asp:GridView>
        <br />
    
    </div>
        <asp:Button ID="SaveAllButton" runat="server" Text="Сохранить" OnClick="SaveAllButton_Click" />
&nbsp;<asp:Button ID="AddRowButton" runat="server" Text="Добавить поле" OnClick="AddRowButton_Click" />
</asp:Content>
