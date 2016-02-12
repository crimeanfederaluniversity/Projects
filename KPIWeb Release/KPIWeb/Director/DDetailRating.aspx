<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="DDetailRating.aspx.cs" Inherits="KPIWeb.Director.DDetailRating" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
         <asp:Panel runat="server" ID="top_panel2" CssClass="top_panel" Height="40" Visible="true">    
        <div>    
      <asp:Button ID="GoBackButton" runat="server" OnClientClick="showLoadPanel();"  Text="Назад" Width="125px" Enabled="True" OnClick="GoBackButton_Click" />
        &nbsp; &nbsp; <asp:Button ID="Button22" OnClientClick="showLoadPanel()" runat="server"  Text="На главную" Width="125px" Enabled="True" OnClick="Button22_Click" />
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp; &nbsp; &nbsp; &nbsp; &nbsp;<asp:Button ID="Button5" runat="server" OnClientClick="showLoadPanel()" CssClass="button_right" OnClick="Button5_Click" Text="Нормативные документы" Width="300px" />
            &nbsp; &nbsp; &nbsp; &nbsp;
        </div>
              </asp:Panel>
    <br />
     <br />
     <br />
     <asp:Label ID="ReportTitle" runat="server" Text="ReportTitle" Font-Size="20pt"></asp:Label>
     <br />
     <asp:Label ID="PageFullName" runat="server" Text="" Font-Size="15pt"></asp:Label>
     <br />
               <br />
        <asp:GridView ID="Grid" runat="server" CssClass="result_gw sova" AutoGenerateColumns="False" style="margin-top: 0px;">
             <Columns>                
                 <asp:BoundField DataField="ID"   HeaderText="" Visible="false" />
                  <asp:BoundField DataField="Abb"   HeaderText="Шифр" Visible="True" />
                 <asp:BoundField DataField="Name" HeaderText="Название ПД" Visible="True" /> 
                    <asp:TemplateField  HeaderText="Значение">
                      <ItemTemplate>
                          <asp:Label ID="Value"  runat="server"  Text='<%# Eval("Value") %>' ></asp:Label>                       
                       </ItemTemplate>
                 </asp:TemplateField>
                  </Columns>
              </asp:GridView>
   
         <br />
         <asp:Label ID="FormulaLable" runat="server" Text="FormulaLable" Visible="False"></asp:Label>
   
</asp:Content>
