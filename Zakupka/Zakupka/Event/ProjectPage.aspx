<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" EnableEventValidation="false" AutoEventWireup="true" CodeBehind="ProjectPage.aspx.cs" Inherits="Zakupka.Event.ProjectPage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <asp:Button ID="Back" runat="server" Text="Назад" OnClick="Back_Click" />
      <h2><asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>
      </h2>
    <p>
        <asp:GridView ID="GridView1" runat="server"  AutoGenerateColumns="False"  >
               <Columns>                
             <asp:BoundField DataField="projectID"   HeaderText="ID" Visible="false" />    
                 <asp:BoundField DataField="projectName" HeaderText="Название проекта" Visible="True" />   
          <asp:TemplateField HeaderText="Перейти">
                        <ItemTemplate>
                            <asp:Button ID="GoButton" runat="server" CommandName="Select" OnClientClick="showLoadPanel()" Text="Перейти" Width="200px" CommandArgument='<%# Eval("projectID") %>' OnClick="GoButtonClick"/>
                        </ItemTemplate>
                    </asp:TemplateField>
                    </Columns>
                    </asp:GridView>
    </p>
        <h3>Добавление нового проекта:</h3>Введите название проекта: 
    <p>
        <asp:TextBox ID="TextBox1" runat="server" Width="400px"></asp:TextBox>
    </p>
    <p>
        <asp:Button ID="SaveButton" runat="server" Text="Сохранить" OnClick="SaveButtonClick" />
    </p>
</asp:Content>
