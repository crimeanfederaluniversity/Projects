<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" EnableEventValidation="false" AutoEventWireup="true" CodeBehind="ProjectPage.aspx.cs" Inherits="Zakupka.Event.ProjectPage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <br />
    <asp:Button ID="Back" runat="server" CssClass="btn btn-default" Text="Назад" OnClick="Back_Click" />
      <h2><asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>
      </h2>
    <p>
        <asp:GridView ID="GridView1" runat="server"  AutoGenerateColumns="False" Width="100%" class="table table-striped edm-table edm-PocessEdit-table centered-block" >
               <Columns>                
             <asp:BoundField DataField="projectID"   HeaderText="ID" Visible="false" />    
                 <asp:BoundField DataField="projectName" HeaderText="Название проекта" Visible="True" />   
          <asp:TemplateField HeaderText="Перейти">
                        <ItemTemplate>
                            <asp:Button ID="GoButton" runat="server" CommandName="Select" CssClass="btn btn-default" OnClientClick="showLoadPanel()" Text="Перейти" Width="200px" CommandArgument='<%# Eval("projectID") %>' OnClick="GoButtonClick"/>
                        </ItemTemplate>
                    </asp:TemplateField>
                    </Columns>
                    </asp:GridView>
    </p>
        <h3>Добавление нового проекта:</h3>Введите название проекта: 
    <p>
        <asp:TextBox ID="TextBox1"  runat="server" Width="500px"></asp:TextBox>
    </p>
    <p>
        <asp:Button ID="SaveButton" runat="server" CssClass="btn btn-default" Text="Сохранить" OnClick="SaveButtonClick" />
    </p>
</asp:Content>
