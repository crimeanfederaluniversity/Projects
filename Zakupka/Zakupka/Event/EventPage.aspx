﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" EnableEventValidation="false" AutoEventWireup="true" CodeBehind="EventPage.aspx.cs" Inherits="Zakupka.Event.EventPage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
   
    <p>
        <asp:Button ID="Button1" runat="server" Text="Вид 1" CssClass="btn btn-default" OnClick="Button1_Click" />
    </p>
    <h2>Список мероприятий:</h2>
    <p>
    
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
        <h3>Добавление нового мероприятия:</h3><p>
        <asp:TextBox ID="TextBox1" runat="server" Width="500px"></asp:TextBox>
    </p>
    <p>
        <asp:Button ID="SaveButton" runat="server" CssClass="btn btn-default" Text="Сохранить" OnClick="SaveButtonClick" />
    </p>

</asp:Content>
