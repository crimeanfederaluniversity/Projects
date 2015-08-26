<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="Questions.aspx.cs" Inherits="Competition.Questions" %>

<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent" >
     <h2><span style="font-size: 30px">Внесение вопросов в базу</span></h2>
    <form id="form1" runat="server">
    <div>
    
        <br />
    
    </div>
        <asp:TextBox ID="TextBox1" runat="server" Width="1011px"></asp:TextBox>
        <br />
        <asp:TextBox ID="TextBox2" runat="server"></asp:TextBox>
        <br />
        <br />
        <asp:CheckBoxList ID="CheckBoxList1" runat="server">
        </asp:CheckBoxList>
        <br />
        <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" style="height: 25px" Text="Сохранить" />
    </form>
  </asp:Content>   
