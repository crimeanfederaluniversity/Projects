<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="UserMainPage.aspx.cs" Inherits="Competitions.User.UserMainPage" %>
<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
    <div>
        <h2><span style="font-size: 30px">Добро пожаловать в систему "Конкурсы и проекты Программы развития" </span></h2>
        <br />
         <asp:Button ID="MyApplication" runat="server" Text="Мои заявки" OnClick="MyApplication_Click" />
        <br />
        <br />
         <asp:GridView ID="MainGV" runat="server" AutoGenerateColumns="False">
            <Columns>
                <asp:BoundField DataField="ID"   HeaderText="" Visible="false" />
                <asp:BoundField DataField="Number"   HeaderText="Шифр" Visible="true" />
                <asp:BoundField DataField="Name"   HeaderText="Конкурс" Visible="true" />
                <asp:TemplateField HeaderText="Подать заявку">
                        <ItemTemplate>
                           <asp:Button ID="NewApplication" runat="server" OnClientClick="return confirm('Вы уверены, что хотите подать заявку?');" Text="Подать заявку" CommandArgument='<%# Eval("ID") %>' OnClick="NewApplication_Click" />
                        </ItemTemplate>
                </asp:TemplateField>
              
            </Columns>
        </asp:GridView>

        <br />

       
    </div>
</asp:Content>
