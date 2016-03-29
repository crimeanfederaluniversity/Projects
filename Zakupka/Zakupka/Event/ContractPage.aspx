<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" EnableEventValidation="false" AutoEventWireup="true" CodeBehind="ContractPage.aspx.cs" Inherits="Zakupka.Event.ContractPage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <asp:Button ID="Back" runat="server" Text="Назад" OnClick="Back_Click" />
        <h2><asp:Label ID="Label1" runat="server" Text="Label"></asp:Label></h2>
    <p>
        <asp:GridView ID="GridView1" runat="server"  AutoGenerateColumns="False"  >
               <Columns>                
             <asp:BoundField DataField="contractID"   HeaderText="Номер договора" Visible="true" />    
                 <asp:BoundField DataField="contractName" HeaderText="Название договора" Visible="True" />   
         <asp:TemplateField HeaderText="Шаг 1">
                        <ItemTemplate>
                            <asp:Button ID="EditButton" runat="server" CommandName="Select" OnClientClick="showLoadPanel()" Text="Редактировать" Width="200px" CommandArgument='<%# Eval("contractID") %>' OnClick="EditButtonClick"/>
                        </ItemTemplate>
                    </asp:TemplateField>
                         <asp:TemplateField HeaderText="Шаг 2">
                        <ItemTemplate>
                            <asp:Button ID="EditButton2" runat="server" CommandName="Select" OnClientClick="showLoadPanel()" Text="Редактировать" Width="200px" CommandArgument='<%# Eval("contractID") %>' OnClick="EditButton2Click"/>
                        </ItemTemplate>
                    </asp:TemplateField>
                    </Columns>
                    </asp:GridView>
    </p>
        <h3>Добавление нового договора:</h3>Введите название договора: 
    <p>
        <asp:TextBox ID="TextBox1" runat="server" Width="400px"></asp:TextBox>
    </p>
    <p>
        <asp:Button ID="SaveButton" runat="server" Text="Сохранить" OnClick="SaveButtonClick" />
    </p>
</asp:Content>
