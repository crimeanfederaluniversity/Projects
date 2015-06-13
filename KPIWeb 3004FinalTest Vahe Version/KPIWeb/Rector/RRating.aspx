<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="RRating.aspx.cs" Inherits="KPIWeb.Rector.RRating" %>
<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">  

    <div>   
         <br />

         <asp:Label ID="Title" runat="server" Text="Заголовок" Font-Size="20pt"></asp:Label>

         <br />
         <br />
        <style>     
            .my_result_gw tr + tr  td + td  {
                text-align:left;
            }
        </style>
                
         <asp:GridView ID="Grid" runat="server" CssClass="my_result_gw sova"
            AutoGenerateColumns="False"
            style="margin-top: 0px;">
             <Columns>                
                 <asp:BoundField DataField="Number"   HeaderText="Номер" Visible="true" />    
                 <asp:BoundField DataField="Name" HeaderText="Название показателя" Visible="True" />          
                 <asp:BoundField DataField="Value" HeaderText="Значение" Visible="True" />
                 <asp:BoundField DataField="PlannedValue" HeaderText="Плановое значение ЦП" Visible="True" />
                 
                    <asp:TemplateField HeaderText="Детализация показателя по академиям">
                        <ItemTemplate>
                            <asp:Button ID="Button1" runat="server" CommandName="Select" Text="Детализировать" CommandArgument='<%# Eval("ID") %>' Width="200px" 
                                OnClick="Button1Click " />
                        </ItemTemplate>
                    </asp:TemplateField>
                    
                  <asp:TemplateField HeaderText="Детализация показателя по факультетам">
                        <ItemTemplate>
                            <asp:Button ID="Button2" runat="server" CommandName="Select" Text="Детализировать" CommandArgument='<%# Eval("ID") %>' Width="200px" 
                                OnClick="Button2Click " />
                        </ItemTemplate>
                    </asp:TemplateField>
                 
                </Columns>
        </asp:GridView>   
         <br />
    </div>
</asp:Content>