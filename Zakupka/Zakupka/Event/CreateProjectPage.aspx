<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CreateProjectPage.aspx.cs" Inherits="Zakupka.Event.CreateProjectPage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
        <h3>Cоздание нового проекта:</h3>Введите название проекта: 
    <p>
        <asp:TextBox ID="TextBox1" runat="server" Width="500px" TextMode="MultiLine"></asp:TextBox>
    </p>
       <p>
           <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False">
               <Columns>
                   <asp:TemplateField HeaderText="ID" Visible="false">
                       <ItemTemplate>
                           <asp:Label ID="ID" runat="server" Text='<%# Bind("ID") %>' Visible="True"></asp:Label>
                       </ItemTemplate>
                   </asp:TemplateField>
                   <asp:TemplateField HeaderText="Название мероприятия">
                       <ItemTemplate>
                           <asp:Label ID="Name" runat="server" Text='<%# Bind("Name") %>' Visible="True"></asp:Label>
                       </ItemTemplate>
                   </asp:TemplateField>
                   <asp:TemplateField HeaderText="Принадлежность">
                       <ItemTemplate>
                           <asp:CheckBox ID="Access" runat="server"  Text="" />
                       </ItemTemplate>
                   </asp:TemplateField>
               </Columns>
           </asp:GridView>
    </p>
    <p>
        <asp:Button ID="SaveButton" runat="server" CssClass="btn btn-default" Text="Сохранить" OnClick="SaveButtonClick" />
    </p>
</asp:Content>
