<%@ Page Language="C#" AutoEventWireup="true"  MasterPageFile="~/Site.Master" CodeBehind="SendInvite.aspx.cs" Inherits="KPIWeb.AutomationDepartment.SendInvite" %>
 <asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">  

     <br /><br />

     <asp:Button ID="Button1" runat="server" Text="Отправить всем незарегистрированным" OnClientClick="return confirm('Вы уверены что хотите отправить email каждому незарегистрированному пользователю?')" OnClick="Button1_Click" />

     <br />
     <br />


     <asp:Button ID="Button2" runat="server" Text="Отправить всем отмеченным ниже" OnClientClick="return confirm('Вы уверены что хотите отправить email пользователям прикрепленным к отмеченным академиям?')" OnClick="Button2_Click" />

     <br />
     <br />

     <asp:CheckBoxList ID="CheckBoxList1" runat="server">
     </asp:CheckBoxList>


     </asp:Content>