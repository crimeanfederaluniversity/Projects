<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="DRating.aspx.cs" Inherits="KPIWeb.Director.DRating" %>
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
        &nbsp; &nbsp; <asp:Button ID="Button22" OnClientClick="showLoadPanel()" runat="server"  Text="На главную" Width="125px" Enabled="True" OnClick="Button22_Click" />
         &nbsp;&nbsp;
            <asp:DropDownList ID="RectorChooseReportDropDown" runat="server" Height="20px" Width="550px" AutoPostBack="True" OnSelectedIndexChanged="RectorChooseReportDropDown_SelectedIndexChanged">
            </asp:DropDownList>
            &nbsp; &nbsp;
            
            &nbsp; &nbsp;<asp:Button ID="Button5" runat="server" OnClientClick="showLoadPanel()" CssClass="button_right" OnClick="Button5_Click" Text="Нормативные документы" Width="300px" />
            &nbsp; &nbsp; &nbsp; &nbsp;
        </div>

    </asp:Panel>
     <div>   
      
         <br />
      
     <br />
     <asp:Label ID="ReportTitle" runat="server" Text="ReportTitle" Font-Size="20pt"></asp:Label>
         <br />
         <br />
         <asp:Label ID="PageFullName" runat="server" Font-Size="15pt" Text="Значение целевых показателей для Таврической академии"></asp:Label>
         <br />
     <br />
    

         <asp:GridView ID="Grid" runat="server" CssClass="sova" AutoGenerateColumns="False">
             <Columns>                
                   
                 <asp:BoundField DataField="Name"  ItemStyle-CssClass="namepokazat" HeaderText="Название показателя" Visible="True" />          
                 <asp:BoundField DataField="Value" ItemStyle-CssClass="Valuev" HeaderText="Значение" Visible="True" />
                 <asp:BoundField DataField="PlannedValue" ItemStyle-CssClass="planvalCP" HeaderText="Плановое значение ЦП" Visible="True" />
                 
                   
                    
                  <asp:TemplateField HeaderText="Подробнее">
                        <ItemTemplate>
                            <asp:Button ID="Detail" runat="server" CommandName="Select" Text="Разложить"  CommandArgument='<%# Eval("ID") %>' Width="200px" Enabled='<%# Eval("ButtonEnabled") %>'  OnClick="DetailClick" />
                        </ItemTemplate>
                    </asp:TemplateField>
                 
                

                </Columns>
        </asp:GridView>   
         <br />
    </div>
</asp:Content>
