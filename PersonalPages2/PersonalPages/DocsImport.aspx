<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="DocsImport.aspx.cs" Inherits="PersonalPages.DocsImport" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div>
        <asp:Button ID="GoBackButton" runat="server" OnClick="GoBackButton_Click" Text="Назад" Width="157px" />
        <br />
        <br />
        Все загруженные документы:<br />
   <asp:GridView ID="DocsGV" runat="server" AutoGenerateColumns="False">
        <Columns>
            <asp:BoundField DataField="ID" HeaderText="" Visible="false" />
            <asp:BoundField DataField="Name" HeaderText="Название документа" Visible="true" />
            <asp:TemplateField HeaderText="Группа судентов">
                <ItemTemplate>
                   <asp:Label ID="Name" runat="server"  Visible="true" Text='<%# Bind("Name") %>'  ></asp:Label> 
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Удалить документ">
                <ItemTemplate>
                    <asp:Button ID="DocDeleteButton" runat="server" CommandArgument='<%# Eval("ID") %>' OnClick="DocDeleteButtonClick" Text="Удалить" />
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
        <br />
        Загрузить документ:<asp:FileUpload ID="FileUpload1" runat="server" Height="24px" Width="255px" />
        <br />
        Выберите группу студентов, которые должны видеть этот документ:<br />
        <asp:CheckBoxList ID="CheckBoxList1" runat="server">
        </asp:CheckBoxList>
        <br />
        <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Cохранить" />
    </div>
</asp:Content>
