<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" EnableEventValidation="false" AutoEventWireup="true" CodeBehind="EventPage.aspx.cs" Inherits="Zakupka.Event.EventPage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
   
    <p>
        &nbsp;</p>
    <p>
        <asp:Button ID="Back" runat="server" CssClass="btn btn-default" Text="Назад" OnClick="Back_Click" />
    </p>
     <h4><asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>   </h4>
        <asp:GridView ID="GridView1" runat="server"  AutoGenerateColumns="False" Width="100%"  class="table table-striped edm-table edm-PocessEdit-table centered-block" >
               <Columns>                
             <asp:BoundField DataField="eventID"   HeaderText="ID" Visible="false" />    
                 <asp:BoundField DataField="eventName" HeaderText="Название мероприятия" Visible="True" />   
          <asp:TemplateField HeaderText="Перейти">
                        <ItemTemplate>
                            <asp:Button ID="GoButton" runat="server" CommandName="Select" CssClass="btn btn-default" OnClientClick="showLoadPanel()" Text="Перейти" Width="200px" CommandArgument='<%# Eval("eventID") %>' OnClick="GoButtonClick"/>
                        </ItemTemplate>
                    </asp:TemplateField>
               
                    </Columns>
                    </asp:GridView>
    </p>
     

</asp:Content>
