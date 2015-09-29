<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="RRating.aspx.cs" Inherits="KPIWeb.Rector.RRating" %>
<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">  
    

        <style type="text/css">
   .button_right 
   {
       float:right
   }     
</style> 

                <br />
                <asp:Panel runat="server" ID="top_panel2" CssClass="top_panel" Height="40" Visible="true">    
        <div>    
      <asp:Button ID="GoBackButton" runat="server" OnClientClick="showLoadPanel();"  Text="Назад" Width="125px" Enabled="True" OnClick="GoBackButton_Click" />
      <asp:Button ID="GoForwardButton" runat="server" OnClientClick="showLoadPanel();"  Text="Вперед" Width="125px" OnClick="GoForwardButton_Click" />
        &nbsp; &nbsp; <asp:Button ID="Button22" OnClientClick="showLoadPanel()" runat="server"  Text="На главную" Width="125px" Enabled="True" OnClick="Button22_Click" />
        &nbsp; &nbsp;<asp:Button ID="Button5" runat="server" OnClientClick="showLoadPanel()" CssClass="button_right" OnClick="Button5_Click" Text="Нормативные документы" Width="300px" />
            &nbsp; &nbsp; &nbsp; &nbsp;
        </div>

    </asp:Panel>
    
    <div>   
         <br />

         <asp:Label ID="Title" runat="server" Text="Заголовок" Font-Size="20pt"></asp:Label>

         <br />
         <br />
         <style>     

             body {
             top:35px;
             }
             .numba {
              text-align:center;
             
             }
             .namepokazat {
              text-align:left;
              padding:5px 10px;
             }
             .Valuev {
               text-align:center;
             }
             .planvalCP {
                 text-align:center;
             }

                       table {

              
               border-width:15px;
               background-color: #ffffff;
               opacity: 0.9;
               }
              table th {
                text-align: center;
            }
              
               
        </style>
                
         <asp:GridView ID="Grid" runat="server" CssClass="sova"
            AutoGenerateColumns="False"
            >
             <Columns>                
                 <asp:BoundField DataField="Number" ItemStyle-CssClass="numba"  HeaderText="Номер" Visible="true" />    
                 <asp:BoundField DataField="Name"  ItemStyle-CssClass="namepokazat" HeaderText="Название показателя" Visible="True" />          
                 <asp:BoundField DataField="Value" ItemStyle-CssClass="Valuev" HeaderText="Значение" Visible="True" />
                 <asp:BoundField DataField="PlannedValue" ItemStyle-CssClass="planvalCP" HeaderText="Плановое значение ЦП" Visible="True" />
                 
                    <asp:TemplateField HeaderText="Детализация показателя по академиям">
                        <ItemTemplate>
                            <asp:Button ID="Button1" runat="server" CommandName="Select" Text='<%# Eval("Button_Text") %>' CommandArgument='<%# Eval("ID") %>' Width="200px" 
                                OnClick="Button1Click " />
                        </ItemTemplate>
                    </asp:TemplateField>
                    
                  <asp:TemplateField HeaderText="Детализация показателя по факультетам">
                        <ItemTemplate>
                            <asp:Button ID="Button2" runat="server" CommandName="Select" Text='<%# Eval("Button_Text") %>' CommandArgument='<%# Eval("ID") %>' Width="200px" 
                                OnClick="Button2Click " />
                        </ItemTemplate>
                    </asp:TemplateField>
                 
                

                </Columns>
        </asp:GridView>   
         <br />
    </div>
</asp:Content>