<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ChooseApplication.aspx.cs" Inherits="Competitions.Admin.Applications" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <asp:Button ID="GoBackButton" runat="server" OnClick="Button1_Click" Text="Назад" Width="130px" />
    <br />
     <h2><span style="font-size: 20px">Список заявок, ожидающих обработки:</span></h2>
    <h2><span style="font-size: 30px">
        <asp:Label ID="Label1" runat="server" Text="На данный момент у Вас нет активных заявок" Visible="False"></asp:Label>
        </span></h2>
  
    <br />
     <asp:GridView ID="ApplicationGV" runat="server" AutoGenerateColumns="False"  OnRowDataBound="GridView1_RowDataBound">
            <Columns>
                <asp:BoundField DataField="ID"   HeaderText="Код заявки" Visible="true" />
                <asp:BoundField DataField="Name"   HeaderText="Название" Visible="true" />
                <asp:BoundField DataField="Email"   HeaderText="Отправитель" Visible="true" />
                <asp:BoundField DataField="Description"   HeaderText="Описание" Visible="false" />
                <asp:BoundField DataField="Competition"   HeaderText="Конкурс" Visible="true" />
                <asp:BoundField DataField="SendedDataTime"   HeaderText="Дата отправки на рассмотрение" Visible="true" />
               <asp:TemplateField HeaderText="Скачать заявку">
                        <ItemTemplate>
                            <asp:Button ID="ApplicationButton" runat="server" CommandName="Select" Text="Скачать" CommandArgument='<%# Eval("ID") %>' Width="150px" OnClick="ApplicationButtonClick"/>
                        </ItemTemplate>
                </asp:TemplateField>
                 
                 <asp:TemplateField HeaderText="Прикрепить экспертов к заявке">
                        <ItemTemplate>
                            <asp:Button ID="ExpertChangeButton" runat="server" CommandName="Select" Text="Прикрепить" CommandArgument='<%# Eval("ID") %>' Width="150px" OnClick="ExpertChangeButtonClick"/>
                        </ItemTemplate>
                </asp:TemplateField> 
                
               
                   <asp:TemplateField HeaderText="Действие с заявкой">
                        <ItemTemplate>
                            <asp:Button ID="AcceptButton" runat="server" CommandName="Select" OnClientClick="return confirm('Вы уверены, что хотите принять заявку?');" Text="Принять" CommandArgument='<%# Eval("ID") %>' Width="150px" OnClick="AcceptButtonClick"/>                      
                        <asp:Button ID="BackToUserButton" runat="server" CommandName="Select" OnClientClick="return confirm('Вы уверены, что хотите вернуть заявку на доработку отправителю?');" Text="Вернуть отправителю" CommandArgument='<%# Eval("ID") %>' Width="150px" OnClick="BackToUserButtonClick"/>                      
                             </ItemTemplate>
                </asp:TemplateField>
             
            </Columns>
        </asp:GridView>
</asp:Content>
