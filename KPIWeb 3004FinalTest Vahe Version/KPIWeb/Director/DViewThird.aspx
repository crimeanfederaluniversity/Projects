<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="DViewThird.aspx.cs" Inherits="KPIWeb.Director.DViewThird" %>

<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
           <h2>Список подразделений</h2>
           
        <asp:GridView ID="GridView1" runat="server" 
            AutoGenerateColumns="False"
            style="margin-top: 0px">
             <Columns>        
                         
                 <asp:BoundField DataField="StructName" HeaderText="Структурное подразделение" Visible="True" />          

                  <asp:TemplateField HeaderText="Подробнее">
                        <ItemTemplate>
                            <asp:Button ID="ButtonViewReport" runat="server" CommandName="Select" Text="Просмотреть" Width="200px" CommandArgument='<%# Eval("StructID") %>' OnClick="ButtonDetailClick"/>
                        </ItemTemplate>
                    </asp:TemplateField>

                   <asp:BoundField DataField="Status" HeaderText="Статус данных" Visible="True" />

                </Columns>
        </asp:GridView>
           <br />
       </asp:Content>