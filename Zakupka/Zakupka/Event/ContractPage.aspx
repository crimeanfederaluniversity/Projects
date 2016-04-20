<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" EnableEventValidation="false" AutoEventWireup="true" CodeBehind="ContractPage.aspx.cs" Inherits="Zakupka.Event.ContractPage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <asp:Button ID="Back" runat="server" Text="Назад" CssClass="btn btn-default"  OnClick="Back_Click" />
        <h2><asp:Label ID="Label1" runat="server" Text="Label"></asp:Label></h2>
    <p>
        <asp:GridView ID="GridView1" runat="server"  AutoGenerateColumns="False" Width="100%" class="table table-striped edm-table edm-PocessEdit-table centered-block" >
               <Columns>                
             <asp:BoundField DataField="contractID"   HeaderText="Номер договора" Visible="true" />    
                 <asp:BoundField DataField="contractName" HeaderText="Название договора" Visible="True" />   
         <asp:TemplateField HeaderText="Редактировать">
                        <ItemTemplate>
                            <asp:Button ID="EditButton" runat="server" CommandName="Select" OnClientClick="showLoadPanel()" CssClass="btn btn-default" Text="Редактировать" Width="200px" CommandArgument='<%# Eval("contractID") %>' OnClick="EditButtonClick"/>
                        </ItemTemplate>
                    </asp:TemplateField>
                        
                     <asp:TemplateField HeaderText="Удалить">
                        <ItemTemplate>
                            <asp:Button ID="DeleteButton" runat="server" CommandName="Select" OnClientClick="javascript:if (!askForIsOkWithLoading('Вы уверены что хотите удалить договор?') == true )  return false;" CssClass="btn btn-default" Text="Удалить" Width="200px" CommandArgument='<%# Eval("contractID") %>' OnClick="DeleteButtonClick"/>
                        </ItemTemplate>
                    </asp:TemplateField>
                    </Columns>
                    </asp:GridView>
    </p>
   
</asp:Content>
