<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="RRating.aspx.cs" Inherits="KPIWeb.Rector.RRating" %>
<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">  
    
                <asp:Panel runat="server" ID="top_panel2" CssClass="top_panel" Height="40" Visible="true">    
        <div>    
      <asp:Button ID="GoBackButton" runat="server" OnClientClick="JavaScript:window.history.back(1); return false; showLoadPanel();"  Text="Назад" Width="125px" Enabled="True" />
      <asp:Button ID="GoForwardButton" runat="server" OnClientClick="JavaScript:window.history.forward(1); return false; showLoadPanel();"  Text="Вперед" Width="125px" />
        &nbsp; &nbsp; <asp:Button ID="Button22" OnClientClick="showLoadPanel()" runat="server"  Text="На главную" Width="125px" Enabled="True" OnClick="Button22_Click" />
        &nbsp; &nbsp;       
        <asp:Button ID="Button11" runat="server" OnClientClick="showLoadPanel()" CssClass="button_right"  Text="Выбор отчета" Enabled="False" Width="225px" />
            &nbsp; &nbsp; &nbsp; &nbsp;
        </div>

    </asp:Panel>
    
    <div>   
         <br />

         <asp:Label ID="Title" runat="server" Text="Заголовок" Font-Size="20pt"></asp:Label>

         <br />
         <br />
         <style>     
            .my_result_gw tr + tr  td + td  {
                text-align:center;
            }
             body {
             top:35px;
             }
                       table {

              
               border-width:15px;
               background-color: #ffffff;
               opacity: 0.9;
               }
              table th {
                text-align: center;
            }
              
               table td + td {
                text-align: center;
            }
        </style>
                
         <asp:GridView ID="Grid" runat="server" CssClass="my_result_gw sova"
            AutoGenerateColumns="False"
            >
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