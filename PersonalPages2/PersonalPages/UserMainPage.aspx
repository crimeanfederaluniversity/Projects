<%@ Page Title="" Language="C#" MasterPageFile="~/Second.Master" AutoEventWireup="true" EnableEventValidation="false" CodeBehind="UserMainPage.aspx.cs" Inherits="PersonalPages.UserMainPage" %>


<asp:Content ID="Content1" ContentPlaceHolderID="SecondLevel" runat="server">  

          <asp:Panel ID="Panel" runat="server" Height="295px">
              <asp:Button ID="Button1" runat="server" Text="ректору" OnClick="Button1_Click" />
              <asp:Button ID="Button2" runat="server" OnClick="Button2_Click" Text="пропуск" />
              <asp:Button ID="Button3" runat="server" OnClick="Button3_Click" Text="академ" />
 
                          
          </asp:Panel>
   
</asp:Content>
